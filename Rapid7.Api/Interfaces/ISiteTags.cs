using Rapid7.Api.Models;
using Rapid7.Api.Models.Tags;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>The tags applied to a site (<c>api/3/sites/{id}/tags</c>). Changing them needs the Manage Site Tags privilege.</summary>
public interface ISiteTags
{
	/// <summary>Lists the tags applied to the site (<c>GET api/3/sites/{id}/tags</c>).</summary>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The site's tags.</returns>
	[Get("api/3/sites/{siteId}/tags")]
	Task<ResourceList<Tag>> ListAsync(int siteId, CancellationToken cancellationToken);

	/// <summary>
	/// Replaces the tags applied to the site (<c>PUT api/3/sites/{id}/tags</c>; the body is a bare JSON array of tag
	/// identifiers).
	/// </summary>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="tagIds">The identifiers of every tag the site should have.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the affected resources.</returns>
	[Put("api/3/sites/{siteId}/tags")]
	Task<Links> ReplaceAllAsync(int siteId, [Body] IEnumerable<int> tagIds, CancellationToken cancellationToken);

	/// <summary>Applies a tag to the site (<c>PUT api/3/sites/{id}/tags/{tagId}</c>, with no body).</summary>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="tagId">The identifier of the tag.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the affected resources.</returns>
	[Put("api/3/sites/{siteId}/tags/{tagId}")]
	Task<Links> AddAsync(int siteId, int tagId, CancellationToken cancellationToken);

	/// <summary>Removes a tag from the site (<c>DELETE api/3/sites/{id}/tags/{tagId}</c>).</summary>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="tagId">The identifier of the tag.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the affected resources.</returns>
	[Delete("api/3/sites/{siteId}/tags/{tagId}")]
	Task<Links> RemoveAsync(int siteId, int tagId, CancellationToken cancellationToken);
}
