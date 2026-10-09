using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Assets;

/// <summary>How a tag came to be applied to an asset.</summary>
public enum AssetTagSourceType
{
	/// <summary>The console does not know, or the value is one this library does not recognise.</summary>
	[JsonStringEnumMemberName("unknown")]
	Unknown = 0,

	/// <summary>Through a site the asset belongs to.</summary>
	[JsonStringEnumMemberName("site")]
	Site,

	/// <summary>Through an asset group the asset belongs to.</summary>
	[JsonStringEnumMemberName("asset-group")]
	AssetGroup,

	/// <summary>Through the search criteria of the tag.</summary>
	[JsonStringEnumMemberName("criteria")]
	Criteria,

	/// <summary>Directly on the asset.</summary>
	[JsonStringEnumMemberName("tag")]
	Tag
}
