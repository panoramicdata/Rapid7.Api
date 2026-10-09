using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Remediations;

/// <summary>What a solution was matched against.</summary>
public enum SolutionMatchType
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>Not matched.</summary>
	[JsonStringEnumMemberName("none")]
	None,

	/// <summary>The vulnerability check.</summary>
	[JsonStringEnumMemberName("check")]
	Check,

	/// <summary>The operating system fingerprint.</summary>
	[JsonStringEnumMemberName("operating-system")]
	OperatingSystem,

	/// <summary>A service fingerprint.</summary>
	[JsonStringEnumMemberName("service")]
	Service,

	/// <summary>A software fingerprint.</summary>
	[JsonStringEnumMemberName("software")]
	Software,
}
