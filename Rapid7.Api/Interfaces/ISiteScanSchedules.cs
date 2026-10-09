using Rapid7.Api.Models;
using Rapid7.Api.Models.Sites;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>
/// A site's scan schedules (<c>api/3/sites/{id}/scan_schedules</c>). Changing schedules requires the Manage Scan
/// Schedules privilege. An enabled schedule starts scans at its run times; create disabled schedules to configure
/// without scanning.
/// </summary>
public interface ISiteScanSchedules
{
	/// <summary>Lists a site's scan schedules (<c>GET api/3/sites/{id}/scan_schedules</c>).</summary>
	/// <param name="siteId">The site identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The scan schedules.</returns>
	[Get("api/3/sites/{siteId}/scan_schedules")]
	Task<ResourceList<ScanSchedule>> ListAsync(int siteId, CancellationToken cancellationToken);

	/// <summary>
	/// Replaces all of a site's scan schedules (<c>PUT api/3/sites/{id}/scan_schedules</c>): schedules left out are
	/// deleted.
	/// </summary>
	/// <param name="siteId">The site identifier.</param>
	/// <param name="schedules">The site's scan schedules.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the site's scan schedules.</returns>
	[Put("api/3/sites/{siteId}/scan_schedules")]
	Task<LinksResource> ReplaceAllAsync(int siteId, [Body] IEnumerable<ScanSchedule> schedules, CancellationToken cancellationToken);

	/// <summary>Adds a scan schedule to a site (<c>POST api/3/sites/{id}/scan_schedules</c>).</summary>
	/// <param name="siteId">The site identifier.</param>
	/// <param name="schedule">The new schedule.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The new schedule's identifier and a link to it.</returns>
	[Post("api/3/sites/{siteId}/scan_schedules")]
	Task<CreatedReference<int>> CreateAsync(int siteId, [Body] ScanSchedule schedule, CancellationToken cancellationToken);

	/// <summary>Deletes all of a site's scan schedules (<c>DELETE api/3/sites/{id}/scan_schedules</c>).</summary>
	/// <param name="siteId">The site identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the site.</returns>
	[Delete("api/3/sites/{siteId}/scan_schedules")]
	Task<LinksResource> DeleteAllAsync(int siteId, CancellationToken cancellationToken);

	/// <summary>Reads one of a site's scan schedules (<c>GET api/3/sites/{id}/scan_schedules/{scheduleId}</c>).</summary>
	/// <param name="siteId">The site identifier.</param>
	/// <param name="scheduleId">The scan schedule identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The scan schedule.</returns>
	[Get("api/3/sites/{siteId}/scan_schedules/{scheduleId}")]
	Task<ScanSchedule> GetAsync(int siteId, int scheduleId, CancellationToken cancellationToken);

	/// <summary>Replaces one of a site's scan schedules (<c>PUT api/3/sites/{id}/scan_schedules/{scheduleId}</c>).</summary>
	/// <param name="siteId">The site identifier.</param>
	/// <param name="scheduleId">The scan schedule identifier.</param>
	/// <param name="schedule">The schedule's new settings.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the scan schedule.</returns>
	[Put("api/3/sites/{siteId}/scan_schedules/{scheduleId}")]
	Task<LinksResource> UpdateAsync(int siteId, int scheduleId, [Body] ScanSchedule schedule, CancellationToken cancellationToken);

	/// <summary>Deletes one of a site's scan schedules (<c>DELETE api/3/sites/{id}/scan_schedules/{scheduleId}</c>).</summary>
	/// <param name="siteId">The site identifier.</param>
	/// <param name="scheduleId">The scan schedule identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the site's scan schedules.</returns>
	[Delete("api/3/sites/{siteId}/scan_schedules/{scheduleId}")]
	Task<LinksResource> DeleteAsync(int siteId, int scheduleId, CancellationToken cancellationToken);
}
