using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.ScanTemplates;

/// <summary>The TCP ports a scan template scans for services, and the probing method.</summary>
public sealed class ScanTemplateTcpServiceDiscovery : ScanTemplatePortDiscovery
{
	/// <summary>How TCP ports are probed.</summary>
	[JsonPropertyName("method")]
	public ScanTemplateTcpMethod? Method { get; init; }
}
