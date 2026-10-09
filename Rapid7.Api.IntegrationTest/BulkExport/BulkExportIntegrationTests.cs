using Rapid7.Api.Models.BulkExport;

namespace Rapid7.Api.IntegrationTest.BulkExport;

/// <summary>
/// Runs an export round trip against a live Insight platform organisation: exports only read InsightVM data. The API key
/// needs Platform Administrator permissions, and the asset software export is an early-access feature.
/// </summary>
[Collection(Rapid7TestGroup.Name)]
public class BulkExportIntegrationTests(Rapid7Fixture fixture)
{
	private static readonly ExportWaitOptions Wait = new() { PollInterval = TimeSpan.FromSeconds(15), Timeout = TimeSpan.FromMinutes(30) };

	private Rapid7BulkExportClient Client => fixture.BulkExportClient;

	private static CancellationToken CancellationToken => TestContext.Current.CancellationToken;

	[Fact]
	public async Task AssetSoftwareExport_RoundTrip_CreatesWaitsDownloadsAndReadsRecords()
	{
		var export = await Client.ExportAssetSoftwareAsync(Wait, CancellationToken);

		export.Status.Should().Be(ExportStatus.Succeeded);
		export.Result.Should().Contain(r => r.Prefix != null && r.Prefix.Contains(ExportDatasets.AssetSoftware, StringComparison.OrdinalIgnoreCase));

		var records = new List<AssetSoftwareRecord>();
		await foreach (var record in Client.ReadRecordsAsync<AssetSoftwareRecord>(export, ExportDatasets.AssetSoftware, CancellationToken))
		{
			records.Add(record);
			if (records.Count == 5)
			{
				break;
			}
		}

		records.Should().NotBeEmpty();
		records.Should().OnlyContain(r => !string.IsNullOrEmpty(r.AssetId));
	}

	[Fact]
	public async Task GetExport_ReturnsFreshUrlsForACreatedExport()
	{
		var response = await Client.Exports.CreateAssetSoftwareExportAsync(new CreateAssetSoftwareExportRequest(), CancellationToken);
		var id = response.Data!.Export!.Id!;

		var export = await Client.GetExportAsync(id, CancellationToken);

		export.Id.Should().Be(id);
		export.Status.Should().NotBe(ExportStatus.Unknown);
	}

	[Fact]
	public async Task GetExport_UnknownId_RaisesRapid7GraphQLException()
	{
		var act = () => Client.GetExportAsync("rapid7api-test-no-such-export", CancellationToken);

		await act.Should().ThrowAsync<Rapid7GraphQLException>();
	}
}
