using Rapid7.Api.Models;
using Rapid7.Api.Models.Policies;
using Rapid7.Api.Test.Support;
using System.Net;

namespace Rapid7.Api.Test.Groups;

public class PolicyGroupsTests
{
	private static readonly PageOptions Paging = new() { Page = 0, Size = 500, Sort = ["id,DESC"] };

	private const string PagingQuery = "?page=0&size=500&sort=id%2CDESC";

	[Fact]
	public async Task GetGroupsAsync_SendsPaging()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.PolicyGroups.GetGroupsAsync(84, Paging, ct), PolicyJson.GroupPage);

		call.ShouldBe(HttpMethod.Get, "/api/3/policies/84/groups", PagingQuery);
	}

	[Fact]
	public async Task GetGroupsAsync_MapsEveryField()
	{
		var page = await TestClient.ReadAsync((c, ct) => c.PolicyGroups.GetGroupsAsync(84, null, ct), PolicyJson.GroupPage);

		page.Resources.Should().ContainSingle().Which.ShouldBeExampleGroup();
	}

	[Fact]
	public async Task GetGroupAsync_SendsGet()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.PolicyGroups.GetGroupAsync(84, 71, ct), PolicyJson.Group);

		call.ShouldBe(HttpMethod.Get, "/api/3/policies/84/groups/71");
	}

	[Fact]
	public async Task GetGroupAsync_MapsEveryField()
		=> (await TestClient.ReadAsync((c, ct) => c.PolicyGroups.GetGroupAsync(84, 71, ct), PolicyJson.Group)).ShouldBeExampleGroup();

	[Fact]
	public async Task GetChildrenAsync_SendsGet()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.PolicyGroups.GetChildrenAsync(84, 71, ct), PolicyJson.ItemPage);

		call.ShouldBe(HttpMethod.Get, "/api/3/policies/84/groups/71/children");
	}

	[Fact]
	public async Task GetChildrenAsync_MapsEveryField()
	{
		var page = await TestClient.ReadAsync((c, ct) => c.PolicyGroups.GetChildrenAsync(84, 71, ct), PolicyJson.ItemPage);

		page.Resources.Should().ContainSingle().Which.ShouldBeExampleItem();
	}

	[Fact]
	public async Task GetRulesAsync_SendsPaging()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.PolicyGroups.GetRulesAsync(84, 71, Paging, ct), PolicyJson.RulePage);

		call.ShouldBe(HttpMethod.Get, "/api/3/policies/84/groups/71/rules", PagingQuery);
	}

	[Fact]
	public async Task GetAssetResultsAsync_SendsApplicableOnly()
	{
		var options = new PolicyResultListOptions { ApplicableOnly = true };

		var call = await TestClient.CaptureAsync((c, ct) => c.PolicyGroups.GetAssetResultsAsync(84, 71, options, ct), PolicyJson.AssetPage);

		call.ShouldBe(HttpMethod.Get, "/api/3/policies/84/groups/71/assets", "?applicableOnly=true");
	}

	[Fact]
	public async Task GetAssetResultAsync_SendsGet()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.PolicyGroups.GetAssetResultAsync(84, 71, 282, ct), PolicyJson.Asset);

		call.ShouldBe(HttpMethod.Get, "/api/3/policies/84/groups/71/assets/282");
	}

	[Fact]
	public Task GetGroupAsync_NotFound_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync((c, ct) => c.PolicyGroups.GetGroupAsync(84, 999, ct), HttpStatusCode.NotFound, PolicyJson.NotFound, PolicyJson.NotFoundMessage);
}
