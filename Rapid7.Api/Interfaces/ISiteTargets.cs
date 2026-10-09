using Rapid7.Api.Models;
using Rapid7.Api.Models.AssetGroups;
using Rapid7.Api.Models.Sites;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>
/// What a static site scans: its included and excluded targets (<c>api/3/sites/{id}/included_targets</c>,
/// <c>excluded_targets</c>) and asset groups (<c>included_asset_groups</c>, <c>excluded_asset_groups</c>). Changing them
/// needs the Specify Scan Targets privilege. Each target is a host name, an IPv4 or IPv6 address, an IPv4 range
/// (<c>10.0.0.1 - 10.0.0.9</c>) or a CIDR block.
/// </summary>
public interface ISiteTargets
{
	/// <summary>Gets the site's included targets (<c>GET api/3/sites/{id}/included_targets</c>).</summary>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The included targets.</returns>
	[Get("api/3/sites/{siteId}/included_targets")]
	Task<ScanTargetsResource> GetIncludedTargetsAsync(int siteId, CancellationToken cancellationToken);

	/// <summary>Adds targets to the site's included targets (<c>POST api/3/sites/{id}/included_targets</c>; the body is a bare JSON array).</summary>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="addresses">The targets to add.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>A reference to the site.</returns>
	[Post("api/3/sites/{siteId}/included_targets")]
	Task<CreatedReference<int>> AddIncludedTargetsAsync(int siteId, [Body] IEnumerable<string> addresses, CancellationToken cancellationToken);

	/// <summary>Replaces the site's included targets (<c>PUT api/3/sites/{id}/included_targets</c>; the body is a bare JSON array).</summary>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="addresses">The complete list of included targets.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the affected resources.</returns>
	[Put("api/3/sites/{siteId}/included_targets")]
	Task<LinksResource> ReplaceIncludedTargetsAsync(int siteId, [Body] IEnumerable<string> addresses, CancellationToken cancellationToken);

	/// <summary>
	/// Removes targets from the site's included targets (<c>DELETE api/3/sites/{id}/included_targets</c>; unusually, the
	/// DELETE carries a body: a bare JSON array).
	/// </summary>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="addresses">The targets to remove.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the affected resources.</returns>
	[Delete("api/3/sites/{siteId}/included_targets")]
	Task<LinksResource> RemoveIncludedTargetsAsync(int siteId, [Body] IEnumerable<string> addresses, CancellationToken cancellationToken);

	/// <summary>Gets the site's excluded targets (<c>GET api/3/sites/{id}/excluded_targets</c>).</summary>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The excluded targets.</returns>
	[Get("api/3/sites/{siteId}/excluded_targets")]
	Task<ScanTargetsResource> GetExcludedTargetsAsync(int siteId, CancellationToken cancellationToken);

	/// <summary>Adds targets to the site's excluded targets (<c>POST api/3/sites/{id}/excluded_targets</c>; the body is a bare JSON array).</summary>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="addresses">The targets to add.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>A reference to the site.</returns>
	[Post("api/3/sites/{siteId}/excluded_targets")]
	Task<CreatedReference<int>> AddExcludedTargetsAsync(int siteId, [Body] IEnumerable<string> addresses, CancellationToken cancellationToken);

	/// <summary>Replaces the site's excluded targets (<c>PUT api/3/sites/{id}/excluded_targets</c>; the body is a bare JSON array).</summary>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="addresses">The complete list of excluded targets.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the affected resources.</returns>
	[Put("api/3/sites/{siteId}/excluded_targets")]
	Task<LinksResource> ReplaceExcludedTargetsAsync(int siteId, [Body] IEnumerable<string> addresses, CancellationToken cancellationToken);

	/// <summary>
	/// Removes targets from the site's excluded targets (<c>DELETE api/3/sites/{id}/excluded_targets</c>; unusually, the
	/// DELETE carries a body: a bare JSON array).
	/// </summary>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="addresses">The targets to remove.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the affected resources.</returns>
	[Delete("api/3/sites/{siteId}/excluded_targets")]
	Task<LinksResource> RemoveExcludedTargetsAsync(int siteId, [Body] IEnumerable<string> addresses, CancellationToken cancellationToken);

	/// <summary>Lists the asset groups the site includes (<c>GET api/3/sites/{id}/included_asset_groups</c>).</summary>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The included asset groups.</returns>
	[Get("api/3/sites/{siteId}/included_asset_groups")]
	Task<ResourceList<AssetGroup>> ListIncludedAssetGroupsAsync(int siteId, CancellationToken cancellationToken);

	/// <summary>
	/// Replaces the asset groups the site includes (<c>PUT api/3/sites/{id}/included_asset_groups</c>; the body is a bare
	/// JSON array of asset group identifiers).
	/// </summary>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="assetGroupIds">The identifiers of every asset group to include.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the affected resources.</returns>
	[Put("api/3/sites/{siteId}/included_asset_groups")]
	Task<LinksResource> ReplaceIncludedAssetGroupsAsync(int siteId, [Body] IEnumerable<int> assetGroupIds, CancellationToken cancellationToken);

	/// <summary>Removes every included asset group from the site (<c>DELETE api/3/sites/{id}/included_asset_groups</c>).</summary>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the affected resources.</returns>
	[Delete("api/3/sites/{siteId}/included_asset_groups")]
	Task<LinksResource> RemoveAllIncludedAssetGroupsAsync(int siteId, CancellationToken cancellationToken);

	/// <summary>Removes one asset group from those the site includes (<c>DELETE api/3/sites/{id}/included_asset_groups/{assetGroupId}</c>).</summary>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="assetGroupId">The identifier of the asset group.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the affected resources.</returns>
	[Delete("api/3/sites/{siteId}/included_asset_groups/{assetGroupId}")]
	Task<LinksResource> RemoveIncludedAssetGroupAsync(int siteId, int assetGroupId, CancellationToken cancellationToken);

	/// <summary>Lists the asset groups the site excludes (<c>GET api/3/sites/{id}/excluded_asset_groups</c>).</summary>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The excluded asset groups.</returns>
	[Get("api/3/sites/{siteId}/excluded_asset_groups")]
	Task<ResourceList<AssetGroup>> ListExcludedAssetGroupsAsync(int siteId, CancellationToken cancellationToken);

	/// <summary>
	/// Replaces the asset groups the site excludes (<c>PUT api/3/sites/{id}/excluded_asset_groups</c>; the body is a bare
	/// JSON array of asset group identifiers).
	/// </summary>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="assetGroupIds">The identifiers of every asset group to exclude.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the affected resources.</returns>
	[Put("api/3/sites/{siteId}/excluded_asset_groups")]
	Task<LinksResource> ReplaceExcludedAssetGroupsAsync(int siteId, [Body] IEnumerable<int> assetGroupIds, CancellationToken cancellationToken);

	/// <summary>Removes every excluded asset group from the site (<c>DELETE api/3/sites/{id}/excluded_asset_groups</c>).</summary>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the affected resources.</returns>
	[Delete("api/3/sites/{siteId}/excluded_asset_groups")]
	Task<LinksResource> RemoveAllExcludedAssetGroupsAsync(int siteId, CancellationToken cancellationToken);

	/// <summary>Removes one asset group from those the site excludes (<c>DELETE api/3/sites/{id}/excluded_asset_groups/{assetGroupId}</c>).</summary>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="assetGroupId">The identifier of the asset group.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the affected resources.</returns>
	[Delete("api/3/sites/{siteId}/excluded_asset_groups/{assetGroupId}")]
	Task<LinksResource> RemoveExcludedAssetGroupAsync(int siteId, int assetGroupId, CancellationToken cancellationToken);
}
