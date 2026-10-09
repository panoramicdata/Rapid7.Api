using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.ScanEngines;

/// <summary>How a site's assets are defined.</summary>
public enum ScanEngineSiteType
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>Assets reported by Insight Agents.</summary>
	[JsonStringEnumMemberName("agent")]
	Agent,

	/// <summary>Assets found through a discovery connection.</summary>
	[JsonStringEnumMemberName("dynamic")]
	Dynamic,

	/// <summary>Assets listed explicitly as scan targets.</summary>
	[JsonStringEnumMemberName("static")]
	Static
}
