using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.AssetDiscovery;

/// <summary>An asset discovered by a Sonar query.</summary>
public sealed class DiscoveryAsset : Links
{
	/// <summary>The address of the asset.</summary>
	[JsonPropertyName("address")]
	public string? Address { get; init; }

	/// <summary>The host name of the asset.</summary>
	[JsonPropertyName("name")]
	public string? Name { get; init; }
}
