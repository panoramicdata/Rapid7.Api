using Rapid7.Api.Models.Cloud;
using Rapid7.Api.Test.Support;
using System.Net;

namespace Rapid7.Api.Test.Groups;

public class CloudScansTests
{
	private const string ScanPage = $$"""
		{
			"data": [{{CloudJson.Scan}}],
			"metadata": { "number": 0, "size": 10, "totalResources": 1, "totalPages": 1 },
			"links": [{ "href": "https://us.api.insight.test/vm/v4/integration/scan?page=0&size=10", "rel": "self" }]
		}
		""";

	private const string DispatchJson = """
		{
			"scans": [{
				"id": "bcc3fd8f-7bb6-41cc-a52f-4046c8742bf0",
				"engine_id": "b177d730-5cde-468a-89a4-2c5b9d26b465",
				"name": "Scan API Example Name",
				"asset_ids": ["org-1-default-asset-1", "org-1-default-asset-2"]
			}],
			"unscanned_assets": [{ "id": "org-1-default-asset-4", "reason": "Too many assets in request." }]
		}
		""";

	[Fact]
	public async Task ListAsync_SendsGet_WithPagingAndDetails()
	{
		var call = await TestClient.CaptureAsync(
			TestClient.CreateCloud,
			(c, ct) => c.Scans.ListAsync(new CloudScanListOptions { Page = 1, Size = 10, IncludeDetails = true }, ct),
			ScanPage);

		call.ShouldBe(HttpMethod.Get, "/vm/v4/integration/scan", "?includeDetails=true&page=1&size=10");
	}

	[Fact]
	public async Task ListAsync_WithoutOptions_SendsNoQuery()
	{
		var call = await TestClient.CaptureAsync(TestClient.CreateCloud, (c, ct) => c.Scans.ListAsync(null, ct), ScanPage);

		call.ShouldBe(HttpMethod.Get, "/vm/v4/integration/scan");
	}

	[Fact]
	public async Task ListAsync_MapsEveryField()
	{
		var page = await TestClient.ReadAsync(TestClient.CreateCloud, (c, ct) => c.Scans.ListAsync(null, ct), ScanPage);

		page.Metadata!.TotalResources.Should().Be(1);
		page.Metadata.Cursor.Should().BeNull();
		var scan = page.Data.Should().ContainSingle().Subject;
		scan.Id.Should().Be("bcc3fd8f-7bb6-41cc-a52f-4046c8742bf0");
		scan.Name.Should().Be("Scan API Example Name");
		scan.EngineId.Should().Be("b177d730-5cde-468a-89a4-2c5b9d26b465");
		scan.Status.Should().Be("Success");
		scan.Started.Should().Be(new DateTimeOffset(2020, 5, 12, 9, 0, 12, 199, TimeSpan.Zero));
		scan.Finished.Should().Be(new DateTimeOffset(2020, 5, 12, 9, 8, 54, 404, TimeSpan.Zero));
		scan.Details.Should().Be("Scanned 3 assets");
		scan.AssetIds.Should().BeEmpty();
	}

	[Theory]
	[InlineData(true, "?includeDetails=true")]
	[InlineData(false, "?includeDetails=false")]
	public async Task GetAsync_SendsGetWithTheDetailsFlag(bool includeDetails, string query)
	{
		var call = await TestClient.CaptureAsync(
			TestClient.CreateCloud,
			(c, ct) => c.Scans.GetAsync("bcc3fd8f-7bb6-41cc-a52f-4046c8742bf0", includeDetails, ct),
			CloudJson.Scan);

		call.ShouldBe(HttpMethod.Get, "/vm/v4/integration/scan/bcc3fd8f-7bb6-41cc-a52f-4046c8742bf0", query);
	}

	[Fact]
	public async Task GetAsync_ReadsObjectDetailsAsJsonText()
	{
		var scan = await TestClient.ReadAsync(
			TestClient.CreateCloud,
			(c, ct) => c.Scans.GetAsync("s", true, ct),
			"""{"id":"s","details":{"assets":3}}""");

		scan.Details.Should().Be("""{"assets":3}""");
	}

	[Fact]
	public async Task StartAsync_PostsTheScanForm()
	{
		var request = new CloudScanRequest
		{
			Name = "Scan API Example Name",
			AssetIds = ["org-1-default-asset-1"],
			EngineIds = ["b177d730-5cde-468a-89a4-2c5b9d26b465"],
			VulnerabilityIds = ["acrobat-cve-2018-16030"],
			SolutionIds = ["unknown-acrobat-cve-2018-16030"],
			CredentialSources = ["lab"],
			ResultConsumer = "lab",
			StartTime = new DateTimeOffset(2026, 1, 2, 3, 4, 5, TimeSpan.Zero),
			Exclusions = new CloudScanRequest { AssetIds = ["org-1-default-asset-2"] }
		};

		var call = await TestClient.CaptureAsync(TestClient.CreateCloud, (c, ct) => c.Scans.StartAsync(request, ct), DispatchJson);

		call.ShouldBe(
			HttpMethod.Post,
			"/vm/v4/integration/scan",
			body: """{"name":"Scan API Example Name","asset_ids":["org-1-default-asset-1"],"engine_ids":["b177d730-5cde-468a-89a4-2c5b9d26b465"],"vulnerability_ids":["acrobat-cve-2018-16030"],"solution_ids":["unknown-acrobat-cve-2018-16030"],"credential_sources":["lab"],"result_consumer":"lab","start_time":"2026-01-02T03:04:05+00:00","exclusions":{"asset_ids":["org-1-default-asset-2"]}}""");
	}

	[Fact]
	public async Task StartAsync_MapsTheDispatchedScans()
	{
		var dispatch = await TestClient.ReadAsync(
			TestClient.CreateCloud,
			(c, ct) => c.Scans.StartAsync(new CloudScanRequest(), ct),
			DispatchJson);

		var scan = dispatch.Scans.Should().ContainSingle().Subject;
		scan.Id.Should().Be("bcc3fd8f-7bb6-41cc-a52f-4046c8742bf0");
		scan.AssetIds.Should().Equal("org-1-default-asset-1", "org-1-default-asset-2");
		var unscanned = dispatch.UnscannedAssets.Should().ContainSingle().Subject;
		unscanned.Id.Should().Be("org-1-default-asset-4");
		unscanned.Reason.Should().Be("Too many assets in request.");
	}

	[Fact]
	public async Task StartAsync_IsRefusedByAReadOnlyClient()
	{
		var stub = new StubHandler();
		using var client = TestClient.CreateCloud(stub, o => o.ReadOnly = true);

		var act = () => client.Scans.StartAsync(new CloudScanRequest(), TestContext.Current.CancellationToken);

		await act.Should().ThrowAsync<Rapid7ReadOnlyException>();
		stub.Calls.Should().BeEmpty();
	}

	[Fact]
	public async Task StopAsync_PostsToStopWithoutABody()
	{
		var stub = new StubHandler();
		stub.Enqueue(HttpStatusCode.Accepted, string.Empty);
		using var client = TestClient.CreateCloud(stub);

		await client.Scans.StopAsync("bcc3fd8f", TestContext.Current.CancellationToken);

		stub.Calls.Should().ContainSingle().Which.ShouldBe(HttpMethod.Post, "/vm/v4/integration/scan/bcc3fd8f/stop");
	}

	[Fact]
	public Task StopAsync_NotFound_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync(
			TestClient.CreateCloud,
			(c, ct) => c.Scans.StopAsync("missing", ct),
			HttpStatusCode.NotFound,
			CloudJson.NotFound,
			"The requested resource does not exist.");
}
