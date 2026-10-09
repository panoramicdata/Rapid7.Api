using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Credentials;

/// <summary>The optional details shared by <see cref="SharedCredential"/> and <see cref="SharedCredentialRequest"/>.</summary>
public abstract class SharedCredentialBase
{
	/// <summary>A description of the credential.</summary>
	[JsonPropertyName("description")]
	public string? Description { get; init; }

	/// <summary>The host name or IP address the credential is restricted to.</summary>
	[JsonPropertyName("hostRestriction")]
	public string? HostRestriction { get; init; }

	/// <summary>The port (1 to 65535) the credential is restricted to.</summary>
	[JsonPropertyName("portRestriction")]
	public int? PortRestriction { get; init; }
}
