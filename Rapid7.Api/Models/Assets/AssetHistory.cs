using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Assets;

/// <summary>One change in the history of an asset, such as a scan or an import.</summary>
public sealed class AssetHistory
{
	/// <summary>When the change happened.</summary>
	[JsonPropertyName("date")]
	public DateTimeOffset? Date { get; init; }

	/// <summary>A description of the source of the change, when one was given.</summary>
	[JsonPropertyName("description")]
	public string? Description { get; init; }

	/// <summary>The scan that made the change, for a scan entry.</summary>
	[JsonPropertyName("scanId")]
	public long? ScanId { get; init; }

	/// <summary>The kind of change, such as <c>SCAN</c>, <c>ASSET-IMPORT</c> or <c>VULNERABILITY_EXCEPTION_APPLIED</c>.</summary>
	[JsonPropertyName("type")]
	public string? Type { get; init; }

	/// <summary>The user who made the change, when a user made it.</summary>
	[JsonPropertyName("user")]
	public string? User { get; init; }

	/// <summary>The version of the asset after the change.</summary>
	[JsonPropertyName("version")]
	public int? Version { get; init; }

	/// <summary>The vulnerability exception involved, for an exception entry.</summary>
	[JsonPropertyName("vulnerabilityExceptionId")]
	public int? VulnerabilityExceptionId { get; init; }
}
