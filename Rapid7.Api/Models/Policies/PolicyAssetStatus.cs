using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Policies;

/// <summary>How an asset fared against a policy, policy group or policy rule.</summary>
public enum PolicyAssetStatus
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>The asset is compliant.</summary>
	[JsonStringEnumMemberName("passed")]
	Passed,

	/// <summary>The asset is not compliant.</summary>
	[JsonStringEnumMemberName("failed")]
	Failed,

	/// <summary>The policy does not apply to the asset.</summary>
	[JsonStringEnumMemberName("notApplicable")]
	NotApplicable
}
