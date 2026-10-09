using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.PolicyOverrides;

/// <summary>A review action on a policy override, sent in the path of the status change endpoint.</summary>
public enum PolicyOverrideStatusChange
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>Withdraw the override.</summary>
	[JsonStringEnumMemberName("recall")]
	Recall,

	/// <summary>Approve the override.</summary>
	[JsonStringEnumMemberName("approve")]
	Approve,

	/// <summary>Reject the override.</summary>
	[JsonStringEnumMemberName("reject")]
	Reject
}
