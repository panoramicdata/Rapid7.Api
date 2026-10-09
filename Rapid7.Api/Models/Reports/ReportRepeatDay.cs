using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Reports;

/// <summary>A day of the week in a report schedule.</summary>
public enum ReportRepeatDay
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
