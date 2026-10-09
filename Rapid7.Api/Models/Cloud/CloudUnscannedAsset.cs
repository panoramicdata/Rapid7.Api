using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Cloud;

/// <summary>An asset a scan request named but could not scan.</summary>
public sealed class CloudUnscannedAsset
{
	/// <summary>The asset identifier.</summary>
	[JsonPropertyName("id")]
	public string? Id { get; init; }

	/// <summary>Why the asset was not scanned.</summary>
	[JsonPropertyName("reason")]
	public string? Reason { get; init; }
}
