using Rapid7.Api.Models;
using Rapid7.Api.Models.Reports;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>Report templates and output formats (<c>api/3/report_templates</c>, <c>api/3/report_formats</c>).</summary>
public interface IReportTemplates
{
	/// <summary>Lists every report template (<c>GET api/3/report_templates</c>).</summary>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The templates.</returns>
	[Get("api/3/report_templates")]
	Task<ResourceList<ReportTemplate>> GetTemplatesAsync(CancellationToken cancellationToken);

	/// <summary>Gets one report template (<c>GET api/3/report_templates/{id}</c>).</summary>
	/// <param name="id">The identifier of the template, such as <c>audit-report</c>.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The template.</returns>
	[Get("api/3/report_templates/{id}")]
	Task<ReportTemplate> GetTemplateAsync(string id, CancellationToken cancellationToken);

	/// <summary>Lists the report output formats and the templates each supports (<c>GET api/3/report_formats</c>).</summary>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The formats.</returns>
	[Get("api/3/report_formats")]
	Task<ResourceList<AvailableReportFormat>> GetFormatsAsync(CancellationToken cancellationToken);
}
