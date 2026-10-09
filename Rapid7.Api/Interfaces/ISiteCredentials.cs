using Rapid7.Api.Models;
using Rapid7.Api.Models.Sites;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>
/// The scan credentials of a site: its own site credentials (<c>api/3/sites/{id}/site_credentials</c>) and the shared
/// credentials assigned to it (<c>api/3/sites/{id}/shared_credentials</c>). Changing them needs the Manage Site
/// Credentials privilege.
/// </summary>
public interface ISiteCredentials
{
	/// <summary>Lists the site's own credentials (<c>GET api/3/sites/{id}/site_credentials</c>). Secrets are not returned.</summary>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Every site credential.</returns>
	[Get("api/3/sites/{siteId}/site_credentials")]
	Task<ResourceList<SiteCredential>> ListAsync(int siteId, CancellationToken cancellationToken);

	/// <summary>
	/// Replaces the site's credentials with <paramref name="credentials"/> (<c>PUT api/3/sites/{id}/site_credentials</c>):
	/// credentials not in the list are deleted.
	/// </summary>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="credentials">The complete set of site credentials.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the affected resources.</returns>
	[Put("api/3/sites/{siteId}/site_credentials")]
	Task<LinksResource> ReplaceAllAsync(int siteId, [Body] IEnumerable<SiteCredential> credentials, CancellationToken cancellationToken);

	/// <summary>Adds a credential to the site (<c>POST api/3/sites/{id}/site_credentials</c>).</summary>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="credential">The credential, without an identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The identifier of the new credential.</returns>
	[Post("api/3/sites/{siteId}/site_credentials")]
	Task<CreatedReference<int>> CreateAsync(int siteId, [Body] SiteCredential credential, CancellationToken cancellationToken);

	/// <summary>Deletes every credential of the site (<c>DELETE api/3/sites/{id}/site_credentials</c>).</summary>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the affected resources.</returns>
	[Delete("api/3/sites/{siteId}/site_credentials")]
	Task<LinksResource> DeleteAllAsync(int siteId, CancellationToken cancellationToken);

	/// <summary>Gets one of the site's credentials (<c>GET api/3/sites/{id}/site_credentials/{credentialId}</c>). Secrets are not returned.</summary>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="credentialId">The identifier of the site credential.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The credential.</returns>
	[Get("api/3/sites/{siteId}/site_credentials/{credentialId}")]
	Task<SiteCredential> GetAsync(int siteId, int credentialId, CancellationToken cancellationToken);

	/// <summary>Replaces one of the site's credentials (<c>PUT api/3/sites/{id}/site_credentials/{credentialId}</c>).</summary>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="credentialId">The identifier of the site credential.</param>
	/// <param name="credential">The new definition, secrets included.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the affected resources.</returns>
	[Put("api/3/sites/{siteId}/site_credentials/{credentialId}")]
	Task<LinksResource> UpdateAsync(int siteId, int credentialId, [Body] SiteCredential credential, CancellationToken cancellationToken);

	/// <summary>Deletes one of the site's credentials (<c>DELETE api/3/sites/{id}/site_credentials/{credentialId}</c>).</summary>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="credentialId">The identifier of the site credential.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the affected resources.</returns>
	[Delete("api/3/sites/{siteId}/site_credentials/{credentialId}")]
	Task<LinksResource> DeleteAsync(int siteId, int credentialId, CancellationToken cancellationToken);

	/// <summary>
	/// Turns one of the site's credentials on or off for its scans
	/// (<c>PUT api/3/sites/{id}/site_credentials/{credentialId}/enabled</c>; the body is a bare JSON boolean).
	/// </summary>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="credentialId">The identifier of the site credential.</param>
	/// <param name="enabled">Whether scans use the credential.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the affected resources.</returns>
	[Put("api/3/sites/{siteId}/site_credentials/{credentialId}/enabled")]
	Task<LinksResource> SetEnabledAsync(int siteId, int credentialId, [Body] bool enabled, CancellationToken cancellationToken);

	/// <summary>
	/// Lists the shared credentials assigned to the site, with whether its scans use each
	/// (<c>GET api/3/sites/{id}/shared_credentials</c>).
	/// </summary>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Every shared credential assigned to the site.</returns>
	[Get("api/3/sites/{siteId}/shared_credentials")]
	Task<ResourceList<SiteSharedCredential>> ListSharedAsync(int siteId, CancellationToken cancellationToken);

	/// <summary>
	/// Turns a shared credential on or off for the site's scans
	/// (<c>PUT api/3/sites/{id}/shared_credentials/{credentialId}/enabled</c>; the body is a bare JSON boolean).
	/// </summary>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="credentialId">The identifier of the shared credential.</param>
	/// <param name="enabled">Whether the site's scans use the credential.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the affected resources.</returns>
	[Put("api/3/sites/{siteId}/shared_credentials/{credentialId}/enabled")]
	Task<LinksResource> SetSharedEnabledAsync(int siteId, int credentialId, [Body] bool enabled, CancellationToken cancellationToken);
}
