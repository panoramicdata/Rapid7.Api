using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.AssetDiscovery;

/// <summary>A saved Sonar query: criteria that discover assets from Rapid7 Project Sonar data.</summary>
public sealed class SonarQuery : Links
{
	/// <summary>The criteria of the query.</summary>
	[JsonPropertyName("criteria")]
	public SonarCriteria? Criteria { get; init; }

	/// <summary>The identifier of the query.</summary>
	[JsonPropertyName("id")]
	public long? Id { get; init; }

	/// <summary>The name of the query.</summary>
	[JsonPropertyName("name")]
	public string? Name { get; init; }
}
