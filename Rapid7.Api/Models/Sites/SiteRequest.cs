using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Sites;

/// <summary>The settings common to creating and updating a site.</summary>
public abstract class SiteRequest
{
	/// <summary>The site name, unique on the console.</summary>
	[JsonPropertyName("name")]
	public required string Name { get; init; }

	/// <summary>A description of the site.</summary>
	[JsonPropertyName("description")]
	public string? Description { get; init; }
}
