using Rapid7.Api.Test.Support;
using System.Net;

namespace Rapid7.Api.Test.Groups;

public class SiteTargetsTests
{
	private const string TargetsJson = """
		{
			"addresses": [ "192.0.2.0/24", "198.51.100.1 - 198.51.100.9", "host.example.test", "2001:db8::1" ],
			"links": [ { "href": "https://console.test:3780/api/3/sites/7/included_targets", "rel": "self" } ]
		}
		""";

	private const string AssetGroupsJson = """
		{
			"resources": [
				{
					"assets": 768,
					"description": "Assets needing urgent remediation.",
					"id": 61,
					"name": "High Risk Assets",
					"riskScore": 4457823.78,
					"type": "dynamic",
					"links": [ { "href": "https://console.test:3780/api/3/asset_groups/61", "rel": "self" } ]
				}
			],
			"links": [ { "href": "https://console.test:3780/api/3/sites/7/included_asset_groups", "rel": "self" } ]
		}
		""";

	private static readonly string[] Addresses = ["192.0.2.0/24", "host.example.test"];

	private const string AddressesBody = """["192.0.2.0/24","host.example.test"]""";

	[Fact]
	public async Task GetIncludedTargetsAsync_SendsGet()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteTargets.GetIncludedTargetsAsync(7, ct), TargetsJson);

		call.ShouldBe(HttpMethod.Get, "/api/3/sites/7/included_targets");
	}

	[Fact]
	public async Task GetIncludedTargetsAsync_MapsEveryAddress()
	{
		var targets = await TestClient.ReadAsync((c, ct) => c.SiteTargets.GetIncludedTargetsAsync(7, ct), TargetsJson);

		targets.Addresses.Should().Equal("192.0.2.0/24", "198.51.100.1 - 198.51.100.9", "host.example.test", "2001:db8::1");
		targets.Links.Should().ContainSingle().Which.Href.Should().EndWith("/sites/7/included_targets");
	}

	[Fact]
	public async Task AddIncludedTargetsAsync_SendsPostWithABareArray()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteTargets.AddIncludedTargetsAsync(7, Addresses, ct), SiteMembershipJson.Reference);

		call.ShouldBe(HttpMethod.Post, "/api/3/sites/7/included_targets", body: AddressesBody);
	}

	[Fact]
	public async Task AddIncludedTargetsAsync_ReturnsTheSiteReference()
	{
		var reference = await TestClient.ReadAsync((c, ct) => c.SiteTargets.AddIncludedTargetsAsync(7, Addresses, ct), SiteMembershipJson.Reference);

		reference.Id.Should().Be(7);
		reference.ShouldLinkToSite();
	}

	[Fact]
	public async Task ReplaceIncludedTargetsAsync_SendsPutWithABareArray()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteTargets.ReplaceIncludedTargetsAsync(7, Addresses, ct), SiteMembershipJson.Links);

		call.ShouldBe(HttpMethod.Put, "/api/3/sites/7/included_targets", body: AddressesBody);
	}

	[Fact]
	public async Task RemoveIncludedTargetsAsync_SendsDeleteWithABareArray()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteTargets.RemoveIncludedTargetsAsync(7, Addresses, ct), SiteMembershipJson.Links);

		call.ShouldBe(HttpMethod.Delete, "/api/3/sites/7/included_targets", body: AddressesBody);
	}

	[Fact]
	public async Task GetExcludedTargetsAsync_SendsGet()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteTargets.GetExcludedTargetsAsync(7, ct), TargetsJson);

		call.ShouldBe(HttpMethod.Get, "/api/3/sites/7/excluded_targets");
	}

	[Fact]
	public async Task AddExcludedTargetsAsync_SendsPostWithABareArray()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteTargets.AddExcludedTargetsAsync(7, Addresses, ct), SiteMembershipJson.Reference);

		call.ShouldBe(HttpMethod.Post, "/api/3/sites/7/excluded_targets", body: AddressesBody);
	}

	[Fact]
	public async Task ReplaceExcludedTargetsAsync_SendsPutWithABareArray()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteTargets.ReplaceExcludedTargetsAsync(7, Addresses, ct), SiteMembershipJson.Links);

		call.ShouldBe(HttpMethod.Put, "/api/3/sites/7/excluded_targets", body: AddressesBody);
	}

	[Fact]
	public async Task RemoveExcludedTargetsAsync_SendsDeleteWithABareArray()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteTargets.RemoveExcludedTargetsAsync(7, Addresses, ct), SiteMembershipJson.Links);

		call.ShouldBe(HttpMethod.Delete, "/api/3/sites/7/excluded_targets", body: AddressesBody);
	}

	[Fact]
	public async Task ListIncludedAssetGroupsAsync_SendsGet()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteTargets.ListIncludedAssetGroupsAsync(7, ct), AssetGroupsJson);

		call.ShouldBe(HttpMethod.Get, "/api/3/sites/7/included_asset_groups");
	}

	[Fact]
	public async Task ListIncludedAssetGroupsAsync_MapsTheAssetGroups()
	{
		var list = await TestClient.ReadAsync((c, ct) => c.SiteTargets.ListIncludedAssetGroupsAsync(7, ct), AssetGroupsJson);

		var group = list.Resources.Should().ContainSingle().Subject;
		group.Id.Should().Be(61);
		group.Name.Should().Be("High Risk Assets");
		group.Description.Should().Be("Assets needing urgent remediation.");
		group.Assets.Should().Be(768);
		group.RiskScore.Should().Be(4457823.78);
		group.Links.Should().ContainSingle().Which.Href.Should().EndWith("/asset_groups/61");
		list.Links.Should().ContainSingle();
	}

	[Fact]
	public async Task ReplaceIncludedAssetGroupsAsync_SendsPutWithABareIdArray()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteTargets.ReplaceIncludedAssetGroupsAsync(7, [61, 62], ct), SiteMembershipJson.Links);

		call.ShouldBe(HttpMethod.Put, "/api/3/sites/7/included_asset_groups", body: "[61,62]");
	}

	[Fact]
	public async Task RemoveAllIncludedAssetGroupsAsync_SendsDelete()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteTargets.RemoveAllIncludedAssetGroupsAsync(7, ct), SiteMembershipJson.Links);

		call.ShouldBe(HttpMethod.Delete, "/api/3/sites/7/included_asset_groups");
	}

	[Fact]
	public async Task RemoveIncludedAssetGroupAsync_SendsDeleteToTheGroup()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteTargets.RemoveIncludedAssetGroupAsync(7, 61, ct), SiteMembershipJson.Links);

		call.ShouldBe(HttpMethod.Delete, "/api/3/sites/7/included_asset_groups/61");
	}

	[Fact]
	public async Task ListExcludedAssetGroupsAsync_SendsGet()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteTargets.ListExcludedAssetGroupsAsync(7, ct), AssetGroupsJson);

		call.ShouldBe(HttpMethod.Get, "/api/3/sites/7/excluded_asset_groups");
	}

	[Fact]
	public async Task ReplaceExcludedAssetGroupsAsync_SendsPutWithABareIdArray()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteTargets.ReplaceExcludedAssetGroupsAsync(7, [61], ct), SiteMembershipJson.Links);

		call.ShouldBe(HttpMethod.Put, "/api/3/sites/7/excluded_asset_groups", body: "[61]");
	}

	[Fact]
	public async Task RemoveAllExcludedAssetGroupsAsync_SendsDelete()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteTargets.RemoveAllExcludedAssetGroupsAsync(7, ct), SiteMembershipJson.Links);

		call.ShouldBe(HttpMethod.Delete, "/api/3/sites/7/excluded_asset_groups");
	}

	[Fact]
	public async Task RemoveExcludedAssetGroupAsync_SendsDeleteToTheGroup()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteTargets.RemoveExcludedAssetGroupAsync(7, 61, ct), SiteMembershipJson.Links);

		call.ShouldBe(HttpMethod.Delete, "/api/3/sites/7/excluded_asset_groups/61");
	}

	[Fact]
	public Task AddIncludedTargetsAsync_InvalidAddress_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync(
			(c, ct) => c.SiteTargets.AddIncludedTargetsAsync(7, ["not an address"], ct),
			HttpStatusCode.BadRequest,
			"""{"status":"BAD_REQUEST","message":"The address 'not an address' is not valid.","links":[]}""",
			"The address 'not an address' is not valid.");
}
