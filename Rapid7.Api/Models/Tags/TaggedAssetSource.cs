using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Tags;

/// <summary>How a tag reaches an asset.</summary>
public enum TaggedAssetSource
{
	/// <summary>The console does not know (also any value this library does not recognise).</summary>
	[JsonStringEnumMemberName("unknown")]
	Unknown = 0,

	/// <summary>Through a tagged site the asset belongs to.</summary>
	[JsonStringEnumMemberName("site")]
	Site,

	/// <summary>Through a tagged asset group the asset belongs to.</summary>
	[JsonStringEnumMemberName("asset-group")]
	AssetGroup,

	/// <summary>Through the tag's search criteria.</summary>
	[JsonStringEnumMemberName("criteria")]
	Criteria,

	/// <summary>Applied to the asset directly.</summary>
	[JsonStringEnumMemberName("tag")]
	Tag
}
