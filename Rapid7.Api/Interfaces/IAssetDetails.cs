using Rapid7.Api.Models;
using Rapid7.Api.Models.Assets;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>
/// What was discovered on an asset (databases, files, software, users and groups) and the tags applied to it
/// (<c>api/3/assets/{id}/...</c>).
/// </summary>
public interface IAssetDetails
{
	/// <summary>Lists the databases enumerated on an asset (<c>GET api/3/assets/{id}/databases</c>).</summary>
	/// <param name="assetId">The identifier of the asset.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The databases.</returns>
	[Get("api/3/assets/{assetId}/databases")]
	Task<ResourceList<Database>> ListDatabasesAsync(long assetId, CancellationToken cancellationToken);

	/// <summary>Lists the files and directories discovered on an asset (<c>GET api/3/assets/{id}/files</c>).</summary>
	/// <param name="assetId">The identifier of the asset.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The files and directories.</returns>
	[Get("api/3/assets/{assetId}/files")]
	Task<ResourceList<AssetFile>> ListFilesAsync(long assetId, CancellationToken cancellationToken);

	/// <summary>Lists the software fingerprinted on an asset (<c>GET api/3/assets/{id}/software</c>).</summary>
	/// <param name="assetId">The identifier of the asset.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The software.</returns>
	[Get("api/3/assets/{assetId}/software")]
	Task<ResourceList<Software>> ListSoftwareAsync(long assetId, CancellationToken cancellationToken);

	/// <summary>Lists the group accounts enumerated on an asset (<c>GET api/3/assets/{id}/user_groups</c>).</summary>
	/// <param name="assetId">The identifier of the asset.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The group accounts.</returns>
	[Get("api/3/assets/{assetId}/user_groups")]
	Task<ResourceList<GroupAccount>> ListUserGroupsAsync(long assetId, CancellationToken cancellationToken);

	/// <summary>Lists the user accounts enumerated on an asset (<c>GET api/3/assets/{id}/users</c>).</summary>
	/// <param name="assetId">The identifier of the asset.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The user accounts.</returns>
	[Get("api/3/assets/{assetId}/users")]
	Task<ResourceList<UserAccount>> ListUsersAsync(long assetId, CancellationToken cancellationToken);

	/// <summary>
	/// Lists the tags applied to an asset, directly or through its sites, asset groups or tag criteria
	/// (<c>GET api/3/assets/{id}/tags</c>).
	/// </summary>
	/// <param name="assetId">The identifier of the asset.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The tags, each with how it came to be applied.</returns>
	[Get("api/3/assets/{assetId}/tags")]
	Task<ResourceList<AssetTag>> ListTagsAsync(long assetId, CancellationToken cancellationToken);

	/// <summary>Applies a tag to an asset directly (<c>PUT api/3/assets/{id}/tags/{tagId}</c>).</summary>
	/// <param name="assetId">The identifier of the asset.</param>
	/// <param name="tagId">The identifier of the tag.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to related resources.</returns>
	[Put("api/3/assets/{assetId}/tags/{tagId}")]
	Task<LinksResource> AddTagAsync(long assetId, int tagId, CancellationToken cancellationToken);

	/// <summary>
	/// Removes a tag applied directly to an asset (<c>DELETE api/3/assets/{id}/tags/{tagId}</c>). A tag applied through a
	/// site, asset group or criteria stays.
	/// </summary>
	/// <param name="assetId">The identifier of the asset.</param>
	/// <param name="tagId">The identifier of the tag.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to related resources.</returns>
	[Delete("api/3/assets/{assetId}/tags/{tagId}")]
	Task<LinksResource> RemoveTagAsync(long assetId, int tagId, CancellationToken cancellationToken);
}
