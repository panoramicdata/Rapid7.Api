using Rapid7.Api.Interfaces;

namespace Rapid7.Api;

public sealed partial class Rapid7Client
{
	/// <summary>Report configurations and their generation (<c>api/3/reports</c>).</summary>
	public IReports Reports => field ??= For<IReports>();

	/// <summary>Report templates and formats (<c>api/3/report_templates</c>, <c>api/3/report_formats</c>).</summary>
	public IReportTemplates ReportTemplates => field ??= For<IReportTemplates>();

	/// <summary>Generated report instances and their files (<c>api/3/reports/{id}/history</c>).</summary>
	public IReportInstances ReportInstances => field ??= For<IReportInstances>();
}
