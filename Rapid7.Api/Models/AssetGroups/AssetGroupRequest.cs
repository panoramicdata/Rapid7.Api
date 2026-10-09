using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.AssetGroups;

/// <summary>The details of an asset group to create, or to replace an existing group's details with.</summary>
public sealed class AssetGroupRequest : AssetGroupDefinition
{
	/// <summary>The name of the group, unique on the console.</summary>
	[JsonPropertyName("name")]
	public required string Name { get; init; }

	/// <summary>Whether the group is static or dynamic. A dynamic group needs <see cref="AssetGroupDefinition.SearchCriteria"/>.</summary>
	[JsonPropertyName("type")]
	public required AssetGroupType Type { get; init; }
}
