using Rapid7.Api.Models;
using Rapid7.Api.Test.Support;

namespace Rapid7.Api.Test.Core;

/// <summary><see cref="Rapid7Paging.ReadAllAsync"/>: when it stops, what it requests, and how it validates.</summary>
public class PagingTests
{
	/// <summary>Serves <paramref name="pages"/> in turn, recording the options each request carried.</summary>
	private sealed class PageSource(params Page<int>?[] pages)
	{
		public List<PageOptions> Requests { get; } = [];

		public Task<Page<int>> GetAsync(PageOptions options, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			Requests.Add(options);
			return Task.FromResult(pages[Requests.Count - 1]!);
		}
	}

	private static Page<int> Page(int[] resources, long? totalPages = null)
		=> new() { Resources = resources, PageInfo = totalPages is { } total ? new PageMetadata { TotalPages = total } : null };

	private static Task<List<int>> ReadAllAsync(PageSource source, int pageSize = 2, CancellationToken? cancellationToken = null)
	{
		var token = cancellationToken ?? TestContext.Current.CancellationToken;
		return Rapid7Paging.ReadAllAsync(source.GetAsync, pageSize, token).ToListAsync(token).AsTask();
	}

	[Fact]
	public async Task ReadsUntilTheLastPageTheConsoleReports()
	{
		var source = new PageSource(Page([1, 2], 3), Page([3, 4], 3), Page([5], 3));

		(await ReadAllAsync(source)).Should().Equal(1, 2, 3, 4, 5);
		source.Requests.Select(r => (r.Page, r.Size)).Should().Equal((0, 2), (1, 2), (2, 2));
		source.Requests.Should().OnlyContain(r => r.Sort == null);
	}

	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	public async Task StopsAfterTheFirstPage_WhenThereIsAtMostOne(long totalPages)
	{
		var source = new PageSource(Page([1, 2], totalPages));

		(await ReadAllAsync(source)).Should().Equal(1, 2);
		source.Requests.Should().ContainSingle();
	}

	[Fact]
	public async Task WithoutPageMetadata_AShortPageIsTheLast()
	{
		var source = new PageSource(Page([1, 2]), Page([3]));

		(await ReadAllAsync(source)).Should().Equal(1, 2, 3);
		source.Requests.Should().HaveCount(2);
	}

	[Fact]
	public async Task WithoutPageMetadata_AnEmptyPageEndsAnExactMultiple()
	{
		var source = new PageSource(Page([1, 2]), Page([]));

		(await ReadAllAsync(source)).Should().Equal(1, 2);
		source.Requests.Should().HaveCount(2);
	}

	[Fact]
	public async Task AnEmptyPage_EndsTheRead_EvenWhenMorePagesAreReported()
	{
		var source = new PageSource(Page([1, 2], 5), Page([], 5));

		(await ReadAllAsync(source)).Should().Equal(1, 2);
		source.Requests.Should().HaveCount(2, "a collection that shrinks while it is read must not be requested page after empty page");
	}

	[Fact]
	public async Task ANullPage_IsReported()
	{
		var act = () => ReadAllAsync(new PageSource((Page<int>?)null));

		await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("*no page*");
	}

	[Theory]
	[InlineData(0)]
	[InlineData(-1)]
	[InlineData(501)]
	public void AnInvalidPageSize_IsRejectedWhenCalled(int pageSize)
	{
		var act = () => Rapid7Paging.ReadAllAsync(new PageSource().GetAsync, pageSize, CancellationToken.None);

		act.Should().Throw<ArgumentOutOfRangeException>().Which.ParamName.Should().Be("pageSize");
	}

	[Fact]
	public void ANullPageReader_IsRejectedWhenCalled()
	{
		var act = () => Rapid7Paging.ReadAllAsync<int>(null!, 10, CancellationToken.None);

		act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("getPage");
	}

	[Fact]
	public async Task TheMaximumPageSize_IsAccepted()
	{
		var source = new PageSource(Page([1]));

		(await ReadAllAsync(source, Rapid7Paging.MaxPageSize)).Should().Equal(1);
		source.Requests[0].Size.Should().Be(500);
	}

	[Fact]
	public async Task Cancellation_IsCheckedBeforeEachPageRequest()
	{
		using var cts = new CancellationTokenSource();
		var source = new PageSource(Page([1, 2], 3), Page([3, 4], 3));
		var read = new List<int>();

		var act = async () =>
		{
			await foreach (var item in Rapid7Paging.ReadAllAsync(source.GetAsync, 2, cts.Token))
			{
				read.Add(item);
				await cts.CancelAsync();
			}
		};

		await act.Should().ThrowAsync<OperationCanceledException>();
		read.Should().Equal(1, 2);
		source.Requests.Should().ContainSingle();
	}

	[Fact]
	public async Task TheEnumeratorsToken_IsPassedToEachRequest()
	{
		using var cts = new CancellationTokenSource();
		var seen = new List<CancellationToken>();

		await Rapid7Paging.ReadAllAsync<int>((_, ct) =>
		{
			seen.Add(ct);
			return Task.FromResult(Page([]));
		}, 2, CancellationToken.None).ToListAsync(cts.Token);

		seen.Should().ContainSingle().Which.CanBeCanceled.Should().BeTrue();
	}

	[Fact]
	public async Task ThroughAClient_PagesAreRequestedWithPageAndSize()
	{
		var stub = new StubHandler();
		stub.Enqueue(System.Net.HttpStatusCode.OK, """{"resources":[{"name":"a"},{"name":"b"}],"page":{"number":0,"size":2,"totalPages":2,"totalResources":3},"links":[]}""");
		stub.Enqueue(System.Net.HttpStatusCode.OK, """{"resources":[{"name":"c"}],"page":{"number":1,"size":2,"totalPages":2,"totalResources":3},"links":[]}""");
		using var client = TestClient.Create(stub);
		var probe = client.For<IProbe>();

		var names = await Rapid7Paging.ReadAllAsync(probe.ListAsync, 2, TestContext.Current.CancellationToken).Select(p => p.Name).ToListAsync(TestContext.Current.CancellationToken);

		names.Should().Equal("a", "b", "c");
		stub.Calls.Select(c => c.Uri.Query).Should().Equal("?page=0&size=2", "?page=1&size=2");
	}
}
