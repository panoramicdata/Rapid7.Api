using Rapid7.Api.Models.BulkExport;
using System.Runtime.CompilerServices;

namespace Rapid7.Api;

public sealed partial class Rapid7BulkExportClient
{
	/// <summary>
	/// Downloads one export file and streams its rows as records. For example:
	/// <c>await foreach (var software in client.ReadRecordsAsync&lt;AssetSoftwareRecord&gt;(url, ct)) { ... }</c>.
	/// </summary>
	/// <typeparam name="T">The record type of the file's dataset.</typeparam>
	/// <param name="url">The file's pre-signed URL, from <see cref="ExportResult.Urls"/>.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The records, in file order.</returns>
	public async IAsyncEnumerable<T> ReadRecordsAsync<T>(Uri url, [EnumeratorCancellation] CancellationToken cancellationToken)
		where T : BulkExportRecord, new()
	{
		var stream = await DownloadAsync(url, cancellationToken).ConfigureAwait(false);
		await using (stream.ConfigureAwait(false))
		{
			await foreach (var record in Rapid7Parquet.ReadAsync<T>(stream, cancellationToken).ConfigureAwait(false))
			{
				yield return record;
			}
		}
	}

	/// <summary>
	/// Downloads every file of one dataset of a succeeded export, in turn, and streams their rows as records. A result
	/// belongs to the dataset when its prefix, or the last segment of its prefix path, is the dataset name. Yields nothing
	/// when the export has no files for the dataset. The URLs expire 15 minutes after the export was queried, so read
	/// promptly, or query the export again with <see cref="GetExportAsync"/>.
	/// </summary>
	/// <typeparam name="T">The record type of the dataset, such as <see cref="AssetSoftwareRecord"/>.</typeparam>
	/// <param name="export">The succeeded export.</param>
	/// <param name="dataset">The dataset, one of <see cref="ExportDatasets"/>.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The records of every file of the dataset.</returns>
	/// <exception cref="ArgumentNullException"><paramref name="export"/> is <see langword="null"/>.</exception>
	/// <exception cref="ArgumentException"><paramref name="dataset"/> is <see langword="null"/>, empty or white space.</exception>
	public IAsyncEnumerable<T> ReadRecordsAsync<T>(Export export, string dataset, CancellationToken cancellationToken)
		where T : BulkExportRecord, new()
	{
		ArgumentNullException.ThrowIfNull(export);
		ArgumentException.ThrowIfNullOrWhiteSpace(dataset);
		return ReadDatasetAsync<T>(export, dataset, cancellationToken);
	}

	private async IAsyncEnumerable<T> ReadDatasetAsync<T>(
		Export export,
		string dataset,
		[EnumeratorCancellation] CancellationToken cancellationToken)
		where T : BulkExportRecord, new()
	{
		foreach (var url in export.Result.Where(r => IsDataset(r.Prefix, dataset)).SelectMany(r => r.Urls))
		{
			await foreach (var record in ReadRecordsAsync<T>(url, cancellationToken).ConfigureAwait(false))
			{
				yield return record;
			}
		}
	}

	/// <summary>Whether a result prefix (a name, or a path such as <c>exports/1/asset/</c>) names the dataset.</summary>
	internal static bool IsDataset(string? prefix, string dataset)
		=> prefix is not null
			&& string.Equals(prefix.TrimEnd('/').Split('/')[^1], dataset, StringComparison.OrdinalIgnoreCase);
}
