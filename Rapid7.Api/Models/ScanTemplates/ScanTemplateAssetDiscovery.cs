using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.ScanTemplates;

/// <summary>How a scan template finds live assets and fingerprints them.</summary>
public sealed record ScanTemplateAssetDiscovery
{
	/// <summary>Whether ARP pings are sent (the console's default is <see langword="true"/>).</summary>
	[JsonPropertyName("sendArpPings")]
	public bool? SendArpPings { get; init; }

	/// <summary>Whether ICMP pings are sent.</summary>
	[JsonPropertyName("sendIcmpPings")]
	public bool? SendIcmpPings { get; init; }

	/// <summary>TCP ports probed to find live assets; none by default.</summary>
	[JsonPropertyName("tcpPorts")]
	public IReadOnlyList<int>? TcpPorts { get; init; }

	/// <summary>UDP ports probed to find live assets; none by default.</summary>
	[JsonPropertyName("udpPorts")]
	public IReadOnlyList<int>? UdpPorts { get; init; }

	/// <summary>Whether a TCP reset answer counts as a live asset (the console's default is <see langword="true"/>).</summary>
	[JsonPropertyName("treatTcpResetAsAsset")]
	public bool? TreatTcpResetAsAsset { get; init; }

	/// <summary>Whether TCP/IP stacks are fingerprinted to identify hardware, operating system and software.</summary>
	[JsonPropertyName("ipFingerprintingEnabled")]
	public bool? IpFingerprintingEnabled { get; init; }

	/// <summary>How many attempts are made to fingerprint the operating system, from 0 to 1000 (default 4).</summary>
	[JsonPropertyName("fingerprintRetries")]
	public int? FingerprintRetries { get; init; }

	/// <summary>The certainty, from 0 to 1, a fingerprint needs to be accepted (default 0.16).</summary>
	[JsonPropertyName("fingerprintMinimumCertainty")]
	public double? FingerprintMinimumCertainty { get; init; }

	/// <summary>Whether Whois is queried during discovery.</summary>
	[JsonPropertyName("collectWhoisInformation")]
	public bool? CollectWhoisInformation { get; init; }
}
