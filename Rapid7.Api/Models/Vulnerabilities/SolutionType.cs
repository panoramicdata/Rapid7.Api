using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Vulnerabilities;

/// <summary>The kind of remediation a solution describes.</summary>
public enum SolutionType
{
	/// <summary>The console's <c>"unknown"</c> type, or a value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>A configuration change.</summary>
	[JsonStringEnumMemberName("configuration")]
	Configuration,

	/// <summary>A roll-up patch that bundles other patches.</summary>
	[JsonStringEnumMemberName("rollup-patch")]
	RollupPatch,

	/// <summary>A patch.</summary>
	[JsonStringEnumMemberName("patch")]
	Patch,
}
