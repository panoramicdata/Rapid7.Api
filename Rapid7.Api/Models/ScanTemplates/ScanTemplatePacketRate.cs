using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.ScanTemplates;

/// <summary>The range of packets per second a scan template's discovery sends, each bound from 0 to 15000 (default 0).</summary>
public sealed record ScanTemplatePacketRate : ScanTemplateRange<int?>
{
	/// <summary>
	/// Whether the minimum rate is kept even against targets that rate-limit TCP resets (<c>defeat-rst-ratelimit</c>),
	/// which is faster but can be less accurate.
	/// </summary>
	[JsonPropertyName("defeatRateLimit")]
	public bool? DefeatRateLimit { get; init; }
}
