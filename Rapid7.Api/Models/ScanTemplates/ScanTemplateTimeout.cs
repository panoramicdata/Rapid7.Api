using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.ScanTemplates;

/// <summary>How long a scan template's discovery waits for an answer, as ISO 8601 durations (for example <c>PT0.5S</c>).</summary>
public sealed class ScanTemplateTimeout : ScanTemplateRange<string>
{
	/// <summary>The first wait, before the console adapts it.</summary>
	[JsonPropertyName("initial")]
	public string? Initial { get; init; }
}
