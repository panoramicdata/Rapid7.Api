using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.ScanTemplates;

/// <summary>How a scan template finds live assets.</summary>
public sealed class ScanTemplateAssetDiscovery
{
	/// <summary>Whether to send ICMP pings.</summary>
	[JsonPropertyName("sendIcmpPings")]
	public bool? SendIcmpPings { get; init; }

	/// <summary>Whether to send ARP pings.</summary>
	[JsonPropertyName("sendArpPings")]
	public bool? SendArpPings { get; init; }

	/// <summary>The TCP ports to probe for live assets.</summary>
	[JsonPropertyName("tcpPorts")]
	public IReadOnlyList<int> TcpPorts { get; init; } = [];

	/// <summary>The UDP ports to probe for live assets.</summary>
	[JsonPropertyName("udpPorts")]
	public IReadOnlyList<int> UdpPorts { get; init; } = [];

	/// <summary>Whether a TCP reset counts as a live asset.</summary>
	[JsonPropertyName("treatTcpResetAsAsset")]
	public bool? TreatTcpResetAsAsset { get; init; }

	/// <summary>Whether to fingerprint operating systems over IP.</summary>
	[JsonPropertyName("ipFingerprintingEnabled")]
	public bool? IpFingerprintingEnabled { get; init; }

	/// <summary>How many times to retry fingerprinting (0 to 1000).</summary>
	[JsonPropertyName("fingerprintRetries")]
	public int? FingerprintRetries { get; init; }

	/// <summary>The lowest certainty (0 to 1) at which a fingerprint is accepted.</summary>
	[JsonPropertyName("fingerprintMinimumCertainty")]
	public double? FingerprintMinimumCertainty { get; init; }

	/// <summary>Whether to collect Whois information.</summary>
	[JsonPropertyName("collectWhoisInformation")]
	public bool? CollectWhoisInformation { get; init; }
}
