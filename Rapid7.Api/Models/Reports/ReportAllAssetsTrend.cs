using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Reports;

/// <summary>The all-assets trends in a risk trend report.</summary>
public sealed class ReportAllAssetsTrend
{
	/// <summary>Whether to show the total risk of all assets.</summary>
	[JsonPropertyName("total")]
	public bool? Total { get; init; }

	/// <summary>Which further trend to show.</summary>
	[JsonPropertyName("trend")]
	public AllAssetsTrend? Trend { get; init; }
}
