using Rapid7.Api.Models;
using Rapid7.Api.Models.Vulnerabilities;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>
/// Vulnerability solutions (<c>api/3/solutions</c>): how to remediate vulnerabilities, and how solutions depend on and
/// supersede each other.
/// </summary>
public interface ISolutions
{
	/// <summary>Lists one page of solutions (<c>GET api/3/solutions</c>).</summary>
	/// <param name="paging">Paging and sorting, or <see langword="null"/> for the first page of 10.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>One page of solutions.</returns>
	[Get("api/3/solutions")]
	Task<Page<Solution>> ListAsync([Query] PageOptions? paging, CancellationToken cancellationToken);

	/// <summary>Gets one solution (<c>GET api/3/solutions/{id}</c>).</summary>
	/// <param name="id">The solution identifier, such as <c>ubuntu-upgrade-libexpat1</c>.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The solution.</returns>
	[Get("api/3/solutions/{id}")]
	Task<Solution> GetAsync(string id, CancellationToken cancellationToken);

	/// <summary>Lists the identifiers of the solutions that must be applied before this one (<c>GET api/3/solutions/{id}/prerequisites</c>).</summary>
	/// <param name="id">The solution identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The prerequisite solution identifiers, unpaged.</returns>
	[Get("api/3/solutions/{id}/prerequisites")]
	Task<ResourceList<string>> ListPrerequisitesAsync(string id, CancellationToken cancellationToken);

	/// <summary>Lists the solutions this solution supersedes (<c>GET api/3/solutions/{id}/supersedes</c>).</summary>
	/// <param name="id">The solution identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The superseded solutions, unpaged.</returns>
	[Get("api/3/solutions/{id}/supersedes")]
	Task<ResourceList<Solution>> ListSupersededAsync(string id, CancellationToken cancellationToken);

	/// <summary>Lists the solutions that supersede this solution (<c>GET api/3/solutions/{id}/superseding</c>).</summary>
	/// <param name="id">The solution identifier.</param>
	/// <param name="rollup">
	/// <see langword="true"/> for only the highest-level ("rollup") superseding solutions, <see langword="false"/> for all,
	/// or <see langword="null"/> for the console's default (rollup only).
	/// </param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The superseding solutions, unpaged.</returns>
	[Get("api/3/solutions/{id}/superseding")]
	Task<ResourceList<Solution>> ListSupersedingAsync(string id, [Query] bool? rollup, CancellationToken cancellationToken);
}
