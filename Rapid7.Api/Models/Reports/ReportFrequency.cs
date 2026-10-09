using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Reports;

/// <summary>When a report is generated.</summary>
public sealed class ReportFrequency
{
	/// <summary>Whether the report is generated after scans, on a schedule, or only on demand.</summary>
	[JsonPropertyName("type")]
	public ReportFrequencyType? Type { get; init; }

	/// <summary>For a schedule, when generation starts.</summary>
	[JsonPropertyName("start")]
	public DateTimeOffset? Start { get; init; }

	/// <summary>For a schedule, how often it repeats.</summary>
	[JsonPropertyName("repeat")]
	public ReportRepeat? Repeat { get; init; }

	/// <summary>For a schedule, the next run times the console has planned; returned only, ignored when sent.</summary>
	[JsonPropertyName("nextRuntimes")]
	public IReadOnlyList<string>? NextRuntimes { get; init; }
}
