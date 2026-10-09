using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Policies;

/// <summary>Compliance totals across every policy.</summary>
public sealed class PolicySummary : Links
{
	/// <summary>The number of policies on the console.</summary>
	[JsonPropertyName("numberOfPolicies")]
	public int? NumberOfPolicies { get; init; }

	/// <summary>The number of policies evaluated against assets with applicable results.</summary>
	[JsonPropertyName("scannedPolicies")]
	public int? ScannedPolicies { get; init; }

	/// <summary>The number of policies whose compliance rose between the last two scans.</summary>
	[JsonPropertyName("increasedCompliance")]
	public int? IncreasedCompliance { get; init; }

	/// <summary>The number of policies whose compliance fell between the last two scans.</summary>
	[JsonPropertyName("decreasedCompliance")]
	public int? DecreasedCompliance { get; init; }

	/// <summary>The share of compliant rules across every policy, from 0 to 1.</summary>
	[JsonPropertyName("overallCompliance")]
	public double? OverallCompliance { get; init; }
}
