using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Policies;

/// <summary>A group of rules (and further groups) within a compliance policy, with its results across assets.</summary>
public sealed class PolicyGroup : PolicyComponent
{
	/// <summary>The policy the group belongs to.</summary>
	[JsonPropertyName("policy")]
	public PolicyMetadata? Policy { get; init; }
}
