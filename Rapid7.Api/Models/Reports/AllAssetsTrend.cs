using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Reports;

/// <summary>Which all-assets trend a risk trend report shows.</summary>
public enum AllAssetsTrend
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>The average risk.</summary>
	[JsonStringEnumMemberName("average-risk")]
	AverageRisk,

	/// <summary>The number of assets.</summary>
	[JsonStringEnumMemberName("number-of-assets")]
	NumberOfAssets,

	/// <summary>No trend.</summary>
	[JsonStringEnumMemberName("none")]
	None
}
