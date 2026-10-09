using Rapid7.Api.Models;
using Rapid7.Api.Models.AssetDiscovery;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>Discovery connections to external asset sources (<c>api/3/discovery_connections</c>).</summary>
public interface IDiscoveryConnections
{
	/// <summary>Lists one page of the discovery connections (<c>GET api/3/discovery_connections</c>).</summary>
	/// <param name="paging">Paging and sorting, or <see langword="null"/> for the first page of 10.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The page of connections.</returns>
	[Get("api/3/discovery_connections")]
	Task<Page<DiscoveryConnection>> ListAsync([Query] PageOptions? paging, CancellationToken cancellationToken);

	/// <summary>Reads a discovery connection (<c>GET api/3/discovery_connections/{id}</c>).</summary>
	/// <param name="connectionId">The identifier of the connection.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The connection.</returns>
	[Get("api/3/discovery_connections/{connectionId}")]
	Task<DiscoveryConnection> GetAsync(long connectionId, CancellationToken cancellationToken);

	/// <summary>
	/// Reconnects a discovery connection that has lost contact with its source (<c>POST api/3/discovery_connections/{id}/connect</c>).
	/// The console answers with no body.
	/// </summary>
	/// <param name="connectionId">The identifier of the connection.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>A task that completes when the console has accepted the request.</returns>
	[Post("api/3/discovery_connections/{connectionId}/connect")]
	Task ReconnectAsync(long connectionId, CancellationToken cancellationToken);
}
