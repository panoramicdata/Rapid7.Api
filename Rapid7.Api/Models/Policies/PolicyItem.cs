using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Policies;

/// <summary>A rule or group directly under a policy or policy group, with a summary of its results.</summary>
public sealed class PolicyItem : PolicyContentResource
{
	/// <summary>The identifier of the rule or group.</summary>
	[JsonPropertyName("id")]
	public long? Id { get; init; }

	/// <summary>Whether this is a rule or a group.</summary>
	[JsonPropertyName("type")]
	public PolicyItemType? Type { get; init; }

	/// <summary>The name.</summary>
	[JsonPropertyName("name")]
	public string? Name { get; init; }

	/// <summary>For a rule, whether an active policy override applies to it.</summary>
	[JsonPropertyName("hasOverride")]
	public bool? HasOverride { get; init; }

	/// <summary>For a rule, whether its role is unscored.</summary>
	[JsonPropertyName("isUnscored")]
	public bool? IsUnscored { get; init; }

	/// <summary>How many assets pass, fail or are not applicable.</summary>
	[JsonPropertyName("assets")]
	public PolicyResultCounts? Assets { get; init; }

	/// <summary>For a group, how many of its rules pass, fail or are not applicable.</summary>
	[JsonPropertyName("rules")]
	public PolicyRuleCounts? Rules { get; init; }

	/// <summary>The policy the item belongs to.</summary>
	[JsonPropertyName("policy")]
	public PolicyMetadata? Policy { get; init; }
}
