using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.PolicyOverrides;

/// <summary>The submission or review of a policy override: who acted, when, and their comment.</summary>
public sealed class PolicyOverrideAction : Links
{
	/// <summary>The comment (at most 1024 characters).</summary>
	[JsonPropertyName("comment")]
	public string? Comment { get; init; }

	/// <summary>When the action happened.</summary>
	[JsonPropertyName("date")]
	public DateTimeOffset? Date { get; init; }

	/// <summary>The login name of the user who acted.</summary>
	[JsonPropertyName("name")]
	public string? Name { get; init; }

	/// <summary>The identifier of the user who acted.</summary>
	[JsonPropertyName("user")]
	public int? User { get; init; }
}
