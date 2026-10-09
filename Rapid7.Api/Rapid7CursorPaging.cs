using Rapid7.Api.Models.Cloud;
using System.Runtime.CompilerServices;

namespace Rapid7.Api;

/// <summary>
/// Reads every resource of a paged Cloud Integrations (v4) collection, one page request at a time, following the cursor each
/// page returns.
/// </summary>
public static class Rapid7CursorPaging
{
	/// <summary>
	/// Streams every resource of a paged collection: requests page 0, then each following page with the cursor the previous
	/// page returned, until the last. For example:
	/// <c>Rapid7CursorPaging.ReadAllAsync((page, ct) =&gt; cloud.Sites.ListAsync(page, ct), 100, ct)</c>. To sort, copy the
	/// page, size and cursor into options of your own that also set <see cref="Models.PageOptions.Sort"/>.
	/// </summary>
	/// <typeparam name="T">The resource type.</typeparam>
	/// <param name="getPage">Requests one page, given the paging options (page number, size and cursor) to send.</param>
	/// <param name="pageSize">The page size to request, at least 1.</param>
	/// <param name="cancellationToken">A cancellation token, checked before each page request.</param>
	/// <returns>The resources, in the order the pages return them.</returns>
	public static async IAsyncEnumerable<T> ReadAllAsync<T>(
		Func<CursorPageOptions, CancellationToken, Task<CursorPage<T>>> getPage,
		int pageSize,
		[EnumeratorCancellation] CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(getPage);
		ArgumentOutOfRangeException.ThrowIfLessThan(pageSize, 1);
		string? cursor = null;
		for (var number = 0; ; number++)
		{
			cancellationToken.ThrowIfCancellationRequested();
			var options = new CursorPageOptions { Page = number, Size = pageSize, Cursor = cursor };
			var page = await getPage(options, cancellationToken).ConfigureAwait(false);
			foreach (var resource in page.Data)
			{
				yield return resource;
			}

			if (IsLast(page, number, pageSize))
			{
				yield break;
			}

			// Collections that page by number alone return no cursor; keep the last one for those that do.
			cursor = page.Metadata?.Cursor ?? cursor;
		}
	}

	/// <summary>
	/// The last page: it is empty, the metadata says so, or (when the API omits metadata) the page came back short.
	/// </summary>
	private static bool IsLast<T>(CursorPage<T> page, int number, int pageSize)
		=> page.Data.Count == 0
			|| (page.Metadata is { } metadata ? number + 1 >= metadata.TotalPages : page.Data.Count < pageSize);
}
