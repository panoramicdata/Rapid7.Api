using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Cloud;

/// <summary>A scan run through the Insight platform.</summary>
public sealed class CloudScan
{
	/// <summary>The scan identifier.</summary>
	[JsonPropertyName("id")]
	public string? Id { get; init; }

	/// <summary>The scan name.</summary>
	[JsonPropertyName("name")]
	public string? Name { get; init; }

	/// <summary>The identifier of the scan engine running the scan.</summary>
	[JsonPropertyName("engine_id")]
	public string? EngineId { get; init; }

	/// <summary>The scan status, such as <c>Success</c>.</summary>
	[JsonPropertyName("status")]
	public string? Status { get; init; }

	/// <summary>When the scan started.</summary>
	[JsonPropertyName("started")]
	public DateTimeOffset? Started { get; init; }

	/// <summary>When the scan finished.</summary>
	[JsonPropertyName("finished")]
	public DateTimeOffset? Finished { get; init; }

	/// <summary>
	/// Additional details about the scan, when requested with <c>includeDetails</c>. The specification does not document
	/// their shape: text is returned as is, and an object or array as its JSON text.
	/// </summary>
	[JsonPropertyName("details")]
	public string? Details { get; init; }

	/// <summary>The identifiers of the assets being scanned; returned only when a scan is started.</summary>
	[JsonPropertyName("asset_ids")]
	public IReadOnlyList<string> AssetIds { get; init; } = [];
}
