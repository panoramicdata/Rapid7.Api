using Rapid7.Api.Models.Reports;
using Rapid7.Api.Test.Support;
using System.Net;

namespace Rapid7.Api.Test.Groups;

public class ReportInstancesTests
{
	private static readonly byte[] PdfBytes = [0x25, 0x50, 0x44, 0x46, 0x2D, 0x31, 0x2E, 0x37, 0x0A, 0x00, 0xFF];

	[Fact]
	public async Task GetInstancesAsync_SendsGet()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.ReportInstances.GetInstancesAsync(17, ct), ReportJson.InstanceList);

		call.ShouldBe(HttpMethod.Get, "/api/3/reports/17/history");
	}

	[Fact]
	public async Task GetInstancesAsync_MapsEveryField()
	{
		var list = await TestClient.ReadAsync((c, ct) => c.ReportInstances.GetInstancesAsync(17, ct), ReportJson.InstanceList);

		ShouldBeExampleInstance(list.Resources.Should().ContainSingle().Subject);
	}

	[Fact]
	public async Task GetInstanceAsync_SendsGet()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.ReportInstances.GetInstanceAsync(17, "latest", ct), ReportJson.Instance);

		call.ShouldBe(HttpMethod.Get, "/api/3/reports/17/history/latest");
	}

	[Fact]
	public async Task GetInstanceAsync_MapsEveryField()
		=> ShouldBeExampleInstance(await TestClient.ReadAsync((c, ct) => c.ReportInstances.GetInstanceAsync(17, "5", ct), ReportJson.Instance));

	[Fact]
	public async Task GetInstanceAsync_MapsTheUnknownStatus()
	{
		var instance = await TestClient.ReadAsync((c, ct) => c.ReportInstances.GetInstanceAsync(17, "6", ct), """{"id":6,"status":"unknown","links":[]}""");

		instance.Status.Should().Be(ReportInstanceStatus.Unknown);
		instance.Size.Should().BeNull();
	}

	[Fact]
	public async Task DeleteInstanceAsync_SendsDelete()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.ReportInstances.DeleteInstanceAsync(17, "5", ct), ReportJson.LinksJson);

		call.ShouldBe(HttpMethod.Delete, "/api/3/reports/17/history/5");
	}

	[Fact]
	public async Task DownloadAsync_SendsGet_AcceptingAnyMediaType()
	{
		var stub = new StubHandler();
		stub.EnqueueFile(PdfBytes, "application/pdf", "report.pdf");
		using var client = TestClient.Create(stub);

		using var content = await client.ReportInstances.DownloadAsync(17, "latest", TestContext.Current.CancellationToken);

		var call = stub.Calls.Should().ContainSingle().Subject;
		call.ShouldBe(HttpMethod.Get, "/api/3/reports/17/history/latest/output");
		call.Headers.Accept.ToString().Should().Be("*/*");
	}

	[Fact]
	public async Task DownloadAsync_ReturnsTheBytesAndMediaType()
	{
		var stub = new StubHandler();
		stub.EnqueueFile(PdfBytes, "application/pdf", "report.pdf");
		using var client = TestClient.Create(stub);
		var ct = TestContext.Current.CancellationToken;

		using var content = await client.ReportInstances.DownloadAsync(17, "5", ct);

		content.Headers.ContentType!.MediaType.Should().Be("application/pdf");
		content.Headers.ContentDisposition!.FileName.Should().Be("report.pdf");
		(await content.ReadAsByteArrayAsync(ct)).Should().Equal(PdfBytes);
	}

	[Fact]
	public Task DownloadAsync_NotFound_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync(
			(c, ct) => c.ReportInstances.DownloadAsync(17, "99", ct),
			HttpStatusCode.NotFound,
			PolicyJson.NotFound,
			PolicyJson.NotFoundMessage);

	private static void ShouldBeExampleInstance(ReportInstance instance) => instance.Should().BeEquivalentTo(new
	{
		Generated = new DateTimeOffset(2026, 6, 1, 18, 56, 3, TimeSpan.Zero),
		Id = 5,
		Links = new[] { new { Rel = "Download" } },
		Size = new { Bytes = 24789050L, Formatted = "23.6 MB" },
		Status = ReportInstanceStatus.Complete,
		Uri = new Uri("https://console.test:3780/reports/17/5/report.pdf")
	});
}
