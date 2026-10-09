using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Reports;

/// <summary>How often a report schedule repeats.</summary>
/// <remarks>The console requires <see cref="Every"/> and <see cref="Interval"/> when a schedule repeats.</remarks>
public sealed class ReportRepeat
{
	/// <summary>The unit of time the schedule repeats in.</summary>
	[JsonPropertyName("every")]
	public ReportRepeatUnit? Every { get; init; }

	/// <summary>How many units pass between runs.</summary>
	[JsonPropertyName("interval")]
	public int? Interval { get; init; }

	/// <summary>For <see cref="ReportRepeatUnit.DayOfMonth"/>, the day of the week.</summary>
	[JsonPropertyName("dayOfWeek")]
	public ReportRepeatDay? DayOfWeek { get; init; }

	/// <summary>For <see cref="ReportRepeatUnit.DayOfMonth"/>, the week of the month (1 to 5).</summary>
	[JsonPropertyName("weekOfMonth")]
	public int? WeekOfMonth { get; init; }
}
