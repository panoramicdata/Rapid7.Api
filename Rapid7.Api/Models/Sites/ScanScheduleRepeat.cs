using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Sites;

/// <summary>How often a scan schedule repeats after its first run.</summary>
public sealed class ScanScheduleRepeat
{
	/// <summary>The unit of repetition (required).</summary>
	[JsonPropertyName("every")]
	public ScanScheduleFrequency Every { get; init; }

	/// <summary>How many units apart the runs are (required): <c>2</c> with <see cref="ScanScheduleFrequency.Week"/> is fortnightly.</summary>
	[JsonPropertyName("interval")]
	public int Interval { get; init; }

	/// <summary>For <see cref="ScanScheduleFrequency.DayOfMonth"/>, the weekday to run on.</summary>
	[JsonPropertyName("dayOfWeek")]
	public ScanScheduleDay? DayOfWeek { get; init; }

	/// <summary>For <see cref="ScanScheduleFrequency.DayOfMonth"/>, the week of the month (1 to 5) to run in.</summary>
	[JsonPropertyName("weekOfMonth")]
	public int? WeekOfMonth { get; init; }

	/// <summary>For <see cref="ScanScheduleFrequency.DateOfMonth"/>, whether to run on the last day of the month instead.</summary>
	[JsonPropertyName("lastDayOfMonth")]
	public bool? LastDayOfMonth { get; init; }
}
