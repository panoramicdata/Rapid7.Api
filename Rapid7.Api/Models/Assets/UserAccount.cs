using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Assets;

/// <summary>A user account enumerated on an asset or service.</summary>
public sealed class UserAccount
{
	/// <summary>The full name of the user, when known.</summary>
	[JsonPropertyName("fullName")]
	public string? FullName { get; init; }

	/// <summary>The identifier of the user on the asset.</summary>
	[JsonPropertyName("id")]
	public int? Id { get; init; }

	/// <summary>The account name.</summary>
	[JsonPropertyName("name")]
	public string? Name { get; init; }
}
