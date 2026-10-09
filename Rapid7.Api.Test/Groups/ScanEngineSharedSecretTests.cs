using Rapid7.Api.Test.Support;
using System.Net;

namespace Rapid7.Api.Test.Groups;

public class ScanEngineSharedSecretTests
{
	private const string Secret = "8A1B-C2D3-E4F5-0617-2839";

	[Fact]
	public async Task GetAsync_SendsGet_AcceptingPlainText()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.ScanEngineSharedSecret.GetAsync(ct), Secret);

		call.ShouldBe(HttpMethod.Get, "/api/3/scan_engines/shared_secret");
		call.Headers.Accept.ToString().Should().Be("text/plain");
	}

	[Fact]
	public async Task GetOrCreateAsync_SendsPost_AcceptingPlainText()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.ScanEngineSharedSecret.GetOrCreateAsync(ct), Secret);

		// With a [Headers] attribute Refit sends a bodiless POST as empty, untyped content (Content-Length: 0).
		call.Method.Should().Be(HttpMethod.Post);
		call.Uri.AbsolutePath.Should().Be("/api/3/scan_engines/shared_secret");
		call.Uri.Query.Should().BeEmpty();
		call.Body.Should().BeEmpty();
		call.ContentType.Should().BeNull();
		call.Headers.Accept.ToString().Should().Be("text/plain");
	}

	[Fact]
	public async Task RevokeAsync_SendsDelete()
		=> (await TestClient.CaptureAsync((c, ct) => c.ScanEngineSharedSecret.RevokeAsync(ct), ScanJson.LinksOnly))
			.ShouldBe(HttpMethod.Delete, "/api/3/scan_engines/shared_secret");

	[Fact]
	public async Task GetTimeToLiveAsync_SendsGet()
		=> (await TestClient.CaptureAsync((c, ct) => c.ScanEngineSharedSecret.GetTimeToLiveAsync(ct), "3600"))
			.ShouldBe(HttpMethod.Get, "/api/3/scan_engines/shared_secret/time_to_live");

	[Fact]
	public async Task GetAsync_ReturnsTheSecretText()
		=> (await TestClient.ReadAsync((c, ct) => c.ScanEngineSharedSecret.GetAsync(ct), Secret)).Should().Be(Secret);

	[Fact]
	public async Task GetOrCreateAsync_ReturnsTheSecretText()
		=> (await TestClient.ReadAsync((c, ct) => c.ScanEngineSharedSecret.GetOrCreateAsync(ct), Secret)).Should().Be(Secret);

	[Fact]
	public async Task GetTimeToLiveAsync_ReturnsSeconds()
		=> (await TestClient.ReadAsync((c, ct) => c.ScanEngineSharedSecret.GetTimeToLiveAsync(ct), "3599")).Should().Be(3599);

	[Fact]
	public Task GetAsync_NoSecret_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync(
			(c, ct) => c.ScanEngineSharedSecret.GetAsync(ct),
			HttpStatusCode.NotFound,
			"""{"status":"NOT_FOUND","message":"No shared secret exists.","links":[]}""",
			"No shared secret exists.");
}
