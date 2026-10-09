using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.PolicyOverrides;

/// <summary>A new policy override to submit for review.</summary>
public sealed class PolicyOverrideRequest
{
	/// <summary>The initial state; submit <see cref="PolicyOverrideState.UnderReview"/> unless you have the privilege to approve it directly.</summary>
	[JsonPropertyName("state")]
	public required PolicyOverrideState State { get; init; }

	/// <summary>The rule, assets and result the override applies.</summary>
	[JsonPropertyName("scope")]
	public required PolicyOverrideScopeRequest Scope { get; init; }

	/// <summary>Why the override is submitted.</summary>
	[JsonPropertyName("submit")]
	public required PolicyOverrideSubmission Submit { get; init; }

	/// <summary>When the override expires; leave <see langword="null"/> for no expiry.</summary>
	[JsonPropertyName("expires")]
	public DateTimeOffset? Expires { get; init; }
}
