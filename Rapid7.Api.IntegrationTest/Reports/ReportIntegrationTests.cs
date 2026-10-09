using Rapid7.Api.Models;
using Rapid7.Api.Models.Reports;

namespace Rapid7.Api.IntegrationTest.Reports;

/// <summary>
/// Reads report templates, formats, configurations and history from a live console, and round-trips a report
/// configuration the test creates. Nothing is ever generated: generating runs work on the console and emails recipients.
/// </summary>
[Collection(Rapid7TestGroup.Name)]
public class ReportIntegrationTests(Rapid7Fixture fixture)
{
	private static CancellationToken Ct => TestContext.Current.CancellationToken;

	private Rapid7Client Client => fixture.Client;

	[Fact]
	public async Task Templates_AreListedAndReadable()
	{
		var templates = await Client.ReportTemplates.GetTemplatesAsync(Ct);

		templates.Resources.Should().NotBeEmpty("every console ships built-in report templates");
		var first = templates.Resources[0];
		var template = await Client.ReportTemplates.GetTemplateAsync(first.Id!, Ct);
		template.Id.Should().Be(first.Id);
	}

	[Fact]
	public async Task GetFormatsAsync_ListsPdf()
	{
		var formats = await Client.ReportTemplates.GetFormatsAsync(Ct);

		formats.Resources.Should().Contain(f => f.Format == ReportFormat.Pdf);
	}

	[Fact]
	public async Task Report_CreateReadUpdateDelete_RoundTrips()
	{
		var name = Rapid7Fixture.UniqueName("report");
		var configuration = new ReportConfiguration
		{
			Name = name,
			Format = ReportFormat.SqlQuery,
			Query = "SELECT asset_id FROM dim_asset",
			Version = "2.3.0",
			Frequency = new ReportFrequency { Type = ReportFrequencyType.None }
		};

		var created = await Client.Reports.CreateAsync(configuration, Ct);
		var id = created.Id;
		try
		{
			var report = await Client.Reports.GetAsync(id, Ct);
			report.Name.Should().Be(name);
			report.Format.Should().Be(ReportFormat.SqlQuery);

			var renamed = name + "-renamed";
			await Client.Reports.UpdateAsync(id, new ReportConfiguration
			{
				Name = renamed,
				Format = report.Format,
				Query = report.Query,
				Version = report.Version,
				Frequency = report.Frequency
			}, Ct);
			(await Client.Reports.GetAsync(id, Ct)).Name.Should().Be(renamed);

			(await Client.ReportInstances.GetInstancesAsync(id, Ct)).Resources.Should().BeEmpty("the report was never generated");
		}
		finally
		{
			await Client.Reports.DeleteAsync(id, CancellationToken.None);
		}
	}

	[Fact]
	public async Task ExistingReport_HistoryAndLatestOutputAreReadable()
	{
		var reports = await Client.Reports.GetReportsAsync(new PageOptions { Size = 1 }, Ct);
		if (reports.Resources.Count == 0 || reports.Resources[0].Id is not int reportId)
		{
			return;
		}

		var instances = await Client.ReportInstances.GetInstancesAsync(reportId, Ct);
		var complete = instances.Resources.FirstOrDefault(i => i.Status == ReportInstanceStatus.Complete);
		if (complete?.Id is not int instanceId)
		{
			return;
		}

		var instance = await Client.ReportInstances.GetInstanceAsync(reportId, instanceId.ToString(System.Globalization.CultureInfo.InvariantCulture), Ct);
		instance.Id.Should().Be(instanceId);

		using var content = await Client.ReportInstances.DownloadAsync(reportId, "latest", Ct);
		(await content.ReadAsByteArrayAsync(Ct)).Should().NotBeEmpty();
	}
}
