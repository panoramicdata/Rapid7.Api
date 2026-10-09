using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Policies;

/// <summary>How the results of a policy rule count towards compliance.</summary>
public enum PolicyRuleRole
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>The rule is checked and scored.</summary>
	[JsonStringEnumMemberName("full")]
	Full,

	/// <summary>The rule is checked, but its result does not affect the compliance score.</summary>
	[JsonStringEnumMemberName("unscored")]
	Unscored,

	/// <summary>The rule is not checked.</summary>
	[JsonStringEnumMemberName("unchecked")]
	Unchecked
}
