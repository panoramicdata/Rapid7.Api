using Rapid7.Api.Models;
using Rapid7.Api.Models.Reports;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>The generated instances (history) of a report, and their files (<c>api/3/reports/{id}/history</c>).</summary>
/// <remarks>An instance is named by its identifier, or <c>latest</c> for the most recent one.</remarks>
public interface IReportInstances
{
	/// <summary>Lists the generated instances of a report (<c>GET api/3/reports/{id}/history</c>).</summary>
	/// <param name="reportId">The identifier of the report.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Every instance of the report.</returns>
	[Get("api/3/reports/{reportId}/history")]
	Task<ResourceList<ReportInstance>> GetInstancesAsync(int reportId, CancellationToken cancellationToken);

	/// <summary>Gets one generated instance of a report (<c>GET api/3/reports/{id}/history/{instance}</c>).</summary>
	/// <param name="reportId">The identifier of the report.</param>
	/// <param name="instance">The identifier of the instance, or <c>latest</c>.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The instance.</returns>
	[Get("api/3/reports/{reportId}/history/{instance}")]
	Task<ReportInstance> GetInstanceAsync(int reportId, string instance, CancellationToken cancellationToken);

	/// <summary>Deletes one generated instance of a report (<c>DELETE api/3/reports/{id}/history/{instance}</c>).</summary>
	/// <param name="reportId">The identifier of the report.</param>
	/// <param name="instance">The identifier of the instance, or <c>latest</c>.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to related resources.</returns>
	[Delete("api/3/reports/{reportId}/history/{instance}")]
	Task<Links> DeleteInstanceAsync(int reportId, string instance, CancellationToken cancellationToken);

	/// <summary>
	/// Downloads the file a report instance produced (<c>GET api/3/reports/{id}/history/{instance}/output</c>): a PDF, HTML,
	/// CSV, XML or other document depending on the report's format, usually GZip-compressed.
	/// </summary>
	/// <param name="reportId">The identifier of the report.</param>
	/// <param name="instance">The identifier of the instance, or <c>latest</c>.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>
	/// The file as <see cref="HttpContent"/>, whose headers carry its media type and (usually) file name. The caller owns it
	/// and must dispose it.
	/// </returns>
	[Get("api/3/reports/{reportId}/history/{instance}/output")]
	[Headers("Accept: */*")]
	Task<HttpContent> DownloadAsync(int reportId, string instance, CancellationToken cancellationToken);
}
