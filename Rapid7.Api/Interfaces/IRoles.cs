using Rapid7.Api.Models;
using Rapid7.Api.Models.Users;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>Roles users can be assigned, and the privileges each grants (<c>api/3/roles</c>).</summary>
public interface IRoles
{
	/// <summary>Lists every role (<c>GET api/3/roles</c>).</summary>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The roles.</returns>
	[Get("api/3/roles")]
	Task<ResourceList<Role>> ListAsync(CancellationToken cancellationToken);

	/// <summary>Gets a role (<c>GET api/3/roles/{id}</c>).</summary>
	/// <param name="id">The identifier of the role, such as <c>global-admin</c>.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The role.</returns>
	[Get("api/3/roles/{id}")]
	Task<Role> GetAsync(string id, CancellationToken cancellationToken);

	/// <summary>Updates a role's name, description and privileges (<c>PUT api/3/roles/{id}</c>).</summary>
	/// <param name="id">The identifier of the role.</param>
	/// <param name="request">The new details.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the role.</returns>
	[Put("api/3/roles/{id}")]
	Task<LinksResource> UpdateAsync(string id, [Body] RoleRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a role (<c>DELETE api/3/roles/{id}</c>). Built-in roles, and roles assigned to any user, cannot be deleted.
	/// </summary>
	/// <param name="id">The identifier of the role.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Related links.</returns>
	[Delete("api/3/roles/{id}")]
	Task<LinksResource> DeleteAsync(string id, CancellationToken cancellationToken);

	/// <summary>Lists the users assigned a role (<c>GET api/3/roles/{id}/users</c>).</summary>
	/// <param name="id">The identifier of the role.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The user identifiers.</returns>
	[Get("api/3/roles/{id}/users")]
	Task<ResourceList<int>> ListUsersAsync(string id, CancellationToken cancellationToken);
}
