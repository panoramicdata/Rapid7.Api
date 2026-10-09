using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.ScanTemplates;

/// <summary>How long discovery waits for an answer between retries: a range of ISO 8601 durations plus the first wait.</summary>
public sealed record ScanTemplateTimeout : ScanTemplateRange<string>
{
	/// <summary>The first wait, as an ISO 8601 duration (for example <c>PT0.5S</c>).</summary>
	[JsonPropertyName("initial")]
	public string? Initial { get; init; }
}
