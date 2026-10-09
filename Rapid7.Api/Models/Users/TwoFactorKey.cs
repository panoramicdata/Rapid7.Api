using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Users;

/// <summary>A user's two-factor authentication token seed.</summary>
/// <remarks>
/// <see cref="Key"/> is a secret: anyone holding it can generate the user's one-time codes. Do not log or persist it
/// beyond enrolling it in an authenticator.
/// </remarks>
public sealed class TwoFactorKey : LinksResource
{
	/// <summary>The token seed (key) to enrol in an authenticator app, or <see langword="null"/> when none is set. A secret.</summary>
	[JsonPropertyName("key")]
	public string? Key { get; init; }
}
