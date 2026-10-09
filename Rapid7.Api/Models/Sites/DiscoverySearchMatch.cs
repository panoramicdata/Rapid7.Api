using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Sites;

/// <summary>How the filters of a dynamic site's discovery search combine.</summary>
public enum DiscoverySearchMatch
{
	/// <summary>A value this client does not recognise.</summary>
	Unknown = 0,

	/// <summary>An asset is included when any filter matches.</summary>
	[JsonStringEnumMemberName("any")]
	Any,

	/// <summary>An asset is included only when every filter matches.</summary>
	[JsonStringEnumMemberName("all")]
	All,
}
