using Rapid7.Api.Models;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>
/// The shared secret a scan engine uses to pair with the console when the engine initiates the connection
/// (<c>api/3/scan_engines/shared_secret</c>). Needs the Manage Scan Engines privilege.
/// </summary>
public interface IScanEngineSharedSecret
{
	/// <summary>
	/// Gets the current shared secret (<c>GET api/3/scan_engines/shared_secret</c>). The console answers 404, raised as
	/// <see cref="Rapid7ApiException"/>, when no secret has been generated or the last one has expired.
	/// </summary>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The secret, as plain text.</returns>
	[Get("api/3/scan_engines/shared_secret")]
	[Headers("Accept: text/plain")]
	Task<string> GetAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Returns the current shared secret, generating a new one when none is valid
	/// (<c>POST api/3/scan_engines/shared_secret</c>). A secret stays valid for a limited time; see
	/// <see cref="GetTimeToLiveAsync"/>.
	/// </summary>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The secret, as plain text.</returns>
	[Post("api/3/scan_engines/shared_secret")]
	[Headers("Accept: text/plain")]
	Task<string> GetOrCreateAsync(CancellationToken cancellationToken);

	/// <summary>Revokes the current shared secret, if there is one (<c>DELETE api/3/scan_engines/shared_secret</c>).</summary>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to related resources.</returns>
	[Delete("api/3/scan_engines/shared_secret")]
	Task<Links> RevokeAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Gets how many seconds the current shared secret remains valid
	/// (<c>GET api/3/scan_engines/shared_secret/time_to_live</c>). The console answers 404, raised as
	/// <see cref="Rapid7ApiException"/>, when there is no valid secret.
	/// </summary>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The remaining lifetime, in seconds.</returns>
	[Get("api/3/scan_engines/shared_secret/time_to_live")]
	Task<long> GetTimeToLiveAsync(CancellationToken cancellationToken);
}
