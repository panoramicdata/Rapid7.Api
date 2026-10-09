using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Users;

/// <summary>The account details shared by <see cref="UserCreateRequest"/> and <see cref="UserUpdateRequest"/>.</summary>
public abstract class UserRequest
{
	/// <summary>The login name. Required.</summary>
	[JsonPropertyName("login")]
	public required string Login { get; init; }

	/// <summary>The full name. Required.</summary>
	[JsonPropertyName("name")]
	public required string Name { get; init; }

	/// <summary>The role and access to give the user. Required.</summary>
	[JsonPropertyName("role")]
	public required UserRoleAssignment Role { get; init; }

	/// <summary>The email address.</summary>
	[JsonPropertyName("email")]
	public string? Email { get; init; }

	/// <summary>Whether the account is enabled; the console defaults to <see langword="true"/>.</summary>
	[JsonPropertyName("enabled")]
	public bool? Enabled { get; init; }

	/// <summary>Whether the user must change their password at their next sign-in; defaults to <see langword="false"/>.</summary>
	[JsonPropertyName("passwordResetOnLogin")]
	public bool? PasswordResetOnLogin { get; init; }

	/// <summary>The source that authenticates the user; the console's own store when <see langword="null"/>.</summary>
	[JsonPropertyName("authentication")]
	public AuthenticationSourceAssignment? Authentication { get; init; }

	/// <summary>The user's language preferences.</summary>
	[JsonPropertyName("locale")]
	public LocalePreferences? Locale { get; init; }
}
