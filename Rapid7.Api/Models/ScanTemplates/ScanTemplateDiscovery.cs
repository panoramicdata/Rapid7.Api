using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.ScanTemplates;

/// <summary>How a scan template discovers assets and services, and how fast.</summary>
public sealed class ScanTemplateDiscovery
{
	/// <summary>How live assets are found.</summary>
	[JsonPropertyName("asset")]
	public ScanTemplateAssetDiscovery? Asset { get; init; }

	/// <summary>How services are found.</summary>
	[JsonPropertyName("service")]
	public ScanTemplateServiceDiscovery? Service { get; init; }

	/// <summary>Discovery speed and timing.</summary>
	[JsonPropertyName("performance")]
	public ScanTemplateDiscoveryPerformance? Performance { get; init; }
}
