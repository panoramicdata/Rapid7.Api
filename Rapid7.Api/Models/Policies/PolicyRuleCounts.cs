using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Policies;

/// <summary>Counts of the rule results under a policy or policy group.</summary>
public sealed class PolicyRuleCounts : PolicyResultCounts
{
	/// <summary>How many rules have the unscored role.</summary>
	[JsonPropertyName("unscored")]
	public int? Unscored { get; init; }
}
