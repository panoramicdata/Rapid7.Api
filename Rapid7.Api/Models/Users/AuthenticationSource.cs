using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Users;

/// <summary>A source that authenticates user accounts, such as the console itself, LDAP, Kerberos or SAML.</summary>
public sealed class AuthenticationSource : Links
{
	/// <summary>The identifier of the source.</summary>
	[JsonPropertyName("id")]
	public int? Id { get; init; }

	/// <summary>The name of the source.</summary>
	[JsonPropertyName("name")]
	public string? Name { get; init; }

	/// <summary>The kind of source.</summary>
	[JsonPropertyName("type")]
	public AuthenticationSourceType? Type { get; init; }

	/// <summary>Whether the source is external to the console (<see langword="true"/>) or internal.</summary>
	[JsonPropertyName("external")]
	public bool? External { get; init; }
}
