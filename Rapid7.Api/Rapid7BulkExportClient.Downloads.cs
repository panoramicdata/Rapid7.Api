namespace Rapid7.Api;

public sealed partial class Rapid7BulkExportClient
{
	/// <summary>
	/// Downloads one export file from its pre-signed URL (see <see cref="Models.BulkExport.ExportResult.Urls"/>). The
	/// request goes through this client's transport, certificate settings and retries, but without the API key: the URL
	/// carries its own authorisation and points at another host.
	/// </summary>
	/// <param name="url">The pre-signed URL, absolute; valid for 15 minutes after the export was queried.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The file content as a stream, read as it arrives. The caller disposes it.</returns>
	/// <exception cref="ArgumentException"><paramref name="url"/> is not absolute.</exception>
	/// <exception cref="Rapid7ApiException">The download failed, for example because the URL has expired.</exception>
	public async Task<Stream> DownloadAsync(Uri url, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(url);
		if (!url.IsAbsoluteUri)
		{
			throw new ArgumentException("A download URL must be absolute.", nameof(url));
		}

		var response = await _downloadClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
		if (!response.IsSuccessStatusCode)
		{
			using (response)
			{
				throw (await Rapid7ErrorMapper.CreateAsync(response).ConfigureAwait(false))!;
			}
		}

		// The content stream releases the connection when the caller disposes it, as HttpClient.GetStreamAsync does.
		return await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
	}
}
