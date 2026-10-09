using Parquet;
using Parquet.Serialization;
using Rapid7.Api.Models.BulkExport;
using Rapid7.Api.Serialization;
using System.Runtime.CompilerServices;

namespace Rapid7.Api;

/// <summary>
/// Reads Bulk Export Parquet files as typed records: <see cref="AssetRecord"/>, <see cref="AssetPolicyRecord"/>,
/// <see cref="AssetVulnerabilityRecord"/>, <see cref="VulnerabilityExceptionRecord"/>,
/// <see cref="VulnerabilityRemediationRecord"/> or <see cref="AssetSoftwareRecord"/>.
/// </summary>
public static class Rapid7Parquet
{
	private const int CopyBufferSize = 81920;

	/// <summary>
	/// Streams the rows of a Parquet file as records, one row group at a time. Columns are matched to properties by name
	/// (case-insensitively); a column the record does not know is ignored, and a property whose column is missing stays
	/// <see langword="null"/> (or empty, for a list).
	/// </summary>
	/// <typeparam name="T">The record type of the file's dataset.</typeparam>
	/// <param name="parquet">
	/// The whole Parquet file. Parquet is read from the end, so a stream that cannot seek (such as a download) is first
	/// copied to a temporary file, deleted afterwards. The stream is not disposed.
	/// </param>
	/// <param name="cancellationToken">A cancellation token, checked before each row group.</param>
	/// <returns>The records, in file order.</returns>
	public static async IAsyncEnumerable<T> ReadAsync<T>(Stream parquet, [EnumeratorCancellation] CancellationToken cancellationToken)
		where T : BulkExportRecord, new()
	{
		ArgumentNullException.ThrowIfNull(parquet);
		if (parquet.CanSeek)
		{
			await foreach (var record in ReadSeekableAsync<T>(parquet, cancellationToken).ConfigureAwait(false))
			{
				yield return record;
			}

			yield break;
		}

		var buffer = new FileStream(
			Path.GetTempFileName(),
			FileMode.Create,
			FileAccess.ReadWrite,
			FileShare.None,
			CopyBufferSize,
			FileOptions.DeleteOnClose | FileOptions.Asynchronous);
		await using (buffer.ConfigureAwait(false))
		{
			await parquet.CopyToAsync(buffer, cancellationToken).ConfigureAwait(false);
			await foreach (var record in ReadSeekableAsync<T>(buffer, cancellationToken).ConfigureAwait(false))
			{
				yield return record;
			}
		}
	}

	private static async IAsyncEnumerable<T> ReadSeekableAsync<T>(Stream stream, [EnumeratorCancellation] CancellationToken cancellationToken)
		where T : BulkExportRecord, new()
	{
		int rowGroups;
		stream.Position = 0;
		var reader = await ParquetReader.CreateAsync(stream, null, leaveStreamOpen: true, cancellationToken).ConfigureAwait(false);
		await using (reader.ConfigureAwait(false))
		{
			rowGroups = reader.RowGroupCount;
		}

		for (var rowGroup = 0; rowGroup < rowGroups; rowGroup++)
		{
			cancellationToken.ThrowIfCancellationRequested();
			stream.Position = 0;
			var rows = await ParquetSerializer.DeserializeUntypedAsync(stream, null, rowGroup, cancellationToken).ConfigureAwait(false);
			foreach (var row in rows.Data)
			{
				yield return ParquetRecordMapper<T>.Map(row);
			}
		}
	}
}
