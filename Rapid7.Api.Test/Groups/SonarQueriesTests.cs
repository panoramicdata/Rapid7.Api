using Rapid7.Api.Models.AssetDiscovery;
using Rapid7.Api.Test.Support;
using System.Net;
using static Rapid7.Api.Test.Groups.AssetSamples;

namespace Rapid7.Api.Test.Groups;

public class SonarQueriesTests
{
	private const string Criteria = """
		{"filters":[{"domain":"example.test","type":"domain-contains"},{"days":30,"type":"scan-date-within-the-last"},{"lower":"192.0.2.1","type":"ip-address-range","upper":"192.0.2.254"}]}
		""";

	private const string CriteriaBody = """{"filters":[{"domain":"example.test","type":"domain-contains"},{"days":30,"type":"scan-date-within-the-last"},{"lower":"192.0.2.1","type":"ip-address-range","upper":"192.0.2.254"}]}""";

	private const string Query = $$"""{"criteria":{{Criteria}},"id":14,"links":[{{SelfLink}}],"name":"Assets in Domain"}""";

	private const string DiscoveredAsset = $$"""{"address":"192.0.2.203","links":[{{SelfLink}}],"name":"desktop-27.example.test"}""";

	private static readonly SonarCriteria SampleCriteria = new()
	{
		Filters =
		[
			new SonarCriterion { Type = SonarCriterionType.DomainContains, Domain = "example.test" },
			new SonarCriterion { Type = SonarCriterionType.ScanDateWithinTheLast, Days = 30 },
			new SonarCriterion { Type = SonarCriterionType.IpAddressRange, Lower = "192.0.2.1", Upper = "192.0.2.254" },
		],
	};

	private static readonly SonarQueryRequest SampleRequest = new() { Name = "Assets in Domain", Criteria = SampleCriteria };

	private const string RequestBody = $$"""{"name":"Assets in Domain","criteria":{{CriteriaBody}}}""";

	[Fact]
	public Task ListAsync_SendsGet()
		=> SendsAsync((c, ct) => c.SonarQueries.ListAsync(ct), HttpMethod.Get, "/api/3/sonar_queries", response: EmptyList);

	[Fact]
	public Task CreateAsync_PostsTheQuery()
		=> SendsAsync(
			(c, ct) => c.SonarQueries.CreateAsync(SampleRequest, ct),
			HttpMethod.Post,
			"/api/3/sonar_queries",
			body: RequestBody,
			response: """{"id":14,"links":[]}""");

	[Fact]
	public Task GetAsync_SendsGet()
		=> SendsAsync((c, ct) => c.SonarQueries.GetAsync(14, ct), HttpMethod.Get, "/api/3/sonar_queries/14", response: Query);

	[Fact]
	public Task UpdateAsync_PutsTheQuery()
		=> SendsAsync((c, ct) => c.SonarQueries.UpdateAsync(14, SampleRequest, ct), HttpMethod.Put, "/api/3/sonar_queries/14", body: RequestBody);

	[Fact]
	public Task DeleteAsync_SendsDelete()
		=> SendsAsync((c, ct) => c.SonarQueries.DeleteAsync(14, ct), HttpMethod.Delete, "/api/3/sonar_queries/14");

	[Fact]
	public Task ListAssetsAsync_SendsGet()
		=> SendsAsync((c, ct) => c.SonarQueries.ListAssetsAsync(14, ct), HttpMethod.Get, "/api/3/sonar_queries/14/assets", response: EmptyList);

	[Fact]
	public Task SearchAsync_PostsTheCriteria()
		=> SendsAsync((c, ct) => c.SonarQueries.SearchAsync(SampleCriteria, ct), HttpMethod.Post, "/api/3/sonar_queries/search", body: CriteriaBody, response: "[]");

	[Fact]
	public async Task SearchAsync_IsAllowedOnAReadOnlyClient()
	{
		var stub = TestClient.Stub($"[{DiscoveredAsset}]");
		using var client = TestClient.Create(stub, o => o.ReadOnly = true);

		var assets = await client.SonarQueries.SearchAsync(new SonarCriteria(), TestContext.Current.CancellationToken);

		ShouldRoundTrip(assets.Should().ContainSingle().Subject, DiscoveredAsset);
	}

	[Fact]
	public async Task CreateAsync_ReturnsTheQueryId()
	{
		var created = await TestClient.ReadAsync((c, ct) => c.SonarQueries.CreateAsync(SampleRequest, ct), """{"id":14,"links":[]}""");

		created.Id.Should().Be(14L);
	}

	[Fact]
	public async Task ListAsync_MapsEveryField()
	{
		var list = await TestClient.ReadAsync((c, ct) => c.SonarQueries.ListAsync(ct), List(Query));

		ShouldRoundTrip(list.Resources.Should().ContainSingle().Subject, Query);
	}

	[Fact]
	public async Task GetAsync_MapsEveryField()
	{
		var query = await TestClient.ReadAsync((c, ct) => c.SonarQueries.GetAsync(14, ct), Query);

		ShouldRoundTrip(query, Query);
		query.Criteria!.Filters[1].Type.Should().Be(SonarCriterionType.ScanDateWithinTheLast);
		query.Criteria.Filters[1].Days.Should().Be(30);
	}

	[Fact]
	public async Task ListAssetsAsync_MapsEveryField()
	{
		var list = await TestClient.ReadAsync((c, ct) => c.SonarQueries.ListAssetsAsync(14, ct), List(DiscoveredAsset));

		var asset = list.Resources.Should().ContainSingle().Subject;
		ShouldRoundTrip(asset, DiscoveredAsset);
		asset.Name.Should().Be("desktop-27.example.test");
	}

	[Fact]
	public Task GetAsync_NotFound_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync((c, ct) => c.SonarQueries.GetAsync(999, ct), HttpStatusCode.NotFound, NotFound, NotFoundMessage);
}
