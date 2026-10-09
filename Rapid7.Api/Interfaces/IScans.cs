using Rapid7.Api.Models;
using Rapid7.Api.Models.Scans;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>
/// Scans: listing them across the console or per site, reading one, starting a site scan, and pausing, resuming or
/// stopping one (<c>api/3/scans</c> and <c>api/3/sites/{id}/scans</c>).
/// </summary>
public interface IScans
{
	/// <summary>Lists, a page at a time, the scans of every site (<c>GET api/3/scans</c>).</summary>
	/// <param name="options">
	/// Paging, sorting and whether to list running or past scans, or <see langword="null"/> for the first page of past
	/// scans sorted by <c>id</c>.
	/// </param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>One page of scans.</returns>
	[Get("api/3/scans")]
	Task<Page<GlobalScan>> ListAsync([Query] ScanListOptions? options, CancellationToken cancellationToken);

	/// <summary>Gets a scan (<c>GET api/3/scans/{id}</c>).</summary>
	/// <param name="scanId">The scan identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The scan.</returns>
	[Get("api/3/scans/{scanId}")]
	Task<Scan> GetAsync(long scanId, CancellationToken cancellationToken);

	/// <summary>
	/// Pauses, resumes or stops a scan (<c>POST api/3/scans/{id}/{status}</c>). Only a running scan can be paused, only a
	/// paused scan resumed, and only a running or paused scan stopped; anything else is rejected with 400.
	/// </summary>
	/// <param name="scanId">The scan identifier.</param>
	/// <param name="status">The change to make.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the scan.</returns>
	[Post("api/3/scans/{scanId}/{status}")]
	Task<Links> SetStatusAsync(long scanId, ScanStatusChange status, CancellationToken cancellationToken);

	/// <summary>Lists, a page at a time, the scans of one site (<c>GET api/3/sites/{id}/scans</c>).</summary>
	/// <param name="siteId">The site identifier.</param>
	/// <param name="options">
	/// Paging, sorting and whether to list running or past scans, or <see langword="null"/> for the first page of past
	/// scans sorted by <c>id</c>.
	/// </param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>One page of scans.</returns>
	[Get("api/3/sites/{siteId}/scans")]
	Task<Page<Scan>> ListForSiteAsync(int siteId, [Query] ScanListOptions? options, CancellationToken cancellationToken);

	/// <summary>
	/// Starts a scan of a site now (<c>POST api/3/sites/{id}/scans</c>), optionally narrowed to some hosts or asset
	/// groups, or run with another engine or template. Needs permission to start scans on the site.
	/// </summary>
	/// <param name="siteId">The site identifier.</param>
	/// <param name="request">Overrides for this scan; an empty request scans the whole site with its own settings.</param>
	/// <param name="overrideBlackout">
	/// <see langword="true"/> to ask to scan during a blackout window (<c>overrideBlackout</c>), which needs the
	/// privilege to override blackouts; <see langword="null"/> for the console's default, <see langword="false"/>.
	/// </param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The new scan's identifier.</returns>
	[Post("api/3/sites/{siteId}/scans")]
	Task<CreatedReference<long>> StartForSiteAsync(
		int siteId,
		[Body] AdhocScanRequest request,
		[AliasAs("overrideBlackout")] bool? overrideBlackout,
		CancellationToken cancellationToken);
}
