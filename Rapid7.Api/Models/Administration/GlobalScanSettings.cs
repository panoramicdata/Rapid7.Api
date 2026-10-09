using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Administration;

/// <summary>The console's global scan settings. Durations are ISO 8601, such as <c>PT15S</c>.</summary>
public sealed class GlobalScanSettings
{
	/// <summary>The timeout for connecting to remote scan engines.</summary>
	[JsonPropertyName("connectionTimeout")]
	public string? ConnectionTimeout { get; init; }

	/// <summary>The timeout for reading from remote scan engines.</summary>
	[JsonPropertyName("readTimeout")]
	public string? ReadTimeout { get; init; }

	/// <summary>The idle timeout when checking the status of running scans.</summary>
	[JsonPropertyName("statusIdleTimeout")]
	public string? StatusIdleTimeout { get; init; }

	/// <summary>The number of threads that check the status of running scans.</summary>
	[JsonPropertyName("statusThreads")]
	public int? StatusThreads { get; init; }

	/// <summary>The most scan threads any scan uses; <c>-1</c> leaves it to the scan template.</summary>
	[JsonPropertyName("maximumThreads")]
	public int? MaximumThreads { get; init; }

	/// <summary>Whether incremental scan results are enabled.</summary>
	[JsonPropertyName("incremental")]
	public bool? Incremental { get; init; }
}
