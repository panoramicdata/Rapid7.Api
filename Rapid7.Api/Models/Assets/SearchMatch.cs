using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Assets;

/// <summary>How the filters of a <see cref="SearchCriteria"/> combine.</summary>
public enum SearchMatch
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>An asset matches when any one filter matches.</summary>
	[JsonStringEnumMemberName("any")]
	Any,

	/// <summary>An asset matches only when every filter matches.</summary>
	[JsonStringEnumMemberName("all")]
	All
}
