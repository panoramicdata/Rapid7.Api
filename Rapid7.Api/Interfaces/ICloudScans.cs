using Rapid7.Api.Models.Cloud;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>Scans run through the Insight platform (<c>v4/integration/scan</c>).</summary>
public interface ICloudScans
{
	/// <summary>Lists one page of scans (<c>GET v4/integration/scan</c>).</summary>
	/// <param name="options">The page number and size, and whether to include details, or <see langword="null"/>.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>One page of scans.</returns>
	[Get("v4/integration/scan")]
	Task<CursorPage<CloudScan>> ListAsync([Query] CloudScanListOptions? options, CancellationToken cancellationToken);

	/// <summary>Gets one scan (<c>GET v4/integration/scan/{id}</c>).</summary>
	/// <param name="id">The scan identifier.</param>
	/// <param name="includeDetails">Whether to include additional details about the scan (<c>includeDetails</c>).</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The scan.</returns>
	[Get("v4/integration/scan/{id}")]
	Task<CloudScan> GetAsync(string id, [AliasAs("includeDetails")] bool includeDetails, CancellationToken cancellationToken);

	/// <summary>
	/// Starts a scan of the given assets (<c>POST v4/integration/scan</c>). This scans real machines; a read-only client
	/// refuses it.
	/// </summary>
	/// <param name="request">What to scan, with which engines, and when.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The scans started, and the assets that could not be scanned with the reason for each.</returns>
	[Post("v4/integration/scan")]
	Task<CloudScanDispatch> StartAsync([Body] CloudScanRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Stops a running scan (<c>POST v4/integration/scan/{id}/stop</c>). The API accepts the request (202) and stops the
	/// scan asynchronously; a read-only client refuses it.
	/// </summary>
	/// <param name="id">The scan identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>A task that completes when the request has been accepted.</returns>
	[Post("v4/integration/scan/{id}/stop")]
	Task StopAsync(string id, CancellationToken cancellationToken);
}
