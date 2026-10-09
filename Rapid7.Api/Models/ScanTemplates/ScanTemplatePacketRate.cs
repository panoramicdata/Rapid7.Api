using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.ScanTemplates;

/// <summary>The packet rate bounds of a scan template's discovery, in packets per second (0 to 15000).</summary>
public sealed class ScanTemplatePacketRate : ScanTemplateRange<int?>
{
	/// <summary>Whether to ignore rate limiting by the target.</summary>
	[JsonPropertyName("defeatRateLimit")]
	public bool? DefeatRateLimit { get; init; }
}
