using Rapid7.Api.Models.ScanEngines;
using Rapid7.Api.Test.Support;

namespace Rapid7.Api.Test.Groups;

public class SiteScanEngineTests
{
	private const string EngineJson = """
		{
			"id": 2,
			"name": "Corporate Scan Engine 001",
			"address": "scan-engine-001.example.test",
			"port": 40894,
			"status": "active",
			"productVersion": "6.6.250",
			"contentVersion": "1316832890",
			"serialNumber": "0123456789abcdef",
			"isAWSPreAuthEngine": false,
			"lastRefreshedDate": "2026-10-01T08:00:00Z",
			"lastUpdatedDate": "2026-09-28T03:30:00Z",
			"sites": [ 7, 9 ],
			"links": [ { "href": "https://console.test:3780/api/3/scan_engines/2", "rel": "self" } ]
		}
		""";

	[Fact]
	public async Task GetAsync_SendsGetToTheSiteScanEngine()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteScanEngine.GetAsync(SiteFixtures.SiteId, ct), EngineJson);

		call.ShouldBe(HttpMethod.Get, "/api/3/sites/7/scan_engine");
	}

	[Fact]
	public async Task GetAsync_MapsEveryField()
	{
		var engine = await TestClient.ReadAsync((c, ct) => c.SiteScanEngine.GetAsync(SiteFixtures.SiteId, ct), EngineJson);

		engine.Id.Should().Be(2);
		engine.Name.Should().Be("Corporate Scan Engine 001");
		engine.Address.Should().Be("scan-engine-001.example.test");
		engine.Port.Should().Be(40894);
		engine.Status.Should().Be(ScanEngineStatus.Active);
		engine.ProductVersion.Should().Be("6.6.250");
		engine.ContentVersion.Should().Be("1316832890");
		engine.SerialNumber.Should().Be("0123456789abcdef");
		engine.IsAwsPreAuthEngine.Should().BeFalse();
		engine.LastRefreshedDate.Should().Be(new DateTimeOffset(2026, 10, 1, 8, 0, 0, TimeSpan.Zero));
		engine.LastUpdatedDate.Should().Be(new DateTimeOffset(2026, 9, 28, 3, 30, 0, TimeSpan.Zero));
		engine.Sites.Should().Equal(7, 9);
		engine.Items.Should().ContainSingle();
	}

	[Theory]
	[InlineData("not-responding", ScanEngineStatus.NotResponding)]
	[InlineData("pending-authorization", ScanEngineStatus.PendingAuthorization)]
	[InlineData("incompatible-version", ScanEngineStatus.IncompatibleVersion)]
	[InlineData("unknown", ScanEngineStatus.Unknown)]
	public async Task GetAsync_MapsEveryStatus(string wire, ScanEngineStatus expected)
	{
		var engine = await TestClient.ReadAsync(
			(c, ct) => c.SiteScanEngine.GetAsync(SiteFixtures.SiteId, ct),
			$$"""{"id":2,"name":"e","address":"a","port":1,"status":"{{wire}}","links":[]}""");

		engine.Status.Should().Be(expected);
	}

	[Fact]
	public async Task SetAsync_PutsTheBareEngineId()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteScanEngine.SetAsync(SiteFixtures.SiteId, 5, ct), SiteFixtures.LinksJson);

		call.ShouldBe(HttpMethod.Put, "/api/3/sites/7/scan_engine", body: "5");
	}

	[Fact]
	public async Task SetAsync_MapsTheLinks()
	{
		var links = await TestClient.ReadAsync((c, ct) => c.SiteScanEngine.SetAsync(SiteFixtures.SiteId, 5, ct), SiteFixtures.LinksJson);

		links.ShouldBeSiteLinks();
	}

	[Fact]
	public Task GetAsync_NotFound_RaisesRapid7ApiException()
		=> SiteFixtures.ShouldRaiseNotFoundAsync((c, ct) => c.SiteScanEngine.GetAsync(SiteFixtures.SiteId, ct));
}
