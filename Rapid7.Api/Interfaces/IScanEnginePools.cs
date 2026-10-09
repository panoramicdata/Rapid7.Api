using Rapid7.Api.Models;
using Rapid7.Api.Models.ScanEngines;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>
/// Engine pools: groups of scan engines that share the scanning of their sites, and their membership
/// (<c>api/3/scan_engine_pools</c>). Managing pools needs the Manage Scan Engines privilege.
/// </summary>
public interface IScanEnginePools
{
	/// <summary>Lists the engine pools available for scanning (<c>GET api/3/scan_engine_pools</c>).</summary>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Every engine pool, in one unpaged list.</returns>
	[Get("api/3/scan_engine_pools")]
	Task<ResourceList<EnginePool>> ListAsync(CancellationToken cancellationToken);

	/// <summary>Creates an engine pool (<c>POST api/3/scan_engine_pools</c>).</summary>
	/// <param name="request">The pool's name, engines and sites.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The new pool's identifier.</returns>
	[Post("api/3/scan_engine_pools")]
	Task<CreatedReference<int>> CreateAsync([Body] EnginePoolRequest request, CancellationToken cancellationToken);

	/// <summary>Gets an engine pool (<c>GET api/3/scan_engine_pools/{id}</c>).</summary>
	/// <param name="poolId">The engine pool identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The engine pool.</returns>
	[Get("api/3/scan_engine_pools/{poolId}")]
	Task<EnginePool> GetAsync(int poolId, CancellationToken cancellationToken);

	/// <summary>Replaces an engine pool's settings (<c>PUT api/3/scan_engine_pools/{id}</c>).</summary>
	/// <param name="poolId">The engine pool identifier.</param>
	/// <param name="request">The pool's new settings.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the pool.</returns>
	[Put("api/3/scan_engine_pools/{poolId}")]
	Task<Links> UpdateAsync(int poolId, [Body] EnginePoolRequest request, CancellationToken cancellationToken);

	/// <summary>Deletes an engine pool; its engines stay paired (<c>DELETE api/3/scan_engine_pools/{id}</c>).</summary>
	/// <param name="poolId">The engine pool identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to related resources.</returns>
	[Delete("api/3/scan_engine_pools/{poolId}")]
	Task<Links> DeleteAsync(int poolId, CancellationToken cancellationToken);

	/// <summary>Lists the identifiers of the scan engines in a pool (<c>GET api/3/scan_engine_pools/{id}/engines</c>).</summary>
	/// <param name="poolId">The engine pool identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The scan engine identifiers.</returns>
	[Get("api/3/scan_engine_pools/{poolId}/engines")]
	Task<ResourceList<int>> ListEnginesAsync(int poolId, CancellationToken cancellationToken);

	/// <summary>
	/// Replaces the scan engines in a pool with the ones given (<c>PUT api/3/scan_engine_pools/{id}/engines</c>).
	/// </summary>
	/// <param name="poolId">The engine pool identifier.</param>
	/// <param name="engineIds">The identifiers of every engine the pool should contain.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the pool's engines.</returns>
	[Put("api/3/scan_engine_pools/{poolId}/engines")]
	Task<Links> SetEnginesAsync(int poolId, [Body] IEnumerable<int> engineIds, CancellationToken cancellationToken);

	/// <summary>Adds a scan engine to a pool (<c>PUT api/3/scan_engine_pools/{id}/engines/{engineId}</c>).</summary>
	/// <param name="poolId">The engine pool identifier.</param>
	/// <param name="engineId">The scan engine identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the pool and engine.</returns>
	[Put("api/3/scan_engine_pools/{poolId}/engines/{engineId}")]
	Task<Links> AddEngineAsync(int poolId, int engineId, CancellationToken cancellationToken);

	/// <summary>Removes a scan engine from a pool (<c>DELETE api/3/scan_engine_pools/{id}/engines/{engineId}</c>).</summary>
	/// <param name="poolId">The engine pool identifier.</param>
	/// <param name="engineId">The scan engine identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the pool.</returns>
	[Delete("api/3/scan_engine_pools/{poolId}/engines/{engineId}")]
	Task<Links> RemoveEngineAsync(int poolId, int engineId, CancellationToken cancellationToken);

	/// <summary>Lists the identifiers of the sites that scan with a pool (<c>GET api/3/scan_engine_pools/{id}/sites</c>).</summary>
	/// <param name="poolId">The engine pool identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The site identifiers.</returns>
	[Get("api/3/scan_engine_pools/{poolId}/sites")]
	Task<ResourceList<int>> ListSitesAsync(int poolId, CancellationToken cancellationToken);
}
