using Rapid7.Api.Models;
using Rapid7.Api.Models.Assets;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>The assets of a site (<c>api/3/sites/{id}/assets</c>).</summary>
public interface ISiteAssets
{
	/// <summary>Lists one page of the site's assets (<c>GET api/3/sites/{id}/assets</c>).</summary>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="paging">Paging and sorting, or <see langword="null"/> for the first page of 10.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>One page of assets.</returns>
	[Get("api/3/sites/{siteId}/assets")]
	Task<Page<Asset>> ListAsync(int siteId, [Query] PageOptions? paging, CancellationToken cancellationToken);

	/// <summary>
	/// Removes every asset from the site (<c>DELETE api/3/sites/{id}/assets</c>). An asset is deleted from the console
	/// altogether when asset linking is off, or when it belonged to this site only. Needs the Purge Site Asset Data
	/// privilege.
	/// </summary>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the affected resources.</returns>
	[Delete("api/3/sites/{siteId}/assets")]
	Task<LinksResource> RemoveAllAsync(int siteId, CancellationToken cancellationToken);

	/// <summary>
	/// Removes one asset from the site (<c>DELETE api/3/sites/{id}/assets/{assetId}</c>); the asset is deleted only when it
	/// belongs to no other site. Needs the Purge Site Asset Data privilege.
	/// </summary>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="assetId">The identifier of the asset.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the affected resources.</returns>
	[Delete("api/3/sites/{siteId}/assets/{assetId}")]
	Task<LinksResource> RemoveAsync(int siteId, long assetId, CancellationToken cancellationToken);
}
