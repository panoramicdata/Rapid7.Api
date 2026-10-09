using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Assets;

/// <summary>One way a tag came to be applied to an asset, with a link to the resource responsible.</summary>
public sealed class AssetTagSource : LinksResource
{
	/// <summary>The identifier of the resource responsible, such as the site or asset group.</summary>
	[JsonPropertyName("id")]
	public int? Id { get; init; }

	/// <summary>How the tag came to be applied.</summary>
	[JsonPropertyName("source")]
	public AssetTagSourceType? Source { get; init; }
}
