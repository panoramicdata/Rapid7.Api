using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.PolicyOverrides;

/// <summary>What a new policy override applies to.</summary>
public sealed class PolicyOverrideScopeRequest
{
	/// <summary>The identifier of the policy rule.</summary>
	[JsonPropertyName("rule")]
	public required long Rule { get; init; }

	/// <summary>Which assets the override applies to.</summary>
	[JsonPropertyName("type")]
	public required PolicyOverrideScopeType Type { get; init; }

	/// <summary>The identifier of the asset, required for an asset-specific override.</summary>
	[JsonPropertyName("asset")]
	public long? Asset { get; init; }

	/// <summary>The result the override substitutes.</summary>
	[JsonPropertyName("newResult")]
	public required PolicyOverrideResult NewResult { get; init; }
}
