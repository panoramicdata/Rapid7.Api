using Rapid7.Api.Models.BulkExport;
using System.Net;

namespace Rapid7.Api;

public sealed partial class Rapid7BulkExportClient
{
	/// <summary>The clock the wait helpers measure their timeout with; replaced in tests.</summary>
	internal TimeProvider TimeProvider { get; set; } = TimeProvider.System;

	/// <summary>How the wait helpers wait between status queries; replaced in tests.</summary>
	internal Func<TimeSpan, CancellationToken, Task> Delay { get; set; } = Task.Delay;

	/// <summary>
	/// Starts a policy export and waits for it to succeed. Its files hold the <c>asset</c>, <c>asset_policy</c> and
	/// <c>asset_scan_policy</c> datasets.
	/// </summary>
	/// <param name="options">How often to poll and how long to wait, or <see langword="null"/> for the defaults.</param>
	/// <param name="cancellationToken">A cancellation token, honoured while waiting.</param>
	/// <returns>The succeeded export, with fresh download URLs.</returns>
	/// <exception cref="Rapid7ExportFailedException">The export failed.</exception>
	/// <exception cref="TimeoutException">The export did not finish within <see cref="ExportWaitOptions.Timeout"/>.</exception>
	public async Task<Export> ExportPoliciesAsync(ExportWaitOptions? options, CancellationToken cancellationToken)
	{
		var response = await Exports.CreatePolicyExportAsync(new CreatePolicyExportRequest(), cancellationToken).ConfigureAwait(false);
		return await WaitForExportAsync(CreatedId(response.Data?.Export), options, cancellationToken).ConfigureAwait(false);
	}

	/// <summary>
	/// Starts a vulnerability export and waits for it to succeed. Its files hold the <c>asset</c>,
	/// <c>asset_vulnerability</c> and <c>vulnerability_exception</c> datasets.
	/// </summary>
	/// <param name="options">How often to poll and how long to wait, or <see langword="null"/> for the defaults.</param>
	/// <param name="cancellationToken">A cancellation token, honoured while waiting.</param>
	/// <returns>The succeeded export, with fresh download URLs.</returns>
	/// <exception cref="Rapid7ExportFailedException">The export failed.</exception>
	/// <exception cref="TimeoutException">The export did not finish within <see cref="ExportWaitOptions.Timeout"/>.</exception>
	public async Task<Export> ExportVulnerabilitiesAsync(ExportWaitOptions? options, CancellationToken cancellationToken)
	{
		var response = await Exports.CreateVulnerabilityExportAsync(new CreateVulnerabilityExportRequest(), cancellationToken).ConfigureAwait(false);
		return await WaitForExportAsync(CreatedId(response.Data?.Export), options, cancellationToken).ConfigureAwait(false);
	}

	/// <summary>
	/// Starts a vulnerability remediation export for a date range and waits for it to succeed. Its files hold the
	/// <c>vulnerability_remediation</c> dataset.
	/// </summary>
	/// <param name="startDate">The first day of the range (August 2025 or later).</param>
	/// <param name="endDate">The last day: after <paramref name="startDate"/>, and at most 31 days after it.</param>
	/// <param name="options">How often to poll and how long to wait, or <see langword="null"/> for the defaults.</param>
	/// <param name="cancellationToken">A cancellation token, honoured while waiting.</param>
	/// <returns>The succeeded export, with fresh download URLs.</returns>
	/// <exception cref="ArgumentOutOfRangeException">The date range is empty, reversed or longer than 31 days.</exception>
	/// <exception cref="Rapid7ExportFailedException">The export failed.</exception>
	/// <exception cref="TimeoutException">The export did not finish within <see cref="ExportWaitOptions.Timeout"/>.</exception>
	public async Task<Export> ExportVulnerabilityRemediationsAsync(
		DateOnly startDate,
		DateOnly endDate,
		ExportWaitOptions? options,
		CancellationToken cancellationToken)
	{
		var request = new CreateVulnerabilityRemediationExportRequest(startDate, endDate);
		var response = await Exports.CreateVulnerabilityRemediationExportAsync(request, cancellationToken).ConfigureAwait(false);
		return await WaitForExportAsync(CreatedId(response.Data?.Export), options, cancellationToken).ConfigureAwait(false);
	}

	/// <summary>
	/// Starts an asset software export (early access) and waits for it to succeed. Its files hold the
	/// <c>asset_software</c> dataset.
	/// </summary>
	/// <param name="options">How often to poll and how long to wait, or <see langword="null"/> for the defaults.</param>
	/// <param name="cancellationToken">A cancellation token, honoured while waiting.</param>
	/// <returns>The succeeded export, with fresh download URLs.</returns>
	/// <exception cref="Rapid7ExportFailedException">The export failed.</exception>
	/// <exception cref="TimeoutException">The export did not finish within <see cref="ExportWaitOptions.Timeout"/>.</exception>
	public async Task<Export> ExportAssetSoftwareAsync(ExportWaitOptions? options, CancellationToken cancellationToken)
	{
		var response = await Exports.CreateAssetSoftwareExportAsync(new CreateAssetSoftwareExportRequest(), cancellationToken).ConfigureAwait(false);
		return await WaitForExportAsync(CreatedId(response.Data?.Export), options, cancellationToken).ConfigureAwait(false);
	}

	/// <summary>Reads an export: its status and, once it has succeeded, fresh download URLs (valid for 15 minutes).</summary>
	/// <param name="exportId">The export identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The export.</returns>
	/// <exception cref="Rapid7GraphQLException">The API reported an error, or returned no export.</exception>
	public async Task<Export> GetExportAsync(string exportId, CancellationToken cancellationToken)
	{
		var response = await Exports.GetExportAsync(new GetExportRequest(exportId), cancellationToken).ConfigureAwait(false);
		return response.Data?.Export
			?? throw new Rapid7GraphQLException(HttpStatusCode.OK, $"The Bulk Export API returned no export with id {exportId}.", []);
	}

	/// <summary>
	/// Polls an export until it succeeds, then returns it with fresh download URLs. Queries the status at once, then every
	/// <see cref="ExportWaitOptions.PollInterval"/>.
	/// </summary>
	/// <param name="exportId">The export identifier.</param>
	/// <param name="options">How often to poll and how long to wait, or <see langword="null"/> for the defaults.</param>
	/// <param name="cancellationToken">A cancellation token, honoured while waiting.</param>
	/// <returns>The succeeded export.</returns>
	/// <exception cref="Rapid7ExportFailedException">The export failed.</exception>
	/// <exception cref="TimeoutException">The export did not finish within <see cref="ExportWaitOptions.Timeout"/>.</exception>
	public async Task<Export> WaitForExportAsync(string exportId, ExportWaitOptions? options, CancellationToken cancellationToken)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(exportId);
		options ??= new ExportWaitOptions();
		options.Validate();
		var started = TimeProvider.GetTimestamp();
		while (true)
		{
			var export = await GetExportAsync(exportId, cancellationToken).ConfigureAwait(false);
			switch (export.Status)
			{
				case ExportStatus.Succeeded:
					return export;
				case ExportStatus.Failed:
					throw new Rapid7ExportFailedException(export);
			}

			if (TimeProvider.GetElapsedTime(started) >= options.Timeout)
			{
				throw new TimeoutException($"Bulk export {exportId} did not finish within {options.Timeout}; it was last {export.Status}.");
			}

			await Delay(options.PollInterval, cancellationToken).ConfigureAwait(false);
		}
	}

	private static string CreatedId(ExportReference? created)
		=> created?.Id is { Length: > 0 } id
			? id
			: throw new Rapid7GraphQLException(HttpStatusCode.OK, "The Bulk Export API did not return the identifier of the new export.", []);
}
