using Rapid7.Api.Models;
using Rapid7.Api.Models.ScanEngines;
using Rapid7.Api.Models.Scans;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>
/// Scan engines paired with the Security Console, and the pools, scans and sites of each (<c>api/3/scan_engines</c>).
/// Managing engines needs the Manage Scan Engines privilege (a Global Administrator has it).
/// </summary>
public interface IScanEngines
{
	/// <summary>Lists the scan engines available for scanning (<c>GET api/3/scan_engines</c>).</summary>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Every scan engine, in one unpaged list.</returns>
	[Get("api/3/scan_engines")]
	Task<ResourceList<ScanEngine>> ListAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Registers a scan engine with the console (<c>POST api/3/scan_engines</c>). The engine must be reachable at the
	/// address and port given, and must trust the console before it can scan.
	/// </summary>
	/// <param name="request">The engine's name, address, port and sites.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The new engine's identifier.</returns>
	[Post("api/3/scan_engines")]
	Task<CreatedReference<int>> CreateAsync([Body] ScanEngineRequest request, CancellationToken cancellationToken);

	/// <summary>Gets a scan engine (<c>GET api/3/scan_engines/{id}</c>).</summary>
	/// <param name="engineId">The scan engine identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The scan engine.</returns>
	[Get("api/3/scan_engines/{engineId}")]
	Task<ScanEngine> GetAsync(int engineId, CancellationToken cancellationToken);

	/// <summary>Replaces a scan engine's settings (<c>PUT api/3/scan_engines/{id}</c>).</summary>
	/// <param name="engineId">The scan engine identifier.</param>
	/// <param name="request">The engine's new settings.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the engine.</returns>
	[Put("api/3/scan_engines/{engineId}")]
	Task<Links> UpdateAsync(int engineId, [Body] ScanEngineRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Removes a scan engine from the console (<c>DELETE api/3/scan_engines/{id}</c>). Sites that scan with it must be
	/// moved to another engine first.
	/// </summary>
	/// <param name="engineId">The scan engine identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to related resources.</returns>
	[Delete("api/3/scan_engines/{engineId}")]
	Task<Links> DeleteAsync(int engineId, CancellationToken cancellationToken);

	/// <summary>Lists the engine pools a scan engine belongs to (<c>GET api/3/scan_engines/{id}/scan_engine_pools</c>).</summary>
	/// <param name="engineId">The scan engine identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The engine pools, in one unpaged list.</returns>
	[Get("api/3/scan_engines/{engineId}/scan_engine_pools")]
	Task<ResourceList<EnginePool>> ListPoolsAsync(int engineId, CancellationToken cancellationToken);

	/// <summary>Lists, a page at a time, the scans a scan engine has run (<c>GET api/3/scan_engines/{id}/scans</c>).</summary>
	/// <param name="engineId">The scan engine identifier.</param>
	/// <param name="paging">Paging and sorting, or <see langword="null"/> for the first page sorted by <c>id</c>.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>One page of scans.</returns>
	[Get("api/3/scan_engines/{engineId}/scans")]
	Task<Page<Scan>> ListScansAsync(int engineId, [Query] PageOptions? paging, CancellationToken cancellationToken);

	/// <summary>Lists, a page at a time, the sites assigned to a scan engine (<c>GET api/3/scan_engines/{id}/sites</c>).</summary>
	/// <param name="engineId">The scan engine identifier.</param>
	/// <param name="paging">Paging and sorting, or <see langword="null"/> for the first page.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>One page of sites.</returns>
	[Get("api/3/scan_engines/{engineId}/sites")]
	Task<Page<ScanEngineSite>> ListSitesAsync(int engineId, [Query] PageOptions? paging, CancellationToken cancellationToken);
}
