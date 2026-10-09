using Rapid7.Api.Models;
using Rapid7.Api.Models.Users;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>
/// The non-administrator users with access to a site (<c>api/3/sites/{id}/users</c>). Changing the list needs the Manage
/// Site Access privilege; administrators always have access and are not listed.
/// </summary>
public interface ISiteUsers
{
	/// <summary>Lists the non-administrator users with access to the site (<c>GET api/3/sites/{id}/users</c>).</summary>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The users with access.</returns>
	[Get("api/3/sites/{siteId}/users")]
	Task<ResourceList<User>> ListAsync(int siteId, CancellationToken cancellationToken);

	/// <summary>
	/// Replaces the site's access list (<c>PUT api/3/sites/{id}/users</c>; the body is a bare JSON array of user
	/// identifiers).
	/// </summary>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="userIds">The identifiers of every user who should have access.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the affected resources.</returns>
	[Put("api/3/sites/{siteId}/users")]
	Task<LinksResource> ReplaceAllAsync(int siteId, [Body] IEnumerable<int> userIds, CancellationToken cancellationToken);

	/// <summary>Grants a user access to the site (<c>POST api/3/sites/{id}/users</c>; the body is the bare user identifier).</summary>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="userId">The identifier of the user.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>A reference to the user.</returns>
	[Post("api/3/sites/{siteId}/users")]
	Task<CreatedReference<int>> AddAsync(int siteId, [Body] int userId, CancellationToken cancellationToken);

	/// <summary>Revokes a user's access to the site (<c>DELETE api/3/sites/{id}/users/{userId}</c>).</summary>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="userId">The identifier of the user.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the affected resources.</returns>
	[Delete("api/3/sites/{siteId}/users/{userId}")]
	Task<LinksResource> RemoveAsync(int siteId, int userId, CancellationToken cancellationToken);
}
