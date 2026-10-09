using Rapid7.Api.Models;
using System.Runtime.CompilerServices;

namespace Rapid7.Api;

/// <summary>Reads every resource of a paged Security Console collection, one page request at a time.</summary>
public static class Rapid7Paging
{
	/// <summary>The largest page size the Security Console accepts.</summary>
	public const int MaxPageSize = 500;

	/// <summary>
	/// Streams every resource of a paged collection, requesting page after page until the last. For example:
	/// <c>Rapid7Paging.ReadAllAsync((page, ct) =&gt; client.Sites.ListAsync(page, ct), 500, ct)</c>.
	/// </summary>
	/// <typeparam name="T">The resource type.</typeparam>
	/// <param name="getPage">Requests one page, given the paging options to send.</param>
	/// <param name="pageSize">The page size to request, from 1 to <see cref="MaxPageSize"/>.</param>
	/// <param name="cancellationToken">A cancellation token, checked before each page request.</param>
	/// <returns>The resources, in the order the pages return them.</returns>
	public static async IAsyncEnumerable<T> ReadAllAsync<T>(
		Func<PageOptions, CancellationToken, Task<Page<T>>> getPage,
		int pageSize,
		[EnumeratorCancellation] CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(getPage);
		ArgumentOutOfRangeException.ThrowIfLessThan(pageSize, 1);
		ArgumentOutOfRangeException.ThrowIfGreaterThan(pageSize, MaxPageSize);
		for (var number = 0; ; number++)
		{
			cancellationToken.ThrowIfCancellationRequested();
			var page = await getPage(new PageOptions { Page = number, Size = pageSize }, cancellationToken).ConfigureAwait(false);
			foreach (var resource in page.Resources)
			{
				yield return resource;
			}

			// The last page: the console says so, or (when it omits page metadata) the page came back short.
			if (page.PageInfo is { } info ? number + 1 >= info.TotalPages : page.Resources.Count < pageSize)
			{
				yield break;
			}
		}
	}
}
