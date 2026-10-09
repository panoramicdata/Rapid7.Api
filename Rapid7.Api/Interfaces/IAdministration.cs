using Rapid7.Api.Models;
using Rapid7.Api.Models.Administration;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>
/// Security Console administration (<c>api/3/administration</c>): host and version details, licence, logs, properties,
/// settings and console commands. Most operations require Global Administrator. Running commands and changing the
/// licence affect the whole console.
/// </summary>
public interface IAdministration
{
	/// <summary>
	/// Runs a console command, as typed in the Security Console's command window, and returns its output
	/// (<c>POST api/3/administration/commands</c>). The command is sent as <c>text/plain</c>. Commands can restart, update
	/// or reconfigure the console: use with care. Requires Global Administrator.
	/// </summary>
	/// <param name="command">The command, such as <c>ver</c> or <c>show host</c>.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The command's output.</returns>
	[Post("api/3/administration/commands")]
	Task<ConsoleCommandOutput> ExecuteCommandAsync([Body] string command, CancellationToken cancellationToken);

	/// <summary>Gets details of the console host and installation (<c>GET api/3/administration/info</c>).</summary>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The host, hardware, JVM and version details.</returns>
	[Get("api/3/administration/info")]
	Task<ConsoleInfo> GetInfoAsync(CancellationToken cancellationToken);

	/// <summary>Gets the licence's status, features and limits (<c>GET api/3/administration/license</c>). Requires Global Administrator.</summary>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The licence.</returns>
	[Get("api/3/administration/license")]
	Task<License> GetLicenseAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Licenses the console with an activation key (<c>POST api/3/administration/license?key=...</c>). Replaces the current
	/// licence. The key travels in the query string; the client never logs query strings. Requires Global Administrator.
	/// </summary>
	/// <param name="key">The licence activation key.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Related links.</returns>
	[Post("api/3/administration/license")]
	Task<Links> ActivateLicenseAsync([Query][AliasAs("key")] string key, CancellationToken cancellationToken);

	/// <summary>
	/// Licenses the console with a licence (<c>.lic</c>) file, sent as the <c>license</c> part of a
	/// <c>multipart/form-data</c> body (<c>POST api/3/administration/license</c>). Replaces the current licence. Requires
	/// Global Administrator.
	/// </summary>
	/// <param name="license">The licence file, for example <c>new StreamPart(stream, "console.lic", "application/octet-stream")</c>.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Related links.</returns>
	[Multipart]
	[Post("api/3/administration/license")]
	Task<Links> UploadLicenseAsync([AliasAs("license")] StreamPart license, CancellationToken cancellationToken);

	/// <summary>
	/// Downloads console logs as one zip archive (<c>GET api/3/administration/logs</c>). The caller disposes the returned
	/// content. Logs can contain host names, user names and IP addresses. Requires Global Administrator.
	/// </summary>
	/// <param name="names">The logs to include, sent as repeated <c>name</c> parameters.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The <c>application/zip</c> content; read it with <c>ReadAsStreamAsync</c> and dispose it.</returns>
	[Get("api/3/administration/logs")]
	[Headers("Accept: application/zip")]
	Task<HttpContent> GetLogsAsync([Query(CollectionFormat.Multi)][AliasAs("name")] IEnumerable<ConsoleLog> names, CancellationToken cancellationToken);

	/// <summary>Gets the console's Java system and environment properties (<c>GET api/3/administration/properties</c>).</summary>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The properties.</returns>
	[Get("api/3/administration/properties")]
	Task<EnvironmentProperties> GetPropertiesAsync(CancellationToken cancellationToken);

	/// <summary>Gets the console's administration settings (<c>GET api/3/administration/settings</c>). Requires Global Administrator.</summary>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The settings.</returns>
	[Get("api/3/administration/settings")]
	Task<ConsoleSettings> GetSettingsAsync(CancellationToken cancellationToken);
}
