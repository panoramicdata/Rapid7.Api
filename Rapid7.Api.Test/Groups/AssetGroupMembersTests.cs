using Rapid7.Api.Test.Support;
using System.Net;
using static Rapid7.Api.Test.Groups.AssetSamples;

namespace Rapid7.Api.Test.Groups;

public class AssetGroupMembersTests
{
	[Fact]
	public Task ListAssetsAsync_SendsGet()
		=> SendsAsync((c, ct) => c.AssetGroupMembers.ListAssetsAsync(61, ct), HttpMethod.Get, "/api/3/asset_groups/61/assets", response: EmptyList);

	[Fact]
	public Task SetAssetsAsync_PutsTheAssetIds()
		=> SendsAsync((c, ct) => c.AssetGroupMembers.SetAssetsAsync(61, [282, 5000000000], ct), HttpMethod.Put, "/api/3/asset_groups/61/assets", body: "[282,5000000000]");

	[Fact]
	public Task RemoveAllAssetsAsync_SendsDelete()
		=> SendsAsync((c, ct) => c.AssetGroupMembers.RemoveAllAssetsAsync(61, ct), HttpMethod.Delete, "/api/3/asset_groups/61/assets");

	[Fact]
	public Task AddAssetAsync_SendsPutWithoutABody()
		=> SendsAsync((c, ct) => c.AssetGroupMembers.AddAssetAsync(61, 282, ct), HttpMethod.Put, "/api/3/asset_groups/61/assets/282");

	[Fact]
	public Task RemoveAssetAsync_SendsDelete()
		=> SendsAsync((c, ct) => c.AssetGroupMembers.RemoveAssetAsync(61, 282, ct), HttpMethod.Delete, "/api/3/asset_groups/61/assets/282");

	[Fact]
	public Task ListUsersAsync_SendsGet()
		=> SendsAsync((c, ct) => c.AssetGroupMembers.ListUsersAsync(61, ct), HttpMethod.Get, "/api/3/asset_groups/61/users", response: EmptyList);

	[Fact]
	public Task SetUsersAsync_PutsTheUserIds()
		=> SendsAsync((c, ct) => c.AssetGroupMembers.SetUsersAsync(61, [1, 42], ct), HttpMethod.Put, "/api/3/asset_groups/61/users", body: "[1,42]");

	[Fact]
	public Task AddUserAsync_SendsPutWithoutABody()
		=> SendsAsync((c, ct) => c.AssetGroupMembers.AddUserAsync(61, 42, ct), HttpMethod.Put, "/api/3/asset_groups/61/users/42");

	[Fact]
	public Task RemoveUserAsync_SendsDelete()
		=> SendsAsync((c, ct) => c.AssetGroupMembers.RemoveUserAsync(61, 42, ct), HttpMethod.Delete, "/api/3/asset_groups/61/users/42");

	[Fact]
	public async Task ListAssetsAsync_MapsTheAssetIds()
	{
		var list = await TestClient.ReadAsync((c, ct) => c.AssetGroupMembers.ListAssetsAsync(61, ct), List("282,5000000000"));

		list.Resources.Should().Equal(282L, 5000000000L);
		list.Items.Should().ContainSingle();
	}

	[Fact]
	public async Task ListUsersAsync_MapsTheUserIds()
	{
		var list = await TestClient.ReadAsync((c, ct) => c.AssetGroupMembers.ListUsersAsync(61, ct), List("1,42"));

		list.Resources.Should().Equal(1, 42);
	}

	[Fact]
	public async Task SetAssetsAsync_MapsTheLinks()
	{
		var links = await TestClient.ReadAsync((c, ct) => c.AssetGroupMembers.SetAssetsAsync(61, [282], ct), LinksJson);

		links.Items.Should().ContainSingle().Which.Href.Should().Be("https://console.test:3780/api/3/assets/282");
	}

	[Fact]
	public Task AddAssetAsync_BadRequest_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync(
			(c, ct) => c.AssetGroupMembers.AddAssetAsync(61, 282, ct),
			HttpStatusCode.BadRequest,
			"""{"status":"BAD_REQUEST","message":"Assets can only be added to static asset groups.","links":[]}""",
			"Assets can only be added to static asset groups.");
}
