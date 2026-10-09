using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Vulnerabilities;

/// <summary>A CVSS v2 impact component (confidentiality, integrity or availability). Read from either the abbreviated or the full form.</summary>
public enum CvssV2Impact
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>No impact.</summary>
	[JsonStringEnumMemberName("N")]
	None,

	/// <summary>A partial impact.</summary>
	[JsonStringEnumMemberName("P")]
	Partial,

	/// <summary>A total impact.</summary>
	[JsonStringEnumMemberName("C")]
	Complete,
}
