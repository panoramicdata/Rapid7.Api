using Rapid7.Api.Models;
using Rapid7.Api.Models.Users;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>The sources that authenticate user accounts: the console, LDAP, Kerberos, SAML (<c>api/3/authentication_sources</c>).</summary>
public interface IAuthenticationSources
{
	/// <summary>Lists every authentication source (<c>GET api/3/authentication_sources</c>).</summary>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The sources.</returns>
	[Get("api/3/authentication_sources")]
	Task<ResourceList<AuthenticationSource>> ListAsync(CancellationToken cancellationToken);

	/// <summary>Gets an authentication source (<c>GET api/3/authentication_sources/{id}</c>).</summary>
	/// <param name="id">The identifier of the source.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The source.</returns>
	[Get("api/3/authentication_sources/{id}")]
	Task<AuthenticationSource> GetAsync(int id, CancellationToken cancellationToken);

	/// <summary>Lists the users that sign in through a source (<c>GET api/3/authentication_sources/{id}/users</c>).</summary>
	/// <param name="id">The identifier of the source.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The user identifiers.</returns>
	[Get("api/3/authentication_sources/{id}/users")]
	Task<ResourceList<int>> ListUsersAsync(int id, CancellationToken cancellationToken);
}
