using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Assets;

/// <summary>The vulnerabilities found on an asset, by severity, with the number of exploits and malware kits.</summary>
public sealed class AssetVulnerabilities : VulnerabilityCounts
{
	/// <summary>The number of distinct exploits that can target the vulnerabilities of the asset.</summary>
	[JsonPropertyName("exploits")]
	public long? Exploits { get; init; }

	/// <summary>The number of distinct malware kits that can target the vulnerabilities of the asset.</summary>
	[JsonPropertyName("malwareKits")]
	public long? MalwareKits { get; init; }
}
