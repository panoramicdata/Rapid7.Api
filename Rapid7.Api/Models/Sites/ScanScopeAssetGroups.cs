using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Sites;

/// <summary>A list of asset groups within a scan scope.</summary>
public sealed class ScanScopeAssetGroups
{
	/// <summary>The asset group identifiers.</summary>
	[JsonPropertyName("assetGroupIDs")]
	public IReadOnlyList<int> AssetGroupIds { get; init; } = [];
}
