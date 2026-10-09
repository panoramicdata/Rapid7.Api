using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Assets;

/// <summary>Whether a tag ships with the console or was created by a user.</summary>
public enum AssetTagOrigin
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>A tag that ships with the console.</summary>
	[JsonStringEnumMemberName("built-in")]
	BuiltIn,

	/// <summary>A tag created by a user.</summary>
	[JsonStringEnumMemberName("custom")]
	Custom
}
