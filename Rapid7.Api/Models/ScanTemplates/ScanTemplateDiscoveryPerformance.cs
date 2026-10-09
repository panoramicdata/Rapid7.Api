using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.ScanTemplates;

/// <summary>How fast a scan template discovers assets and services.</summary>
public sealed class ScanTemplateDiscoveryPerformance
{
	/// <summary>The packet rate, in packets per second.</summary>
	[JsonPropertyName("packetRate")]
	public ScanTemplatePacketRate? PacketRate { get; init; }

	/// <summary>How many probes may be outstanding at once.</summary>
	[JsonPropertyName("parallelism")]
	public ScanTemplateRange<int?>? Parallelism { get; init; }

	/// <summary>The delay between probes, as ISO 8601 durations.</summary>
	[JsonPropertyName("scanDelay")]
	public ScanTemplateRange<string>? ScanDelay { get; init; }

	/// <summary>How long to wait for an answer, as ISO 8601 durations.</summary>
	[JsonPropertyName("timeout")]
	public ScanTemplateTimeout? Timeout { get; init; }

	/// <summary>How many times to retry an unanswered probe (1 to 15).</summary>
	[JsonPropertyName("retryLimit")]
	public int? RetryLimit { get; init; }
}
