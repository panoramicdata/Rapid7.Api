using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Vulnerabilities;

/// <summary>The CVSS v3 Scope (S) component. Read from either the abbreviated or the full form.</summary>
public enum CvssScope
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>The impact stays within the vulnerable component's security authority.</summary>
	[JsonStringEnumMemberName("U")]
	Unchanged,

	/// <summary>The impact reaches beyond the vulnerable component's security authority.</summary>
	[JsonStringEnumMemberName("C")]
	Changed,
}
