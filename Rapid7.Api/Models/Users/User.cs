using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Users;

/// <summary>A user account of the Security Console.</summary>
public sealed class User : Links
{
	/// <summary>The identifier of the user.</summary>
	[JsonPropertyName("id")]
	public int? Id { get; init; }

	/// <summary>The logon name.</summary>
	[JsonPropertyName("login")]
	public string? Login { get; init; }

	/// <summary>The full name.</summary>
	[JsonPropertyName("name")]
	public string? Name { get; init; }

	/// <summary>The email address.</summary>
	[JsonPropertyName("email")]
	public string? Email { get; init; }

	/// <summary>Whether the account is enabled.</summary>
	[JsonPropertyName("enabled")]
	public bool? Enabled { get; init; }

	/// <summary>Whether the account is locked after too many failed logons.</summary>
	[JsonPropertyName("locked")]
	public bool? Locked { get; init; }
}
