using System.Text.Json.Serialization;

namespace Rapid7.Api.Models;

/// <summary>
/// Something a user did to a policy override or a vulnerability exception (submitting or reviewing it): who acted, when,
/// and their comment.
/// </summary>
public sealed class UserAction : LinksResource
{
	/// <summary>The user's comment (at most 1024 characters for policy overrides).</summary>
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
