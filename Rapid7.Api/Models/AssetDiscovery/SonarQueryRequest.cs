using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.AssetDiscovery;

/// <summary>The name and criteria of a Sonar query to create, or to replace an existing query with.</summary>
public sealed class SonarQueryRequest
{
	/// <summary>The name of the query.</summary>
	[JsonPropertyName("name")]
	public required string Name { get; init; }

	/// <summary>The criteria of the query.</summary>
	[JsonPropertyName("criteria")]
	public SonarCriteria? Criteria { get; init; }
}
