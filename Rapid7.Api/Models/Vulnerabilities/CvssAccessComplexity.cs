using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Vulnerabilities;

/// <summary>The CVSS v2 Access Complexity (AC) component. Read from either the abbreviated (<c>"M"</c>) or the full (<c>"MEDIUM"</c>) form.</summary>
public enum CvssAccessComplexity
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>No special conditions are needed to exploit the vulnerability.</summary>
	[JsonStringEnumMemberName("L")]
	Low,

	/// <summary>Somewhat specialised access conditions are needed.</summary>
	[JsonStringEnumMemberName("M")]
	Medium,

	/// <summary>Specialised access conditions are needed.</summary>
	[JsonStringEnumMemberName("H")]
	High,
}
