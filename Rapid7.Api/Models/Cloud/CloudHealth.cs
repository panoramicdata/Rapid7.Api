using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Cloud;

/// <summary>The health of the Cloud Integrations API, as reported by <c>GET admin/health</c>.</summary>
public sealed class CloudHealth
{
	/// <summary>The overall status of the service.</summary>
	[JsonPropertyName("status")]
	public CloudHealthStatus Status { get; init; }

	/// <summary>The health of each component of the service, by component name; empty when not reported.</summary>
	[JsonPropertyName("components")]
	public IReadOnlyDictionary<string, CloudHealthComponent> Components { get; init; } = new Dictionary<string, CloudHealthComponent>();

	/// <summary>How long the health check took, as reported by the service; absent when not reported.</summary>
	[JsonPropertyName("duration")]
	public string? Duration { get; init; }

	/// <summary>The component names grouped by status; absent when not reported.</summary>
	[JsonPropertyName("summary")]
	public CloudHealthSummary? Summary { get; init; }
}
