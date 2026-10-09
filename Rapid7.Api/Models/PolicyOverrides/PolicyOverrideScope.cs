using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.PolicyOverrides;

/// <summary>What a policy override applies to.</summary>
public sealed class PolicyOverrideScope : LinksResource
{
	/// <summary>The identifier of the policy rule.</summary>
	[JsonPropertyName("rule")]
	public long Rule { get; init; }

	/// <summary>Which assets the override applies to.</summary>
	[JsonPropertyName("type")]
	public PolicyOverrideScopeType Type { get; init; }

	/// <summary>The identifier of the asset, for an asset-specific override.</summary>
	[JsonPropertyName("asset")]
	public long? Asset { get; init; }

	/// <summary>The result the override substitutes.</summary>
	[JsonPropertyName("newResult")]
	public PolicyOverrideResult NewResult { get; init; }

	/// <summary>The result the rule produced before the override.</summary>
	[JsonPropertyName("originalResult")]
	public PolicyCheckResult? OriginalResult { get; init; }
}
