using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Policies;

/// <summary>The properties shared by policy groups and policy rules.</summary>
public abstract class PolicyComponent : PolicyResource
{
	/// <summary>The name.</summary>
	[JsonPropertyName("name")]
	public string? Name { get; init; }

	/// <summary>How many assets pass, fail or are not applicable.</summary>
	[JsonPropertyName("assets")]
	public PolicyResultCounts? Assets { get; init; }

	/// <summary>The benchmark it belongs to.</summary>
	[JsonPropertyName("benchmark")]
	public PolicyBenchmark? Benchmark { get; init; }
}
