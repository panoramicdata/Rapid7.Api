using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Assets;

/// <summary>The display colour of a tag.</summary>
public enum AssetTagColor
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>The default colour for the tag type.</summary>
	[JsonStringEnumMemberName("default")]
	Default,

	/// <summary>Blue.</summary>
	[JsonStringEnumMemberName("blue")]
	Blue,

	/// <summary>Green.</summary>
	[JsonStringEnumMemberName("green")]
	Green,

	/// <summary>Orange.</summary>
	[JsonStringEnumMemberName("orange")]
	Orange,

	/// <summary>Red.</summary>
	[JsonStringEnumMemberName("red")]
	Red,

	/// <summary>Purple.</summary>
	[JsonStringEnumMemberName("purple")]
	Purple
}
