using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.ScanTemplates;

/// <summary>A part of a scan template that the console returns with its own hypermedia links.</summary>
/// <remarks>
/// Scan template types are records used both to read and to write templates: read one, change it with a
/// <see langword="with"/> expression, and send it back. Their collections stay <see langword="null"/> when absent, so a
/// template built in code sends only what was set.
/// </remarks>
public abstract record ScanTemplateSection
{
	/// <summary>
	/// Hypermedia links, set by the console in responses and ignored by it in requests; leave them
	/// <see langword="null"/> in templates you build.
	/// </summary>
	[JsonPropertyName("links")]
	public IReadOnlyList<Link>? Links { get; init; }
}
