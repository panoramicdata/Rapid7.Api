using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Vulnerabilities;

/// <summary>A hypermedia link to a resource outside the console, such as an advisory.</summary>
public class ExternalLink
{
	/// <summary>A deprecation notice for the link, when supplied.</summary>
	[JsonPropertyName("deprecation")]
	public string? Deprecation { get; init; }

	/// <summary>The target URI.</summary>
	[JsonPropertyName("href")]
	public string? Href { get; init; }

	/// <summary>The language of the target, when supplied.</summary>
	[JsonPropertyName("hreflang")]
	public string? Hreflang { get; init; }

	/// <summary>The media the target is intended for, when supplied.</summary>
	[JsonPropertyName("media")]
	public string? Media { get; init; }

	/// <summary>A name for the link, when supplied.</summary>
	[JsonPropertyName("name")]
	public string? Name { get; init; }

	/// <summary>A profile hint for the target, when supplied.</summary>
	[JsonPropertyName("profile")]
	public string? Profile { get; init; }

	/// <summary>The link relation.</summary>
	[JsonPropertyName("rel")]
	public string? Rel { get; init; }

	/// <summary>A title for the link, when supplied.</summary>
	[JsonPropertyName("title")]
	public string? Title { get; init; }

	/// <summary>The media type of the target, when supplied.</summary>
	[JsonPropertyName("type")]
	public string? Type { get; init; }
}
