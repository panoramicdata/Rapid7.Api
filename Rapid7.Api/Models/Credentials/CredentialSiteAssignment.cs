using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Credentials;

/// <summary>Which sites a shared scan credential is available to.</summary>
public enum CredentialSiteAssignment
{
	/// <summary>A value this client does not recognise.</summary>
	Unknown = 0,

	/// <summary>Every current and future site (<c>all-sites</c>).</summary>
	[JsonStringEnumMemberName("all-sites")]
	AllSites,

	/// <summary>Only the sites listed in <c>sites</c> (<c>specific-sites</c>).</summary>
	[JsonStringEnumMemberName("specific-sites")]
	SpecificSites
}
