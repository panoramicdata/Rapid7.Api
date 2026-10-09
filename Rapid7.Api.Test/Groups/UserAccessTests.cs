using Rapid7.Api.Test.Support;
using System.Net;

namespace Rapid7.Api.Test.Groups;

public class UserAccessTests
{
	[Fact]
	public async Task ListSitesAsync_SendsGetToTheUsersSites()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.UserAccess.ListSitesAsync(9, ct), AccessJson.Ids);

		call.ShouldBe(HttpMethod.Get, "/api/3/users/9/sites");
	}

	[Fact]
	public async Task ListSitesAsync_MapsTheSiteIds()
		=> AccessJson.ShouldBeTheIds(await TestClient.ReadAsync((c, ct) => c.UserAccess.ListSitesAsync(9, ct), AccessJson.Ids));

	[Fact]
	public async Task SetSitesAsync_PutsTheIdsAsAnArray()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.UserAccess.SetSitesAsync(9, [1, 2, 3], ct), AccessJson.LinksOnly);

		call.ShouldBe(HttpMethod.Put, "/api/3/users/9/sites", body: "[1,2,3]");
	}

	[Fact]
	public async Task RevokeAllSitesAsync_DeletesTheUsersSites()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.UserAccess.RevokeAllSitesAsync(9, ct), AccessJson.LinksOnly);

		call.ShouldBe(HttpMethod.Delete, "/api/3/users/9/sites");
	}

	[Fact]
	public async Task GrantSiteAsync_PutsTheSite()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.UserAccess.GrantSiteAsync(9, 5, ct), AccessJson.LinksOnly);

		call.ShouldBe(HttpMethod.Put, "/api/3/users/9/sites/5");
	}

	[Fact]
	public async Task RevokeSiteAsync_DeletesTheSite()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.UserAccess.RevokeSiteAsync(9, 5, ct), AccessJson.LinksOnly);

		call.ShouldBe(HttpMethod.Delete, "/api/3/users/9/sites/5");
	}

	[Fact]
	public async Task ListAssetGroupsAsync_SendsGetToTheUsersAssetGroups()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.UserAccess.ListAssetGroupsAsync(9, ct), AccessJson.Ids);

		call.ShouldBe(HttpMethod.Get, "/api/3/users/9/asset_groups");
	}

	[Fact]
	public async Task ListAssetGroupsAsync_MapsTheAssetGroupIds()
		=> AccessJson.ShouldBeTheIds(await TestClient.ReadAsync((c, ct) => c.UserAccess.ListAssetGroupsAsync(9, ct), AccessJson.Ids));

	[Fact]
	public async Task SetAssetGroupsAsync_PutsTheIdsAsAnArray()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.UserAccess.SetAssetGroupsAsync(9, [61, 62], ct), AccessJson.LinksOnly);

		call.ShouldBe(HttpMethod.Put, "/api/3/users/9/asset_groups", body: "[61,62]");
	}

	[Fact]
	public async Task RevokeAllAssetGroupsAsync_DeletesTheUsersAssetGroups()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.UserAccess.RevokeAllAssetGroupsAsync(9, ct), AccessJson.LinksOnly);

		call.ShouldBe(HttpMethod.Delete, "/api/3/users/9/asset_groups");
	}

	[Fact]
	public async Task GrantAssetGroupAsync_PutsTheAssetGroup()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.UserAccess.GrantAssetGroupAsync(9, 61, ct), AccessJson.LinksOnly);

		call.ShouldBe(HttpMethod.Put, "/api/3/users/9/asset_groups/61");
	}

	[Fact]
	public async Task RevokeAssetGroupAsync_DeletesTheAssetGroup()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.UserAccess.RevokeAssetGroupAsync(9, 61, ct), AccessJson.LinksOnly);

		call.ShouldBe(HttpMethod.Delete, "/api/3/users/9/asset_groups/61");
	}

	[Fact]
	public async Task GrantSiteAsync_ReturnsTheLinks()
		=> AccessJson.ShouldBeTheSelfLink(await TestClient.ReadAsync((c, ct) => c.UserAccess.GrantSiteAsync(9, 5, ct), AccessJson.LinksOnly));

	[Fact]
	public Task GrantSiteAsync_UserWithAllSites_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync(
			(c, ct) => c.UserAccess.GrantSiteAsync(9, 5, ct),
			HttpStatusCode.BadRequest,
			AccessJson.Error("BAD_REQUEST", "The user already has access to all sites."),
			"The user already has access to all sites.");
}
