using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Users;

/// <summary>A Security Console user account. Passwords are never returned.</summary>
public sealed class User : LinksResource
{
	/// <summary>The identifier of the user.</summary>
	[JsonPropertyName("id")]
	public int? Id { get; init; }

	/// <summary>The login name.</summary>
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

	/// <summary>Whether the account is locked after too many failed sign-in attempts (see <c>IUsers.UnlockAsync</c>).</summary>
	[JsonPropertyName("locked")]
	public bool? Locked { get; init; }

	/// <summary>The source that authenticates the user.</summary>
	[JsonPropertyName("authentication")]
	public AuthenticationSource? Authentication { get; init; }

	/// <summary>The user's language preferences.</summary>
	[JsonPropertyName("locale")]
	public LocalePreferences? Locale { get; init; }

	/// <summary>The user's role and access.</summary>
	[JsonPropertyName("role")]
	public UserRole? Role { get; init; }
}
