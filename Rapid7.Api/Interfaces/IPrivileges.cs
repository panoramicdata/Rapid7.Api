using Rapid7.Api.Models;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>
/// The privileges a role can grant (<c>api/3/privileges</c>). A privilege identifier is a lowercase, hyphenated name such
/// as <c>all-permissions</c>, <c>manage-sites</c> or <c>view-site-asset-data</c>.
/// </summary>
public interface IPrivileges
{
	/// <summary>Lists every privilege a role can grant (<c>GET api/3/privileges</c>).</summary>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The privilege identifiers.</returns>
	[Get("api/3/privileges")]
	Task<ResourceList<string>> ListAsync(CancellationToken cancellationToken);

	/// <summary>Gets a privilege (<c>GET api/3/privileges/{id}</c>); the console answers with links only.</summary>
	/// <param name="id">The privilege identifier, such as <c>manage-sites</c>.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the privilege and related resources.</returns>
	[Get("api/3/privileges/{id}")]
	Task<LinksResource> GetAsync(string id, CancellationToken cancellationToken);

	/// <summary>Lists the users whose role grants a privilege (<c>GET api/3/privileges/{id}/users</c>).</summary>
	/// <param name="id">The privilege identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The user identifiers.</returns>
	[Get("api/3/privileges/{id}/users")]
	Task<ResourceList<int>> ListUsersAsync(string id, CancellationToken cancellationToken);
}
