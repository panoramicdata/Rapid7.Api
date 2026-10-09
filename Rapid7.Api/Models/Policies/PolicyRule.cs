using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Policies;

/// <summary>A rule of a compliance policy, with its results across assets.</summary>
public sealed class PolicyRule : PolicyComponent
{
	/// <summary>Whether the rule was created by users.</summary>
	[JsonPropertyName("isCustom")]
	public bool? IsCustom { get; init; }

	/// <summary>How the rule's results count towards compliance.</summary>
	[JsonPropertyName("role")]
	public PolicyRuleRole? Role { get; init; }
}
