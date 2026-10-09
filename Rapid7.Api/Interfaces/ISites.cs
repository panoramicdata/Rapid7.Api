using Rapid7.Api.Models;
using Rapid7.Api.Models.Sites;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>Sites: listing, creating, reading, updating and deleting them (<c>api/3/sites</c>).</summary>
public interface ISites
{
	/// <summary>
	/// Lists one page of the sites the caller can access (<c>GET api/3/sites</c>). Read every page with
	/// <see cref="Rapid7Paging"/>.
	/// </summary>
	/// <param name="paging">The page, page size and sort order, or <see langword="null"/> for the first ten sites by identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>One page of sites.</returns>
	[Get("api/3/sites")]
	Task<Page<Site>> ListAsync([Query] PageOptions? paging, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a site (<c>POST api/3/sites</c>). Requires the Manage Sites privilege. The site is not scanned until a scan
	/// is started or scheduled.
	/// </summary>
	/// <param name="request">The new site.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The new site's identifier and a link to it.</returns>
	[Post("api/3/sites")]
	Task<CreatedReference<int>> CreateAsync([Body] SiteCreateRequest request, CancellationToken cancellationToken);

	/// <summary>Reads a site (<c>GET api/3/sites/{id}</c>).</summary>
	/// <param name="siteId">The site identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The site.</returns>
	[Get("api/3/sites/{siteId}")]
	Task<Site> GetAsync(int siteId, CancellationToken cancellationToken);

	/// <summary>
	/// Replaces a site's name, description, importance, scan engine and scan template (<c>PUT api/3/sites/{id}</c>).
	/// Requires the Manage Sites privilege.
	/// </summary>
	/// <param name="siteId">The site identifier.</param>
	/// <param name="request">The site's new settings.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the site.</returns>
	[Put("api/3/sites/{siteId}")]
	Task<Links> UpdateAsync(int siteId, [Body] SiteUpdateRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a site with its configuration and scan history (<c>DELETE api/3/sites/{id}</c>). Assets that belong to no
	/// other site are deleted too. Requires the Manage Sites privilege.
	/// </summary>
	/// <param name="siteId">The site identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to related resources.</returns>
	[Delete("api/3/sites/{siteId}")]
	Task<Links> DeleteAsync(int siteId, CancellationToken cancellationToken);
}
