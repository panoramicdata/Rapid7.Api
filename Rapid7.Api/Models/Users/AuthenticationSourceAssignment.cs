using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Users;

/// <summary>The authentication source a created or updated user account signs in with.</summary>
public sealed class AuthenticationSourceAssignment
{
	/// <summary>The kind of source. Required.</summary>
	[JsonPropertyName("type")]
	public required AuthenticationSourceType Type { get; init; }

	/// <summary>
	/// The identifier of a source of that kind (see <c>IAuthenticationSources</c>); leave <see langword="null"/> to let the
	/// console choose a source of the given kind.
	/// </summary>
	[JsonPropertyName("id")]
	public int? Id { get; init; }
}
