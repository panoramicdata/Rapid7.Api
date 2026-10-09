using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Vulnerabilities;

/// <summary>The CVSS v3 User Interaction (UI) component. Read from either the abbreviated or the full form.</summary>
public enum CvssUserInteraction
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>No user interaction is needed.</summary>
	[JsonStringEnumMemberName("N")]
	None,

	/// <summary>A user must take some action.</summary>
	[JsonStringEnumMemberName("R")]
	Required,
}
