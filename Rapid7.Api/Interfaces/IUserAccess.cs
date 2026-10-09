using Rapid7.Api.Models;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>
/// The sites and asset groups a user can access (<c>api/3/users/{id}/sites</c> and <c>api/3/users/{id}/asset_groups</c>).
/// Individual grants are ignored, and cannot be made, for a user whose role gives access to all sites or all asset
/// groups. Changes require Global Administrator.
/// </summary>
public interface IUserAccess
{
	/// <summary>Lists the sites the user can access (<c>GET api/3/users/{id}/sites</c>).</summary>
	/// <param name="id">The identifier of the user.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The site identifiers.</returns>
	[Get("api/3/users/{id}/sites")]
	Task<ResourceList<int>> ListSitesAsync(int id, CancellationToken cancellationToken);

	/// <summary>Replaces the sites the user can access (<c>PUT api/3/users/{id}/sites</c>).</summary>
	/// <param name="id">The identifier of the user.</param>
	/// <param name="siteIds">The identifiers of every site the user should access.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Related links.</returns>
	[Put("api/3/users/{id}/sites")]
	Task<LinksResource> SetSitesAsync(int id, [Body] IEnumerable<int> siteIds, CancellationToken cancellationToken);

	/// <summary>Revokes the user's access to every site (<c>DELETE api/3/users/{id}/sites</c>).</summary>
	/// <param name="id">The identifier of the user.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Related links.</returns>
	[Delete("api/3/users/{id}/sites")]
	Task<LinksResource> RevokeAllSitesAsync(int id, CancellationToken cancellationToken);

	/// <summary>Grants the user access to one site (<c>PUT api/3/users/{id}/sites/{siteId}</c>).</summary>
	/// <param name="id">The identifier of the user.</param>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Related links.</returns>
	[Put("api/3/users/{id}/sites/{siteId}")]
	Task<LinksResource> GrantSiteAsync(int id, int siteId, CancellationToken cancellationToken);

	/// <summary>Revokes the user's access to one site (<c>DELETE api/3/users/{id}/sites/{siteId}</c>).</summary>
	/// <param name="id">The identifier of the user.</param>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Related links.</returns>
	[Delete("api/3/users/{id}/sites/{siteId}")]
	Task<LinksResource> RevokeSiteAsync(int id, int siteId, CancellationToken cancellationToken);

	/// <summary>Lists the asset groups the user can access (<c>GET api/3/users/{id}/asset_groups</c>).</summary>
	/// <param name="id">The identifier of the user.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The asset group identifiers.</returns>
	[Get("api/3/users/{id}/asset_groups")]
	Task<ResourceList<int>> ListAssetGroupsAsync(int id, CancellationToken cancellationToken);

	/// <summary>Replaces the asset groups the user can access (<c>PUT api/3/users/{id}/asset_groups</c>).</summary>
	/// <param name="id">The identifier of the user.</param>
	/// <param name="assetGroupIds">The identifiers of every asset group the user should access.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Related links.</returns>
	[Put("api/3/users/{id}/asset_groups")]
	Task<LinksResource> SetAssetGroupsAsync(int id, [Body] IEnumerable<int> assetGroupIds, CancellationToken cancellationToken);

	/// <summary>Revokes the user's access to every asset group (<c>DELETE api/3/users/{id}/asset_groups</c>).</summary>
	/// <param name="id">The identifier of the user.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Related links.</returns>
	[Delete("api/3/users/{id}/asset_groups")]
	Task<LinksResource> RevokeAllAssetGroupsAsync(int id, CancellationToken cancellationToken);

	/// <summary>Grants the user access to one asset group (<c>PUT api/3/users/{id}/asset_groups/{assetGroupId}</c>).</summary>
	/// <param name="id">The identifier of the user.</param>
	/// <param name="assetGroupId">The identifier of the asset group.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Related links.</returns>
	[Put("api/3/users/{id}/asset_groups/{assetGroupId}")]
	Task<LinksResource> GrantAssetGroupAsync(int id, int assetGroupId, CancellationToken cancellationToken);

	/// <summary>Revokes the user's access to one asset group (<c>DELETE api/3/users/{id}/asset_groups/{assetGroupId}</c>).</summary>
	/// <param name="id">The identifier of the user.</param>
	/// <param name="assetGroupId">The identifier of the asset group.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Related links.</returns>
	[Delete("api/3/users/{id}/asset_groups/{assetGroupId}")]
	Task<LinksResource> RevokeAssetGroupAsync(int id, int assetGroupId, CancellationToken cancellationToken);
}
