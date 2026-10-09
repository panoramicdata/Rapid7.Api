using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Users;

/// <summary>A new password for a user account (<c>PUT api/3/users/{id}/password</c>).</summary>
/// <remarks><see cref="Password"/> is a secret: the client sends it in the request body and never logs it.</remarks>
public sealed class PasswordChange
{
	/// <summary>The new password. A secret.</summary>
	[JsonPropertyName("password")]
	public required string Password { get; init; }
}
