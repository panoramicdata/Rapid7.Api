using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Vulnerabilities;

/// <summary>Text the console supplies both as HTML and as plain text (descriptions, solution steps and summaries).</summary>
public sealed class Content
{
	/// <summary>The HTML form.</summary>
	[JsonPropertyName("html")]
	public string? Html { get; init; }

	/// <summary>The plain-text form.</summary>
	[JsonPropertyName("text")]
	public string? Text { get; init; }
}
