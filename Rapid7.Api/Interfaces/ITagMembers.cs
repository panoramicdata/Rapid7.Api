using Rapid7.Api.Models;
using Rapid7.Api.Models.Tags;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>
/// The assets, asset groups and sites a tag is applied to (<c>api/3/tags/{id}/assets</c>, <c>.../asset_groups</c>,
/// <c>.../sites</c>). Tagging an asset group or a site tags every asset in it.
/// </summary>
public interface ITagMembers
{
	/// <summary>Lists the assets a tag applies to, and how it reaches each (<c>GET api/3/tags/{id}/assets</c>).</summary>
	/// <param name="tagId">The tag identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The tagged assets.</returns>
	[Get("api/3/tags/{tagId}/assets")]
	Task<ResourceList<TaggedAsset>> ListAssetsAsync(int tagId, CancellationToken cancellationToken);

	/// <summary>Applies a tag to an asset directly (<c>PUT api/3/tags/{id}/assets/{assetId}</c>).</summary>
	/// <param name="tagId">The tag identifier.</param>
	/// <param name="assetId">The asset identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the tag and asset.</returns>
	[Put("api/3/tags/{tagId}/assets/{assetId}")]
	Task<Links> AddAssetAsync(int tagId, long assetId, CancellationToken cancellationToken);

	/// <summary>
	/// Removes a tag applied directly to an asset (<c>DELETE api/3/tags/{id}/assets/{assetId}</c>). An asset tagged
	/// through a site, asset group or search criteria keeps the tag.
	/// </summary>
	/// <param name="tagId">The tag identifier.</param>
	/// <param name="assetId">The asset identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to related resources.</returns>
	[Delete("api/3/tags/{tagId}/assets/{assetId}")]
	Task<Links> RemoveAssetAsync(int tagId, long assetId, CancellationToken cancellationToken);

	/// <summary>Lists the identifiers of the asset groups a tag is applied to (<c>GET api/3/tags/{id}/asset_groups</c>).</summary>
	/// <param name="tagId">The tag identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The asset group identifiers.</returns>
	[Get("api/3/tags/{tagId}/asset_groups")]
	Task<ResourceList<int>> ListAssetGroupsAsync(int tagId, CancellationToken cancellationToken);

	/// <summary>Replaces the asset groups a tag is applied to (<c>PUT api/3/tags/{id}/asset_groups</c>).</summary>
	/// <param name="tagId">The tag identifier.</param>
	/// <param name="assetGroupIds">The identifiers of every asset group the tag should apply to.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the tag's asset groups.</returns>
	[Put("api/3/tags/{tagId}/asset_groups")]
	Task<Links> SetAssetGroupsAsync(int tagId, [Body] IEnumerable<int> assetGroupIds, CancellationToken cancellationToken);

	/// <summary>Removes a tag from every asset group (<c>DELETE api/3/tags/{id}/asset_groups</c>).</summary>
	/// <param name="tagId">The tag identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to related resources.</returns>
	[Delete("api/3/tags/{tagId}/asset_groups")]
	Task<Links> RemoveAllAssetGroupsAsync(int tagId, CancellationToken cancellationToken);

	/// <summary>Applies a tag to an asset group (<c>PUT api/3/tags/{id}/asset_groups/{assetGroupId}</c>).</summary>
	/// <param name="tagId">The tag identifier.</param>
	/// <param name="assetGroupId">The asset group identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the tag and asset group.</returns>
	[Put("api/3/tags/{tagId}/asset_groups/{assetGroupId}")]
	Task<Links> AddAssetGroupAsync(int tagId, int assetGroupId, CancellationToken cancellationToken);

	/// <summary>Removes a tag from an asset group (<c>DELETE api/3/tags/{id}/asset_groups/{assetGroupId}</c>).</summary>
	/// <param name="tagId">The tag identifier.</param>
	/// <param name="assetGroupId">The asset group identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to related resources.</returns>
	[Delete("api/3/tags/{tagId}/asset_groups/{assetGroupId}")]
	Task<Links> RemoveAssetGroupAsync(int tagId, int assetGroupId, CancellationToken cancellationToken);

	/// <summary>Lists the identifiers of the sites a tag is applied to (<c>GET api/3/tags/{id}/sites</c>).</summary>
	/// <param name="tagId">The tag identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The site identifiers.</returns>
	[Get("api/3/tags/{tagId}/sites")]
	Task<ResourceList<int>> ListSitesAsync(int tagId, CancellationToken cancellationToken);

	/// <summary>Replaces the sites a tag is applied to (<c>PUT api/3/tags/{id}/sites</c>).</summary>
	/// <param name="tagId">The tag identifier.</param>
	/// <param name="siteIds">The identifiers of every site the tag should apply to.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the tag's sites.</returns>
	[Put("api/3/tags/{tagId}/sites")]
	Task<Links> SetSitesAsync(int tagId, [Body] IEnumerable<int> siteIds, CancellationToken cancellationToken);

	/// <summary>Removes a tag from every site (<c>DELETE api/3/tags/{id}/sites</c>).</summary>
	/// <param name="tagId">The tag identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to related resources.</returns>
	[Delete("api/3/tags/{tagId}/sites")]
	Task<Links> RemoveAllSitesAsync(int tagId, CancellationToken cancellationToken);

	/// <summary>Applies a tag to a site (<c>PUT api/3/tags/{id}/sites/{siteId}</c>).</summary>
	/// <param name="tagId">The tag identifier.</param>
	/// <param name="siteId">The site identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the tag and site.</returns>
	[Put("api/3/tags/{tagId}/sites/{siteId}")]
	Task<Links> AddSiteAsync(int tagId, int siteId, CancellationToken cancellationToken);

	/// <summary>Removes a tag from a site (<c>DELETE api/3/tags/{id}/sites/{siteId}</c>).</summary>
	/// <param name="tagId">The tag identifier.</param>
	/// <param name="siteId">The site identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to related resources.</returns>
	[Delete("api/3/tags/{tagId}/sites/{siteId}")]
	Task<Links> RemoveSiteAsync(int tagId, int siteId, CancellationToken cancellationToken);
}
