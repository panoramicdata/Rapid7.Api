using System.Text.Json.Serialization;
using Rapid7.Api.Models.Assets;

namespace Rapid7.Api.Models.AssetGroups;

/// <summary>An asset group, with its member count, risk score and vulnerability counts.</summary>
public sealed class AssetGroup : AssetGroupDefinition
{
	/// <summary>The number of assets in the group.</summary>
	[JsonPropertyName("assets")]
	public int? Assets { get; init; }

	/// <summary>The identifier of the group.</summary>
	[JsonPropertyName("id")]
	public int? Id { get; init; }

	/// <summary>Links to the group and its related resources.</summary>
	[JsonPropertyName("links")]
	public IReadOnlyList<Link> Links { get; init; } = [];

	/// <summary>The name of the group.</summary>
	[JsonPropertyName("name")]
	public string Name { get; init; } = string.Empty;

	/// <summary>The total risk score of the assets in the group.</summary>
	[JsonPropertyName("riskScore")]
	public double? RiskScore { get; init; }

	/// <summary>Whether the group is static or dynamic.</summary>
	[JsonPropertyName("type")]
	public AssetGroupType Type { get; init; }

	/// <summary>The vulnerabilities found on the assets of the group, by severity.</summary>
	[JsonPropertyName("vulnerabilities")]
	public VulnerabilityCounts? Vulnerabilities { get; init; }
}
