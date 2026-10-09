using System.Text.Json.Serialization;

namespace Rapid7.Api.Models;

/// <summary>A hypermedia link to a related resource or operation.</summary>
public sealed class Link
{
	/// <summary>The target: a URI, or a URI template (RFC 6570).</summary>
	[JsonPropertyName("href")]
	public string? Href { get; init; }

	/// <summary>The link relation, such as <c>self</c>, <c>next</c>, or the type of the linked resource.</summary>
	[JsonPropertyName("rel")]
	public string? Rel { get; init; }

	/// <summary>A title for the link, when supplied.</summary>
	[JsonPropertyName("title")]
	public string? Title { get; init; }

	/// <summary>The media type of the target, when supplied.</summary>
	[JsonPropertyName("type")]
	public string? Type { get; init; }

	/// <inheritdoc />
	public override string ToString() => $"{Rel}: {Href}";
}
