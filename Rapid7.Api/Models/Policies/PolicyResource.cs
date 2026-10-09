using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Policies;

/// <summary>The properties shared by policies, policy groups and policy rules.</summary>
public abstract class PolicyResource : LinksResource
{
	/// <summary>The identifier, as text.</summary>
	[JsonPropertyName("id")]
	public string? Id { get; init; }

	/// <summary>The numeric identifier, which the endpoint paths use.</summary>
	[JsonPropertyName("surrogateId")]
	public long? SurrogateId { get; init; }

	/// <summary>The title shown to users.</summary>
	[JsonPropertyName("title")]
	public string? Title { get; init; }

	/// <summary>A description.</summary>
	[JsonPropertyName("description")]
	public string? Description { get; init; }

	/// <summary>Where it comes from: <c>Built-in</c> for content shipped with the console, <c>Custom</c> for content users created.</summary>
	[JsonPropertyName("scope")]
	public string? Scope { get; init; }

	/// <summary>The overall compliance status, across the assets the caller can see.</summary>
	[JsonPropertyName("status")]
	public PolicyComplianceStatus? Status { get; init; }
}
