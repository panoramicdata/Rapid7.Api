using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Cloud;

/// <summary>A scan engine registered with the Insight platform.</summary>
public sealed class CloudScanEngine
{
	/// <summary>The scan engine identifier.</summary>
	[JsonPropertyName("id")]
	public string? Id { get; init; }

	/// <summary>The scan engine name.</summary>
	[JsonPropertyName("name")]
	public string? Name { get; init; }

	/// <summary>The host name or address of the scan engine.</summary>
	[JsonPropertyName("host_name")]
	public string? HostName { get; init; }

	/// <summary>The scan engine status, such as <c>HEALTHY</c>.</summary>
	[JsonPropertyName("status")]
	public string? Status { get; init; }

	/// <summary>When the platform last heard from the scan engine.</summary>
	[JsonPropertyName("last_seen")]
	public DateTimeOffset? LastSeen { get; init; }

	/// <summary>When the scan engine was registered.</summary>
	[JsonPropertyName("registered")]
	public DateTimeOffset? Registered { get; init; }

	/// <summary>The scan engine profile, holding its custom configuration.</summary>
	[JsonPropertyName("profile")]
	public CloudScanEngineProfile? Profile { get; init; }
}
