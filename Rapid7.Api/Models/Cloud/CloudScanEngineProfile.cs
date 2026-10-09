using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Cloud;

/// <summary>The profile of a scan engine.</summary>
public sealed class CloudScanEngineProfile
{
	/// <summary>The custom configuration applied to the scan engine.</summary>
	[JsonPropertyName("configuration")]
	public CloudScanEngineConfiguration? Configuration { get; init; }
}
