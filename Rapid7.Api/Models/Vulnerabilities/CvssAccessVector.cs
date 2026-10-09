using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Vulnerabilities;

/// <summary>The CVSS v2 Access Vector (AV) component. Read from either the abbreviated (<c>"N"</c>) or the full (<c>"NETWORK"</c>) form.</summary>
public enum CvssAccessVector
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>Exploitable only with local access.</summary>
	[JsonStringEnumMemberName("L")]
	Local,

	/// <summary>Exploitable from the adjacent network.</summary>
	[JsonStringEnumMemberName("A")]
	Adjacent,

	/// <summary>Exploitable remotely over the network.</summary>
	[JsonStringEnumMemberName("N")]
	Network,
}
