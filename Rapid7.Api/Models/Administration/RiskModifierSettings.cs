using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Administration;

/// <summary>The risk multiplier applied for each criticality tag.</summary>
public sealed class RiskModifierSettings
{
	/// <summary>The multiplier for very high criticality.</summary>
	[JsonPropertyName("veryHigh")]
	public double? VeryHigh { get; init; }

	/// <summary>The multiplier for high criticality.</summary>
	[JsonPropertyName("high")]
	public double? High { get; init; }

	/// <summary>The multiplier for medium criticality.</summary>
	[JsonPropertyName("medium")]
	public double? Medium { get; init; }

	/// <summary>The multiplier for low criticality.</summary>
	[JsonPropertyName("low")]
	public double? Low { get; init; }

	/// <summary>The multiplier for very low criticality.</summary>
	[JsonPropertyName("veryLow")]
	public double? VeryLow { get; init; }
}
