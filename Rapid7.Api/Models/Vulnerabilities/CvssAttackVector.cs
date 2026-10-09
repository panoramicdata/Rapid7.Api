using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Vulnerabilities;

/// <summary>The CVSS v3 Attack Vector (AV) component. Read from either the abbreviated or the full form.</summary>
public enum CvssAttackVector
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>Exploitable remotely over the network.</summary>
	[JsonStringEnumMemberName("N")]
	Network,

	/// <summary>Exploitable from the adjacent network.</summary>
	[JsonStringEnumMemberName("A")]
	Adjacent,

	/// <summary>Exploitable with local access.</summary>
	[JsonStringEnumMemberName("L")]
	Local,

	/// <summary>Exploitable only with physical access.</summary>
	[JsonStringEnumMemberName("P")]
	Physical,
}
