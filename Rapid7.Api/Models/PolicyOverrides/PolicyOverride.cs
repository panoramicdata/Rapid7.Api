using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.PolicyOverrides;

/// <summary>A policy override: a request to treat a policy rule's result differently, and its review.</summary>
public sealed class PolicyOverride : Links
{
	/// <summary>The identifier of the policy override.</summary>
	[JsonPropertyName("id")]
	public long? Id { get; init; }

	/// <summary>Where the override is in its review lifecycle.</summary>
	[JsonPropertyName("state")]
	public PolicyOverrideState State { get; init; }

	/// <summary>The rule, assets and result the override applies.</summary>
	[JsonPropertyName("scope")]
	public PolicyOverrideScope Scope { get; init; } = new();

	/// <summary>Who submitted the override, when and why.</summary>
	[JsonPropertyName("submit")]
	public PolicyOverrideAction Submit { get; init; } = new();

	/// <summary>Who reviewed the override, when and why; absent until it is reviewed.</summary>
	[JsonPropertyName("review")]
	public PolicyOverrideAction? Review { get; init; }

	/// <summary>When the override expires, if it does.</summary>
	[JsonPropertyName("expires")]
	public DateTimeOffset? Expires { get; init; }
}
