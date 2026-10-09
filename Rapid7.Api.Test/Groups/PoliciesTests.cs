using Rapid7.Api.Models.Policies;
using Rapid7.Api.Test.Support;
using System.Net;

namespace Rapid7.Api.Test.Groups;

public class PoliciesTests
{
	[Fact]
	public async Task GetPoliciesAsync_SendsTheFilterAndPaging()
	{
		var options = new PolicyListOptions { Filter = "CIS", ScannedOnly = true, Page = 1, Size = 50, Sort = ["title,ASC"] };

		var call = await TestClient.CaptureAsync((c, ct) => c.Policies.GetPoliciesAsync(options, ct), PolicyJson.PolicyPage);

		call.ShouldBe(HttpMethod.Get, "/api/3/policies", "?filter=CIS&scannedOnly=true&page=1&size=50&sort=title%2CASC");
	}

	[Fact]
	public async Task GetPoliciesAsync_WithoutOptions_SendsNoQuery()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.Policies.GetPoliciesAsync(null, ct), PolicyJson.PolicyPage);

		call.ShouldBe(HttpMethod.Get, "/api/3/policies");
	}

	[Fact]
	public async Task GetPoliciesAsync_MapsThePage()
	{
		var page = await TestClient.ReadAsync((c, ct) => c.Policies.GetPoliciesAsync(null, ct), PolicyJson.PolicyPage);

		page.PageInfo!.TotalResources.Should().Be(1);
		page.Items.Should().ContainSingle().Which.Rel.Should().Be("self");
		page.Resources.Should().ContainSingle().Which.ShouldBeExamplePolicy();
	}

	[Fact]
	public async Task GetPolicyAsync_SendsGet()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.Policies.GetPolicyAsync(84, ct), PolicyJson.Policy);

		call.ShouldBe(HttpMethod.Get, "/api/3/policies/84");
	}

	[Fact]
	public async Task GetPolicyAsync_MapsEveryField()
		=> (await TestClient.ReadAsync((c, ct) => c.Policies.GetPolicyAsync(84, ct), PolicyJson.Policy)).ShouldBeExamplePolicy();

	[Fact]
	public async Task GetChildrenAsync_SendsGet()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.Policies.GetChildrenAsync(84, ct), PolicyJson.ItemPage);

		call.ShouldBe(HttpMethod.Get, "/api/3/policies/84/children");
	}

	[Fact]
	public async Task GetChildrenAsync_MapsEveryField()
	{
		var page = await TestClient.ReadAsync((c, ct) => c.Policies.GetChildrenAsync(84, ct), PolicyJson.ItemPage);

		page.Resources.Should().ContainSingle().Which.ShouldBeExampleItem();
	}

	[Fact]
	public async Task GetAssetResultsAsync_SendsApplicableOnlyAndPaging()
	{
		var options = new PolicyResultListOptions { ApplicableOnly = true, Size = 100 };

		var call = await TestClient.CaptureAsync((c, ct) => c.Policies.GetAssetResultsAsync(84, options, ct), PolicyJson.AssetPage);

		call.ShouldBe(HttpMethod.Get, "/api/3/policies/84/assets", "?applicableOnly=true&size=100");
	}

	[Fact]
	public async Task GetAssetResultsAsync_MapsEveryField()
	{
		var page = await TestClient.ReadAsync((c, ct) => c.Policies.GetAssetResultsAsync(84, null, ct), PolicyJson.AssetPage);

		page.Resources.Should().ContainSingle().Which.ShouldBeExampleAsset();
	}

	[Fact]
	public async Task GetAssetResultAsync_SendsGet()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.Policies.GetAssetResultAsync(84, 282, ct), PolicyJson.Asset);

		call.ShouldBe(HttpMethod.Get, "/api/3/policies/84/assets/282");
	}

	[Fact]
	public async Task GetAssetResultAsync_MapsEveryField()
		=> (await TestClient.ReadAsync((c, ct) => c.Policies.GetAssetResultAsync(84, 282, ct), PolicyJson.Asset)).ShouldBeExampleAsset();

	[Fact]
	public async Task GetSummaryAsync_SendsGet()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.Policies.GetSummaryAsync(ct), PolicyJson.Summary);

		call.ShouldBe(HttpMethod.Get, "/api/3/policy/summary");
	}

	[Fact]
	public async Task GetSummaryAsync_MapsEveryField()
	{
		var summary = await TestClient.ReadAsync((c, ct) => c.Policies.GetSummaryAsync(ct), PolicyJson.Summary);

		summary.Should().BeEquivalentTo(new
		{
			DecreasedCompliance = 1,
			IncreasedCompliance = 2,
			Items = new[] { new { Rel = "self" } },
			NumberOfPolicies = 120,
			OverallCompliance = 0.81,
			ScannedPolicies = 5
		});
	}

	[Fact]
	public Task GetPolicyAsync_NotFound_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync((c, ct) => c.Policies.GetPolicyAsync(999, ct), HttpStatusCode.NotFound, PolicyJson.NotFound, PolicyJson.NotFoundMessage);
}
