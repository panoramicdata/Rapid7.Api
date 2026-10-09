using Rapid7.Api.Models;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>The assets in an asset group and the users who can access it (<c>api/3/asset_groups/{id}/assets</c>, <c>.../users</c>).</summary>
public interface IAssetGroupMembers
{
	/// <summary>Lists the identifiers of the assets in an asset group (<c>GET api/3/asset_groups/{id}/assets</c>).</summary>
	/// <param name="assetGroupId">The identifier of the group.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The asset identifiers.</returns>
	[Get("api/3/asset_groups/{assetGroupId}/assets")]
	Task<ResourceList<long>> ListAssetsAsync(int assetGroupId, CancellationToken cancellationToken);

	/// <summary>
	/// Replaces the assets in a static asset group (<c>PUT api/3/asset_groups/{id}/assets</c>).
	/// </summary>
	/// <param name="assetGroupId">The identifier of the group.</param>
	/// <param name="assetIds">The identifiers of every asset the group is to contain.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to related resources.</returns>
	[Put("api/3/asset_groups/{assetGroupId}/assets")]
	Task<Links> SetAssetsAsync(int assetGroupId, [Body] IEnumerable<long> assetIds, CancellationToken cancellationToken);

	/// <summary>Removes every asset from a static asset group (<c>DELETE api/3/asset_groups/{id}/assets</c>).</summary>
	/// <param name="assetGroupId">The identifier of the group.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to related resources.</returns>
	[Delete("api/3/asset_groups/{assetGroupId}/assets")]
	Task<Links> RemoveAllAssetsAsync(int assetGroupId, CancellationToken cancellationToken);

	/// <summary>Adds an asset to a static asset group (<c>PUT api/3/asset_groups/{id}/assets/{assetId}</c>).</summary>
	/// <param name="assetGroupId">The identifier of the group.</param>
	/// <param name="assetId">The identifier of the asset.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to related resources.</returns>
	[Put("api/3/asset_groups/{assetGroupId}/assets/{assetId}")]
	Task<Links> AddAssetAsync(int assetGroupId, long assetId, CancellationToken cancellationToken);

	/// <summary>Removes an asset from a static asset group (<c>DELETE api/3/asset_groups/{id}/assets/{assetId}</c>).</summary>
	/// <param name="assetGroupId">The identifier of the group.</param>
	/// <param name="assetId">The identifier of the asset.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to related resources.</returns>
	[Delete("api/3/asset_groups/{assetGroupId}/assets/{assetId}")]
	Task<Links> RemoveAssetAsync(int assetGroupId, long assetId, CancellationToken cancellationToken);

	/// <summary>Lists the identifiers of the users who can access an asset group (<c>GET api/3/asset_groups/{id}/users</c>).</summary>
	/// <param name="assetGroupId">The identifier of the group.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The user identifiers.</returns>
	[Get("api/3/asset_groups/{assetGroupId}/users")]
	Task<ResourceList<int>> ListUsersAsync(int assetGroupId, CancellationToken cancellationToken);

	/// <summary>
	/// Replaces the users who can access an asset group (<c>PUT api/3/asset_groups/{id}/users</c>). Only users with
	/// sufficient privileges can be granted access.
	/// </summary>
	/// <param name="assetGroupId">The identifier of the group.</param>
	/// <param name="userIds">The identifiers of every user who is to have access.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to related resources.</returns>
	[Put("api/3/asset_groups/{assetGroupId}/users")]
	Task<Links> SetUsersAsync(int assetGroupId, [Body] IEnumerable<int> userIds, CancellationToken cancellationToken);

	/// <summary>Grants a user access to an asset group (<c>PUT api/3/asset_groups/{id}/users/{userId}</c>).</summary>
	/// <param name="assetGroupId">The identifier of the group.</param>
	/// <param name="userId">The identifier of the user.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to related resources.</returns>
	[Put("api/3/asset_groups/{assetGroupId}/users/{userId}")]
	Task<Links> AddUserAsync(int assetGroupId, int userId, CancellationToken cancellationToken);

	/// <summary>Revokes a user's access to an asset group (<c>DELETE api/3/asset_groups/{id}/users/{userId}</c>).</summary>
	/// <param name="assetGroupId">The identifier of the group.</param>
	/// <param name="userId">The identifier of the user.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to related resources.</returns>
	[Delete("api/3/asset_groups/{assetGroupId}/users/{userId}")]
	Task<Links> RemoveUserAsync(int assetGroupId, int userId, CancellationToken cancellationToken);
}
