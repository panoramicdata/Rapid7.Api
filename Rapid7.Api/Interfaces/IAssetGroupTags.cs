using Rapid7.Api.Models;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>The tags applied to an asset group (<c>api/3/asset_groups/{id}/tags</c>).</summary>
public interface IAssetGroupTags
{
	/// <summary>Lists the identifiers of the tags applied to an asset group (<c>GET api/3/asset_groups/{id}/tags</c>).</summary>
	/// <param name="assetGroupId">The identifier of the group.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The tag identifiers.</returns>
	[Get("api/3/asset_groups/{assetGroupId}/tags")]
	Task<ResourceList<int>> ListAsync(int assetGroupId, CancellationToken cancellationToken);

	/// <summary>Replaces the tags applied to an asset group (<c>PUT api/3/asset_groups/{id}/tags</c>).</summary>
	/// <param name="assetGroupId">The identifier of the group.</param>
	/// <param name="tagIds">The identifiers of every tag the group is to have.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to related resources.</returns>
	[Put("api/3/asset_groups/{assetGroupId}/tags")]
	Task<Links> SetAsync(int assetGroupId, [Body] IEnumerable<int> tagIds, CancellationToken cancellationToken);

	/// <summary>Removes every tag from an asset group (<c>DELETE api/3/asset_groups/{id}/tags</c>).</summary>
	/// <param name="assetGroupId">The identifier of the group.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to related resources.</returns>
	[Delete("api/3/asset_groups/{assetGroupId}/tags")]
	Task<Links> RemoveAllAsync(int assetGroupId, CancellationToken cancellationToken);

	/// <summary>Applies a tag to an asset group (<c>PUT api/3/asset_groups/{id}/tags/{tagId}</c>).</summary>
	/// <param name="assetGroupId">The identifier of the group.</param>
	/// <param name="tagId">The identifier of the tag.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to related resources.</returns>
	[Put("api/3/asset_groups/{assetGroupId}/tags/{tagId}")]
	Task<Links> AddAsync(int assetGroupId, int tagId, CancellationToken cancellationToken);

	/// <summary>Removes a tag from an asset group (<c>DELETE api/3/asset_groups/{id}/tags/{tagId}</c>).</summary>
	/// <param name="assetGroupId">The identifier of the group.</param>
	/// <param name="tagId">The identifier of the tag.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to related resources.</returns>
	[Delete("api/3/asset_groups/{assetGroupId}/tags/{tagId}")]
	Task<Links> RemoveAsync(int assetGroupId, int tagId, CancellationToken cancellationToken);
}
