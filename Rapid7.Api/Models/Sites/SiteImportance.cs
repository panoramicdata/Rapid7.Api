using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Sites;

/// <summary>How important a site is; the console weights the risk scores of the site's assets by it.</summary>
public enum SiteImportance
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>Very low importance.</summary>
	[JsonStringEnumMemberName("very_low")]
	VeryLow,

	/// <summary>Low importance.</summary>
	[JsonStringEnumMemberName("low")]
	Low,

	/// <summary>Normal importance (the default).</summary>
	[JsonStringEnumMemberName("normal")]
	Normal,

	/// <summary>High importance.</summary>
	[JsonStringEnumMemberName("high")]
	High,

	/// <summary>Very high importance.</summary>
	[JsonStringEnumMemberName("very_high")]
	VeryHigh
}
