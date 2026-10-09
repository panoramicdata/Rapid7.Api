using Rapid7.Api.Models.BulkExport;
using Rapid7.Api.Test.Support;
using System.Net;

namespace Rapid7.Api.Test.Groups;

public class BulkExportTests
{
	private const string GraphQLPath = "/export/graphql";

	[Fact]
	public async Task CreateAssetSoftwareExportAsync_PostsTheMutation()
	{
		var call = await TestClient.CaptureAsync(
			TestClient.CreateBulkExport,
			(c, ct) => c.Exports.CreateAssetSoftwareExportAsync(new CreateAssetSoftwareExportRequest(), ct),
			BulkExportJson.Created("createAssetSoftwareExport"));

		call.ShouldBe(HttpMethod.Post, GraphQLPath, body: BulkExportJson.CreateBody("CreateAssetSoftwareExport", "createAssetSoftwareExport"));
	}

	[Fact]
	public async Task CreatePolicyExportAsync_PostsTheMutation()
	{
		var call = await TestClient.CaptureAsync(
			TestClient.CreateBulkExport,
			(c, ct) => c.Exports.CreatePolicyExportAsync(new CreatePolicyExportRequest(), ct),
			BulkExportJson.Created("createPolicyExport"));

		call.ShouldBe(HttpMethod.Post, GraphQLPath, body: BulkExportJson.CreateBody("CreatePolicyExport", "createPolicyExport"));
	}

	[Fact]
	public async Task CreateVulnerabilityExportAsync_PostsTheMutation()
	{
		var call = await TestClient.CaptureAsync(
			TestClient.CreateBulkExport,
			(c, ct) => c.Exports.CreateVulnerabilityExportAsync(new CreateVulnerabilityExportRequest(), ct),
			BulkExportJson.Created("createVulnerabilityExport"));

		call.ShouldBe(HttpMethod.Post, GraphQLPath, body: BulkExportJson.CreateBody("CreateVulnerabilityExport", "createVulnerabilityExport"));
	}

	[Fact]
	public async Task CreateVulnerabilityRemediationExportAsync_PostsTheMutationWithTheDateRange()
	{
		var request = new CreateVulnerabilityRemediationExportRequest(new DateOnly(2025, 8, 3), new DateOnly(2025, 8, 24));

		var call = await TestClient.CaptureAsync(
			TestClient.CreateBulkExport,
			(c, ct) => c.Exports.CreateVulnerabilityRemediationExportAsync(request, ct),
			BulkExportJson.Created("createVulnerabilityRemediationExport"));

		call.ShouldBe(
			HttpMethod.Post,
			GraphQLPath,
			body: BulkExportJson.Body(
				"mutation CreateVulnerabilityRemediationExport($input: VulnerabilityRemediationExportConfiguration!) { createVulnerabilityRemediationExport(input: $input) { id } }",
				"""{"input":{"startDate":"2025-08-03","endDate":"2025-08-24"}}""",
				"CreateVulnerabilityRemediationExport"));
		request.Variables.Input.StartDate.Should().Be(new DateOnly(2025, 8, 3));
		request.OperationName.Should().Be("CreateVulnerabilityRemediationExport");
	}

	[Fact]
	public async Task GetExportAsync_PostsTheQueryWithTheIdAsAStringLiteral()
	{
		var call = await TestClient.CaptureAsync(
			TestClient.CreateBulkExport,
			(c, ct) => c.Exports.GetExportAsync(new GetExportRequest(BulkExportJson.ExportId), ct),
			BulkExportJson.SucceededExport);

		call.ShouldBe(HttpMethod.Post, GraphQLPath, body: BulkExportJson.Body(BulkExportJson.ExportQuery, "{}", "GetExport"));
	}

	[Fact]
	public void GetExportRequest_EscapesTheId()
	{
		var request = new GetExportRequest("a\"b");

		request.Query.Should().Contain("export(id: \"a" + ((char)92).ToString() + "u0022b\")");
	}

	[Theory]
	[InlineData("")]
	[InlineData(" ")]
	public void GetExportRequest_RejectsABlankId(string id)
	{
		var act = () => new GetExportRequest(id);

		act.Should().Throw<ArgumentException>();
	}

	[Theory]
	[InlineData(2025, 8, 3, 2025, 8, 3)]
	[InlineData(2025, 8, 24, 2025, 8, 3)]
	[InlineData(2025, 8, 1, 2025, 9, 2)]
	public void RemediationRequest_RejectsBadRanges(int y1, int m1, int d1, int y2, int m2, int d2)
	{
		var act = () => new CreateVulnerabilityRemediationExportRequest(new DateOnly(y1, m1, d1), new DateOnly(y2, m2, d2));

		act.Should().Throw<ArgumentOutOfRangeException>();
	}

	[Fact]
	public void RemediationRequest_AcceptsThirtyOneDays()
	{
		var request = new CreateVulnerabilityRemediationExportRequest(new DateOnly(2025, 8, 1), new DateOnly(2025, 9, 1));

		request.Variables.Input.EndDate.Should().Be(new DateOnly(2025, 9, 1));
		CreateVulnerabilityRemediationExportRequest.MaxRange.Should().Be(TimeSpan.FromDays(31));
	}

	[Fact]
	public async Task CreateMutations_MapTheExportId()
	{
		var software = await TestClient.ReadAsync(
			TestClient.CreateBulkExport,
			(c, ct) => c.Exports.CreateAssetSoftwareExportAsync(new CreateAssetSoftwareExportRequest(), ct),
			BulkExportJson.Created("createAssetSoftwareExport"));
		var policy = await TestClient.ReadAsync(
			TestClient.CreateBulkExport,
			(c, ct) => c.Exports.CreatePolicyExportAsync(new CreatePolicyExportRequest(), ct),
			BulkExportJson.Created("createPolicyExport"));
		var vulnerability = await TestClient.ReadAsync(
			TestClient.CreateBulkExport,
			(c, ct) => c.Exports.CreateVulnerabilityExportAsync(new CreateVulnerabilityExportRequest(), ct),
			BulkExportJson.Created("createVulnerabilityExport"));
		var remediation = await TestClient.ReadAsync(
			TestClient.CreateBulkExport,
			(c, ct) => c.Exports.CreateVulnerabilityRemediationExportAsync(
				new CreateVulnerabilityRemediationExportRequest(new DateOnly(2025, 8, 3), new DateOnly(2025, 8, 24)), ct),
			BulkExportJson.Created("createVulnerabilityRemediationExport"));

		software.Data!.Export!.Id.Should().Be(BulkExportJson.ExportId);
		policy.Data!.Export!.Id.Should().Be(BulkExportJson.ExportId);
		vulnerability.Data!.Export!.Id.Should().Be(BulkExportJson.ExportId);
		remediation.Data!.Export!.Id.Should().Be(BulkExportJson.ExportId);
		remediation.Errors.Should().BeEmpty();
	}

	[Fact]
	public async Task GetExportAsync_MapsEveryField()
	{
		var response = await TestClient.ReadAsync(
			TestClient.CreateBulkExport,
			(c, ct) => c.Exports.GetExportAsync(new GetExportRequest(BulkExportJson.ExportId), ct),
			BulkExportJson.SucceededExport);

		var export = response.Data!.Export!;
		export.Id.Should().Be(BulkExportJson.ExportId);
		export.Status.Should().Be(ExportStatus.Succeeded);
		export.Dataset.Should().Be(ExportDatasets.AssetSoftware);
		export.Timestamp.Should().Be(new DateTimeOffset(2026, 10, 9, 8, 15, 30, 123, TimeSpan.Zero));
		export.Result.Should().HaveCount(2);
		export.Result[0].Prefix.Should().Be("asset_software");
		export.Result[0].Urls.Should().Equal(
			new Uri("https://files.example.test/asset_software/part-0.parquet?X-Amz-Signature=abc"),
			new Uri("https://files.example.test/asset_software/part-1.parquet?X-Amz-Signature=def"));
		export.Result[1].Prefix.Should().Be(ExportDatasets.Asset);
	}

	[Theory]
	[InlineData("PENDING", ExportStatus.Pending)]
	[InlineData("PROCESSING", ExportStatus.Processing)]
	[InlineData("FAILED", ExportStatus.Failed)]
	[InlineData("CANCELLED", ExportStatus.Unknown)]
	public async Task GetExportAsync_MapsEachStatus_AndANullResultAsEmpty(string status, ExportStatus expected)
	{
		var response = await TestClient.ReadAsync(
			TestClient.CreateBulkExport,
			(c, ct) => c.Exports.GetExportAsync(new GetExportRequest(BulkExportJson.ExportId), ct),
			BulkExportJson.ExportWithStatus(status));

		response.Data!.Export!.Status.Should().Be(expected);
		response.Data.Export.Result.Should().BeEmpty();
		response.Data.Export.Timestamp.Should().BeNull();
	}

	[Fact]
	public async Task GetExportAsync_ReadsASingleResultObjectAsAList()
	{
		var response = await TestClient.ReadAsync(
			TestClient.CreateBulkExport,
			(c, ct) => c.Exports.GetExportAsync(new GetExportRequest(BulkExportJson.ExportId), ct),
			"""{"data":{"export":{"id":"x","status":"SUCCEEDED","result":{"prefix":"asset","urls":["https://files.example.test/a.parquet"]}}}}""");

		response.Data!.Export!.Result.Should().ContainSingle().Which.Prefix.Should().Be("asset");
	}

	[Fact]
	public async Task GraphQLErrors_InASuccessfulResponse_RaiseRapid7GraphQLException()
	{
		const string errors = """
			{
				"data": null,
				"errors": [
					{
						"message": "Export not found",
						"locations": [{ "line": 1, "column": 19 }],
						"path": ["export", 0],
						"extensions": { "code": "NOT_FOUND" }
					},
					{ "message": "Second problem" }
				]
			}
			""";
		using var client = TestClient.CreateBulkExport(TestClient.Stub(errors));

		Task Act() => client.Exports.GetExportAsync(new GetExportRequest("missing"), TestContext.Current.CancellationToken);

		var thrown = await TestClient.ShouldFailWithGraphQLAsync(Act, HttpStatusCode.OK, "The Bulk Export API returned GraphQL errors: Export not found (at export.0); Second problem");
		thrown.Errors.Should().HaveCount(2);
		var first = thrown.Errors[0];
		first.Locations.Should().ContainSingle();
		first.Locations[0].Line.Should().Be(1);
		first.Locations[0].Column.Should().Be(19);
		first.Path.Should().Equal("export", "0");
		first.Extensions["code"].GetString().Should().Be("NOT_FOUND");
		thrown.Errors[1].Extensions.Should().BeEmpty();
	}

	[Fact]
	public async Task GraphQLErrors_InAFailedResponse_RaiseRapid7GraphQLException_WithTheStatus()
	{
		using var client = TestClient.CreateBulkExport(TestClient.Stub("""{"errors":[{}]}""", HttpStatusCode.BadRequest));

		Task Act() => client.Exports.CreatePolicyExportAsync(new CreatePolicyExportRequest(), TestContext.Current.CancellationToken);

		var thrown = await TestClient.ShouldFailWithGraphQLAsync(Act, HttpStatusCode.BadRequest, "The Bulk Export API returned GraphQL errors: (no message)");
		thrown.Errors[0].ToString().Should().Be("(no message)");
	}

	[Fact]
	public Task Unauthorized_WithoutGraphQLErrors_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync(
			TestClient.CreateBulkExport,
			(c, ct) => c.Exports.CreatePolicyExportAsync(new CreatePolicyExportRequest(), ct),
			HttpStatusCode.Unauthorized,
			"""{"message":"Unauthorized"}""",
			"Unauthorized");

	[Fact]
	public Task BadGateway_WithAnHtmlPage_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync(
			TestClient.CreateBulkExport,
			(c, ct) => c.Exports.CreatePolicyExportAsync(new CreatePolicyExportRequest(), ct),
			HttpStatusCode.BadGateway,
			"<html><body>Bad gateway</body></html>",
			"HTTP 502 (Bad Gateway)");

	[Theory]
	[InlineData("")]
	[InlineData("<html/>")]
	[InlineData("{not json")]
	[InlineData("""{"data":{}}""")]
	public void ReadErrors_IsEmpty_WhenTheBodyHasNoErrors(string body)
		=> Rapid7GraphQL.ReadErrors(body).Should().BeEmpty();

	[Fact]
	public async Task Exports_AreAllowedOnAReadOnlyClient()
	{
		var stub = TestClient.Stub(BulkExportJson.Created("createPolicyExport"));
		using var client = TestClient.CreateBulkExport(stub, o => o.ReadOnly = true);

		await client.Exports.CreatePolicyExportAsync(new CreatePolicyExportRequest(), TestContext.Current.CancellationToken);

		stub.Calls.Should().ContainSingle().Which.Headers.GetValues("X-Api-Key").Should().Equal("fake-api-key");
	}
}
