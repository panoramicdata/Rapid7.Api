using Rapid7.Api.Models;
using Rapid7.Api.Models.ScanEngines;
using Rapid7.Api.Models.Scans;
using Rapid7.Api.Test.Support;
using System.Net;

namespace Rapid7.Api.Test.Groups;

public class ScanEnginesTests
{
	private const string Sites = """
		{
			"resources": [
				{
					"id": 5,
					"name": "External",
					"description": "Internet-facing hosts",
					"importance": "high",
					"type": "dynamic",
					"connectionType": "activesync-office365",
					"assets": 768,
					"lastScanTime": "2026-09-05T02:15:30Z",
					"riskScore": 4457823.78,
					"scanEngine": 2,
					"scanTemplate": "full-audit-without-web-spider",
					"vulnerabilities": { "critical": 1, "severe": 2, "moderate": 3, "total": 6 },
					"links": [{ "href": "https://console.test:3780/api/3/sites/5", "rel": "self" }]
				}
			],
			"page": { "number": 1, "size": 20, "totalPages": 3, "totalResources": 41 },
			"links": []
		}
		""";

	private static readonly ScanEngineRequest Request = new() { Name = "Branch engine", Address = "10.0.0.5", Port = 40814, Sites = [1, 2] };

	[Fact]
	public async Task ListAsync_SendsGet()
		=> (await TestClient.CaptureAsync((c, ct) => c.ScanEngines.ListAsync(ct), ScanJson.Empty))
			.ShouldBe(HttpMethod.Get, "/api/3/scan_engines");

	[Fact]
	public async Task CreateAsync_PostsTheEngine()
		=> (await TestClient.CaptureAsync((c, ct) => c.ScanEngines.CreateAsync(Request, ct), """{"id":9,"links":[]}"""))
			.ShouldBe(HttpMethod.Post, "/api/3/scan_engines", body: """{"name":"Branch engine","address":"10.0.0.5","port":40814,"sites":[1,2]}""");

	[Fact]
	public async Task GetAsync_SendsGet()
		=> (await TestClient.CaptureAsync((c, ct) => c.ScanEngines.GetAsync(2, ct), ScanJson.Engine))
			.ShouldBe(HttpMethod.Get, "/api/3/scan_engines/2");

	[Fact]
	public async Task UpdateAsync_PutsTheEngine_LeavingOutNullSites()
		=> (await TestClient.CaptureAsync(
				(c, ct) => c.ScanEngines.UpdateAsync(2, new ScanEngineRequest { Name = "Renamed", Address = "engine.example.test", Port = 40894 }, ct),
				ScanJson.LinksOnly))
			.ShouldBe(HttpMethod.Put, "/api/3/scan_engines/2", body: """{"name":"Renamed","address":"engine.example.test","port":40894}""");

	[Fact]
	public async Task DeleteAsync_SendsDelete()
		=> (await TestClient.CaptureAsync((c, ct) => c.ScanEngines.DeleteAsync(2, ct), ScanJson.LinksOnly))
			.ShouldBe(HttpMethod.Delete, "/api/3/scan_engines/2");

	[Fact]
	public async Task ListPoolsAsync_SendsGet()
		=> (await TestClient.CaptureAsync((c, ct) => c.ScanEngines.ListPoolsAsync(2, ct), ScanJson.Pools))
			.ShouldBe(HttpMethod.Get, "/api/3/scan_engines/2/scan_engine_pools");

	[Fact]
	public async Task ListScansAsync_SendsPaging()
		=> (await TestClient.CaptureAsync(
				(c, ct) => c.ScanEngines.ListScansAsync(2, new PageOptions { Page = 1, Size = 50, Sort = ["id,DESC"] }, ct),
				ScanJson.GlobalScans))
			.ShouldBe(HttpMethod.Get, "/api/3/scan_engines/2/scans", "?page=1&size=50&sort=id%2CDESC");

	[Fact]
	public async Task ListSitesAsync_SendsGet_WithoutPaging()
		=> (await TestClient.CaptureAsync((c, ct) => c.ScanEngines.ListSitesAsync(2, null, ct), Sites))
			.ShouldBe(HttpMethod.Get, "/api/3/scan_engines/2/sites");

	[Fact]
	public async Task GetAsync_MapsEveryField()
	{
		var engine = await TestClient.ReadAsync((c, ct) => c.ScanEngines.GetAsync(2, ct), ScanJson.Engine);

		engine.Id.Should().Be(2);
		engine.Name.Should().Be("Corporate Scan Engine 001");
		engine.Address.Should().Be("engine-001.example.test");
		engine.Port.Should().Be(40894);
		engine.Sites.Should().Equal(1, 4);
		engine.Status.Should().Be(ScanEngineStatus.PendingAuthorization);
		engine.ContentVersion.Should().Be("1735689600");
		engine.ProductVersion.Should().Be("6.6.250");
		engine.SerialNumber.Should().Be("ENG-0001");
		engine.IsAwsPreAuthorizedEngine.Should().BeFalse();
		engine.LastRefreshedDate.Should().Be(new DateTimeOffset(2026, 9, 1, 10, 0, 0, TimeSpan.Zero));
		engine.LastUpdatedDate.Should().Be(new DateTimeOffset(2026, 9, 2, 11, 30, 0, 500, TimeSpan.Zero));
		engine.Items.Should().ContainSingle().Which.Rel.Should().Be("self");
	}

	[Fact]
	public async Task ListAsync_MapsEngines_AndDefaultsMissingFields()
	{
		var engines = await TestClient.ReadAsync(
			(c, ct) => c.ScanEngines.ListAsync(ct),
			"""{"resources":[{"id":3,"status":"not-responding"},{"status":"some-future-state"}],"links":[]}""");

		engines.Resources.Should().HaveCount(2);
		engines.Resources[0].Status.Should().Be(ScanEngineStatus.NotResponding);
		engines.Resources[0].Name.Should().BeEmpty();
		engines.Resources[0].Address.Should().BeEmpty();
		engines.Resources[0].Sites.Should().BeEmpty();
		engines.Resources[1].Status.Should().Be(ScanEngineStatus.Unknown);
	}

	[Fact]
	public async Task CreateAsync_ReadsTheNewIdentifier()
	{
		var created = await TestClient.ReadAsync((c, ct) => c.ScanEngines.CreateAsync(Request, ct), """{"id":9,"links":[]}""");

		created.Id.Should().Be(9);
	}

	[Fact]
	public async Task ListPoolsAsync_MapsThePools()
	{
		var pools = await TestClient.ReadAsync((c, ct) => c.ScanEngines.ListPoolsAsync(2, ct), ScanJson.Pools);

		pools.Resources.Should().ContainSingle().Which.Name.Should().Be("Datacentre pool");
	}

	[Fact]
	public async Task ListScansAsync_MapsScans()
	{
		var scans = await TestClient.ReadAsync((c, ct) => c.ScanEngines.ListScansAsync(2, null, ct), ScanJson.GlobalScans);

		scans.Resources.Should().HaveCount(2);
		scans.Resources[0].Should().BeOfType<Scan>().Which.Status.Should().Be(ScanStatus.Finished);
		scans.PageInfo!.TotalResources.Should().Be(2);
	}

	[Fact]
	public async Task ListSitesAsync_MapsEveryField()
	{
		var page = await TestClient.ReadAsync((c, ct) => c.ScanEngines.ListSitesAsync(2, null, ct), Sites);

		var site = page.Resources.Should().ContainSingle().Subject;
		site.Id.Should().Be(5);
		site.Name.Should().Be("External");
		site.Description.Should().Be("Internet-facing hosts");
		site.Importance.Should().Be("high");
		site.Type.Should().Be(ScanEngineSiteType.Dynamic);
		site.ConnectionType.Should().Be(ScanEngineSiteConnectionType.ActiveSyncOffice365);
		site.Assets.Should().Be(768);
		site.LastScanTime.Should().Be(new DateTimeOffset(2026, 9, 5, 2, 15, 30, TimeSpan.Zero));
		site.RiskScore.Should().Be(4457823.78);
		site.ScanEngine.Should().Be(2);
		site.ScanTemplate.Should().Be("full-audit-without-web-spider");
		site.Vulnerabilities!.Total.Should().Be(6);
		site.Items.Should().ContainSingle();
		page.PageInfo!.Number.Should().Be(1);
	}

	[Fact]
	public Task DeleteAsync_EngineInUse_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync(
			(c, ct) => c.ScanEngines.DeleteAsync(2, ct),
			HttpStatusCode.BadRequest,
			"""{"status":400,"message":"The scan engine is assigned to sites.","links":[]}""",
			"The scan engine is assigned to sites.");
}
