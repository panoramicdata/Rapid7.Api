using Rapid7.Api.Models.AssetGroups;
using Rapid7.Api.Models.Assets;
using Rapid7.Api.Test.Support;
using System.Net;
using static Rapid7.Api.Test.Groups.AssetSamples;

namespace Rapid7.Api.Test.Groups;

public class AssetGroupsTests
{
	private static readonly SearchCriteria HighRisk = new()
	{
		Match = SearchMatch.Any,
		Filters = [new SearchFilter(SearchField.RiskScore, SearchOperator.IsGreaterThan) { Value = 10000.5 }],
	};

	private const string HighRiskBody = """{"match":"any","filters":[{"field":"risk-score","operator":"is-greater-than","value":10000.5}]}""";

	private static readonly AssetGroupRequest DynamicGroup = new()
	{
		Name = "High Risk Assets",
		Type = AssetGroupType.Dynamic,
		Description = "Assets with unacceptably high risk.",
		SearchCriteria = HighRisk,
	};

	private const string DynamicGroupBody = $$"""{"name":"High Risk Assets","type":"dynamic","description":"Assets with unacceptably high risk.","searchCriteria":{{HighRiskBody}}}""";

	[Fact]
	public Task ListAsync_SendsGetWithFiltersAndPaging()
		=> SendsAsync(
			(c, ct) => c.AssetGroups.ListAsync(new AssetGroupListOptions { Name = "risk", Type = AssetGroupType.Static, Page = 2, Size = 20 }, ct),
			HttpMethod.Get,
			"/api/3/asset_groups",
			"?name=risk&type=static&page=2&size=20",
			response: EmptyPage);

	[Fact]
	public Task CreateAsync_PostsTheGroup()
		=> SendsAsync(
			(c, ct) => c.AssetGroups.CreateAsync(DynamicGroup, ct),
			HttpMethod.Post,
			"/api/3/asset_groups",
			body: DynamicGroupBody,
			response: """{"id":61,"links":[]}""");

	[Fact]
	public Task GetAsync_SendsGet()
		=> SendsAsync((c, ct) => c.AssetGroups.GetAsync(61, ct), HttpMethod.Get, "/api/3/asset_groups/61", response: AssetGroupJson);

	[Fact]
	public Task UpdateAsync_PutsTheGroup()
		=> SendsAsync(
			(c, ct) => c.AssetGroups.UpdateAsync(61, new AssetGroupRequest { Name = "Lab", Type = AssetGroupType.Static }, ct),
			HttpMethod.Put,
			"/api/3/asset_groups/61",
			body: """{"name":"Lab","type":"static"}""");

	[Fact]
	public Task DeleteAsync_SendsDelete()
		=> SendsAsync((c, ct) => c.AssetGroups.DeleteAsync(61, ct), HttpMethod.Delete, "/api/3/asset_groups/61");

	[Fact]
	public Task GetSearchCriteriaAsync_SendsGet()
		=> SendsAsync((c, ct) => c.AssetGroups.GetSearchCriteriaAsync(61, ct), HttpMethod.Get, "/api/3/asset_groups/61/search_criteria", response: SearchCriteriaJson);

	[Fact]
	public Task SetSearchCriteriaAsync_PutsTheCriteria()
		=> SendsAsync((c, ct) => c.AssetGroups.SetSearchCriteriaAsync(61, HighRisk, ct), HttpMethod.Put, "/api/3/asset_groups/61/search_criteria", body: HighRiskBody);

	[Fact]
	public async Task CreateAsync_ReturnsTheGroupId()
	{
		var created = await TestClient.ReadAsync((c, ct) => c.AssetGroups.CreateAsync(DynamicGroup, ct), """{"id":61,"links":[]}""");

		created.Id.Should().Be(61);
	}

	[Fact]
	public async Task ListAsync_MapsEveryField()
	{
		var page = await TestClient.ReadAsync((c, ct) => c.AssetGroups.ListAsync(null, ct), Page(AssetGroupJson));

		ShouldRoundTrip(page.Resources.Should().ContainSingle().Subject, AssetGroupJson);
	}

	[Fact]
	public async Task GetAsync_MapsEveryField()
	{
		var group = await TestClient.ReadAsync((c, ct) => c.AssetGroups.GetAsync(61, ct), AssetGroupJson);

		ShouldRoundTrip(group, AssetGroupJson);
		group.Type.Should().Be(AssetGroupType.Dynamic);
		group.Assets.Should().Be(768);
		group.RiskScore.Should().Be(4457823.78);
		group.Vulnerabilities!.Severe.Should().Be(76);
		group.SearchCriteria!.Match.Should().Be(SearchMatch.All);
		group.Links.Should().ContainSingle();
	}

	[Fact]
	public async Task GetSearchCriteriaAsync_MapsEveryField()
	{
		var criteria = await TestClient.ReadAsync((c, ct) => c.AssetGroups.GetSearchCriteriaAsync(61, ct), SearchCriteriaJson);

		ShouldRoundTrip(criteria, SearchCriteriaJson);
	}

	[Fact]
	public Task GetAsync_NotFound_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync((c, ct) => c.AssetGroups.GetAsync(999, ct), HttpStatusCode.NotFound, NotFound, NotFoundMessage);
}
