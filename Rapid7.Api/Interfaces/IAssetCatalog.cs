using Rapid7.Api.Models;
using Rapid7.Api.Models.Assets;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>
/// The catalogues of operating systems and software fingerprinted across all assets (<c>api/3/operating_systems</c>,
/// <c>api/3/software</c>).
/// </summary>
public interface IAssetCatalog
{
	/// <summary>Lists one page of the operating systems fingerprinted on any asset (<c>GET api/3/operating_systems</c>).</summary>
	/// <param name="paging">Paging and sorting, or <see langword="null"/> for the first page of 10.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The page of operating systems.</returns>
	[Get("api/3/operating_systems")]
	Task<Page<OperatingSystemFingerprint>> ListOperatingSystemsAsync([Query] PageOptions? paging, CancellationToken cancellationToken);

	/// <summary>Reads an operating system from the catalogue (<c>GET api/3/operating_systems/{id}</c>).</summary>
	/// <param name="operatingSystemId">The identifier of the operating system.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The operating system.</returns>
	[Get("api/3/operating_systems/{operatingSystemId}")]
	Task<OperatingSystemFingerprint> GetOperatingSystemAsync(long operatingSystemId, CancellationToken cancellationToken);

	/// <summary>Lists one page of the software fingerprinted on any asset (<c>GET api/3/software</c>).</summary>
	/// <param name="paging">Paging and sorting, or <see langword="null"/> for the first page of 10.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The page of software.</returns>
	[Get("api/3/software")]
	Task<Page<Software>> ListSoftwareAsync([Query] PageOptions? paging, CancellationToken cancellationToken);

	/// <summary>Reads a piece of software from the catalogue (<c>GET api/3/software/{id}</c>).</summary>
	/// <param name="softwareId">The identifier of the software.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The software.</returns>
	[Get("api/3/software/{softwareId}")]
	Task<Software> GetSoftwareAsync(long softwareId, CancellationToken cancellationToken);
}
