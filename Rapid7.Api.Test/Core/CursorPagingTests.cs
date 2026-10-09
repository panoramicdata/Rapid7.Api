using Rapid7.Api.Models.Cloud;

namespace Rapid7.Api.Test.Core;

public class CursorPagingTests
{
	private static CursorPage<int> Page(int[] data, CursorPageMetadata? metadata) => new() { Data = data, Metadata = metadata };

	private static async Task<(List<int> Items, List<CursorPageOptions> Requests)> ReadAllAsync(int pageSize, params CursorPage<int>[] pages)
	{
		var requests = new List<CursorPageOptions>();
		var items = new List<int>();
		await foreach (var item in Rapid7CursorPaging.ReadAllAsync(
			(options, _) =>
			{
				requests.Add(options);
				return Task.FromResult(pages[requests.Count - 1]);
			},
			pageSize,
			TestContext.Current.CancellationToken))
		{
			items.Add(item);
		}

		return (items, requests);
	}

	[Fact]
	public async Task ReadAllAsync_FollowsTheCursorUntilTheLastPage()
	{
		var (items, requests) = await ReadAllAsync(
			2,
			Page([1, 2], new CursorPageMetadata { Number = 0, TotalPages = 3, Cursor = "c1" }),
			Page([3, 4], new CursorPageMetadata { Number = 1, TotalPages = 3, Cursor = "c2" }),
			Page([5], new CursorPageMetadata { Number = 2, TotalPages = 3, Cursor = "c3" }));

		items.Should().Equal(1, 2, 3, 4, 5);
		requests.Select(r => (r.Page, r.Size, r.Cursor)).Should().Equal((0, 2, null), (1, 2, "c1"), (2, 2, "c2"));
	}

	[Fact]
	public async Task ReadAllAsync_KeepsTheLastCursor_WhenAPageReturnsNone()
	{
		var (_, requests) = await ReadAllAsync(
			1,
			Page([1], new CursorPageMetadata { TotalPages = 3, Cursor = "c1" }),
			Page([2], new CursorPageMetadata { TotalPages = 3 }),
			Page([3], new CursorPageMetadata { TotalPages = 3 }));

		requests.Select(r => r.Cursor).Should().Equal(null, "c1", "c1");
	}

	[Fact]
	public async Task ReadAllAsync_StopsOnAnEmptyPage()
	{
		var (items, requests) = await ReadAllAsync(2, Page([], new CursorPageMetadata { TotalPages = 5 }));

		items.Should().BeEmpty();
		requests.Should().ContainSingle();
	}

	[Fact]
	public async Task ReadAllAsync_WithoutMetadata_StopsOnAShortPage()
	{
		var (items, requests) = await ReadAllAsync(2, Page([1, 2], null), Page([3], null));

		items.Should().Equal(1, 2, 3);
		requests.Should().HaveCount(2);
	}

	[Fact]
	public async Task ReadAllAsync_ChecksCancellationBeforeEachPage()
	{
		using var cts = new CancellationTokenSource();
		var calls = 0;

		var act = () => Rapid7CursorPaging.ReadAllAsync(
				(_, _) =>
				{
					calls++;
					cts.Cancel();
					return Task.FromResult(Page([1], new CursorPageMetadata { TotalPages = 9 }));
				},
				1,
				cts.Token)
			.ToListAsync(CancellationToken.None)
			.AsTask();

		await act.Should().ThrowAsync<OperationCanceledException>();
		calls.Should().Be(1);
	}

	[Fact]
	public async Task ReadAllAsync_RejectsBadArguments()
	{
		var nullPage = async () => await Rapid7CursorPaging.ReadAllAsync<int>(null!, 1, CancellationToken.None).GetAsyncEnumerator().MoveNextAsync();
		var zeroSize = async () => await Rapid7CursorPaging.ReadAllAsync((_, _) => Task.FromResult(Page([], null)), 0, CancellationToken.None).GetAsyncEnumerator().MoveNextAsync();

		await nullPage.Should().ThrowAsync<ArgumentNullException>();
		await zeroSize.Should().ThrowAsync<ArgumentOutOfRangeException>();
	}
}
