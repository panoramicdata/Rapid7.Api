using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Sites;

/// <summary>The scan status changes a site alert reports.</summary>
public sealed class AlertScanEvents
{
	/// <summary>Whether to alert when a scan starts.</summary>
	[JsonPropertyName("started")]
	public bool Started { get; init; }

	/// <summary>Whether to alert when a scan is stopped.</summary>
	[JsonPropertyName("stopped")]
	public bool Stopped { get; init; }

	/// <summary>Whether to alert when a scan fails.</summary>
	[JsonPropertyName("failed")]
	public bool Failed { get; init; }

	/// <summary>Whether to alert when a scan is paused.</summary>
	[JsonPropertyName("paused")]
	public bool Paused { get; init; }

	/// <summary>Whether to alert when a scan resumes.</summary>
	[JsonPropertyName("resumed")]
	public bool? Resumed { get; init; }
}
