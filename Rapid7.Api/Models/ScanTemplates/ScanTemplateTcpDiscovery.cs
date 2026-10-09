using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.ScanTemplates;

/// <summary>Which TCP ports are scanned for services, and how.</summary>
public sealed record ScanTemplateTcpDiscovery : ScanTemplatePortDiscovery
{
	/// <summary>How TCP ports are probed (the console's default is <see cref="TcpDiscoveryMethod.Syn"/>).</summary>
	[JsonPropertyName("method")]
	public TcpDiscoveryMethod? Method { get; init; }
}
