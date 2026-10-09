using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Tags;

/// <summary>An asset a tag applies to, and how the tag reaches it.</summary>
public sealed class TaggedAsset
{
	/// <summary>The asset identifier.</summary>
	[JsonPropertyName("id")]
	public long? Id { get; init; }

	/// <summary>The ways the tag applies to the asset (directly, through a site or asset group, or by criteria).</summary>
	[JsonPropertyName("sources")]
	public IReadOnlyList<TaggedAssetSource> Sources { get; init; } = [];
}
