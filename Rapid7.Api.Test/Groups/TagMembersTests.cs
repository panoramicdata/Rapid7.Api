using Rapid7.Api.Models.Tags;
using Rapid7.Api.Test.Support;
using System.Net;

namespace Rapid7.Api.Test.Groups;

public class TagMembersTests
{
	private const string Assets = """
		{
			"resources": [
				{ "id": 282, "sources": ["tag", "site"] },
				{ "id": 283, "sources": ["asset-group", "criteria", "unknown", "something-new"] }
			],
			"links": [{ "href": "https://console.test:3780/api/3/tags/6/assets", "rel": "self" }]
		}
		""";

	[Fact]
	public async Task ListAssetsAsync_SendsGet()
		=> (await TestClient.CaptureAsync((c, ct) => c.TagMembers.ListAssetsAsync(6, ct), Assets))
			.ShouldBe(HttpMethod.Get, "/api/3/tags/6/assets");

	[Fact]
	public async Task AddAssetAsync_SendsPut()
		=> (await TestClient.CaptureAsync((c, ct) => c.TagMembers.AddAssetAsync(6, 4294967296, ct), ScanJson.LinksOnly))
			.ShouldBe(HttpMethod.Put, "/api/3/tags/6/assets/4294967296");

	[Fact]
	public async Task RemoveAssetAsync_SendsDelete()
		=> (await TestClient.CaptureAsync((c, ct) => c.TagMembers.RemoveAssetAsync(6, 282, ct), ScanJson.LinksOnly))
			.ShouldBe(HttpMethod.Delete, "/api/3/tags/6/assets/282");

	[Fact]
	public async Task ListAssetGroupsAsync_SendsGet()
		=> (await TestClient.CaptureAsync((c, ct) => c.TagMembers.ListAssetGroupsAsync(6, ct), ScanJson.Ids))
			.ShouldBe(HttpMethod.Get, "/api/3/tags/6/asset_groups");

	[Fact]
	public async Task SetAssetGroupsAsync_PutsTheIdentifiers()
		=> (await TestClient.CaptureAsync((c, ct) => c.TagMembers.SetAssetGroupsAsync(6, [3, 4], ct), ScanJson.LinksOnly))
			.ShouldBe(HttpMethod.Put, "/api/3/tags/6/asset_groups", body: "[3,4]");

	[Fact]
	public async Task RemoveAllAssetGroupsAsync_SendsDelete()
		=> (await TestClient.CaptureAsync((c, ct) => c.TagMembers.RemoveAllAssetGroupsAsync(6, ct), ScanJson.LinksOnly))
			.ShouldBe(HttpMethod.Delete, "/api/3/tags/6/asset_groups");

	[Fact]
	public async Task AddAssetGroupAsync_SendsPut()
		=> (await TestClient.CaptureAsync((c, ct) => c.TagMembers.AddAssetGroupAsync(6, 3, ct), ScanJson.LinksOnly))
			.ShouldBe(HttpMethod.Put, "/api/3/tags/6/asset_groups/3");

	[Fact]
	public async Task RemoveAssetGroupAsync_SendsDelete()
		=> (await TestClient.CaptureAsync((c, ct) => c.TagMembers.RemoveAssetGroupAsync(6, 3, ct), ScanJson.LinksOnly))
			.ShouldBe(HttpMethod.Delete, "/api/3/tags/6/asset_groups/3");

	[Fact]
	public async Task ListSitesAsync_SendsGet()
		=> (await TestClient.CaptureAsync((c, ct) => c.TagMembers.ListSitesAsync(6, ct), ScanJson.Ids))
			.ShouldBe(HttpMethod.Get, "/api/3/tags/6/sites");

	[Fact]
	public async Task SetSitesAsync_PutsTheIdentifiers()
		=> (await TestClient.CaptureAsync((c, ct) => c.TagMembers.SetSitesAsync(6, [5], ct), ScanJson.LinksOnly))
			.ShouldBe(HttpMethod.Put, "/api/3/tags/6/sites", body: "[5]");

	[Fact]
	public async Task RemoveAllSitesAsync_SendsDelete()
		=> (await TestClient.CaptureAsync((c, ct) => c.TagMembers.RemoveAllSitesAsync(6, ct), ScanJson.LinksOnly))
			.ShouldBe(HttpMethod.Delete, "/api/3/tags/6/sites");

	[Fact]
	public async Task AddSiteAsync_SendsPut()
		=> (await TestClient.CaptureAsync((c, ct) => c.TagMembers.AddSiteAsync(6, 5, ct), ScanJson.LinksOnly))
			.ShouldBe(HttpMethod.Put, "/api/3/tags/6/sites/5");

	[Fact]
	public async Task RemoveSiteAsync_SendsDelete()
		=> (await TestClient.CaptureAsync((c, ct) => c.TagMembers.RemoveSiteAsync(6, 5, ct), ScanJson.LinksOnly))
			.ShouldBe(HttpMethod.Delete, "/api/3/tags/6/sites/5");

	[Fact]
	public async Task ListAssetsAsync_MapsAssetsAndSources()
	{
		var assets = await TestClient.ReadAsync((c, ct) => c.TagMembers.ListAssetsAsync(6, ct), Assets);

		assets.Resources.Should().HaveCount(2);
		assets.Resources[0].Id.Should().Be(282);
		assets.Resources[0].Sources.Should().Equal(TaggedAssetSource.Tag, TaggedAssetSource.Site);
		assets.Resources[1].Sources.Should().Equal(
			TaggedAssetSource.AssetGroup, TaggedAssetSource.Criteria, TaggedAssetSource.Unknown, TaggedAssetSource.Unknown);
		assets.Links.Should().ContainSingle();
	}

	[Fact]
	public Task ListAssetGroupsAsync_MapsTheIdentifiers()
		=> ScanJson.ShouldReadIdsAsync((c, ct) => c.TagMembers.ListAssetGroupsAsync(6, ct));

	[Fact]
	public Task ListSitesAsync_MapsTheIdentifiers()
		=> ScanJson.ShouldReadIdsAsync((c, ct) => c.TagMembers.ListSitesAsync(6, ct));

	[Fact]
	public Task AddAssetAsync_MissingAsset_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync(
			(c, ct) => c.TagMembers.AddAssetAsync(6, 999, ct),
			HttpStatusCode.NotFound,
			"""{"status":"NOT_FOUND","message":"The asset does not exist.","links":[]}""",
			"The asset does not exist.");
}
