using Rapid7.Api.Models;
using Rapid7.Api.Models.Cloud;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>Scan engines registered with the Insight platform (<c>v4/integration/scan/engine</c>).</summary>
public interface ICloudScanEngines
{
	/// <summary>
	/// Lists one page of scan engines (<c>GET v4/integration/scan/engine</c>). The list pages by number only and does not
	/// document sorting.
	/// </summary>
	/// <param name="paging">The page number and size, or <see langword="null"/> for the first page.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>One page of scan engines.</returns>
	[Get("v4/integration/scan/engine")]
	Task<CursorPage<CloudScanEngine>> ListAsync([Query] PageOptions? paging, CancellationToken cancellationToken);

	/// <summary>Gets one scan engine, with its custom configuration (<c>GET v4/integration/scan/engine/{id}</c>).</summary>
	/// <param name="id">The scan engine identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The scan engine.</returns>
	[Get("v4/integration/scan/engine/{id}")]
	Task<CloudScanEngine> GetAsync(string id, CancellationToken cancellationToken);

	/// <summary>
	/// Sets custom properties on a scan engine (<c>POST v4/integration/scan/engine/{id}/configuration</c>). The engine may
	/// need a restart before they take effect; Rapid7 advises contacting its support before changing custom properties. A
	/// read-only client refuses it.
	/// </summary>
	/// <param name="id">The scan engine identifier.</param>
	/// <param name="update">The properties to set.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>A confirmation message.</returns>
	[Post("v4/integration/scan/engine/{id}/configuration")]
	Task<CloudMessage> UpdateConfigurationAsync(string id, [Body] CloudScanEngineConfigurationUpdate update, CancellationToken cancellationToken);

	/// <summary>
	/// Removes custom properties from a scan engine, where set (<c>DELETE v4/integration/scan/engine/{id}/configuration</c>,
	/// with a JSON body naming the properties). The engine may need a restart before the change takes effect. A read-only
	/// client refuses it.
	/// </summary>
	/// <param name="id">The scan engine identifier.</param>
	/// <param name="removal">The names of the properties to remove.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>A confirmation message.</returns>
	[Delete("v4/integration/scan/engine/{id}/configuration")]
	Task<CloudMessage> RemoveConfigurationAsync(string id, [Body] CloudScanEngineConfigurationRemoval removal, CancellationToken cancellationToken);
}
