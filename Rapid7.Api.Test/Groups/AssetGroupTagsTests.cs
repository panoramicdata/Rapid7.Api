using Rapid7.Api.Test.Support;
using System.Net;
using static Rapid7.Api.Test.Groups.AssetSamples;

namespace Rapid7.Api.Test.Groups;

public class AssetGroupTagsTests
{
	[Fact]
	public Task ListAsync_SendsGet()
		=> SendsAsync((c, ct) => c.AssetGroupTags.ListAsync(61, ct), HttpMethod.Get, "/api/3/asset_groups/61/tags", response: EmptyList);

	[Fact]
	public Task SetAsync_PutsTheTagIds()
		=> SendsAsync((c, ct) => c.AssetGroupTags.SetAsync(61, [6, 7], ct), HttpMethod.Put, "/api/3/asset_groups/61/tags", body: "[6,7]");

	[Fact]
	public Task RemoveAllAsync_SendsDelete()
		=> SendsAsync((c, ct) => c.AssetGroupTags.RemoveAllAsync(61, ct), HttpMethod.Delete, "/api/3/asset_groups/61/tags");

	[Fact]
	public Task AddAsync_SendsPutWithoutABody()
		=> SendsAsync((c, ct) => c.AssetGroupTags.AddAsync(61, 6, ct), HttpMethod.Put, "/api/3/asset_groups/61/tags/6");

	[Fact]
	public Task RemoveAsync_SendsDelete()
		=> SendsAsync((c, ct) => c.AssetGroupTags.RemoveAsync(61, 6, ct), HttpMethod.Delete, "/api/3/asset_groups/61/tags/6");

	[Fact]
	public async Task ListAsync_MapsTheTagIds()
	{
		var list = await TestClient.ReadAsync((c, ct) => c.AssetGroupTags.ListAsync(61, ct), List("6,7"));

		list.Resources.Should().Equal(6, 7);
	}

	[Fact]
	public Task SetAsync_NotFound_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync((c, ct) => c.AssetGroupTags.SetAsync(999, [6], ct), HttpStatusCode.NotFound, NotFound, NotFoundMessage);
}
