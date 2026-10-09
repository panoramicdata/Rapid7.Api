using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.ScanTemplates;

/// <summary>Which ports a scan template scans for services.</summary>
public sealed record ScanTemplateServiceDiscovery
{
	/// <summary>TCP port scanning.</summary>
	[JsonPropertyName("tcp")]
	public ScanTemplateTcpDiscovery? Tcp { get; init; }

	/// <summary>UDP port scanning.</summary>
	[JsonPropertyName("udp")]
	public ScanTemplatePortDiscovery? Udp { get; init; }

	/// <summary>
	/// An optional file mapping ports to the services that usually run on them, used when a service cannot be
	/// identified.
	/// </summary>
	[JsonPropertyName("serviceNameFile")]
	public string? ServiceNameFile { get; init; }
}
