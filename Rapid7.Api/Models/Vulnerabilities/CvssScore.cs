using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Vulnerabilities;

/// <summary>The scores and vector common to every CVSS version.</summary>
public abstract class CvssScore
{
	/// <summary>The exploitability sub-score.</summary>
	[JsonPropertyName("exploitScore")]
	public double? ExploitScore { get; init; }

	/// <summary>The impact sub-score.</summary>
	[JsonPropertyName("impactScore")]
	public double? ImpactScore { get; init; }

	/// <summary>The base score, from 0 to 10.</summary>
	[JsonPropertyName("score")]
	public double? Score { get; init; }

	/// <summary>The CVSS vector string.</summary>
	[JsonPropertyName("vector")]
	public string? Vector { get; init; }
}
