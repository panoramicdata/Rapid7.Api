using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.ScanTemplates;

/// <summary>How a scan template discovers assets and the services they run.</summary>
public sealed record ScanTemplateDiscovery
{
	/// <summary>How live assets are found and fingerprinted.</summary>
	[JsonPropertyName("asset")]
	public ScanTemplateAssetDiscovery? Asset { get; init; }

	/// <summary>How fast and how persistently discovery probes.</summary>
	[JsonPropertyName("performance")]
	public ScanTemplateDiscoveryPerformance? Performance { get; init; }

	/// <summary>Which TCP and UDP ports are scanned for services.</summary>
	[JsonPropertyName("service")]
	public ScanTemplateServiceDiscovery? Service { get; init; }
}
