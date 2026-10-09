using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.PolicyOverrides;

/// <summary>The result a policy rule check produced.</summary>
public enum PolicyCheckResult
{
	/// <summary>The result is unknown (also used for values this library does not recognise).</summary>
	[JsonStringEnumMemberName("unknown")]
	Unknown = 0,

	/// <summary>The check passed.</summary>
	[JsonStringEnumMemberName("pass")]
	Pass,

	/// <summary>The check failed.</summary>
	[JsonStringEnumMemberName("fail")]
	Fail,

	/// <summary>The check raised an error.</summary>
	[JsonStringEnumMemberName("error")]
	Error,

	/// <summary>The check does not apply.</summary>
	[JsonStringEnumMemberName("not-applicable")]
	NotApplicable,

	/// <summary>The check was not run.</summary>
	[JsonStringEnumMemberName("not-checked")]
	NotChecked,

	/// <summary>The rule was not selected.</summary>
	[JsonStringEnumMemberName("not-selected")]
	NotSelected,

	/// <summary>The check is informational only.</summary>
	[JsonStringEnumMemberName("informational")]
	Informational,

	/// <summary>The finding was fixed.</summary>
	[JsonStringEnumMemberName("fixed")]
	Fixed
}
