using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Reports;

/// <summary>The size of a generated report.</summary>
public sealed class ReportSize
{
	/// <summary>The size in bytes.</summary>
	[JsonPropertyName("bytes")]
	public long? Bytes { get; init; }

	/// <summary>The size in human-readable form, such as <c>23.6 MB</c>.</summary>
	[JsonPropertyName("formatted")]
	public string? Formatted { get; init; }
}
