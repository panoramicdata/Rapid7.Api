using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Reports;

/// <summary>Which assets count towards a site, group or tag in a risk trend.</summary>
public enum RiskTrendMembership
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>The members at each point in time.</summary>
	[JsonStringEnumMemberName("historical")]
	Historical,

	/// <summary>The members when the report is generated.</summary>
	[JsonStringEnumMemberName("generation")]
	Generation
}
