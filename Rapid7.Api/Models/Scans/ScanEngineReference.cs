using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Scans;

/// <summary>The scan engine a scan was assigned to.</summary>
public sealed class ScanEngineReference
{
	/// <summary>The scan engine identifier.</summary>
	[JsonPropertyName("id")]
	public int? Id { get; init; }

	/// <summary>Whether the engine is newly assigned for this scan.</summary>
	[JsonPropertyName("newScanEngine")]
	public bool? NewScanEngine { get; init; }

	/// <summary>Whether the engine belongs to the whole console or to a silo.</summary>
	[JsonPropertyName("scope")]
	public ScanEngineScope? Scope { get; init; }
}
