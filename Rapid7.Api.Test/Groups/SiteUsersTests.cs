using Rapid7.Api.Test.Support;
using System.Net;

namespace Rapid7.Api.Test.Groups;

public class SiteUsersTests
{
	private const string UsersJson = """
		{
			"resources": [
				{
					"email": "jsmith@example.test",
					"enabled": true,
					"id": 9,
					"locked": false,
					"login": "jsmith",
					"name": "John Smith",
					"links": [ { "href": "https://console.test:3780/api/3/users/9", "rel": "self" } ]
				}
			],
			"links": [ { "href": "https://console.test:3780/api/3/sites/7/users", "rel": "self" } ]
		}
		""";

	private const string UserReferenceJson = """{"id":9,"links":[{"href":"https://console.test:3780/api/3/users/9","rel":"self"}]}""";

	[Fact]
	public async Task ListAsync_SendsGetToTheSiteUsers()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteUsers.ListAsync(7, ct), UsersJson);

		call.ShouldBe(HttpMethod.Get, "/api/3/sites/7/users");
	}

	[Fact]
	public async Task ListAsync_MapsTheUsers()
	{
		var list = await TestClient.ReadAsync((c, ct) => c.SiteUsers.ListAsync(7, ct), UsersJson);

		var user = list.Resources.Should().ContainSingle().Subject;
		user.Id.Should().Be(9);
		user.Login.Should().Be("jsmith");
		user.Name.Should().Be("John Smith");
		user.Links.Should().ContainSingle().Which.Href.Should().EndWith("/users/9");
		list.Links.Should().ContainSingle();
	}

	[Fact]
	public async Task ReplaceAllAsync_SendsPutWithABareIdArray()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteUsers.ReplaceAllAsync(7, [9, 12], ct), SiteMembershipJson.Links);

		call.ShouldBe(HttpMethod.Put, "/api/3/sites/7/users", body: "[9,12]");
	}

	[Fact]
	public async Task AddAsync_SendsPostWithTheBareUserId()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteUsers.AddAsync(7, 9, ct), UserReferenceJson);

		call.ShouldBe(HttpMethod.Post, "/api/3/sites/7/users", body: "9");
	}

	[Fact]
	public async Task AddAsync_ReturnsTheUserReference()
	{
		var reference = await TestClient.ReadAsync((c, ct) => c.SiteUsers.AddAsync(7, 9, ct), UserReferenceJson);

		reference.Id.Should().Be(9);
		reference.Links.Should().ContainSingle().Which.Href.Should().EndWith("/users/9");
	}

	[Fact]
	public async Task RemoveAsync_SendsDeleteToTheUser()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteUsers.RemoveAsync(7, 9, ct), SiteMembershipJson.Links);

		call.ShouldBe(HttpMethod.Delete, "/api/3/sites/7/users/9");
	}

	[Fact]
	public Task AddAsync_Forbidden_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync(
			(c, ct) => c.SiteUsers.AddAsync(7, 9, ct),
			HttpStatusCode.Forbidden,
			"""{"status":"FORBIDDEN","message":"Insufficient privileges.","links":[]}""",
			"Insufficient privileges.");
}
