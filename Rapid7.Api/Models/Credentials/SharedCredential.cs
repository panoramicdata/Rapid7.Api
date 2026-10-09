using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Credentials;

/// <summary>A shared scan credential, available to all sites or to chosen sites. Secrets in its account are never returned.</summary>
public sealed class SharedCredential : SharedCredentialBase
{
	/// <summary>The identifier of the credential.</summary>
	[JsonPropertyName("id")]
	public int? Id { get; init; }

	/// <summary>The name of the credential.</summary>
	[JsonPropertyName("name")]
	public string? Name { get; init; }

	/// <summary>The service and account the credential authenticates with (without its secrets).</summary>
	[JsonPropertyName("account")]
	public CredentialAccount? Account { get; init; }

	/// <summary>Whether the credential is available to all sites or to <see cref="Sites"/> only.</summary>
	[JsonPropertyName("siteAssignment")]
	public CredentialSiteAssignment? SiteAssignment { get; init; }

	/// <summary>The sites the credential is assigned to, when <see cref="SiteAssignment"/> is specific sites.</summary>
	[JsonPropertyName("sites")]
	public IReadOnlyList<int> Sites { get; init; } = [];
}
