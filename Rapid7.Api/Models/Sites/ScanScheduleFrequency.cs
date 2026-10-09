using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Sites;

/// <summary>The unit a scan schedule repeats in.</summary>
public enum ScanScheduleFrequency
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>Every so many hours.</summary>
	[JsonStringEnumMemberName("hour")]
	Hour,

	/// <summary>Every so many days.</summary>
	[JsonStringEnumMemberName("day")]
	Day,

	/// <summary>Every so many weeks.</summary>
	[JsonStringEnumMemberName("week")]
	Week,

	/// <summary>On the same date of every so many months.</summary>
	[JsonStringEnumMemberName("date-of-month")]
	DateOfMonth,

	/// <summary>
	/// On a weekday of a week of every so many months (see <see cref="ScanScheduleRepeat.DayOfWeek"/> and
	/// <see cref="ScanScheduleRepeat.WeekOfMonth"/>).
	/// </summary>
	[JsonStringEnumMemberName("day-of-month")]
	DayOfMonth
}
