using Rapid7.Api.Models.Assets;
using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Scans;

/// <summary>A scan: running, paused or finished.</summary>
public class Scan : Links
{
	/// <summary>The scan identifier.</summary>
	[JsonPropertyName("id")]
	public long? Id { get; init; }

	/// <summary>The name given to the scan.</summary>
	[JsonPropertyName("scanName")]
	public string? ScanName { get; init; }

	/// <summary>How the scan was started, such as <c>Manual</c> or <c>Scheduled</c>.</summary>
	[JsonPropertyName("scanType")]
	public string? ScanType { get; init; }

	/// <summary>The scan's state.</summary>
	[JsonPropertyName("status")]
	public ScanStatus? Status { get; init; }

	/// <summary>Why the scan has its status, when the console gives a reason.</summary>
	[JsonPropertyName("message")]
	public string? Message { get; init; }

	/// <summary>When the scan started.</summary>
	[JsonPropertyName("startTime")]
	public DateTimeOffset? StartTime { get; init; }

	/// <summary>When the scan ended; absent while it runs.</summary>
	[JsonPropertyName("endTime")]
	public DateTimeOffset? EndTime { get; init; }

	/// <summary>How long the scan ran, as an ISO 8601 duration (for example <c>PT1H2M</c>).</summary>
	[JsonPropertyName("duration")]
	public string? Duration { get; init; }

	/// <summary>The display name of the user who started the scan.</summary>
	[JsonPropertyName("startedBy")]
	public string? StartedBy { get; init; }

	/// <summary>The login name of the user who started the scan.</summary>
	[JsonPropertyName("startedByUsername")]
	public string? StartedByUsername { get; init; }

	/// <summary>The identifier of the scan engine that ran the scan.</summary>
	[JsonPropertyName("engineId")]
	public int? EngineId { get; init; }

	/// <summary>The name of the scan engine that ran the scan.</summary>
	[JsonPropertyName("engineName")]
	public string? EngineName { get; init; }

	/// <summary>Details of the scan engine assignment.</summary>
	[JsonPropertyName("engineIds")]
	public ScanEngineReference? EngineIds { get; init; }

	/// <summary>The number of assets the scan found.</summary>
	[JsonPropertyName("assets")]
	public int? Assets { get; init; }

	/// <summary>The vulnerabilities the scan found, by severity.</summary>
	[JsonPropertyName("vulnerabilities")]
	public VulnerabilityCounts? Vulnerabilities { get; init; }
}
