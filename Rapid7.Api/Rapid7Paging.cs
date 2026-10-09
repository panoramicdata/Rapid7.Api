using System.Runtime.CompilerServices;
using Rapid7.Api.Models;

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
	/// <remarks>
	/// Reading stops after the last page the console reports, after an empty page, or (when the console omits page
	/// metadata) after a page shorter than <paramref name="pageSize"/>. The arguments are checked when this is called,
	/// not when enumeration starts.
	/// </remarks>
	/// <typeparam name="T">The resource type.</typeparam>
	/// <param name="getPage">Requests one page, given the paging options to send.</param>
	/// <param name="pageSize">The page size to request, from 1 to <see cref="MaxPageSize"/>.</param>
	/// <param name="cancellationToken">A cancellation token, checked before each page request.</param>
	/// <returns>The resources, in the order the pages return them.</returns>
	/// <exception cref="ArgumentNullException"><paramref name="getPage"/> is <see langword="null"/>.</exception>
	/// <exception cref="ArgumentOutOfRangeException"><paramref name="pageSize"/> is outside 1 to <see cref="MaxPageSize"/>.</exception>
	/// <exception cref="InvalidOperationException"><paramref name="getPage"/> returned no page.</exception>
	public static IAsyncEnumerable<T> ReadAllAsync<T>(
		Func<PageOptions, CancellationToken, Task<Page<T>>> getPage,
		int pageSize,
		CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(getPage);
		ArgumentOutOfRangeException.ThrowIfLessThan(pageSize, 1);
		ArgumentOutOfRangeException.ThrowIfGreaterThan(pageSize, MaxPageSize);
		return ReadPagesAsync(getPage, pageSize, cancellationToken);
	}

	private static async IAsyncEnumerable<T> ReadPagesAsync<T>(
		Func<PageOptions, CancellationToken, Task<Page<T>>> getPage,
		int pageSize,
		[EnumeratorCancellation] CancellationToken cancellationToken)
	{
		for (var number = 0; ; number++)
		{
			cancellationToken.ThrowIfCancellationRequested();
			var page = await getPage(new PageOptions { Page = number, Size = pageSize }, cancellationToken).ConfigureAwait(false)
				?? throw new InvalidOperationException($"The page reader returned no page for page {number}.");
			foreach (var resource in page.Resources)
			{
				yield return resource;
			}

			// The last page: the console says so, the page is empty (the collection shrank while being read), or (when the
			// console omits page metadata) the page came back short.
			var count = page.Resources.Count;
			if (count == 0 || (page.PageInfo is { } info ? number + 1 >= info.TotalPages : count < pageSize))
			{
				yield break;
			}
		}
	}
}
