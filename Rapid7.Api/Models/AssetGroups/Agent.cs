using Rapid7.Api.Models.Assets;
using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.AssetGroups;

/// <summary>An asset assessed by a Rapid7 Insight Agent, with the agent's identifier and last assessment time.</summary>
public sealed class Agent : Asset
{
	/// <summary>The identifier of the Insight Agent.</summary>
	[JsonPropertyName("agentId")]
	public string? AgentId { get; init; }

	/// <summary>When the agent last assessed the asset for vulnerabilities.</summary>
	[JsonPropertyName("lastAssessedForVulnerabilities")]
	public DateTimeOffset LastAssessedForVulnerabilities { get; init; }
}
