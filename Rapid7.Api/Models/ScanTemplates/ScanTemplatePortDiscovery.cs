using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.ScanTemplates;

/// <summary>Which ports of one protocol are scanned for services (used as is for UDP).</summary>
public record ScanTemplatePortDiscovery : ScanTemplateSection
{
	/// <summary>The base set of ports to scan (the console's default is well-known ports).</summary>
	[JsonPropertyName("ports")]
	public ScanTemplatePortSelection? Ports { get; init; }

	/// <summary>Further ports to scan, as a comma-separated list of ports and ranges (for example <c>3078,8000-8080</c>).</summary>
	[JsonPropertyName("additionalPorts")]
	public string? AdditionalPorts { get; init; }

	/// <summary>Ports never to scan, in the same form as <see cref="AdditionalPorts"/>.</summary>
	[JsonPropertyName("excludedPorts")]
	public string? ExcludedPorts { get; init; }
}
