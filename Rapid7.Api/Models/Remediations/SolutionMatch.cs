using Rapid7.Api.Models;
using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Remediations;

/// <summary>One match the console made when selecting a solution for an asset.</summary>
public sealed class SolutionMatch : Links
{
	/// <summary>The identifier of the vulnerability check.</summary>
	[JsonPropertyName("check")]
	public string? Check { get; init; }

	/// <summary>How confident the match is.</summary>
	[JsonPropertyName("confidence")]
	public MatchConfidence? Confidence { get; init; }

	/// <summary>The fingerprint that was matched.</summary>
	[JsonPropertyName("fingerprint")]
	public Fingerprint? Fingerprint { get; init; }

	/// <summary>The identifier of the solution.</summary>
	[JsonPropertyName("solution")]
	public string? Solution { get; init; }

	/// <summary>What the solution was matched against.</summary>
	[JsonPropertyName("type")]
	public SolutionMatchType? Type { get; init; }
}
