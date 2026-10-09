using Rapid7.Api.Models.Administration;
using Rapid7.Api.Test.Support;
using Refit;
using System.Net;

namespace Rapid7.Api.Test.Groups;

/// <summary>Pins the administration requests; <c>AdministrationTests.Mapping.cs</c> maps the responses.</summary>
public partial class AdministrationTests
{
	[Fact]
	public async Task ExecuteCommandAsync_PostsTheCommandAsPlainText()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.Administration.ExecuteCommandAsync("show host", ct), AdministrationJson.CommandOutput);

		call.ShouldBe(HttpMethod.Post, "/api/3/administration/commands", body: "show host");
		call.ContentType.Should().Be("text/plain");
	}

	[Fact]
	public async Task GetInfoAsync_SendsGetToInfo()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.Administration.GetInfoAsync(ct), AdministrationJson.Info);

		call.ShouldBe(HttpMethod.Get, "/api/3/administration/info");
	}

	[Fact]
	public async Task GetLicenseAsync_SendsGetToLicense()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.Administration.GetLicenseAsync(ct), AdministrationJson.License);

		call.ShouldBe(HttpMethod.Get, "/api/3/administration/license");
	}

	[Fact]
	public async Task ActivateLicenseAsync_PostsTheKeyInTheQueryWithoutABody()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.Administration.ActivateLicenseAsync("FAKE-KEY 1", ct), AccessJson.LinksOnly);

		call.ShouldBe(HttpMethod.Post, "/api/3/administration/license", "?key=FAKE-KEY%201");
	}

	[Fact]
	public async Task UploadLicenseAsync_PostsTheFileAsTheLicensePart()
	{
		var bytes = "fake licence"u8.ToArray();
		using var stream = new MemoryStream(bytes);

		var call = await TestClient.CaptureAsync(
			(c, ct) => c.Administration.UploadLicenseAsync(new StreamPart(stream, "console.lic", "application/octet-stream"), ct),
			AccessJson.LinksOnly);

		call.ShouldBe(HttpMethod.Post, "/api/3/administration/license");
		call.ContentType.Should().Be("multipart/form-data");
		var part = call.Parts.Should().ContainSingle().Subject;
		part.Name.Should().Be("license");
		part.FileName.Should().Be("console.lic");
		part.ContentType.Should().Be("application/octet-stream");
		part.Bytes.Should().Equal(bytes);
	}

	[Fact]
	public async Task UploadLicenseAsync_ReturnsTheLinks()
	{
		using var stream = new MemoryStream([1, 2, 3]);

		var links = await TestClient.ReadAsync(
			(c, ct) => c.Administration.UploadLicenseAsync(new StreamPart(stream, "console.lic"), ct),
			AccessJson.LinksOnly);

		AccessJson.ShouldBeTheSelfLink(links);
	}

	[Fact]
	public async Task GetLogsAsync_AsksForAZipOfTheNamedLogs()
	{
		var stub = new StubHandler();
		stub.EnqueueFile([0x50, 0x4B, 0x03, 0x04], "application/zip", "logs.zip");
		using var client = TestClient.Create(stub);

		using var content = await client.Administration.GetLogsAsync([ConsoleLog.Access, ConsoleLog.Audit, ConsoleLog.Auth, ConsoleLog.Nsc], TestContext.Current.CancellationToken);

		var call = stub.Calls.Should().ContainSingle().Subject;
		call.ShouldBe(HttpMethod.Get, "/api/3/administration/logs", "?name=access.log&name=audit.log&name=auth.log&name=nsc.log");
		call.Headers.Accept.ToString().Should().Be("application/zip");
		content.Headers.ContentType!.MediaType.Should().Be("application/zip");
		(await content.ReadAsByteArrayAsync(TestContext.Current.CancellationToken)).Should().Equal(0x50, 0x4B, 0x03, 0x04);
	}

	[Fact]
	public async Task GetPropertiesAsync_SendsGetToProperties()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.Administration.GetPropertiesAsync(ct), AdministrationJson.Properties);

		call.ShouldBe(HttpMethod.Get, "/api/3/administration/properties");
	}

	[Fact]
	public async Task GetSettingsAsync_SendsGetToSettings()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.Administration.GetSettingsAsync(ct), AdministrationJson.Settings);

		call.ShouldBe(HttpMethod.Get, "/api/3/administration/settings");
	}

	[Fact]
	public Task GetLicenseAsync_Forbidden_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync(
			(c, ct) => c.Administration.GetLicenseAsync(ct),
			HttpStatusCode.Forbidden,
			AccessJson.Error("FORBIDDEN", "Global Administrator privileges are required."),
			"Global Administrator privileges are required.");

	[Fact]
	public Task ExecuteCommandAsync_BadRequest_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync(
			(c, ct) => c.Administration.ExecuteCommandAsync("no-such-command", ct),
			HttpStatusCode.BadRequest,
			AccessJson.Error("BAD_REQUEST", "Unknown command."),
			"Unknown command.");

	[Fact]
	public async Task ReadOnlyClient_RefusesToActivateALicence()
	{
		var stub = new StubHandler();
		using var client = TestClient.Create(stub, o => o.ReadOnly = true);

		var act = () => client.Administration.ActivateLicenseAsync("FAKE-KEY", TestContext.Current.CancellationToken);

		await act.Should().ThrowAsync<Rapid7ReadOnlyException>();
		stub.Calls.Should().BeEmpty();
	}
}
