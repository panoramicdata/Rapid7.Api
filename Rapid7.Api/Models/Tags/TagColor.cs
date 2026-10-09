using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Tags;

/// <summary>The colour a tag is shown in.</summary>
public enum TagColor
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>The user interface's default colour.</summary>
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
