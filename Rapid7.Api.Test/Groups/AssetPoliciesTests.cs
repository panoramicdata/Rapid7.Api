using Rapid7.Api.Models;
using Rapid7.Api.Models.Policies;
using Rapid7.Api.Test.Support;
using System.Net;

namespace Rapid7.Api.Test.Groups;

public class AssetPoliciesTests
{
	private static readonly PageOptions Paging = new() { Size = 20 };

	[Fact]
	public async Task GetPoliciesAsync_SendsApplicableOnlyAndPaging()
	{
		var options = new PolicyResultListOptions { ApplicableOnly = true, Page = 3 };

		var call = await TestClient.CaptureAsync((c, ct) => c.AssetPolicies.GetPoliciesAsync(282, options, ct), PolicyJson.PolicyPage);

		call.ShouldBe(HttpMethod.Get, "/api/3/assets/282/policies", "?applicableOnly=true&page=3");
	}

	[Fact]
	public async Task GetPoliciesAsync_MapsEveryField()
	{
		var page = await TestClient.ReadAsync((c, ct) => c.AssetPolicies.GetPoliciesAsync(282, null, ct), PolicyJson.PolicyPage);

		page.Resources.Should().ContainSingle().Which.ShouldBeExamplePolicy();
	}

	[Fact]
	public async Task GetChildrenAsync_SendsGet()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.AssetPolicies.GetChildrenAsync(282, 84, ct), PolicyJson.ItemPage);

		call.ShouldBe(HttpMethod.Get, "/api/3/assets/282/policies/84/children");
	}

	[Fact]
	public async Task GetChildrenAsync_MapsEveryField()
	{
		var page = await TestClient.ReadAsync((c, ct) => c.AssetPolicies.GetChildrenAsync(282, 84, ct), PolicyJson.ItemPage);

		page.Resources.Should().ContainSingle().Which.ShouldBeExampleItem();
	}

	[Fact]
	public async Task GetGroupChildrenAsync_SendsGet()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.AssetPolicies.GetGroupChildrenAsync(282, 84, 71, ct), PolicyJson.ItemPage);

		call.ShouldBe(HttpMethod.Get, "/api/3/assets/282/policies/84/groups/71/children");
	}

	[Fact]
	public async Task GetRulesAsync_SendsPaging()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.AssetPolicies.GetRulesAsync(282, 84, Paging, ct), PolicyJson.RulePage);

		call.ShouldBe(HttpMethod.Get, "/api/3/assets/282/policies/84/rules", "?size=20");
	}

	[Fact]
	public async Task GetRulesAsync_MapsEveryField()
	{
		var page = await TestClient.ReadAsync((c, ct) => c.AssetPolicies.GetRulesAsync(282, 84, null, ct), PolicyJson.RulePage);

		page.Resources.Should().ContainSingle().Which.ShouldBeExampleRule();
	}

	[Fact]
	public async Task GetGroupRulesAsync_SendsPaging()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.AssetPolicies.GetGroupRulesAsync(282, 84, 71, Paging, ct), PolicyJson.RulePage);

		call.ShouldBe(HttpMethod.Get, "/api/3/assets/282/policies/84/groups/71/rules", "?size=20");
	}

	[Fact]
	public Task GetPoliciesAsync_NotFound_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync((c, ct) => c.AssetPolicies.GetPoliciesAsync(999, null, ct), HttpStatusCode.NotFound, PolicyJson.NotFound, PolicyJson.NotFoundMessage);
}
