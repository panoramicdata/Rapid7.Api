using Rapid7.Api.Models.Vulnerabilities;
using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Remediations;

/// <summary>A solution the console matched to a vulnerability on a particular asset, with how it was matched.</summary>
public sealed class MatchedSolution : Solution
{
	/// <summary>How confidently the solution was matched.</summary>
	[JsonPropertyName("confidence")]
	public MatchConfidence? Confidence { get; init; }

	/// <summary>The individual matches made to select the solution.</summary>
	[JsonPropertyName("matches")]
	public IReadOnlyList<SolutionMatch> Matches { get; init; } = [];
}
