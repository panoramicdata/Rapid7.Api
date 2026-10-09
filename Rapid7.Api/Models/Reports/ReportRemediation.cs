using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Reports;

/// <summary>How a remediation report lists solutions.</summary>
public sealed class ReportRemediation
{
	/// <summary>How many solutions to show.</summary>
	[JsonPropertyName("solutions")]
	public int? Solutions { get; init; }

	/// <summary>How the solutions are ordered.</summary>
	[JsonPropertyName("sort")]
	public RemediationSort? Sort { get; init; }
}
