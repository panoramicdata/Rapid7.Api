using Rapid7.Api.Test.Support;
using System.Net;

namespace Rapid7.Api.Test.Groups;

public class PrivilegesTests
{
	[Fact]
	public async Task ListAsync_SendsGetToPrivileges()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.Privileges.ListAsync(ct), UserJson.Privileges);

		call.ShouldBe(HttpMethod.Get, "/api/3/privileges");
	}

	[Fact]
	public async Task ListAsync_MapsThePrivileges()
	{
		var privileges = await TestClient.ReadAsync((c, ct) => c.Privileges.ListAsync(ct), UserJson.Privileges);

		privileges.Resources.Should().Equal("all-permissions", "manage-sites");
		privileges.Items.Should().ContainSingle().Which.Href.Should().Be("https://console.test:3780/api/3/privileges");
	}

	[Fact]
	public async Task GetAsync_SendsGetToThePrivilege()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.Privileges.GetAsync("manage-sites", ct), AccessJson.LinksOnly);

		call.ShouldBe(HttpMethod.Get, "/api/3/privileges/manage-sites");
	}

	[Fact]
	public async Task GetAsync_ReturnsTheLinks()
		=> AccessJson.ShouldBeTheSelfLink(await TestClient.ReadAsync((c, ct) => c.Privileges.GetAsync("manage-sites", ct), AccessJson.LinksOnly));

	[Fact]
	public async Task ListUsersAsync_SendsGetToThePrivilegesUsers()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.Privileges.ListUsersAsync("manage-sites", ct), AccessJson.Ids);

		call.ShouldBe(HttpMethod.Get, "/api/3/privileges/manage-sites/users");
	}

	[Fact]
	public async Task ListUsersAsync_MapsTheUserIds()
		=> AccessJson.ShouldBeTheIds(await TestClient.ReadAsync((c, ct) => c.Privileges.ListUsersAsync("manage-sites", ct), AccessJson.Ids));

	[Fact]
	public Task GetAsync_UnknownPrivilege_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync(
			(c, ct) => c.Privileges.GetAsync("no-such-privilege", ct),
			HttpStatusCode.NotFound,
			AccessJson.Error("NOT_FOUND", "The resource cannot be found."),
			"The resource cannot be found.");
}
