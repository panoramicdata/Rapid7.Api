using Rapid7.Api.Test.Support;
using System.Net;

namespace Rapid7.Api.Test.Groups;

public class SiteTagsTests
{
	private const string TagsJson = """
		{
			"resources": [
				{
					"color": "default",
					"created": "2017-10-07T23:50:01.205Z",
					"id": 6,
					"name": "My Custom Tag",
					"riskModifier": 2,
					"source": "custom",
					"type": "custom",
					"links": [ { "href": "https://console.test:3780/api/3/tags/6", "rel": "self" } ]
				}
			],
			"links": [ { "href": "https://console.test:3780/api/3/sites/7/tags", "rel": "self" } ]
		}
		""";

	[Fact]
	public async Task ListAsync_SendsGetToTheSiteTags()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteTags.ListAsync(7, ct), TagsJson);

		call.ShouldBe(HttpMethod.Get, "/api/3/sites/7/tags");
	}

	[Fact]
	public async Task ListAsync_MapsTheTags()
	{
		var list = await TestClient.ReadAsync((c, ct) => c.SiteTags.ListAsync(7, ct), TagsJson);

		var tag = list.Resources.Should().ContainSingle().Subject;
		tag.Id.Should().Be(6);
		tag.Name.Should().Be("My Custom Tag");
		tag.Created.Should().Be(new DateTimeOffset(2017, 10, 7, 23, 50, 1, 205, TimeSpan.Zero));
		tag.Links.Should().ContainSingle().Which.Href.Should().EndWith("/tags/6");
		list.Links.Should().ContainSingle();
	}

	[Fact]
	public async Task ReplaceAllAsync_SendsPutWithABareIdArray()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteTags.ReplaceAllAsync(7, [6, 8], ct), SiteMembershipJson.Links);

		call.ShouldBe(HttpMethod.Put, "/api/3/sites/7/tags", body: "[6,8]");
	}

	[Fact]
	public async Task AddAsync_SendsPutWithoutABody()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteTags.AddAsync(7, 6, ct), SiteMembershipJson.Links);

		call.ShouldBe(HttpMethod.Put, "/api/3/sites/7/tags/6");
	}

	[Fact]
	public async Task AddAsync_ReturnsTheLinks()
	{
		var links = await TestClient.ReadAsync((c, ct) => c.SiteTags.AddAsync(7, 6, ct), SiteMembershipJson.Links);

		links.ShouldLinkToSite();
	}

	[Fact]
	public async Task RemoveAsync_SendsDeleteToTheTag()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteTags.RemoveAsync(7, 6, ct), SiteMembershipJson.Links);

		call.ShouldBe(HttpMethod.Delete, "/api/3/sites/7/tags/6");
	}

	[Fact]
	public Task AddAsync_MissingTag_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync(
			(c, ct) => c.SiteTags.AddAsync(7, 404, ct),
			HttpStatusCode.NotFound,
			SiteMembershipJson.NotFound,
			SiteMembershipJson.NotFoundMessage);
}
