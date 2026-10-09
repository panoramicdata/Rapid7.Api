using Rapid7.Api.Models.Scans;
using Rapid7.Api.Test.Support;
using System.Net;

namespace Rapid7.Api.Test.Groups;

public class ScansTests
{
	private const string Started = """{"id":31,"links":[{"href":"https://console.test:3780/api/3/scans/31","rel":"self"}]}""";

	[Fact]
	public async Task ListAsync_SendsGet_WithoutOptions()
		=> (await TestClient.CaptureAsync((c, ct) => c.Scans.ListAsync(null, ct), ScanJson.GlobalScans))
			.ShouldBe(HttpMethod.Get, "/api/3/scans");

	[Fact]
	public async Task ListAsync_SendsActiveAndPaging()
		=> (await TestClient.CaptureAsync(
				(c, ct) => c.Scans.ListAsync(new ScanListOptions { Active = true, Page = 2, Size = 100 }, ct),
				ScanJson.GlobalScans))
			.ShouldBe(HttpMethod.Get, "/api/3/scans", "?active=true&page=2&size=100");

	[Fact]
	public async Task GetAsync_SendsGet()
		=> (await TestClient.CaptureAsync((c, ct) => c.Scans.GetAsync(30, ct), ScanJson.Scan))
			.ShouldBe(HttpMethod.Get, "/api/3/scans/30");

	[Theory]
	[InlineData(ScanStatusChange.Pause, "pause")]
	[InlineData(ScanStatusChange.Resume, "resume")]
	[InlineData(ScanStatusChange.Stop, "stop")]
	public async Task SetStatusAsync_PostsTheChangeInThePath(ScanStatusChange change, string wire)
		=> (await TestClient.CaptureAsync((c, ct) => c.Scans.SetStatusAsync(28, change, ct), ScanJson.LinksOnly))
			.ShouldBe(HttpMethod.Post, $"/api/3/scans/28/{wire}");

	[Fact]
	public async Task ListForSiteAsync_SendsActiveFalse()
		=> (await TestClient.CaptureAsync(
				(c, ct) => c.Scans.ListForSiteAsync(5, new ScanListOptions { Active = false, Sort = ["startTime,DESC"] }, ct),
				ScanJson.GlobalScans))
			.ShouldBe(HttpMethod.Get, "/api/3/sites/5/scans", "?active=false&sort=startTime%2CDESC");

	[Fact]
	public async Task StartForSiteAsync_PostsTheOverrides_AndTheBlackoutFlag()
		=> (await TestClient.CaptureAsync(
				(c, ct) => c.Scans.StartForSiteAsync(
					5,
					new AdhocScanRequest
					{
						Name = "Urgent recheck",
						TemplateId = "discovery",
						EngineId = 3,
						Hosts = ["10.0.0.1", "web.example.test"],
						AssetGroupIds = [4]
					},
					true,
					ct),
				Started))
			.ShouldBe(
				HttpMethod.Post,
				"/api/3/sites/5/scans",
				"?overrideBlackout=true",
				"""{"name":"Urgent recheck","templateId":"discovery","engineId":3,"hosts":["10.0.0.1","web.example.test"],"assetGroupIds":[4]}""");

	[Fact]
	public async Task StartForSiteAsync_WithEmptyRequest_SendsAnEmptyObject_AndNoQuery()
		=> (await TestClient.CaptureAsync((c, ct) => c.Scans.StartForSiteAsync(5, new AdhocScanRequest(), null, ct), Started))
			.ShouldBe(HttpMethod.Post, "/api/3/sites/5/scans", body: "{}");

	[Fact]
	public async Task StartForSiteAsync_ReadsTheScanIdentifier()
		=> (await TestClient.ReadAsync((c, ct) => c.Scans.StartForSiteAsync(5, new AdhocScanRequest(), false, ct), Started))
			.Id.Should().Be(31L);

	[Fact]
	public async Task ListAsync_MapsEveryField()
	{
		var page = await TestClient.ReadAsync((c, ct) => c.Scans.ListAsync(null, ct), ScanJson.GlobalScans);

		page.Resources.Should().HaveCount(2);
		var scan = page.Resources[0];
		scan.Id.Should().Be(28);
		scan.ScanName.Should().Be("Weekly external");
		scan.ScanType.Should().Be("Scheduled");
		scan.Status.Should().Be(ScanStatus.Finished);
		scan.Message.Should().Be("Completed normally");
		scan.StartTime.Should().Be(new DateTimeOffset(2026, 9, 5, 1, 0, 0, TimeSpan.Zero));
		scan.EndTime.Should().Be(new DateTimeOffset(2026, 9, 5, 2, 15, 30, TimeSpan.Zero));
		scan.Duration.Should().Be("PT1H15M30S");
		scan.StartedBy.Should().Be("Jane Admin");
		scan.StartedByUsername.Should().Be("jadmin");
		scan.EngineId.Should().Be(2);
		scan.EngineName.Should().Be("Corporate Scan Engine 001");
		scan.EngineIds!.Id.Should().Be(2);
		scan.EngineIds.NewScanEngine.Should().BeTrue();
		scan.EngineIds.Scope.Should().Be(ScanEngineScope.Silo);
		scan.Assets.Should().Be(42);
		scan.Vulnerabilities!.Critical.Should().Be(16);
		scan.Vulnerabilities.Severe.Should().Be(76);
		scan.Vulnerabilities.Moderate.Should().Be(3);
		scan.Vulnerabilities.Total.Should().Be(95);
		scan.SiteId.Should().Be(5);
		scan.SiteName.Should().Be("External");
		scan.Links.ShouldBeSelfOnly();
		page.Resources[1].Status.Should().Be(ScanStatus.Integrating);
		page.Resources[1].SiteId.Should().BeNull();
		page.PageInfo!.TotalPages.Should().Be(1);
	}

	[Fact]
	public async Task GetAsync_MapsTheScan()
	{
		var scan = await TestClient.ReadAsync((c, ct) => c.Scans.GetAsync(30, ct), ScanJson.Scan);

		scan.Id.Should().Be(30);
		scan.ScanType.Should().Be("Manual");
		scan.Status.Should().Be(ScanStatus.Paused);
		scan.EndTime.Should().BeNull();
		scan.EngineIds!.Scope.Should().Be(ScanEngineScope.Global);
		scan.EngineIds.NewScanEngine.Should().BeFalse();
		scan.Vulnerabilities!.Total.Should().Be(3);
	}

	[Theory]
	[InlineData("aborted", ScanStatus.Aborted)]
	[InlineData("running", ScanStatus.Running)]
	[InlineData("stopped", ScanStatus.Stopped)]
	[InlineData("error", ScanStatus.Error)]
	[InlineData("dispatched", ScanStatus.Dispatched)]
	[InlineData("unknown", ScanStatus.Unknown)]
	[InlineData("a-future-state", ScanStatus.Unknown)]
	public async Task GetAsync_MapsEveryStatus(string wire, ScanStatus expected)
		=> (await TestClient.ReadAsync((c, ct) => c.Scans.GetAsync(30, ct), $$"""{"id":30,"status":"{{wire}}","links":[]}"""))
			.Status.Should().Be(expected);

	[Fact]
	public async Task ListForSiteAsync_MapsScans()
	{
		var page = await TestClient.ReadAsync((c, ct) => c.Scans.ListForSiteAsync(5, null, ct), ScanJson.GlobalScans);

		page.Resources.Should().HaveCount(2);
		page.Resources[0].ScanName.Should().Be("Weekly external");
	}

	[Fact]
	public Task SetStatusAsync_WrongState_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync(
			(c, ct) => c.Scans.SetStatusAsync(28, ScanStatusChange.Resume, ct),
			HttpStatusCode.BadRequest,
			"""{"status":"BAD_REQUEST","message":"The scan is not paused.","links":[]}""",
			"The scan is not paused.");
}
