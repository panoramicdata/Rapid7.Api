using Rapid7.Api.Models;
using Rapid7.Api.Models.AssetGroups;
using Rapid7.Api.Models.Assets;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>Static and dynamic asset groups and their search criteria (<c>api/3/asset_groups</c>).</summary>
public interface IAssetGroups
{
	/// <summary>Lists one page of the asset groups the caller can access (<c>GET api/3/asset_groups</c>).</summary>
	/// <param name="options">Name and type filters, paging and sorting, or <see langword="null"/> for the first page of 10.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The page of asset groups.</returns>
	[Get("api/3/asset_groups")]
	Task<Page<AssetGroup>> ListAsync([Query] AssetGroupListOptions? options, CancellationToken cancellationToken);

	/// <summary>
	/// Creates an asset group (<c>POST api/3/asset_groups</c>). Search criteria may be given for either type:
	/// a dynamic group refreshes its membership from them as assets are scanned, while a static group does not change.
	/// </summary>
	/// <param name="request">The name, type, description and search criteria.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The identifier of the new group.</returns>
	[Post("api/3/asset_groups")]
	Task<CreatedReference<int>> CreateAsync([Body] AssetGroupRequest request, CancellationToken cancellationToken);

	/// <summary>Reads an asset group (<c>GET api/3/asset_groups/{id}</c>).</summary>
	/// <param name="assetGroupId">The identifier of the group.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The group.</returns>
	[Get("api/3/asset_groups/{assetGroupId}")]
	Task<AssetGroup> GetAsync(int assetGroupId, CancellationToken cancellationToken);

	/// <summary>Replaces the details of an asset group (<c>PUT api/3/asset_groups/{id}</c>).</summary>
	/// <param name="assetGroupId">The identifier of the group.</param>
	/// <param name="request">The new name, type, description and search criteria.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to related resources.</returns>
	[Put("api/3/asset_groups/{assetGroupId}")]
	Task<LinksResource> UpdateAsync(int assetGroupId, [Body] AssetGroupRequest request, CancellationToken cancellationToken);

	/// <summary>Deletes an asset group; its assets are not deleted (<c>DELETE api/3/asset_groups/{id}</c>).</summary>
	/// <param name="assetGroupId">The identifier of the group.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to related resources.</returns>
	[Delete("api/3/asset_groups/{assetGroupId}")]
	Task<LinksResource> DeleteAsync(int assetGroupId, CancellationToken cancellationToken);

	/// <summary>Reads the search criteria of an asset group (<c>GET api/3/asset_groups/{id}/search_criteria</c>).</summary>
	/// <param name="assetGroupId">The identifier of the group.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The search criteria.</returns>
	[Get("api/3/asset_groups/{assetGroupId}/search_criteria")]
	Task<SearchCriteria> GetSearchCriteriaAsync(int assetGroupId, CancellationToken cancellationToken);

	/// <summary>
	/// Replaces the search criteria of an asset group (<c>PUT api/3/asset_groups/{id}/search_criteria</c>). For a dynamic
	/// group this changes its membership.
	/// </summary>
	/// <param name="assetGroupId">The identifier of the group.</param>
	/// <param name="criteria">The new search criteria.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to related resources.</returns>
	[Put("api/3/asset_groups/{assetGroupId}/search_criteria")]
	Task<LinksResource> SetSearchCriteriaAsync(int assetGroupId, [Body] SearchCriteria criteria, CancellationToken cancellationToken);
}
