using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Vulnerabilities;

/// <summary>The CVSS v2 Authentication (Au) component: how many times an attacker must authenticate. Read from either the abbreviated or the full form.</summary>
public enum CvssAuthentication
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>No authentication is needed.</summary>
	[JsonStringEnumMemberName("N")]
	None,

	/// <summary>One authentication is needed.</summary>
	[JsonStringEnumMemberName("S")]
	[SuppressMessage("Naming", "CA1720:Identifier contains type name", Justification = "CVSS v2 names the value Single, and the member name also reads the full form \"SINGLE\".")]
	Single,

	/// <summary>Two or more authentications are needed.</summary>
	[JsonStringEnumMemberName("M")]
	Multiple,
}
