using Rapid7.Api.Models.Cloud;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>The health of the Cloud Integrations API (<c>admin/health</c>).</summary>
public interface ICloudHealth
{
	/// <summary>
	/// Reports whether the Cloud Integrations API is up, and the health of its components when the service reports them
	/// (<c>GET admin/health</c>).
	/// </summary>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The health of the service.</returns>
	[Get("admin/health")]
	Task<CloudHealth> GetAsync(CancellationToken cancellationToken);
}
