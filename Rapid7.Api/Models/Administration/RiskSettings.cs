using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Administration;

/// <summary>The console's risk scoring settings.</summary>
public sealed class RiskSettings
{
	/// <summary>The risk model, such as <c>risk_score_v2</c>.</summary>
	[JsonPropertyName("model")]
	public string? Model { get; init; }

	/// <summary>Whether risk is adjusted by criticality tags.</summary>
	[JsonPropertyName("adjustWithCriticality")]
	public bool? AdjustWithCriticality { get; init; }

	/// <summary>The multiplier for each criticality, when risk is adjusted.</summary>
	[JsonPropertyName("criticalityModifiers")]
	public RiskModifierSettings? CriticalityModifiers { get; init; }
}
