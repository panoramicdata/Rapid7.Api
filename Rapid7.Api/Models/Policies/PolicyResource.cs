using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Policies;

/// <summary>The properties shared by policies, policy groups and policy rules.</summary>
public abstract class PolicyResource : PolicyContentResource
{
	/// <summary>The identifier, as text.</summary>
	[JsonPropertyName("id")]
	public string? Id { get; init; }

	/// <summary>The numeric identifier, which the endpoint paths use.</summary>
	[JsonPropertyName("surrogateId")]
	public long? SurrogateId { get; init; }
}
