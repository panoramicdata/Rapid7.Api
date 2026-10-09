using Rapid7.Api.Models;
using Rapid7.Api.Models.Cloud;

namespace Rapid7.Api.IntegrationTest.Cloud;

/// <summary>
/// Reads from a live Insight platform organisation through the Cloud Integrations API (v4). Every test only reads: none
/// starts or stops a scan or changes a scan engine configuration.
/// </summary>
[Collection(Rapid7TestGroup.Name)]
public class CloudIntegrationTests(Rapid7Fixture fixture)
{
	private const int SmallPage = 2;

	private static readonly CursorPageOptions FirstSmallPage = new() { Page = 0, Size = SmallPage };

	private Rapid7CloudClient Client => fixture.CloudClient;

	private static CancellationToken CancellationToken => TestContext.Current.CancellationToken;

	[Fact]
	public async Task Health_IsUp()
	{
		var health = await Client.Health.GetAsync(CancellationToken);

		health.Status.Should().Be(CloudHealthStatus.Up);
	}

	[Fact]
	public async Task Assets_SearchReturnsASmallPage_AndEachAssetCanBeRead()
	{
		var page = await Client.Assets.SearchAsync(new CloudAssetSearch(), null, FirstSmallPage, CancellationToken);

		page.Data.Count.Should().BeLessThanOrEqualTo(SmallPage);
		page.Metadata.Should().NotBeNull();
		page.Metadata.Size.Should().Be(SmallPage);
		foreach (var asset in page.Data)
		{
			asset.Id.Should().NotBeNullOrEmpty();
			var read = await Client.Assets.GetAsync(asset.Id, new CloudAssetOptions { IncludeUniqueIdentifiers = true }, CancellationToken);
			read.Id.Should().Be(asset.Id);
		}
	}

	[Fact]
	public async Task Assets_SearchWithAFilterAndComparisonTime_Succeeds()
	{
		var now = DateTimeOffset.UtcNow;
		var search = new CloudAssetSearch { Asset = $"last_scan_end > {now.AddDays(-30):yyyy-MM-ddTHH:mm:ssZ}" };
		var options = new CloudAssetOptions { CurrentTime = now, ComparisonTime = now.AddDays(-7), IncludeSame = false };

		var page = await Client.Assets.SearchAsync(search, options, FirstSmallPage, CancellationToken);

		page.Data.Count.Should().BeLessThanOrEqualTo(SmallPage);
		page.Data.Should().OnlyContain(a => a.LastScanEnd == null || a.LastScanEnd > now.AddDays(-30));
	}

	[Fact]
	public async Task Assets_CursorPagingFollowsTheCursor()
	{
		var ids = new List<string?>();
		await foreach (var asset in Rapid7CursorPaging.ReadAllAsync(
			(paging, ct) => Client.Assets.SearchAsync(new CloudAssetSearch(), null, paging, ct),
			SmallPage,
			CancellationToken))
		{
			ids.Add(asset.Id);
			if (ids.Count == SmallPage * 2)
			{
				break;
			}
		}

		ids.Should().OnlyHaveUniqueItems();
	}

	[Fact]
	public async Task Sites_ListReturnsSiteTags()
	{
		var page = await Client.Sites.ListAsync(FirstSmallPage, CancellationToken);

		page.Data.Count.Should().BeLessThanOrEqualTo(SmallPage);
		page.Data.Should().OnlyContain(s => s.Type == "SITE");
	}

	[Fact]
	public async Task Vulnerabilities_SearchReturnsASmallPage()
	{
		var search = new CloudVulnerabilitySearch { Vulnerability = "severity IN ['Critical']" };

		var page = await Client.Vulnerabilities.SearchAsync(search, FirstSmallPage, CancellationToken);

		page.Data.Count.Should().BeLessThanOrEqualTo(SmallPage);
		page.Data.Should().OnlyContain(v => !string.IsNullOrEmpty(v.Id));
	}

	[Fact]
	public async Task Scans_ListAndGetRead()
	{
		var page = await Client.Scans.ListAsync(new CloudScanListOptions { Page = 0, Size = SmallPage, IncludeDetails = true }, CancellationToken);

		page.Data.Count.Should().BeLessThanOrEqualTo(SmallPage);
		foreach (var scan in page.Data)
		{
			var read = await Client.Scans.GetAsync(scan.Id!, false, CancellationToken);
			read.Id.Should().Be(scan.Id);
		}
	}

	[Fact]
	public async Task ScanEngines_ListAndGetRead()
	{
		var page = await Client.ScanEngines.ListAsync(new PageOptions { Page = 0, Size = SmallPage }, CancellationToken);

		page.Data.Count.Should().BeLessThanOrEqualTo(SmallPage);
		foreach (var engine in page.Data)
		{
			var read = await Client.ScanEngines.GetAsync(engine.Id!, CancellationToken);
			read.Id.Should().Be(engine.Id);
		}
	}

	[Fact]
	public async Task WrongApiKey_RaisesUnauthorized()
	{
		var options = fixture.CreatePlatformOptions();
		options.ApiKey = "definitely-not-a-key";
		using var client = new Rapid7CloudClient(options);

		var act = () => client.Sites.ListAsync(FirstSmallPage, CancellationToken);

		(await act.Should().ThrowAsync<Rapid7ApiException>()).Which.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
	}
}
