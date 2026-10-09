using Rapid7.Api.Models.Scans;

namespace Rapid7.Api.IntegrationTest.Scans;

/// <summary>Reads scans across the console and per site. No scan is started, paused, resumed or stopped.</summary>
[Collection(Rapid7TestGroup.Name)]
public class ScansIntegrationTests(Rapid7Fixture fixture)
{
	private static CancellationToken Ct => TestContext.Current.CancellationToken;

	private Rapid7Client Client => fixture.Client;

	[Fact]
	public async Task PastScan_ReadsDetailsAndItsSitesScans()
	{
		var page = await Client.Scans.ListAsync(new ScanListOptions { Active = false, Size = 1, Sort = ["id,DESC"] }, Ct);
		var listed = page.Resources.Should().ContainSingle("the console must have run at least one scan").Subject;

		var scan = await Client.Scans.GetAsync(listed.Id!.Value, Ct);
		var siteScans = await Client.Scans.ListForSiteAsync(listed.SiteId!.Value, new ScanListOptions { Size = 5 }, Ct);

		scan.Id.Should().Be(listed.Id);
		scan.Status.Should().NotBe(ScanStatus.Running);
		scan.StartTime.Should().NotBeNull();
		siteScans.Resources.Should().NotBeEmpty();
	}

	[Fact]
	public async Task ActiveScans_AreAllInProgress()
	{
		var page = await Client.Scans.ListAsync(new ScanListOptions { Active = true }, Ct);

		page.Resources.Should().OnlyContain(s => s.EndTime == null);
	}
}
