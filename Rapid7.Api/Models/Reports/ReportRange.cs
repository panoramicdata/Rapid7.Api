using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Reports;

/// <summary>The date range a trend report covers.</summary>
public sealed class ReportRange
{
	/// <summary>Where the range starts: a duration (<c>P1Y</c>, <c>P6M</c>, <c>P3M</c>, <c>P1M</c>) or a date.</summary>
	[JsonPropertyName("from")]
	public string? From { get; init; }

	/// <summary>Where the range ends, when <see cref="From"/> is a date.</summary>
	[JsonPropertyName("to")]
	public DateOnly? To { get; init; }

	/// <summary>When <see cref="From"/> is a date, the unit of the reporting periods.</summary>
	[JsonPropertyName("every")]
	public ReportRangeInterval? Every { get; init; }

	/// <summary>When <see cref="From"/> is a date, the number of units in each reporting period.</summary>
	[JsonPropertyName("interval")]
	public int? Interval { get; init; }
}
