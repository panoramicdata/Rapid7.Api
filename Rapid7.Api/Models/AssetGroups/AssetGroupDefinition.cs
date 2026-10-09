using Rapid7.Api.Models.Assets;
using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.AssetGroups;

/// <summary>The editable details of an asset group, shared by <see cref="AssetGroup"/> and <see cref="AssetGroupRequest"/>.</summary>
public abstract class AssetGroupDefinition
{
	/// <summary>The description of the group.</summary>
	[JsonPropertyName("description")]
	public string? Description { get; init; }

	/// <summary>The criteria that select the members of a dynamic group (or the assets to start a static group with).</summary>
	[JsonPropertyName("searchCriteria")]
	public SearchCriteria? SearchCriteria { get; init; }
}
