using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Policies;

/// <summary>
/// The descriptive properties shared by policies, policy groups and policy rules (<see cref="PolicyResource"/>) and by the
/// items listed under a policy or group (<see cref="PolicyItem"/>).
/// </summary>
public abstract class PolicyContentResource : LinksResource
{
	/// <summary>The title shown to users.</summary>
	[JsonPropertyName("title")]
	public string? Title { get; init; }

	/// <summary>A description.</summary>
	[JsonPropertyName("description")]
	public string? Description { get; init; }

	/// <summary>Where it comes from: <c>Built-in</c> for content shipped with the console, <c>Custom</c> for content users created.</summary>
	[JsonPropertyName("scope")]
	public string? Scope { get; init; }

	/// <summary>
	/// The compliance status: across the assets the caller can see or, for an item read in one asset's view, that asset's
	/// status.
	/// </summary>
	[JsonPropertyName("status")]
	public PolicyComplianceStatus? Status { get; init; }
}
