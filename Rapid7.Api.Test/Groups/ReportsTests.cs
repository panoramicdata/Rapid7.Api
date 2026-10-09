using Rapid7.Api.Models;
using Rapid7.Api.Models.Reports;
using Rapid7.Api.Test.Support;
using System.Net;

namespace Rapid7.Api.Test.Groups;

public class ReportsTests
{
	private static readonly ReportConfiguration SqlReport = new()
	{
		Name = "Asset export",
		Format = ReportFormat.SqlQuery,
		Query = "SELECT * FROM dim_asset",
		Version = "2.3.0",
		Scope = new ReportScope { Sites = [5] },
		Frequency = new ReportFrequency { Type = ReportFrequencyType.None }
	};

	private const string SqlReportBody = """{"name":"Asset export","format":"sql-query","query":"SELECT * FROM dim_asset","version":"2.3.0","scope":{"sites":[5]},"frequency":{"type":"none"}}""";

	[Fact]
	public async Task GetReportsAsync_SendsPaging()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.Reports.GetReportsAsync(new PageOptions { Size = 100, Sort = ["name"] }, ct), ReportJson.ReportPage);

		call.ShouldBe(HttpMethod.Get, "/api/3/reports", "?size=100&sort=name");
	}

	[Fact]
	public async Task GetReportsAsync_MapsEveryField()
	{
		var page = await TestClient.ReadAsync((c, ct) => c.Reports.GetReportsAsync(null, ct), ReportJson.ReportPage);

		ShouldBeExampleReport(page.Resources.Should().ContainSingle().Subject);
	}

	[Fact]
	public async Task CreateAsync_PostsTheConfiguration()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.Reports.CreateAsync(SqlReport, ct), ReportJson.Created);

		call.ShouldBe(HttpMethod.Post, "/api/3/reports", body: SqlReportBody);
	}

	[Fact]
	public async Task CreateAsync_ReadsTheNewIdentifier()
	{
		var created = await TestClient.ReadAsync((c, ct) => c.Reports.CreateAsync(SqlReport, ct), ReportJson.Created);

		created.ShouldBeCreated(17);
	}

	[Fact]
	public async Task GetAsync_SendsGet()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.Reports.GetAsync(17, ct), ReportJson.Report);

		call.ShouldBe(HttpMethod.Get, "/api/3/reports/17");
	}

	[Fact]
	public async Task GetAsync_MapsEveryField()
		=> ShouldBeExampleReport(await TestClient.ReadAsync((c, ct) => c.Reports.GetAsync(17, ct), ReportJson.Report));

	[Fact]
	public async Task UpdateAsync_PutsTheConfiguration()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.Reports.UpdateAsync(17, SqlReport, ct), ReportJson.LinksJson);

		call.ShouldBe(HttpMethod.Put, "/api/3/reports/17", body: SqlReportBody);
	}

	[Fact]
	public async Task UpdateAsync_WithAReportReadEarlier_SendsEverySettingButNotTheIdentifierOrLinks()
	{
		var stub = new StubHandler();
		stub.Enqueue(HttpStatusCode.OK, ReportJson.Report);
		stub.Enqueue(HttpStatusCode.OK, ReportJson.LinksJson);
		using var client = TestClient.Create(stub);
		var ct = TestContext.Current.CancellationToken;

		var report = await client.Reports.GetAsync(17, ct);
		var links = await client.Reports.UpdateAsync(17, report, ct);

		stub.Calls[1].ShouldBe(HttpMethod.Put, "/api/3/reports/17", body: ReportJson.ReportBody);
		links.Links.Should().ContainSingle().Which.Rel.Should().Be("Reports");
	}

	[Fact]
	public async Task DeleteAsync_SendsDelete()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.Reports.DeleteAsync(17, ct), ReportJson.LinksJson);

		call.ShouldBe(HttpMethod.Delete, "/api/3/reports/17");
	}

	[Fact]
	public async Task GenerateAsync_PostsWithoutABody()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.Reports.GenerateAsync(17, ct), """{"id":5,"links":[]}""");

		call.ShouldBe(HttpMethod.Post, "/api/3/reports/17/generate");
	}

	[Fact]
	public async Task GenerateAsync_ReadsTheInstanceIdentifier()
	{
		const string json = """{"id":5,"links":[{"href":"https://console.test:3780/api/3/reports/17/history/5","rel":"self"}]}""";

		var instance = await TestClient.ReadAsync((c, ct) => c.Reports.GenerateAsync(17, ct), json);

		instance.Id.Should().Be(5);
		instance.Links.Should().ContainSingle().Which.Href.Should().EndWith("/history/5");
	}

	[Fact]
	public Task CreateAsync_InvalidConfiguration_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync(
			(c, ct) => c.Reports.CreateAsync(new ReportConfiguration { Name = "No format" }, ct),
			HttpStatusCode.BadRequest,
			"""{"status":"BAD_REQUEST","message":"The report format must be specified.","links":[]}""",
			"The report format must be specified.");

	private static void ShouldBeExampleReport(Report report)
	{
		report.Should().BeEquivalentTo(new
		{
			Id = 17,
			Links = new[] { new { Rel = "self" } },
			Name = "Monthly Site Summary",
			Format = ReportFormat.Pdf,
			Template = "executive-overview",
			Owner = 1,
			Language = "en-US",
			Timezone = "Europe/London",
			Baseline = "previous",
			Bureau = "Bureau",
			Component = "Component",
			Enclave = "Enclave",
			Organization = "Example Ltd",
			Query = "SELECT * FROM dim_asset",
			Version = "2.3.0",
			Policy = 84L,
			Policies = new List<long> { 84, 85 },
			Users = new List<int> { 7 },
			Scope = new { Assets = new List<long> { 282 }, Sites = new List<int> { 5 }, AssetGroups = new List<int> { 3 }, Tags = new List<int> { 7 }, Scan = 28L },
			Storage = new { Location = "monthly/site", Path = "$(install_dir)/nsc/reports/$(user)/monthly/site" },
			Remediation = new { Solutions = 25, Sort = RemediationSort.RiskScore }
		});
		ShouldHaveExampleSchedule(report);
		ShouldHaveExampleTrends(report);
	}

	private static void ShouldHaveExampleSchedule(Report report)
	{
		report.Filters.Should().BeEquivalentTo(new
		{
			Severity = ReportSeverityFilter.CriticalAndSevere,
			Statuses = new[]
			{
				ReportVulnerabilityStatus.Vulnerable,
				ReportVulnerabilityStatus.VulnerableVersion,
				ReportVulnerabilityStatus.PotentiallyVulnerable,
				ReportVulnerabilityStatus.VulnerableAndValidated
			},
			Categories = new { Included = new List<string> { "Microsoft" }, Excluded = new List<string> { "Adobe" }, Links = new[] { new { Rel = "Categories" } } }
		});
		report.Frequency.Should().BeEquivalentTo(new
		{
			Type = ReportFrequencyType.Schedule,
			Start = new DateTimeOffset(2026, 10, 1, 4, 0, 0, TimeSpan.Zero),
			NextRuntimes = new List<string> { "2026-11-01T04:00:00Z" },
			Repeat = new { Every = ReportRepeatUnit.DayOfMonth, Interval = 1, DayOfWeek = ReportRepeatDay.Monday, WeekOfMonth = 2 }
		});
		report.Email.Should().BeEquivalentTo(new
		{
			Owner = ReportDistribution.Url,
			Access = ReportDistribution.Zip,
			Additional = ReportDistribution.File,
			AdditionalRecipients = new List<string> { "security@example.test" },
			AssetAccess = true,
			Smtp = new { Global = false, Relay = "mail.example.test", Sender = "reports@example.test" }
		});
	}

	private static void ShouldHaveExampleTrends(Report report)
	{
		report.Range.Should().BeEquivalentTo(new { From = "2026-01-01", To = new DateOnly(2026, 9, 30), Every = ReportRangeInterval.Month, Interval = 1 });
		report.RiskTrend.Should().BeEquivalentTo(new
		{
			From = "P3M",
			To = new DateOnly(2026, 9, 30),
			AllAssets = new { Total = true, Trend = AllAssetsTrend.AverageRisk },
			Assets = true,
			Sites = RiskTrendAggregate.Total,
			AssetGroups = RiskTrendAggregate.Average,
			AssetGroupMembership = RiskTrendMembership.Historical,
			Tags = RiskTrendAggregate.Average,
			TagMembership = RiskTrendMembership.Generation
		});
	}
}
