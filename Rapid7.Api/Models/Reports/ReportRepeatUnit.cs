using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Reports;

/// <summary>The unit of time a report schedule repeats in.</summary>
public enum ReportRepeatUnit
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>Every few hours.</summary>
	[JsonStringEnumMemberName("hour")]
	Hour,

	/// <summary>Every few days.</summary>
	[JsonStringEnumMemberName("day")]
	Day,

	/// <summary>Every few weeks.</summary>
	[JsonStringEnumMemberName("week")]
	Week,

	/// <summary>On a date of the month, every few months.</summary>
	[JsonStringEnumMemberName("date-of-month")]
	DateOfMonth,

	/// <summary>On a weekday of a week of the month, every few months.</summary>
	[JsonStringEnumMemberName("day-of-month")]
	DayOfMonth
}
