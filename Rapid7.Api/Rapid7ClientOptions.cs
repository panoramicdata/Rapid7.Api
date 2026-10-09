using Rapid7.Api.Handlers;
using System.Text.RegularExpressions;

namespace Rapid7.Api;

/// <summary>
/// Configuration for <see cref="Rapid7Client"/>, the InsightVM Security Console API (v3). The values are read once when
/// the client is constructed; changing this object afterwards does not affect an existing client.
/// </summary>
public class Rapid7ClientOptions : Rapid7ConnectionOptions
{
	/// <summary>
	/// Absolute URL of the Security Console, e.g. <c>https://console.example.com:3780</c>. A path prefix (for a reverse
	/// proxy) is kept: every endpoint (<c>api/3/...</c>) is appended to it. Must not carry credentials, a query or a
	/// fragment.
	/// </summary>
	public string BaseUrl { get; set; } = string.Empty;

	/// <summary>The console user name, sent with <see cref="Password"/> as HTTP basic credentials on every request.</summary>
	public string Username { get; set; } = string.Empty;

	/// <summary>The password for <see cref="Username"/>.</summary>
	public string Password { get; set; } = string.Empty;

	/// <summary>
	/// The current two-factor authentication code, sent as the <c>Token</c> header, for accounts with 2FA enabled. Leave
	/// <see langword="null"/> for accounts without it. A code is short-lived, so set a fresh one on a new client.
	/// </summary>
	public string? TwoFactorToken { get; set; }

	/// <summary>
	/// When <see langword="true"/>, the client refuses (with <see cref="Rapid7ReadOnlyException"/>, before anything is
	/// sent) every request that could change the console: any PUT or DELETE, and any POST except searches
	/// (<c>api/3/assets/search</c>, <c>api/3/sonar_queries/search</c>).
	/// </summary>
	public bool ReadOnly { get; set; }

	/// <summary>The client core for a console client at <paramref name="baseAddress"/>, authenticating with these credentials.</summary>
	internal Rapid7ClientCore CreateCore(Uri baseAddress, Regex? readOnlyPosts, HttpMessageHandler innerHandler)
		=> new(this, baseAddress, new BasicAuthenticationHandler(Username, Password, TwoFactorToken), readOnlyPosts, innerHandler);

	internal override void Validate()
	{
		ValidateBaseUrl(BaseUrl, nameof(BaseUrl));
		if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrEmpty(Password))
		{
			throw new ArgumentException("Set both Username and Password.", nameof(Username));
		}

		ValidateConnection();
	}

	/// <inheritdoc />
	public override string ToString()
		=> $"Rapid7ClientOptions {{ BaseUrl = {MaskBaseUrl(BaseUrl)}, Username = {Username}, Password = {Mask(Password)}, TwoFactorToken = {Mask(TwoFactorToken)}, ReadOnly = {ReadOnly} }}";
}
