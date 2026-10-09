using Rapid7.Api.Models;
using Rapid7.Api.Models.ScanEngines;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>The scan engine a site scans with by default (<c>api/3/sites/{id}/scan_engine</c>).</summary>
public interface ISiteScanEngine
{
	/// <summary>Reads the scan engine a site scans with (<c>GET api/3/sites/{id}/scan_engine</c>).</summary>
	/// <param name="siteId">The site identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The scan engine.</returns>
	[Get("api/3/sites/{siteId}/scan_engine")]
	Task<ScanEngine> GetAsync(int siteId, CancellationToken cancellationToken);

	/// <summary>
	/// Sets the scan engine or engine pool a site scans with (<c>PUT api/3/sites/{id}/scan_engine</c>); the body is the
	/// bare identifier. Requires the Manage Sites privilege.
	/// </summary>
	/// <param name="siteId">The site identifier.</param>
	/// <param name="scanEngineId">The scan engine or engine pool identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the site's scan engine.</returns>
	[Put("api/3/sites/{siteId}/scan_engine")]
	Task<LinksResource> SetAsync(int siteId, [Body] int scanEngineId, CancellationToken cancellationToken);
}
