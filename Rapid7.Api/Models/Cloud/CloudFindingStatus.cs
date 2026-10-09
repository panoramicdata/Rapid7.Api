using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Cloud;

/// <summary>The outcome of checking an asset for a vulnerability.</summary>
public enum CloudFindingStatus
{
	/// <summary>Not known (<c>UNKNOWN</c>), or a value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>Checked and not vulnerable (<c>NOT_VULNERABLE</c>).</summary>
	[JsonStringEnumMemberName("NOT_VULNERABLE")]
	NotVulnerable,

	/// <summary>Vulnerable, confirmed by exploiting it (<c>VULNERABLE_EXPL</c>).</summary>
	[JsonStringEnumMemberName("VULNERABLE_EXPL")]
	VulnerableExploited,

	/// <summary>Vulnerable, inferred from the installed version (<c>VULNERABLE_VERS</c>).</summary>
	[JsonStringEnumMemberName("VULNERABLE_VERS")]
	VulnerableVersion,

	/// <summary>Potentially vulnerable (<c>VULNERABLE_POTENTIAL</c>).</summary>
	[JsonStringEnumMemberName("VULNERABLE_POTENTIAL")]
	VulnerablePotential,

	/// <summary>Skipped because the version could not be determined (<c>SKIPPED_VERS</c>).</summary>
	[JsonStringEnumMemberName("SKIPPED_VERS")]
	SkippedVersion,

	/// <summary>Skipped because the check could cause a denial of service (<c>SKIPPED_DOS</c>).</summary>
	[JsonStringEnumMemberName("SKIPPED_DOS")]
	SkippedDenialOfService,

	/// <summary>The check failed unexpectedly (<c>UNEXPECTED_ERR</c>).</summary>
	[JsonStringEnumMemberName("UNEXPECTED_ERR")]
	UnexpectedError,

	/// <summary>Skipped because the check is disabled (<c>SKIPPED_DISABLED</c>).</summary>
	[JsonStringEnumMemberName("SKIPPED_DISABLED")]
	SkippedDisabled,

	/// <summary>Not vulnerable, and not stored (<c>NOT_VULN_DONT_STORE</c>).</summary>
	[JsonStringEnumMemberName("NOT_VULN_DONT_STORE")]
	NotVulnerableNotStored,

	/// <summary>Vulnerable by exploit, but covered by an exception (<c>EXCEPTION_VULN_EXPL</c>).</summary>
	[JsonStringEnumMemberName("EXCEPTION_VULN_EXPL")]
	ExceptionVulnerableExploited,

	/// <summary>Vulnerable by version, but covered by an exception (<c>EXCEPTION_VULN_VERS</c>).</summary>
	[JsonStringEnumMemberName("EXCEPTION_VULN_VERS")]
	ExceptionVulnerableVersion,

	/// <summary>Potentially vulnerable, but covered by an exception (<c>EXCEPTION_VULN_POTL</c>).</summary>
	[JsonStringEnumMemberName("EXCEPTION_VULN_POTL")]
	ExceptionVulnerablePotential,

	/// <summary>Vulnerable by version, but overridden (<c>OVERRIDDEN_VULN_VERS</c>).</summary>
	[JsonStringEnumMemberName("OVERRIDDEN_VULN_VERS")]
	OverriddenVulnerableVersion,

	/// <summary>Superseded by another finding (<c>SUPERSEDED</c>).</summary>
	[JsonStringEnumMemberName("SUPERSEDED")]
	Superseded
}
