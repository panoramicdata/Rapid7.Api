using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.ScanEngines;

/// <summary>The settings of an engine pool to create or update.</summary>
public sealed class EnginePoolRequest
{
	/// <summary>The engine pool name.</summary>
	[JsonPropertyName("name")]
	public required string Name { get; init; }

	/// <summary>The identifiers of the scan engines to place in the pool, or <see langword="null"/> to leave them out.</summary>
	[JsonPropertyName("engines")]
	public IReadOnlyList<int>? Engines { get; init; }

	/// <summary>The identifiers of the sites to assign to the pool, or <see langword="null"/> to leave them out.</summary>
	[JsonPropertyName("sites")]
	public IReadOnlyList<int>? Sites { get; init; }
}
