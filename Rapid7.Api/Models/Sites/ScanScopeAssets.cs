using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Sites;

/// <summary>
/// Scan targets and asset groups to include and exclude: the assets of a new static site, or the part of a site a scan
/// schedule scans (whose included targets and groups must already belong to the site).
/// </summary>
public sealed class ScanScopeAssets
{
	/// <summary>The addresses to scan.</summary>
	[JsonPropertyName("includedTargets")]
	public ScanScopeTargets? IncludedTargets { get; init; }

	/// <summary>The addresses never to scan.</summary>
	[JsonPropertyName("excludedTargets")]
	public ScanScopeTargets? ExcludedTargets { get; init; }

	/// <summary>The asset groups whose assets to scan.</summary>
	[JsonPropertyName("includedAssetGroups")]
	public ScanScopeAssetGroups? IncludedAssetGroups { get; init; }

	/// <summary>The asset groups whose assets never to scan.</summary>
	[JsonPropertyName("excludedAssetGroups")]
	public ScanScopeAssetGroups? ExcludedAssetGroups { get; init; }
}
