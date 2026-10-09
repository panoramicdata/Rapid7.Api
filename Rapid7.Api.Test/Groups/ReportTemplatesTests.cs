using Rapid7.Api.Models.Reports;
using Rapid7.Api.Test.Support;
using System.Net;

namespace Rapid7.Api.Test.Groups;

public class ReportTemplatesTests
{
	[Fact]
	public async Task GetTemplatesAsync_SendsGet()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.ReportTemplates.GetTemplatesAsync(ct), ReportJson.TemplateList);

		call.ShouldBe(HttpMethod.Get, "/api/3/report_templates");
	}

	[Fact]
	public async Task GetTemplatesAsync_MapsEveryField()
	{
		var list = await TestClient.ReadAsync((c, ct) => c.ReportTemplates.GetTemplatesAsync(ct), ReportJson.TemplateList);

		ShouldBeAuditReport(list.Resources.Should().ContainSingle().Subject);
	}

	[Fact]
	public async Task GetTemplateAsync_SendsGet_WithTheIdentifierAsOneSegment()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.ReportTemplates.GetTemplateAsync("audit-report", ct), ReportJson.Template);

		call.ShouldBe(HttpMethod.Get, "/api/3/report_templates/audit-report");
	}

	[Fact]
	public async Task GetTemplateAsync_MapsEveryField()
		=> ShouldBeAuditReport(await TestClient.ReadAsync((c, ct) => c.ReportTemplates.GetTemplateAsync("audit-report", ct), ReportJson.Template));

	[Fact]
	public async Task GetFormatsAsync_SendsGet()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.ReportTemplates.GetFormatsAsync(ct), ReportJson.FormatList);

		call.ShouldBe(HttpMethod.Get, "/api/3/report_formats");
	}

	[Fact]
	public async Task GetFormatsAsync_MapsEveryField()
	{
		var list = await TestClient.ReadAsync((c, ct) => c.ReportTemplates.GetFormatsAsync(ct), ReportJson.FormatList);

		list.Resources.Should().BeEquivalentTo(new[]
		{
			new { Format = ReportFormat.Pdf, Templates = new List<string> { "audit-report", "executive-overview" } },
			new { Format = ReportFormat.CsvExport, Templates = new List<string>() }
		});
	}

	[Fact]
	public Task GetTemplateAsync_NotFound_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync(
			(c, ct) => c.ReportTemplates.GetTemplateAsync("missing", ct),
			HttpStatusCode.NotFound,
			PolicyJson.NotFound,
			PolicyJson.NotFoundMessage);

	private static void ShouldBeAuditReport(ReportTemplate template) => template.Should().BeEquivalentTo(new
	{
		Builtin = true,
		Description = "Details of discovered assets, vulnerabilities and users.",
		Id = "audit-report",
		Links = new[] { new { Rel = "self" } },
		Name = "Audit Report",
		Sections = new List<string> { "Baseline Comparison", "Executive Summary" },
		Type = ReportTemplateType.Document
	});
}
