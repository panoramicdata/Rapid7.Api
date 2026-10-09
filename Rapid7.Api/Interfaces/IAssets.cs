using Rapid7.Api.Models;
using Rapid7.Api.Models.Assets;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>Assets: listing, searching, reading, importing and deleting them (<c>api/3/assets</c>).</summary>
public interface IAssets
{
	/// <summary>Lists one page of the assets the caller can access (<c>GET api/3/assets</c>).</summary>
	/// <param name="paging">Paging and sorting, or <see langword="null"/> for the first page of 10.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The page of assets.</returns>
	[Get("api/3/assets")]
	Task<Page<Asset>> ListAsync([Query] PageOptions? paging, CancellationToken cancellationToken);

	/// <summary>
	/// Lists one page of the assets the caller can access that match search criteria (<c>POST api/3/assets/search</c>).
	/// The request is a POST but changes nothing, so a read-only client allows it.
	/// </summary>
	/// <param name="criteria">The filters, and whether an asset must match all of them or any one.</param>
	/// <param name="paging">Paging and sorting, or <see langword="null"/> for the first page of 10.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The page of matching assets.</returns>
	[Post("api/3/assets/search")]
	Task<Page<Asset>> SearchAsync([Body] SearchCriteria criteria, [Query] PageOptions? paging, CancellationToken cancellationToken);

	/// <summary>Reads an asset with everything discovered on it (<c>GET api/3/assets/{id}</c>).</summary>
	/// <param name="assetId">The identifier of the asset.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The asset.</returns>
	[Get("api/3/assets/{assetId}")]
	Task<Asset> GetAsync(long assetId, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes an asset (<c>DELETE api/3/assets/{id}</c>).
	/// </summary>
	/// <param name="assetId">The identifier of the asset.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to related resources.</returns>
	[Delete("api/3/assets/{assetId}")]
	Task<Links> DeleteAsync(long assetId, CancellationToken cancellationToken);

	/// <summary>
	/// Imports an asset into a site from an external source (<c>POST api/3/sites/{id}/assets</c>): the console creates the
	/// asset, or merges the details into the asset it matches them to.
	/// </summary>
	/// <param name="siteId">The identifier of the site to add the asset to.</param>
	/// <param name="request">The details of the asset; <see cref="AssetCreateRequest.Date"/> is required.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The identifier of the created or updated asset.</returns>
	[Post("api/3/sites/{siteId}/assets")]
	Task<CreatedReference<long>> CreateAsync(int siteId, [Body] AssetCreateRequest request, CancellationToken cancellationToken);
}
