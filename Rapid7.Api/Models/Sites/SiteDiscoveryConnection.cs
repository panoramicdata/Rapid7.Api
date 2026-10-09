using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Sites;

/// <summary>The discovery connection a site is assigned to.</summary>
public sealed class SiteDiscoveryConnection : LinksResource
{
	/// <summary>The identifier of the discovery connection.</summary>
	[JsonPropertyName("id")]
	public long? Id { get; init; }

	/// <summary>The name of the discovery connection.</summary>
	[JsonPropertyName("name")]
	public string? Name { get; init; }

	/// <summary>The kind of discovery connection (meaningful for dynamic sites only).</summary>
	[JsonPropertyName("type")]
	public DiscoveryConnectionType? Type { get; init; }
}
