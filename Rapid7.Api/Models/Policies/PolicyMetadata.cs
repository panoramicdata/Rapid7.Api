using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Policies;

/// <summary>The policy a policy group, rule or child item belongs to.</summary>
public sealed class PolicyMetadata : Links
{
	/// <summary>The name of the policy.</summary>
	[JsonPropertyName("name")]
	public string? Name { get; init; }

	/// <summary>The title of the policy shown to users.</summary>
	[JsonPropertyName("title")]
	public string? Title { get; init; }

	/// <summary>The version of the policy.</summary>
	[JsonPropertyName("version")]
	public string? Version { get; init; }
}
