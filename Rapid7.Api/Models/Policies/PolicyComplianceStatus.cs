using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Policies;

/// <summary>Whether a policy, policy group or policy rule is compliant.</summary>
public enum PolicyComplianceStatus
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>Compliant.</summary>
	[JsonStringEnumMemberName("PASS")]
	Pass,

	/// <summary>Not compliant.</summary>
	[JsonStringEnumMemberName("FAIL")]
	Fail,

	/// <summary>Not applicable to the assets checked.</summary>
	[JsonStringEnumMemberName("NOT_APPLICABLE")]
	NotApplicable
}
