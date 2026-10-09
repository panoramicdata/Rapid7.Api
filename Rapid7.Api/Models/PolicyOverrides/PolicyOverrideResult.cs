using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.PolicyOverrides;

/// <summary>The result a policy override substitutes for the rule's real result.</summary>
public enum PolicyOverrideResult
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>Treat the rule as passed.</summary>
	[JsonStringEnumMemberName("pass")]
	Pass,

	/// <summary>Treat the rule as failed.</summary>
	[JsonStringEnumMemberName("fail")]
	Fail,

	/// <summary>Treat the rule as not applicable.</summary>
	[JsonStringEnumMemberName("not-applicable")]
	NotApplicable,

	/// <summary>Treat the rule as fixed.</summary>
	[JsonStringEnumMemberName("fixed")]
	Fixed
}
