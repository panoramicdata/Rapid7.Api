using Rapid7.Api.Models.BulkExport;
using Rapid7.Api.Test.Support;
using System.Net;

namespace Rapid7.Api.Test.Groups;

/// <summary>The high-level helpers of <see cref="Rapid7BulkExportClient"/>: create and wait, and downloads.</summary>
public sealed class BulkExportClientTests : IDisposable
{
	private static readonly Uri FileUrl = new("https://files.example.test/asset_software/part-0.parquet?X-Amz-Signature=abc%2Fdef");

	private readonly StubHandler _stub = new();
	private readonly ManualTimeProvider _time = new();
	private readonly List<TimeSpan> _delays = [];

	public void Dispose() => _stub.Dispose();

	private Rapid7BulkExportClient CreateClient()
	{
		var client = TestClient.CreateBulkExport(_stub);
		client.TimeProvider = _time;
		client.Delay = (wait, ct) =>
		{
			_delays.Add(wait);
			_time.Advance(wait);
			ct.ThrowIfCancellationRequested();
			return Task.CompletedTask;
		};
		return client;
	}

	private void Answer(params string[] responses)
	{
		foreach (var response in responses)
		{
			_stub.Enqueue(HttpStatusCode.OK, response);
		}
	}

	private string OperationOf(int call) => _stub.Calls[call].Body!.Split("\"operationName\":\"")[1].TrimEnd('"', '}');

	[Fact]
	public async Task ExportAssetSoftwareAsync_CreatesThenPollsUntilSucceeded()
	{
		Answer(
			BulkExportJson.Created("createAssetSoftwareExport"),
			BulkExportJson.ExportWithStatus("PENDING"),
			BulkExportJson.ExportWithStatus("PROCESSING"),
			BulkExportJson.SucceededExport);
		using var client = CreateClient();

		var export = await client.ExportAssetSoftwareAsync(null, TestContext.Current.CancellationToken);

		export.Status.Should().Be(ExportStatus.Succeeded);
		export.Result.Should().HaveCount(2);
		Enumerable.Range(0, 4).Select(OperationOf).Should().Equal("CreateAssetSoftwareExport", "GetExport", "GetExport", "GetExport");
		_stub.Calls[1].Body.Should().Be(BulkExportJson.Body(BulkExportJson.ExportQuery, "{}", "GetExport"));
		_delays.Should().Equal(TimeSpan.FromSeconds(15), TimeSpan.FromSeconds(15));
	}

	[Fact]
	public async Task OtherExports_CreateTheirExportThenWait()
	{
		Answer(BulkExportJson.Created("createPolicyExport"), BulkExportJson.SucceededExport);
		Answer(BulkExportJson.Created("createVulnerabilityExport"), BulkExportJson.SucceededExport);
		Answer(BulkExportJson.Created("createVulnerabilityRemediationExport"), BulkExportJson.SucceededExport);
		using var client = CreateClient();
		var ct = TestContext.Current.CancellationToken;

		(await client.ExportPoliciesAsync(null, ct)).Id.Should().Be(BulkExportJson.ExportId);
		(await client.ExportVulnerabilitiesAsync(null, ct)).Id.Should().Be(BulkExportJson.ExportId);
		(await client.ExportVulnerabilityRemediationsAsync(new DateOnly(2025, 8, 3), new DateOnly(2025, 8, 24), null, ct))
			.Id.Should().Be(BulkExportJson.ExportId);

		Enumerable.Range(0, 6).Where(i => i % 2 == 0).Select(OperationOf)
			.Should().Equal("CreatePolicyExport", "CreateVulnerabilityExport", "CreateVulnerabilityRemediationExport");
		_delays.Should().BeEmpty();
	}

	[Fact]
	public async Task WaitForExportAsync_RaisesRapid7ExportFailedException_WhenTheExportFails()
	{
		Answer(BulkExportJson.ExportWithStatus("FAILED"));
		using var client = CreateClient();

		var act = () => client.WaitForExportAsync(BulkExportJson.ExportId, null, TestContext.Current.CancellationToken);

		var thrown = (await act.Should().ThrowAsync<Rapid7ExportFailedException>()).Which;
		thrown.Export.Status.Should().Be(ExportStatus.Failed);
		thrown.Message.Should().Be($"Bulk export {BulkExportJson.ExportId} failed.");
	}

	[Fact]
	public async Task WaitForExportAsync_TimesOut_AfterTheConfiguredTime()
	{
		Answer([.. Enumerable.Repeat(BulkExportJson.ExportWithStatus("PROCESSING"), 4)]);
		using var client = CreateClient();
		var options = new ExportWaitOptions { PollInterval = TimeSpan.FromSeconds(10), Timeout = TimeSpan.FromSeconds(25) };

		var act = () => client.WaitForExportAsync(BulkExportJson.ExportId, options, TestContext.Current.CancellationToken);

		(await act.Should().ThrowAsync<TimeoutException>()).Which.Message
			.Should().Be($"Bulk export {BulkExportJson.ExportId} did not finish within 00:00:25; it was last Processing.");
		_stub.Calls.Should().HaveCount(4);
		_delays.Should().Equal(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(10));
	}

	[Fact]
	public async Task WaitForExportAsync_HonoursCancellationWhileWaiting()
	{
		Answer(BulkExportJson.ExportWithStatus("PENDING"));
		using var cts = new CancellationTokenSource();
		using var client = CreateClient();
		client.Delay = (_, ct) =>
		{
			cts.Cancel();
			return Task.Delay(Timeout.InfiniteTimeSpan, ct);
		};

		var act = () => client.WaitForExportAsync(BulkExportJson.ExportId, null, cts.Token);

		await act.Should().ThrowAsync<OperationCanceledException>();
		_stub.Calls.Should().ContainSingle();
	}

	[Theory]
	[InlineData(0, 10)]
	[InlineData(10, 0)]
	public async Task WaitForExportAsync_RejectsNonPositiveTimes(int pollSeconds, int timeoutSeconds)
	{
		using var client = CreateClient();
		var options = new ExportWaitOptions { PollInterval = TimeSpan.FromSeconds(pollSeconds), Timeout = TimeSpan.FromSeconds(timeoutSeconds) };

		var act = () => client.WaitForExportAsync("id", options, TestContext.Current.CancellationToken);

		await act.Should().ThrowAsync<ArgumentOutOfRangeException>();
		_stub.Calls.Should().BeEmpty();
	}

	[Fact]
	public async Task WaitForExportAsync_RejectsABlankId()
	{
		using var client = CreateClient();

		var act = () => client.WaitForExportAsync(" ", null, TestContext.Current.CancellationToken);

		await act.Should().ThrowAsync<ArgumentException>();
	}

	[Theory]
	[InlineData("""{"data":{"createPolicyExport":{"id":""}}}""")]
	[InlineData("""{"data":{"createPolicyExport":null}}""")]
	[InlineData("""{"data":null}""")]
	public async Task ExportPoliciesAsync_RaisesRapid7GraphQLException_WithoutAnExportId(string created)
	{
		Answer(created);
		using var client = CreateClient();

		var act = () => client.ExportPoliciesAsync(null, TestContext.Current.CancellationToken);

		(await act.Should().ThrowAsync<Rapid7GraphQLException>()).Which.Message
			.Should().Be("The Bulk Export API did not return the identifier of the new export.");
	}

	[Theory]
	[InlineData("""{"data":{"export":null}}""")]
	[InlineData("""{"data":null}""")]
	public async Task GetExportAsync_RaisesRapid7GraphQLException_WithoutAnExport(string response)
	{
		Answer(response);
		using var client = CreateClient();

		var act = () => client.GetExportAsync("missing", TestContext.Current.CancellationToken);

		var thrown = (await act.Should().ThrowAsync<Rapid7GraphQLException>()).Which;
		thrown.Message.Should().Be("The Bulk Export API returned no export with id missing.");
		thrown.Errors.Should().BeEmpty();
		thrown.StatusCode.Should().Be(HttpStatusCode.OK);
	}

	[Fact]
	public async Task DownloadAsync_GetsThePreSignedUrl_WithoutTheApiKey()
	{
		_stub.EnqueueFile([1, 2, 3], "application/octet-stream", "part-0.parquet");
		using var client = CreateClient();

		await using var stream = await client.DownloadAsync(FileUrl, TestContext.Current.CancellationToken);
		using var copy = new MemoryStream();
		await stream.CopyToAsync(copy, TestContext.Current.CancellationToken);

		copy.ToArray().Should().Equal(1, 2, 3);
		var call = _stub.Calls.Should().ContainSingle().Subject;
		call.Method.Should().Be(HttpMethod.Get);
		call.Uri.Should().Be(FileUrl);
		call.Uri.Query.Should().Be("?X-Amz-Signature=abc%2Fdef");
		call.Headers.Contains("X-Api-Key").Should().BeFalse();
		call.Authorization.Should().BeNull();
		call.Body.Should().BeNull();
	}

	[Fact]
	public async Task DownloadAsync_ExpiredUrl_RaisesRapid7ApiException()
	{
		_stub.Enqueue(HttpStatusCode.Forbidden, "<Error><Code>AccessDenied</Code><Message>Request has expired</Message></Error>");
		using var client = CreateClient();

		await TestClient.ShouldFailWithAsync(
			() => client.DownloadAsync(FileUrl, TestContext.Current.CancellationToken),
			HttpStatusCode.Forbidden,
			"HTTP 403 (Forbidden)");
	}

	[Fact]
	public async Task DownloadAsync_RejectsAMissingOrRelativeUrl()
	{
		using var client = CreateClient();
		var ct = TestContext.Current.CancellationToken;

		await FluentActions.Awaiting(() => client.DownloadAsync(null!, ct)).Should().ThrowAsync<ArgumentNullException>();
		await FluentActions.Awaiting(() => client.DownloadAsync(new Uri("part-0.parquet", UriKind.Relative), ct)).Should().ThrowAsync<ArgumentException>();
		_stub.Calls.Should().BeEmpty();
	}

	[Fact]
	public void ExportFailedException_RequiresTheExport()
	{
		var act = () => new Rapid7ExportFailedException(null!);

		act.Should().Throw<ArgumentNullException>();
	}

	[Fact]
	public void DefaultClient_UsesTheSystemClockAndTaskDelay()
	{
		using var client = TestClient.CreateBulkExport(_stub);

		client.TimeProvider.Should().BeSameAs(TimeProvider.System);
		client.Delay.Should().NotBeNull();
	}
}
