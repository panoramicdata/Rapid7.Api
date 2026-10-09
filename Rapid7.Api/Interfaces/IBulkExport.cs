using Rapid7.Api.Models.BulkExport;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>
/// The Bulk Export GraphQL operations (<c>export/graphql</c>): each method POSTs one fixed GraphQL document. A response
/// with GraphQL errors raises <see cref="Rapid7GraphQLException"/>. Every operation only reads InsightVM data, so all are
/// allowed on a read-only client. The API key needs Platform Administrator permissions. For create-and-wait, downloads and
/// typed records, use the helpers on <see cref="Rapid7BulkExportClient"/>.
/// </summary>
public interface IBulkExport
{
	/// <summary>
	/// Starts an asset software export, producing the <c>asset_software</c> dataset (<c>POST export/graphql</c>, mutation
	/// <c>createAssetSoftwareExport</c>; early access).
	/// </summary>
	/// <param name="request">The request; <c>new CreateAssetSoftwareExportRequest()</c>.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The identifier of the new export.</returns>
	[Post("export/graphql")]
	Task<GraphQLResponse<CreateAssetSoftwareExportData>> CreateAssetSoftwareExportAsync(
		[Body] CreateAssetSoftwareExportRequest request,
		CancellationToken cancellationToken);

	/// <summary>
	/// Starts a policy export, producing the <c>asset</c>, <c>asset_policy</c> and <c>asset_scan_policy</c> datasets
	/// (<c>POST export/graphql</c>, mutation <c>createPolicyExport</c>).
	/// </summary>
	/// <param name="request">The request; <c>new CreatePolicyExportRequest()</c>.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The identifier of the new export.</returns>
	[Post("export/graphql")]
	Task<GraphQLResponse<CreatePolicyExportData>> CreatePolicyExportAsync(
		[Body] CreatePolicyExportRequest request,
		CancellationToken cancellationToken);

	/// <summary>
	/// Starts a vulnerability export, producing the <c>asset</c>, <c>asset_vulnerability</c> and
	/// <c>vulnerability_exception</c> datasets (<c>POST export/graphql</c>, mutation <c>createVulnerabilityExport</c>).
	/// </summary>
	/// <param name="request">The request; <c>new CreateVulnerabilityExportRequest()</c>.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The identifier of the new export.</returns>
	[Post("export/graphql")]
	Task<GraphQLResponse<CreateVulnerabilityExportData>> CreateVulnerabilityExportAsync(
		[Body] CreateVulnerabilityExportRequest request,
		CancellationToken cancellationToken);

	/// <summary>
	/// Starts a vulnerability remediation export for a date range, producing the <c>vulnerability_remediation</c> dataset
	/// (<c>POST export/graphql</c>, mutation <c>createVulnerabilityRemediationExport</c>).
	/// </summary>
	/// <param name="request">The request, carrying the date range.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The identifier of the new export.</returns>
	[Post("export/graphql")]
	Task<GraphQLResponse<CreateVulnerabilityRemediationExportData>> CreateVulnerabilityRemediationExportAsync(
		[Body] CreateVulnerabilityRemediationExportRequest request,
		CancellationToken cancellationToken);

	/// <summary>
	/// Reads an export's status and, once it has succeeded, its file URLs (<c>POST export/graphql</c>, query
	/// <c>export</c>). Each query returns fresh URLs, valid for 15 minutes.
	/// </summary>
	/// <param name="request">The request, carrying the export identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The export, or no export when the identifier is unknown.</returns>
	[Post("export/graphql")]
	Task<GraphQLResponse<GetExportData>> GetExportAsync([Body] GetExportRequest request, CancellationToken cancellationToken);
}
