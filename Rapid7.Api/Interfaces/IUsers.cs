using Rapid7.Api.Models;
using Rapid7.Api.Models.Users;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>
/// Security Console user accounts (<c>api/3/users</c>): create, read, update and delete accounts, change passwords,
/// unlock accounts and list the privileges a user holds. Site and asset group access is in <see cref="IUserAccess"/>;
/// two-factor keys are in <see cref="IUserTwoFactor"/>.
/// </summary>
public interface IUsers
{
	/// <summary>Lists one page of user accounts (<c>GET api/3/users</c>). Requires Global Administrator.</summary>
	/// <param name="paging">Paging and sorting, or <see langword="null"/> for the first page of 10 sorted by <c>id</c>.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>One page of users.</returns>
	[Get("api/3/users")]
	Task<Page<User>> ListAsync([Query] PageOptions? paging, CancellationToken cancellationToken);

	/// <summary>Creates a user account (<c>POST api/3/users</c>). Requires Global Administrator.</summary>
	/// <param name="request">The account, including its initial password (a secret, sent only in the body).</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The identifier of the new user.</returns>
	[Post("api/3/users")]
	Task<CreatedReference<int>> CreateAsync([Body] UserCreateRequest request, CancellationToken cancellationToken);

	/// <summary>Gets a user account (<c>GET api/3/users/{id}</c>). Requires Global Administrator, or being that user.</summary>
	/// <param name="id">The identifier of the user.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The user.</returns>
	[Get("api/3/users/{id}")]
	Task<User> GetAsync(int id, CancellationToken cancellationToken);

	/// <summary>Replaces the details of a user account (<c>PUT api/3/users/{id}</c>). Requires Global Administrator.</summary>
	/// <param name="id">The identifier of the user.</param>
	/// <param name="request">The new details; a password only to change it.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the user.</returns>
	[Put("api/3/users/{id}")]
	Task<LinksResource> UpdateAsync(int id, [Body] UserUpdateRequest request, CancellationToken cancellationToken);

	/// <summary>Deletes a user account (<c>DELETE api/3/users/{id}</c>). Requires Global Administrator.</summary>
	/// <param name="id">The identifier of the user.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Related links.</returns>
	[Delete("api/3/users/{id}")]
	Task<LinksResource> DeleteAsync(int id, CancellationToken cancellationToken);

	/// <summary>
	/// Unlocks an account locked after too many failed sign-in attempts (<c>DELETE api/3/users/{id}/lock</c>). A disabled
	/// account cannot be unlocked. Requires Global Administrator.
	/// </summary>
	/// <param name="id">The identifier of the user.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Related links.</returns>
	[Delete("api/3/users/{id}/lock")]
	Task<LinksResource> UnlockAsync(int id, CancellationToken cancellationToken);

	/// <summary>
	/// Changes a user's password (<c>PUT api/3/users/{id}/password</c>). Users can change only their own password.
	/// </summary>
	/// <param name="id">The identifier of the user.</param>
	/// <param name="request">The new password (a secret, sent only in the body).</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Related links.</returns>
	[Put("api/3/users/{id}/password")]
	Task<LinksResource> ResetPasswordAsync(int id, [Body] PasswordChange request, CancellationToken cancellationToken);

	/// <summary>
	/// Lists the privileges a user's role grants them (<c>GET api/3/users/{id}/privileges</c>), such as
	/// <c>manage-sites</c>. Requires Global Administrator.
	/// </summary>
	/// <param name="id">The identifier of the user.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The privilege identifiers.</returns>
	[Get("api/3/users/{id}/privileges")]
	Task<ResourceList<string>> ListPrivilegesAsync(int id, CancellationToken cancellationToken);
}
