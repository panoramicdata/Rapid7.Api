using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Cloud;

/// <summary>The answer to starting a scan: the scans started, and the assets that could not be scanned and why.</summary>
public sealed class CloudScanDispatch
{
	/// <summary>The scans started, with the assets each covers.</summary>
	[JsonPropertyName("scans")]
	public IReadOnlyList<CloudScan> Scans { get; init; } = [];

	/// <summary>The assets left out, with the reason for each.</summary>
	[JsonPropertyName("unscanned_assets")]
	public IReadOnlyList<CloudUnscannedAsset> UnscannedAssets { get; init; } = [];
}
