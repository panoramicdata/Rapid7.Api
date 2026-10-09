using Rapid7.Api.Models;
using Rapid7.Api.Models.Sites;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>
/// Where a dynamic site's assets come from: its discovery connection (<c>api/3/sites/{id}/discovery_connection</c>) and
/// the search that filters that connection's assets (<c>api/3/sites/{id}/discovery_search_criteria</c>). Changing them
/// needs the Manage Sites privilege.
/// </summary>
public interface ISiteDiscovery
{
	/// <summary>Gets the discovery connection the site is assigned to (<c>GET api/3/sites/{id}/discovery_connection</c>).</summary>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The discovery connection.</returns>
	[Get("api/3/sites/{siteId}/discovery_connection")]
	Task<SiteDiscoveryConnection> GetConnectionAsync(int siteId, CancellationToken cancellationToken);

	/// <summary>
	/// Assigns the site to a discovery connection (<c>PUT api/3/sites/{id}/discovery_connection</c>; the body is the bare
	/// connection identifier).
	/// </summary>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="discoveryConnectionId">The identifier of the discovery connection.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the affected resources.</returns>
	[Put("api/3/sites/{siteId}/discovery_connection")]
	Task<LinksResource> SetConnectionAsync(int siteId, [Body] long discoveryConnectionId, CancellationToken cancellationToken);

	/// <summary>Gets the dynamic site's discovery search (<c>GET api/3/sites/{id}/discovery_search_criteria</c>).</summary>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The search criteria.</returns>
	[Get("api/3/sites/{siteId}/discovery_search_criteria")]
	Task<DiscoverySearchCriteria> GetSearchCriteriaAsync(int siteId, CancellationToken cancellationToken);

	/// <summary>Replaces the dynamic site's discovery search (<c>PUT api/3/sites/{id}/discovery_search_criteria</c>).</summary>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="criteria">The new search criteria.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the affected resources.</returns>
	[Put("api/3/sites/{siteId}/discovery_search_criteria")]
	Task<LinksResource> SetSearchCriteriaAsync(int siteId, [Body] DiscoverySearchCriteria criteria, CancellationToken cancellationToken);
}
