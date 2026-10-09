using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Cloud;

/// <summary>
/// A tag on an asset, or a site: the Cloud Integrations API lists sites as tags whose <see cref="Type"/> is <c>SITE</c>.
/// </summary>
public sealed class CloudTag
{
	/// <summary>The tag or site name.</summary>
	[JsonPropertyName("name")]
	public string? Name { get; init; }

	/// <summary>The kind of tag, such as <c>SITE</c>, <c>CUSTOM</c>, <c>LOCATION</c>, <c>OWNER</c> or <c>CRITICALITY</c>.</summary>
	[JsonPropertyName("type")]
	public string? Type { get; init; }
}
