using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.PolicyOverrides;

/// <summary>The reason given when submitting a policy override.</summary>
public sealed class PolicyOverrideSubmission
{
	/// <summary>Why the override is submitted (at most 1024 characters).</summary>
	[JsonPropertyName("comment")]
	public required string Comment { get; init; }
}
