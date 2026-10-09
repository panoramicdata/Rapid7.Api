using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Reports;

/// <summary>A report output format and the templates it supports.</summary>
public sealed class AvailableReportFormat
{
	/// <summary>The format.</summary>
	[JsonPropertyName("format")]
	public ReportFormat? Format { get; init; }

	/// <summary>The identifiers of the templates the format supports.</summary>
	[JsonPropertyName("templates")]
	public IReadOnlyList<string> Templates { get; init; } = [];
}
