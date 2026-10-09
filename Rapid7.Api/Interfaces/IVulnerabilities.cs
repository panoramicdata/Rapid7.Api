using Rapid7.Api.Models;
using Rapid7.Api.Models.Vulnerabilities;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>
/// The vulnerability catalogue (<c>api/3/vulnerabilities</c>): every vulnerability the Security Console can detect, with
/// its exploits, malware kits, references, solutions and affected assets. Any authenticated user can read the catalogue;
/// the affected assets are limited to those the user can see.
/// </summary>
public interface IVulnerabilities
{
	/// <summary>Lists one page of the vulnerability catalogue (<c>GET api/3/vulnerabilities</c>).</summary>
	/// <param name="paging">Paging and sorting, or <see langword="null"/> for the first page of 10.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>One page of vulnerabilities.</returns>
	[Get("api/3/vulnerabilities")]
	Task<Page<Vulnerability>> ListAsync([Query] PageOptions? paging, CancellationToken cancellationToken);

	/// <summary>Gets one vulnerability (<c>GET api/3/vulnerabilities/{id}</c>).</summary>
	/// <param name="id">The vulnerability identifier, such as <c>windows-hotfix-ms03-007</c>.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The vulnerability.</returns>
	[Get("api/3/vulnerabilities/{id}")]
	Task<Vulnerability> GetAsync(string id, CancellationToken cancellationToken);

	/// <summary>Lists the identifiers of the assets the vulnerability was found on (<c>GET api/3/vulnerabilities/{id}/assets</c>).</summary>
	/// <param name="id">The vulnerability identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The asset identifiers, unpaged.</returns>
	[Get("api/3/vulnerabilities/{id}/assets")]
	Task<ResourceList<long>> ListAffectedAssetsAsync(string id, CancellationToken cancellationToken);

	/// <summary>Lists one page of the known exploits for a vulnerability (<c>GET api/3/vulnerabilities/{id}/exploits</c>).</summary>
	/// <param name="id">The vulnerability identifier.</param>
	/// <param name="paging">Paging and sorting, or <see langword="null"/> for the first page of 10.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>One page of exploits.</returns>
	[Get("api/3/vulnerabilities/{id}/exploits")]
	Task<Page<Exploit>> ListExploitsAsync(string id, [Query] PageOptions? paging, CancellationToken cancellationToken);

	/// <summary>Lists one page of the malware kits known to exploit a vulnerability (<c>GET api/3/vulnerabilities/{id}/malware_kits</c>).</summary>
	/// <param name="id">The vulnerability identifier.</param>
	/// <param name="paging">Paging and sorting, or <see langword="null"/> for the first page of 10.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>One page of malware kits.</returns>
	[Get("api/3/vulnerabilities/{id}/malware_kits")]
	Task<Page<MalwareKit>> ListMalwareKitsAsync(string id, [Query] PageOptions? paging, CancellationToken cancellationToken);

	/// <summary>Lists one page of the external references for a vulnerability (<c>GET api/3/vulnerabilities/{id}/references</c>).</summary>
	/// <param name="id">The vulnerability identifier.</param>
	/// <param name="paging">Paging and sorting, or <see langword="null"/> for the first page of 10.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>One page of references.</returns>
	[Get("api/3/vulnerabilities/{id}/references")]
	Task<Page<VulnerabilityReference>> ListReferencesAsync(string id, [Query] PageOptions? paging, CancellationToken cancellationToken);

	/// <summary>
	/// Lists the identifiers of the solutions that remediate a vulnerability (<c>GET api/3/vulnerabilities/{id}/solutions</c>);
	/// read each one with <see cref="ISolutions.GetAsync"/>.
	/// </summary>
	/// <param name="id">The vulnerability identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The solution identifiers, unpaged.</returns>
	[Get("api/3/vulnerabilities/{id}/solutions")]
	Task<ResourceList<string>> ListSolutionsAsync(string id, CancellationToken cancellationToken);
}
