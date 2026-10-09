using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.AssetGroups;

/// <summary>A static or dynamic group of assets.</summary>
public sealed class AssetGroup : Links
{
	/// <summary>The identifier of the asset group.</summary>
	[JsonPropertyName("id")]
	public int? Id { get; init; }

	/// <summary>The name of the asset group.</summary>
	[JsonPropertyName("name")]
	public string? Name { get; init; }

	/// <summary>A description of the asset group.</summary>
	[JsonPropertyName("description")]
	public string? Description { get; init; }

	/// <summary>The kind of group: <c>static</c> or <c>dynamic</c>.</summary>
	[JsonPropertyName("type")]
	public string? Type { get; init; }

	/// <summary>The number of assets in the group.</summary>
	[JsonPropertyName("assets")]
	public int? Assets { get; init; }

	/// <summary>The total risk score of the group's assets.</summary>
	[JsonPropertyName("riskScore")]
	public double? RiskScore { get; init; }
}
