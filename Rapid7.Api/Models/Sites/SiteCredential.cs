using Rapid7.Api.Models.Credentials;
using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Sites;

/// <summary>
/// A scan credential that belongs to one site (as opposed to a shared credential, which can be assigned to many). The
/// same shape is read, created and updated.
/// </summary>
public sealed class SiteCredential
{
	/// <summary>The service the credential authenticates to, and the secrets that service needs.</summary>
	[JsonPropertyName("account")]
	public required SharedCredentialAccount Account { get; init; }

	/// <summary>A free-text description.</summary>
	[JsonPropertyName("description")]
	public string? Description { get; init; }

	/// <summary>Whether scans of the site use the credential.</summary>
	[JsonPropertyName("enabled")]
	public bool? Enabled { get; init; }

	/// <summary>A host name or IP address the credential is restricted to, if any.</summary>
	[JsonPropertyName("hostRestriction")]
	public string? HostRestriction { get; init; }

	/// <summary>The identifier of the credential; leave it unset when creating one.</summary>
	[JsonPropertyName("id")]
	public int? Id { get; init; }

	/// <summary>The name of the credential.</summary>
	[JsonPropertyName("name")]
	public required string Name { get; init; }

	/// <summary>A port (1 to 65535) the credential is restricted to, if any.</summary>
	[JsonPropertyName("portRestriction")]
	public int? PortRestriction { get; init; }
}
