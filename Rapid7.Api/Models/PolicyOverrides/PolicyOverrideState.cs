using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.PolicyOverrides;

/// <summary>Where a policy override is in its review lifecycle.</summary>
public enum PolicyOverrideState
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>Submitted and waiting for review.</summary>
	[JsonStringEnumMemberName("under-review")]
	UnderReview,

	/// <summary>Approved, and in effect.</summary>
	[JsonStringEnumMemberName("approved")]
	Approved,

	/// <summary>Rejected by a reviewer.</summary>
	[JsonStringEnumMemberName("rejected")]
	Rejected,

	/// <summary>Deleted.</summary>
	[JsonStringEnumMemberName("deleted")]
	Deleted,

	/// <summary>Past its expiry date.</summary>
	[JsonStringEnumMemberName("expired")]
	Expired
}
