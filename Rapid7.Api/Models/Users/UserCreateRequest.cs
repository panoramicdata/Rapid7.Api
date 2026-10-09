using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Users;

/// <summary>A new user account (<c>POST api/3/users</c>).</summary>
/// <remarks><see cref="Password"/> is a secret: the client sends it in the request body and never logs it.</remarks>
public sealed class UserCreateRequest : UserRequest
{
	/// <summary>The initial password. Required; a secret.</summary>
	[JsonPropertyName("password")]
	public required string Password { get; init; }
}
