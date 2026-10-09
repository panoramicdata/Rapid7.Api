using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Vulnerabilities;

/// <summary>The CVSS v3 Attack Complexity (AC) component. Read from either the abbreviated or the full form.</summary>
public enum CvssAttackComplexity
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>No special conditions are needed.</summary>
	[JsonStringEnumMemberName("L")]
	Low,

	/// <summary>Success depends on conditions beyond the attacker's control.</summary>
	[JsonStringEnumMemberName("H")]
	High,
}
