using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Users;

/// <summary>The new details of a user account (<c>PUT api/3/users/{id}</c>); the whole account is replaced.</summary>
/// <remarks><see cref="Password"/> is a secret: the client sends it in the request body and never logs it.</remarks>
public sealed class UserUpdateRequest : UserRequest
{
	/// <summary>A new password, or <see langword="null"/> to keep the current one. A secret.</summary>
	[JsonPropertyName("password")]
	public string? Password { get; init; }
}
