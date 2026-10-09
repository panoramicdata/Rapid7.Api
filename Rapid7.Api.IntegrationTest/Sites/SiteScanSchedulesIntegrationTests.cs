using Rapid7.Api.Models.Sites;

namespace Rapid7.Api.IntegrationTest.Sites;

/// <summary>Disabled scan schedules on a test site: they never start a scan.</summary>
[Collection(Rapid7TestGroup.Name)]
public class SiteScanSchedulesIntegrationTests(Rapid7Fixture fixture)
{
	private static ScanSchedule DisabledSchedule(string scanName) => new()
	{
		Enabled = false,
		ScanName = scanName,
		Start = DateTimeOffset.UtcNow.AddYears(1),
		Repeat = new ScanScheduleRepeat { Every = ScanScheduleFrequency.Week, Interval = 1 },
		OnScanRepeat = ScanScheduleOverlap.ResumeScan
	};

	[Fact]
	public Task Schedule_CreateGetUpdateListDelete()
		=> ScratchSite.RunAsync(fixture, async (siteId, ct) =>
		{
			var schedules = fixture.Client.SiteScanSchedules;
			var created = await schedules.CreateAsync(siteId, DisabledSchedule("rapid7api-test-weekly"), ct);

			var schedule = await schedules.GetAsync(siteId, created.Id, ct);
			schedule.Enabled.Should().BeFalse();
			schedule.Repeat!.Every.Should().Be(ScanScheduleFrequency.Week);

			await schedules.UpdateAsync(siteId, created.Id, DisabledSchedule("rapid7api-test-renamed"), ct);
			(await schedules.GetAsync(siteId, created.Id, ct)).ScanName.Should().Be("rapid7api-test-renamed");

			(await schedules.ListAsync(siteId, ct)).Resources.Should().ContainSingle(s => s.Id == created.Id);

			await schedules.DeleteAsync(siteId, created.Id, ct);
			(await schedules.ListAsync(siteId, ct)).Resources.Should().BeEmpty();
		});

	[Fact]
	public Task Schedules_ReplaceAllAndDeleteAll()
		=> ScratchSite.RunAsync(fixture, async (siteId, ct) =>
		{
			var schedules = fixture.Client.SiteScanSchedules;

			await schedules.ReplaceAllAsync(siteId, [DisabledSchedule("rapid7api-test-a"), DisabledSchedule("rapid7api-test-b")], ct);
			(await schedules.ListAsync(siteId, ct)).Resources.Should().HaveCount(2);

			await schedules.DeleteAllAsync(siteId, ct);
			(await schedules.ListAsync(siteId, ct)).Resources.Should().BeEmpty();
		});
}
