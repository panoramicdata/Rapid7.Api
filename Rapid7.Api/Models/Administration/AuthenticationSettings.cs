using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Administration;

/// <summary>The console's sign-in settings.</summary>
public sealed class AuthenticationSettings
{
	/// <summary>Whether two-factor authentication is enabled (<c>2fa</c>).</summary>
	[JsonPropertyName("2fa")]
	public bool? TwoFactorAuthentication { get; init; }

	/// <summary>The number of failed sign-in attempts after which an account is locked.</summary>
	[JsonPropertyName("loginLockThreshold")]
	public int? LoginLockThreshold { get; init; }
}
