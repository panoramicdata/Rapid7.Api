using Rapid7.Api.Models.Credentials;
using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Sites;

/// <summary>A shared credential as assigned to one site, with whether that site's scans use it.</summary>
public sealed class SiteSharedCredential : Links
{
	/// <summary>Whether the site's scans use the shared credential.</summary>
	[JsonPropertyName("enabled")]
	public bool? Enabled { get; init; }

	/// <summary>The identifier of the shared credential.</summary>
	[JsonPropertyName("id")]
	public int? Id { get; init; }

	/// <summary>The name of the shared credential.</summary>
	[JsonPropertyName("name")]
	public string? Name { get; init; }

	/// <summary>The service the credential authenticates to.</summary>
	[JsonPropertyName("service")]
	public CredentialService? Service { get; init; }
}
