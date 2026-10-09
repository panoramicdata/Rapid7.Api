using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Assets;

/// <summary>A page the web spider found in a web application.</summary>
public sealed class WebPage
{
	/// <summary>How the page was found.</summary>
	[JsonPropertyName("linkType")]
	public WebPageLinkType? LinkType { get; init; }

	/// <summary>The path of the page.</summary>
	[JsonPropertyName("path")]
	public string? Path { get; init; }

	/// <summary>The HTTP status code the page answered with.</summary>
	[JsonPropertyName("response")]
	public int? Response { get; init; }
}
