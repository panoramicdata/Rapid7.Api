using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Credentials;

/// <summary>A shared scan credential to create or replace (<c>POST api/3/shared_credentials</c>, <c>PUT api/3/shared_credentials/{id}</c>).</summary>
/// <remarks>The <see cref="Account"/> carries secrets: the client sends them only in the request body and never logs them.</remarks>
public sealed class SharedCredentialRequest : SharedCredentialBase
{
	/// <summary>The name of the credential. Required.</summary>
	[JsonPropertyName("name")]
	public required string Name { get; init; }

	/// <summary>The service and account to authenticate with, including its secrets. Required.</summary>
	[JsonPropertyName("account")]
	public required CredentialAccount Account { get; init; }

	/// <summary>Whether the credential is available to all sites or to <see cref="Sites"/> only. Required.</summary>
	[JsonPropertyName("siteAssignment")]
	public required CredentialSiteAssignment SiteAssignment { get; init; }

	/// <summary>The sites to assign the credential to; only with <see cref="CredentialSiteAssignment.SpecificSites"/>.</summary>
	[JsonPropertyName("sites")]
	public IReadOnlyList<int>? Sites { get; init; }
}
