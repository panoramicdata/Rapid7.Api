using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.ScanTemplates;

/// <summary>The ports a scan template scans for services over one protocol.</summary>
public abstract class ScanTemplatePortDiscovery : Links
{
	/// <summary>Which ports to scan.</summary>
	[JsonPropertyName("ports")]
	public ScanTemplatePortSet? Ports { get; init; }

	/// <summary>More ports to scan, as a comma-separated list of ports and ranges (<c>8000-8080</c>).</summary>
	[JsonPropertyName("additionalPorts")]
	public string? AdditionalPorts { get; init; }

	/// <summary>Ports not to scan, in the same form.</summary>
	[JsonPropertyName("excludedPorts")]
	public string? ExcludedPorts { get; init; }
}
