using Rapid7.Api.Models.ScanEngines;
using Rapid7.Api.Test.Support;
using System.Net;

namespace Rapid7.Api.Test.Groups;

public class ScanEnginePoolsTests
{
	private const string Pool = """
		{
			"id": 7,
			"name": "Datacentre pool",
			"engines": [2, 3],
			"sites": [5, 6],
			"links": [{ "href": "https://console.test:3780/api/3/scan_engine_pools/7", "rel": "self" }]
		}
		""";

	[Fact]
	public async Task ListAsync_SendsGet()
		=> (await TestClient.CaptureAsync((c, ct) => c.ScanEnginePools.ListAsync(ct), ScanJson.Pools))
			.ShouldBe(HttpMethod.Get, "/api/3/scan_engine_pools");

	[Fact]
	public async Task CreateAsync_PostsThePool()
		=> (await TestClient.CaptureAsync(
				(c, ct) => c.ScanEnginePools.CreateAsync(new EnginePoolRequest { Name = "New pool", Engines = [2, 3], Sites = [5] }, ct),
				"""{"id":8,"links":[]}"""))
			.ShouldBe(HttpMethod.Post, "/api/3/scan_engine_pools", body: """{"name":"New pool","engines":[2,3],"sites":[5]}""");

	[Fact]
	public async Task GetAsync_SendsGet()
		=> (await TestClient.CaptureAsync((c, ct) => c.ScanEnginePools.GetAsync(7, ct), Pool))
			.ShouldBe(HttpMethod.Get, "/api/3/scan_engine_pools/7");

	[Fact]
	public async Task UpdateAsync_PutsThePool_LeavingOutNulls()
		=> (await TestClient.CaptureAsync(
				(c, ct) => c.ScanEnginePools.UpdateAsync(7, new EnginePoolRequest { Name = "Renamed pool" }, ct),
				ScanJson.LinksOnly))
			.ShouldBe(HttpMethod.Put, "/api/3/scan_engine_pools/7", body: """{"name":"Renamed pool"}""");

	[Fact]
	public async Task DeleteAsync_SendsDelete()
		=> (await TestClient.CaptureAsync((c, ct) => c.ScanEnginePools.DeleteAsync(7, ct), ScanJson.LinksOnly))
			.ShouldBe(HttpMethod.Delete, "/api/3/scan_engine_pools/7");

	[Fact]
	public async Task ListEnginesAsync_SendsGet()
		=> (await TestClient.CaptureAsync((c, ct) => c.ScanEnginePools.ListEnginesAsync(7, ct), ScanJson.Ids))
			.ShouldBe(HttpMethod.Get, "/api/3/scan_engine_pools/7/engines");

	[Fact]
	public async Task SetEnginesAsync_PutsTheIdentifiers()
		=> (await TestClient.CaptureAsync((c, ct) => c.ScanEnginePools.SetEnginesAsync(7, [2, 3, 11], ct), ScanJson.LinksOnly))
			.ShouldBe(HttpMethod.Put, "/api/3/scan_engine_pools/7/engines", body: "[2,3,11]");

	[Fact]
	public async Task AddEngineAsync_SendsPut()
		=> (await TestClient.CaptureAsync((c, ct) => c.ScanEnginePools.AddEngineAsync(7, 11, ct), ScanJson.LinksOnly))
			.ShouldBe(HttpMethod.Put, "/api/3/scan_engine_pools/7/engines/11");

	[Fact]
	public async Task RemoveEngineAsync_SendsDelete()
		=> (await TestClient.CaptureAsync((c, ct) => c.ScanEnginePools.RemoveEngineAsync(7, 11, ct), ScanJson.LinksOnly))
			.ShouldBe(HttpMethod.Delete, "/api/3/scan_engine_pools/7/engines/11");

	[Fact]
	public async Task ListSitesAsync_SendsGet()
		=> (await TestClient.CaptureAsync((c, ct) => c.ScanEnginePools.ListSitesAsync(7, ct), ScanJson.Ids))
			.ShouldBe(HttpMethod.Get, "/api/3/scan_engine_pools/7/sites");

	[Fact]
	public async Task GetAsync_MapsEveryField()
	{
		var pool = await TestClient.ReadAsync((c, ct) => c.ScanEnginePools.GetAsync(7, ct), Pool);

		pool.Id.Should().Be(7);
		pool.Name.Should().Be("Datacentre pool");
		pool.Engines.Should().Equal(2, 3);
		pool.Sites.Should().Equal(5, 6);
		pool.Links.Should().ContainSingle().Which.Href.Should().Be("https://console.test:3780/api/3/scan_engine_pools/7");
	}

	[Fact]
	public async Task ListAsync_MapsThePools()
	{
		var pools = await TestClient.ReadAsync((c, ct) => c.ScanEnginePools.ListAsync(ct), ScanJson.Pools);

		var pool = pools.Resources.Should().ContainSingle().Subject;
		pool.Engines.Should().Equal(2, 3);
		pools.Links.Should().ContainSingle();
	}

	[Fact]
	public async Task ListEnginesAsync_MapsTheIdentifiers()
		=> (await TestClient.ReadAsync((c, ct) => c.ScanEnginePools.ListEnginesAsync(7, ct), ScanJson.Ids))
			.Resources.Should().Equal(2, 3, 11);

	[Fact]
	public async Task ListSitesAsync_MapsTheIdentifiers()
		=> (await TestClient.ReadAsync((c, ct) => c.ScanEnginePools.ListSitesAsync(7, ct), ScanJson.Ids))
			.Resources.Should().Equal(2, 3, 11);

	[Fact]
	public async Task CreateAsync_ReadsTheNewIdentifier()
		=> (await TestClient.ReadAsync(
				(c, ct) => c.ScanEnginePools.CreateAsync(new EnginePoolRequest { Name = "New pool" }, ct),
				"""{"id":8,"links":[]}"""))
			.Id.Should().Be(8);

	[Fact]
	public Task GetAsync_Missing_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync(
			(c, ct) => c.ScanEnginePools.GetAsync(99, ct),
			HttpStatusCode.NotFound,
			"""{"status":"NOT_FOUND","message":"The engine pool does not exist.","links":[]}""",
			"The engine pool does not exist.");
}
