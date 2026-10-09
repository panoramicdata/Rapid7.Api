using Rapid7.Api.Models.Sites;
using Rapid7.Api.Test.Support;

namespace Rapid7.Api.Test.Groups;

public class SiteScanSchedulesTests
{
	private const string ScheduleJson = """
		{
			"id": 9,
			"enabled": true,
			"scanName": "Monthly audit",
			"start": "2026-11-01T04:30:00Z",
			"repeat": { "every": "day-of-month", "interval": 1, "dayOfWeek": "sunday", "weekOfMonth": 2, "lastDayOfMonth": false },
			"duration": "PT4H",
			"onScanRepeat": "restart-scan",
			"scanEngineId": 3,
			"scanTemplateId": "full-audit-without-web-spider",
			"assets": {
				"includedTargets": { "addresses": [ "192.0.2.0/25" ], "links": [] },
				"excludedTargets": { "addresses": [ "192.0.2.10" ], "links": [] },
				"includedAssetGroups": { "assetGroupIDs": [ 61 ], "links": [] },
				"excludedAssetGroups": { "assetGroupIDs": [ 62 ], "links": [] }
			},
			"nextRuntimes": [ "2026-11-08T04:30:00Z", "2026-12-13T04:30:00Z" ],
			"links": [ { "href": "https://console.test:3780/api/3/sites/7/scan_schedules/9", "rel": "self" } ]
		}
		""";

	private const string ListJson = $$"""
		{
			"resources": [ {{ScheduleJson}} ],
			"links": [ { "href": "https://console.test:3780/api/3/sites/7/scan_schedules", "rel": "self" } ]
		}
		""";

	private const string ScheduleBody = """{"enabled":false,"scanName":"Weekly discovery","start":"2026-11-01T04:30:00+00:00","repeat":{"every":"week","interval":2},"onScanRepeat":"resume-scan","scanTemplateId":"discovery","nextRuntimes":[],"links":[]}""";

	private static ScanSchedule Schedule => new()
	{
		Enabled = false,
		ScanName = "Weekly discovery",
		Start = new DateTimeOffset(2026, 11, 1, 4, 30, 0, TimeSpan.Zero),
		Repeat = new ScanScheduleRepeat { Every = ScanScheduleFrequency.Week, Interval = 2 },
		OnScanRepeat = ScanScheduleOverlap.ResumeScan,
		ScanTemplateId = "discovery"
	};

	[Fact]
	public async Task ListAsync_SendsGetToTheSchedules()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteScanSchedules.ListAsync(SiteFixtures.SiteId, ct), ListJson);

		call.ShouldBe(HttpMethod.Get, "/api/3/sites/7/scan_schedules");
	}

	[Fact]
	public async Task ListAsync_MapsEverySchedule()
	{
		var list = await TestClient.ReadAsync((c, ct) => c.SiteScanSchedules.ListAsync(SiteFixtures.SiteId, ct), ListJson);

		ShouldBeMonthlyAudit(list.Resources.Should().ContainSingle().Subject);
		list.Items.Should().ContainSingle();
	}

	[Fact]
	public async Task ReplaceAllAsync_PutsTheScheduleArray()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteScanSchedules.ReplaceAllAsync(SiteFixtures.SiteId, [Schedule], ct), SiteFixtures.LinksJson);

		call.ShouldBe(HttpMethod.Put, "/api/3/sites/7/scan_schedules", body: $"[{ScheduleBody}]");
	}

	[Fact]
	public async Task CreateAsync_PostsTheSchedule()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteScanSchedules.CreateAsync(SiteFixtures.SiteId, Schedule, ct), SiteFixtures.CreatedJson);

		call.ShouldBe(HttpMethod.Post, "/api/3/sites/7/scan_schedules", body: ScheduleBody);
	}

	[Fact]
	public async Task CreateAsync_MapsTheNewScheduleId()
	{
		var created = await TestClient.ReadAsync((c, ct) => c.SiteScanSchedules.CreateAsync(SiteFixtures.SiteId, Schedule, ct), SiteFixtures.CreatedJson);

		created.ShouldBeCreated42();
	}

	[Fact]
	public async Task DeleteAllAsync_SendsDeleteToTheSchedules()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteScanSchedules.DeleteAllAsync(SiteFixtures.SiteId, ct), SiteFixtures.LinksJson);

		call.ShouldBe(HttpMethod.Delete, "/api/3/sites/7/scan_schedules");
	}

	[Fact]
	public async Task GetAsync_SendsGetToTheSchedule()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteScanSchedules.GetAsync(SiteFixtures.SiteId, 9, ct), ScheduleJson);

		call.ShouldBe(HttpMethod.Get, "/api/3/sites/7/scan_schedules/9");
	}

	[Fact]
	public async Task GetAsync_MapsEveryField()
	{
		var schedule = await TestClient.ReadAsync((c, ct) => c.SiteScanSchedules.GetAsync(SiteFixtures.SiteId, 9, ct), ScheduleJson);

		ShouldBeMonthlyAudit(schedule);
	}

	[Theory]
	[InlineData("hour", ScanScheduleFrequency.Hour)]
	[InlineData("day", ScanScheduleFrequency.Day)]
	[InlineData("date-of-month", ScanScheduleFrequency.DateOfMonth)]
	public async Task GetAsync_MapsTheOtherFrequencies(string wire, ScanScheduleFrequency expected)
	{
		var schedule = await TestClient.ReadAsync(
			(c, ct) => c.SiteScanSchedules.GetAsync(SiteFixtures.SiteId, 9, ct),
			$$"""{"id":9,"enabled":true,"start":"2026-11-01T04:30:00Z","onScanRepeat":"resume-scan","repeat":{"every":"{{wire}}","interval":3,"lastDayOfMonth":true},"links":[]}""");

		schedule.Repeat!.Every.Should().Be(expected);
		schedule.Repeat.LastDayOfMonth.Should().BeTrue();
		schedule.OnScanRepeat.Should().Be(ScanScheduleOverlap.ResumeScan);
		schedule.Assets.Should().BeNull();
		schedule.NextRuntimes.Should().BeEmpty();
	}

	[Fact]
	public async Task UpdateAsync_PutsTheSchedule()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteScanSchedules.UpdateAsync(SiteFixtures.SiteId, 9, Schedule, ct), SiteFixtures.LinksJson);

		call.ShouldBe(HttpMethod.Put, "/api/3/sites/7/scan_schedules/9", body: ScheduleBody);
	}

	[Fact]
	public async Task DeleteAsync_SendsDeleteToTheSchedule()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteScanSchedules.DeleteAsync(SiteFixtures.SiteId, 9, ct), SiteFixtures.LinksJson);

		call.ShouldBe(HttpMethod.Delete, "/api/3/sites/7/scan_schedules/9");
	}

	[Fact]
	public async Task DeleteAsync_MapsTheLinks()
	{
		var links = await TestClient.ReadAsync((c, ct) => c.SiteScanSchedules.DeleteAsync(SiteFixtures.SiteId, 9, ct), SiteFixtures.LinksJson);

		links.ShouldBeSiteLinks();
	}

	[Fact]
	public Task GetAsync_NotFound_RaisesRapid7ApiException()
		=> SiteFixtures.ShouldRaiseNotFoundAsync((c, ct) => c.SiteScanSchedules.GetAsync(SiteFixtures.SiteId, 9, ct));

	private static void ShouldBeMonthlyAudit(ScanSchedule schedule)
	{
		schedule.Id.Should().Be(9);
		schedule.Enabled.Should().BeTrue();
		schedule.ScanName.Should().Be("Monthly audit");
		schedule.Start.Should().Be(new DateTimeOffset(2026, 11, 1, 4, 30, 0, TimeSpan.Zero));
		schedule.Repeat!.Every.Should().Be(ScanScheduleFrequency.DayOfMonth);
		schedule.Repeat.Interval.Should().Be(1);
		schedule.Repeat.DayOfWeek.Should().Be(ScanScheduleDay.Sunday);
		schedule.Repeat.WeekOfMonth.Should().Be(2);
		schedule.Repeat.LastDayOfMonth.Should().BeFalse();
		schedule.Duration.Should().Be("PT4H");
		schedule.OnScanRepeat.Should().Be(ScanScheduleOverlap.RestartScan);
		schedule.ScanEngineId.Should().Be(3);
		schedule.ScanTemplateId.Should().Be("full-audit-without-web-spider");
		schedule.Assets!.IncludedTargets!.Addresses.Should().Equal("192.0.2.0/25");
		schedule.Assets.ExcludedTargets!.Addresses.Should().Equal("192.0.2.10");
		schedule.Assets.IncludedAssetGroups!.AssetGroupIds.Should().Equal(61);
		schedule.Assets.ExcludedAssetGroups!.AssetGroupIds.Should().Equal(62);
		schedule.NextRuntimes.Should().Equal("2026-11-08T04:30:00Z", "2026-12-13T04:30:00Z");
		schedule.Items.Should().ContainSingle();
	}
}
