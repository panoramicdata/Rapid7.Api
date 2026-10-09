using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Sites;

/// <summary>A day of the week, for a scan schedule that repeats on a weekday of the month.</summary>
public enum ScanScheduleDay
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>Sunday.</summary>
	[JsonStringEnumMemberName("sunday")]
	Sunday,

	/// <summary>Monday.</summary>
	[JsonStringEnumMemberName("monday")]
	Monday,

	/// <summary>Tuesday.</summary>
	[JsonStringEnumMemberName("tuesday")]
	Tuesday,

	/// <summary>Wednesday.</summary>
	[JsonStringEnumMemberName("wednesday")]
	Wednesday,

	/// <summary>Thursday.</summary>
	[JsonStringEnumMemberName("thursday")]
	Thursday,

	/// <summary>Friday.</summary>
	[JsonStringEnumMemberName("friday")]
	Friday,

	/// <summary>Saturday.</summary>
	[JsonStringEnumMemberName("saturday")]
	Saturday
}
