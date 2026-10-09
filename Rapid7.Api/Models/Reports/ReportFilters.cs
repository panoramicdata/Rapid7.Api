using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Reports;

/// <summary>Which vulnerability findings a report includes.</summary>
public sealed class ReportFilters
{
	/// <summary>The severities to include (the console defaults to all).</summary>
	[JsonPropertyName("severity")]
	public ReportSeverityFilter? Severity { get; init; }

	/// <summary>The result statuses to include (the console defaults to vulnerable, vulnerable version and potentially vulnerable).</summary>
	[JsonPropertyName("statuses")]
	public IReadOnlyList<ReportVulnerabilityStatus>? Statuses { get; init; }

	/// <summary>The vulnerability categories to include or exclude.</summary>
	[JsonPropertyName("categories")]
	public ReportCategoryFilters? Categories { get; init; }
}
