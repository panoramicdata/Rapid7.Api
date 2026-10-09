using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Policies;

/// <summary>The benchmark a policy group or rule belongs to.</summary>
public sealed class PolicyBenchmark : LinksResource
{
	/// <summary>The name of the benchmark.</summary>
	[JsonPropertyName("name")]
	public string? Name { get; init; }

	/// <summary>The title of the benchmark.</summary>
	[JsonPropertyName("title")]
	public string? Title { get; init; }

	/// <summary>The version of the benchmark.</summary>
	[JsonPropertyName("version")]
	public string? Version { get; init; }
}
