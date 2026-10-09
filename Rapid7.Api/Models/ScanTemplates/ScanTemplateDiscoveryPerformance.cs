using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.ScanTemplates;

/// <summary>How fast, how widely in parallel and how persistently a scan template's discovery probes.</summary>
public sealed record ScanTemplateDiscoveryPerformance
{
	/// <summary>How many attempts, from 1 to 15, are made to reach a target before it is treated as down.</summary>
	[JsonPropertyName("retryLimit")]
	public int? RetryLimit { get; init; }

	/// <summary>The range of packets sent each second.</summary>
	[JsonPropertyName("packetRate")]
	public ScanTemplatePacketRate? PacketRate { get; init; }

	/// <summary>The range of discovery requests sent in parallel, each bound from 0 to 1000 (0, the default, lets the engine decide).</summary>
	[JsonPropertyName("parallelism")]
	public ScanTemplateRange<int?>? Parallelism { get; init; }

	/// <summary>The range of delays between packets sent to each target, as ISO 8601 durations.</summary>
	[JsonPropertyName("scanDelay")]
	public ScanTemplateRange<string>? ScanDelay { get; init; }

	/// <summary>How long to wait for an answer before retrying.</summary>
	[JsonPropertyName("timeout")]
	public ScanTemplateTimeout? Timeout { get; init; }
}
