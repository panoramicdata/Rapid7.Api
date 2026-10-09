using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Reports;

/// <summary>Which trends a risk trend report shows.</summary>
public sealed class ReportRiskTrend
{
	/// <summary>Where the trend starts: a duration (<c>P1Y</c>, <c>P6M</c>, <c>P3M</c>, <c>P1M</c>) or a date.</summary>
	[JsonPropertyName("from")]
	public string? From { get; init; }

	/// <summary>Where the trend ends, when <see cref="From"/> is a date.</summary>
	[JsonPropertyName("to")]
	public DateOnly? To { get; init; }

	/// <summary>The trends for all assets together.</summary>
	[JsonPropertyName("allAssets")]
	public ReportAllAssetsTrend? AllAssets { get; init; }

	/// <summary>Whether to show a trend for the five highest-risk assets.</summary>
	[JsonPropertyName("assets")]
	public bool? Assets { get; init; }

	/// <summary>The trend shown for sites.</summary>
	[JsonPropertyName("sites")]
	public RiskTrendAggregate? Sites { get; init; }

	/// <summary>The trend shown for asset groups.</summary>
	[JsonPropertyName("assetGroups")]
	public RiskTrendAggregate? AssetGroups { get; init; }

	/// <summary>Which assets count towards each asset group.</summary>
	[JsonPropertyName("assetGroupMembership")]
	public RiskTrendMembership? AssetGroupMembership { get; init; }

	/// <summary>The trend shown for tags.</summary>
	[JsonPropertyName("tags")]
	public RiskTrendAggregate? Tags { get; init; }

	/// <summary>Which assets count towards each tag.</summary>
	[JsonPropertyName("tagMembership")]
	public RiskTrendMembership? TagMembership { get; init; }
}
