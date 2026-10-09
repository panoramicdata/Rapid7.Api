using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Tags;

/// <summary>Where a tag comes from.</summary>
public enum TagSource
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>Supplied with the console.</summary>
	[JsonStringEnumMemberName("built-in")]
	BuiltIn,

	/// <summary>Created by a user.</summary>
	[JsonStringEnumMemberName("custom")]
	Custom
}
