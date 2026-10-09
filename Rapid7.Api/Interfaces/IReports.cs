using Rapid7.Api.Models;
using Rapid7.Api.Models.Reports;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>
/// Report configurations and their generation (<c>api/3/reports</c>). Templates and formats are in
/// <see cref="IReportTemplates"/>, generated instances and their files in <see cref="IReportInstances"/>.
/// </summary>
public interface IReports
{
	/// <summary>Lists the report configurations the caller can see (<c>GET api/3/reports</c>).</summary>
	/// <param name="paging">Paging and sorting; <see langword="null"/> for the defaults.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>One page of reports.</returns>
	[Get("api/3/reports")]
	Task<Page<Report>> GetReportsAsync([Query] PageOptions? paging, CancellationToken cancellationToken);

	/// <summary>Creates a report configuration (<c>POST api/3/reports</c>). It is not generated until asked or scheduled.</summary>
	/// <param name="report">The settings of the report.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The identifier of the new report.</returns>
	[Post("api/3/reports")]
	Task<CreatedReference<int>> CreateAsync([Body] ReportConfiguration report, CancellationToken cancellationToken);

	/// <summary>Gets one report configuration (<c>GET api/3/reports/{id}</c>).</summary>
	/// <param name="id">The identifier of the report.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The report.</returns>
	[Get("api/3/reports/{id}")]
	Task<Report> GetAsync(int id, CancellationToken cancellationToken);

	/// <summary>Replaces the settings of a report configuration (<c>PUT api/3/reports/{id}</c>).</summary>
	/// <param name="id">The identifier of the report.</param>
	/// <param name="report">The new settings; a <see cref="Report"/> read earlier can be sent back (its identifier and links are not sent).</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to related resources.</returns>
	[Put("api/3/reports/{id}")]
	Task<LinksResource> UpdateAsync(int id, [Body] ReportConfiguration report, CancellationToken cancellationToken);

	/// <summary>Deletes a report configuration and its generated instances (<c>DELETE api/3/reports/{id}</c>).</summary>
	/// <param name="id">The identifier of the report.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to related resources.</returns>
	[Delete("api/3/reports/{id}")]
	Task<LinksResource> DeleteAsync(int id, CancellationToken cancellationToken);

	/// <summary>
	/// Starts generating a report (<c>POST api/3/reports/{id}/generate</c>). Generation runs in the background, and the
	/// console emails the result to the recipients the report names.
	/// </summary>
	/// <param name="id">The identifier of the report.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The identifier of the new report instance; follow it with <see cref="IReportInstances"/>.</returns>
	[Post("api/3/reports/{id}/generate")]
	Task<CreatedReference<int>> GenerateAsync(int id, CancellationToken cancellationToken);
}
