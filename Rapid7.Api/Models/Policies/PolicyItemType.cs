using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Policies;

/// <summary>Whether a child of a policy or policy group is a rule or a group.</summary>
public enum PolicyItemType
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>A policy rule.</summary>
	[JsonStringEnumMemberName("rule")]
	Rule,

	/// <summary>A policy group.</summary>
	[JsonStringEnumMemberName("group")]
	Group
}
