using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Cloud;

/// <summary>The names of the Cloud Integrations API components, grouped by health status.</summary>
public sealed class CloudHealthSummary
{
	/// <summary>The components that are healthy.</summary>
	[JsonPropertyName("up")]
	public IReadOnlyList<string> Up { get; init; } = [];

	/// <summary>The components that are down.</summary>
	[JsonPropertyName("down")]
	public IReadOnlyList<string> Down { get; init; } = [];

	/// <summary>The components that are out of service.</summary>
	[JsonPropertyName("outOfService")]
	public IReadOnlyList<string> OutOfService { get; init; } = [];

	/// <summary>The components whose health is unknown.</summary>
	[JsonPropertyName("unknown")]
	public IReadOnlyList<string> Unknown { get; init; } = [];
}
