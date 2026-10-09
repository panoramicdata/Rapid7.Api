using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Reports;

/// <summary>The vulnerability categories a report includes or excludes.</summary>
public sealed class ReportCategoryFilters
{
	/// <summary>The categories to include (the console defaults to all).</summary>
	[JsonPropertyName("included")]
	public IReadOnlyList<string>? Included { get; init; }

	/// <summary>The categories to exclude (the console defaults to none).</summary>
	[JsonPropertyName("excluded")]
	public IReadOnlyList<string>? Excluded { get; init; }

	/// <summary>Links the console returns; ignored when sent.</summary>
	[JsonPropertyName("links")]
	public IReadOnlyList<Link>? Links { get; init; }
}
