using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.ScanEngines;

/// <summary>A pool of scan engines that share the scanning of the sites assigned to it.</summary>
public sealed class EnginePool : Links
{
	/// <summary>The engine pool identifier.</summary>
	[JsonPropertyName("id")]
	public int Id { get; init; }

	/// <summary>The engine pool name.</summary>
	[JsonPropertyName("name")]
	public string Name { get; init; } = string.Empty;

	/// <summary>The identifiers of the scan engines in the pool.</summary>
	[JsonPropertyName("engines")]
	public IReadOnlyList<int> Engines { get; init; } = [];

	/// <summary>The identifiers of the sites assigned to the pool.</summary>
	[JsonPropertyName("sites")]
	public IReadOnlyList<int> Sites { get; init; } = [];
}
