using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Sites;

/// <summary>
/// A schedule for scanning a site, or part of it, at a start time and optionally repeatedly. Read, created and updated
/// as it is; links and <see cref="NextRuntimes"/> are ignored when sent.
/// </summary>
public sealed class ScanSchedule : LinksResource
{
	/// <summary>The schedule identifier (assigned by the console; leave it unset when creating).</summary>
	[JsonPropertyName("id")]
	public int? Id { get; init; }

	/// <summary>Whether the schedule is enabled.</summary>
	[JsonPropertyName("enabled")]
	public bool Enabled { get; init; }

	/// <summary>The name to give the scans the schedule starts.</summary>
	[JsonPropertyName("scanName")]
	public string? ScanName { get; init; }

	/// <summary>When the first scan starts (required).</summary>
	[JsonPropertyName("start")]
	public DateTimeOffset Start { get; init; }

	/// <summary>How often the scan repeats; <see langword="null"/> to run once.</summary>
	[JsonPropertyName("repeat")]
	public ScanScheduleRepeat? Repeat { get; init; }

	/// <summary>
	/// The longest a scan may run, as an ISO 8601 duration (for example <c>PT2H</c>), after which it is stopped; unset for
	/// no limit.
	/// </summary>
	[JsonPropertyName("duration")]
	public string? Duration { get; init; }

	/// <summary>What to do when a run comes round while the previous scan is still running (required).</summary>
	[JsonPropertyName("onScanRepeat")]
	public ScanScheduleOverlap OnScanRepeat { get; init; }

	/// <summary>The scan engine to use instead of the site's.</summary>
	[JsonPropertyName("scanEngineId")]
	public int? ScanEngineId { get; init; }

	/// <summary>The scan template to use instead of the site's.</summary>
	[JsonPropertyName("scanTemplateId")]
	public string? ScanTemplateId { get; init; }

	/// <summary>The part of the site to scan; unset to scan the whole site.</summary>
	[JsonPropertyName("assets")]
	public ScanScopeAssets? Assets { get; init; }

	/// <summary>The next times the schedule will run, as the console reports them.</summary>
	[JsonPropertyName("nextRuntimes")]
	public IReadOnlyList<string> NextRuntimes { get; init; } = [];
}
