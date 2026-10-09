using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Cloud;

/// <summary>The health of one component of the Cloud Integrations API.</summary>
public sealed class CloudHealthComponent
{
	/// <summary>A description of the component or its state.</summary>
	[JsonPropertyName("description")]
	public string? Description { get; init; }

	/// <summary>The status of the component, such as <c>UP</c>.</summary>
	[JsonPropertyName("status")]
	public string? Status { get; init; }
}
