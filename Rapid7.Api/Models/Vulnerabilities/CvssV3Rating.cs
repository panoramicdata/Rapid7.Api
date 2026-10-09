using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Vulnerabilities;

/// <summary>A CVSS v3 none/low/high component: Privileges Required, or a confidentiality, integrity or availability impact. Read from either the abbreviated or the full form.</summary>
public enum CvssV3Rating
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>None.</summary>
	[JsonStringEnumMemberName("N")]
	None,

	/// <summary>Low.</summary>
	[JsonStringEnumMemberName("L")]
	Low,

	/// <summary>High.</summary>
	[JsonStringEnumMemberName("H")]
	High,
}
