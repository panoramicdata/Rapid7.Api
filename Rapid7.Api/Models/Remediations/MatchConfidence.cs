using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Remediations;

/// <summary>How confidently a solution was matched to an asset.</summary>
public enum MatchConfidence
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>An exact match.</summary>
	[JsonStringEnumMemberName("EXACT")]
	Exact,

	/// <summary>A partial match.</summary>
	[JsonStringEnumMemberName("PARTIAL")]
	Partial,

	/// <summary>No match.</summary>
	[JsonStringEnumMemberName("NONE")]
	None,
}
