using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Reports;

/// <summary>A report configuration as the console returns it.</summary>
public sealed class Report : ReportConfiguration
{
	/// <summary>The identifier of the report.</summary>
	[JsonPropertyName("id")]
	public int? Id { get; init; }

	/// <summary>Links to the report and related resources.</summary>
	[JsonPropertyName("links")]
	public IReadOnlyList<Link> Links { get; init; } = [];
}
