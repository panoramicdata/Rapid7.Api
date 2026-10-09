using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Sites;

/// <summary>The organization a site belongs to and its contact details, as shown in reports.</summary>
public sealed class SiteOrganization : LinksResource
{
	/// <summary>The organization name.</summary>
	[JsonPropertyName("name")]
	public string? Name { get; init; }

	/// <summary>The organization's website address.</summary>
	[JsonPropertyName("url")]
	public string? Url { get; init; }

	/// <summary>The name of the contact person.</summary>
	[JsonPropertyName("contact")]
	public string? Contact { get; init; }

	/// <summary>The contact's job title.</summary>
	[JsonPropertyName("jobTitle")]
	public string? JobTitle { get; init; }

	/// <summary>The contact email address.</summary>
	[JsonPropertyName("email")]
	public string? Email { get; init; }

	/// <summary>The contact telephone number.</summary>
	[JsonPropertyName("phone")]
	public string? Phone { get; init; }

	/// <summary>The street address.</summary>
	[JsonPropertyName("address")]
	public string? Address { get; init; }

	/// <summary>The city.</summary>
	[JsonPropertyName("city")]
	public string? City { get; init; }

	/// <summary>The state or province.</summary>
	[JsonPropertyName("state")]
	public string? State { get; init; }

	/// <summary>The postal code.</summary>
	[JsonPropertyName("zipCode")]
	public string? ZipCode { get; init; }

	/// <summary>The country.</summary>
	[JsonPropertyName("country")]
	public string? Country { get; init; }
}
