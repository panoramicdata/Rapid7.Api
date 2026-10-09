using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Reports;

/// <summary>The unit of the reporting periods in a report's date range.</summary>
public enum ReportRangeInterval
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>Days.</summary>
	[JsonStringEnumMemberName("day")]
	Day,

	/// <summary>Months.</summary>
	[JsonStringEnumMemberName("month")]
	Month,

	/// <summary>Years.</summary>
	[JsonStringEnumMemberName("year")]
	Year
}
