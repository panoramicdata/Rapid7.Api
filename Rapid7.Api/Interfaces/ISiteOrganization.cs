using Rapid7.Api.Models;
using Rapid7.Api.Models.Sites;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>The organization and contact details of a site (<c>api/3/sites/{id}/organization</c>).</summary>
public interface ISiteOrganization
{
	/// <summary>Reads a site's organization details (<c>GET api/3/sites/{id}/organization</c>).</summary>
	/// <param name="siteId">The site identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The organization details.</returns>
	[Get("api/3/sites/{siteId}/organization")]
	Task<SiteOrganization> GetAsync(int siteId, CancellationToken cancellationToken);

	/// <summary>
	/// Replaces a site's organization details (<c>PUT api/3/sites/{id}/organization</c>); details left unset are cleared.
	/// Requires the Manage Sites privilege.
	/// </summary>
	/// <param name="siteId">The site identifier.</param>
	/// <param name="organization">The new organization details.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the organization details.</returns>
	[Put("api/3/sites/{siteId}/organization")]
	Task<LinksResource> UpdateAsync(int siteId, [Body] SiteOrganization organization, CancellationToken cancellationToken);
}
