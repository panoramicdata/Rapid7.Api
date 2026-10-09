using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Reports;

/// <summary>Where an extra copy of a generated report is stored on the console.</summary>
public sealed class ReportStorage
{
	/// <summary>The folder, relative to the console's per-user reports folder; may contain variables such as <c>$(report_name)</c>.</summary>
	[JsonPropertyName("location")]
	public string? Location { get; init; }

	/// <summary>The full path of the folder; returned only, ignored when sent.</summary>
	[JsonPropertyName("path")]
	public string? Path { get; init; }
}
