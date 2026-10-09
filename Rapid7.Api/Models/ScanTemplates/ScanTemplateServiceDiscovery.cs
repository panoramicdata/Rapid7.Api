using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.ScanTemplates;

/// <summary>How a scan template finds services on live assets.</summary>
public sealed class ScanTemplateServiceDiscovery
{
	/// <summary>The TCP ports to scan and how.</summary>
	[JsonPropertyName("tcp")]
	public ScanTemplateTcpServiceDiscovery? Tcp { get; init; }

	/// <summary>The UDP ports to scan.</summary>
	[JsonPropertyName("udp")]
	public ScanTemplateUdpServiceDiscovery? Udp { get; init; }

	/// <summary>The file that maps ports to service names.</summary>
	[JsonPropertyName("serviceNameFile")]
	public string? ServiceNameFile { get; init; }
}
