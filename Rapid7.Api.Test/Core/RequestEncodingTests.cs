using Rapid7.Api.Models;
using Rapid7.Api.Test.Support;
using Refit;
using System.Globalization;
using System.Net;

namespace Rapid7.Api.Test.Core;

/// <summary>Pins how the clients encode query strings, path values, bodies and authentication.</summary>
public class RequestEncodingTests
{
	private static (IProbe Probe, StubHandler Stub) Create(string response = """{"resources":[],"links":[]}""")
	{
		var stub = TestClient.Stub(response);
		var client = new HttpClient(stub) { BaseAddress = new Uri(TestClient.ConsoleUrl) };
		return (RestService.For<IProbe>(client, Rapid7Pipeline.Settings), stub);
	}

	[Fact]
	public async Task PageOptions_AreFlattenedIntoTheQuery_WithRepeatedSorts()
	{
		var (probe, stub) = Create();

		await probe.ListAsync(new PageOptions { Page = 2, Size = 500, Sort = ["riskScore,DESC", "id"] }, TestContext.Current.CancellationToken);

		Uri.UnescapeDataString(stub.Calls[0].Uri.Query).Should().Be("?page=2&size=500&sort=riskScore,DESC&sort=id");
	}

	[Fact]
	public async Task NullPageOptions_SendNoQuery()
	{
		var (probe, stub) = Create();

		await probe.ListAsync(null, TestContext.Current.CancellationToken);

		stub.Calls[0].Uri.Query.Should().BeEmpty();
	}

	[Fact]
	public async Task Body_IsCompactJson_LeavingOutNulls_AndPathValuesAreOneSegment()
	{
		var (probe, stub) = Create("""{"id":42,"links":[{"href":"https://console.test:3780/api/3/probe/42","rel":"self"}]}""");

		var created = await probe.CreateAsync("a/b c", new Probe { Name = "x", Enabled = false }, TestContext.Current.CancellationToken);

		stub.Calls[0].ShouldBe(HttpMethod.Post, "/api/3/probe/a%2Fb%20c", body: """{"name":"x","enabled":false}""");
		created.Id.Should().Be(42);
		created.Items.Should().ContainSingle().Which.Rel.Should().Be("self");
	}

	[Fact]
	public async Task ConsoleClient_SendsBasicCredentials_TwoFactorToken_AndAcceptsJson()
	{
		var stub = TestClient.Stub("""{"links":[]}""");
		using var client = TestClient.Create(stub, o => o.TwoFactorToken = "123456");

		await client.Root.GetAsync(TestContext.Current.CancellationToken);

		var call = stub.Calls[0];
		call.Authorization.Should().Be("Basic " + Convert.ToBase64String("nxadmin:fake-password"u8.ToArray()));
		call.Headers.GetValues("Token").Should().Equal("123456");
		call.Headers.Accept.ToString().Should().Be("application/json");
	}

	[Theory]
	[InlineData(null)]
	[InlineData("   ")]
	public async Task ConsoleClient_WithoutATwoFactorToken_SendsNoTokenHeader(string? token)
	{
		var stub = TestClient.Stub("""{"links":[]}""");
		using var client = TestClient.Create(stub, o => o.TwoFactorToken = token);

		await client.Root.GetAsync(TestContext.Current.CancellationToken);

		stub.Calls[0].Headers.Contains("Token").Should().BeFalse();
	}

	[Fact]
	public async Task PlatformClients_SendTheApiKey_AndAcceptJson()
	{
		var stub = TestClient.Stub("""{"links":[]}""");
		using var cloud = TestClient.CreateCloud(stub);
		var probe = cloud.For<IProbe>();

		await probe.ListAsync(null, TestContext.Current.CancellationToken);

		var call = stub.Calls[0];
		call.Uri.ToString().Should().Be("https://us.api.insight.test/vm/api/3/probe");
		call.Headers.GetValues("X-Api-Key").Should().Equal("fake-api-key");
		call.Headers.Accept.ToString().Should().Be("application/json");
	}

	[Fact]
	public async Task BulkExportClient_SendsTheApiKey()
	{
		var stub = TestClient.Stub("""{"links":[]}""");
		using var export = TestClient.CreateBulkExport(stub);

		await export.For<IProbe>().ListAsync(null, TestContext.Current.CancellationToken);

		stub.Calls[0].Uri.ToString().Should().Be("https://us.api.insight.test/api/3/probe");
		stub.Calls[0].Headers.GetValues("X-Api-Key").Should().Equal("fake-api-key");
	}

	[Fact]
	public async Task ARequestThatChoosesItsOwnAccept_KeepsIt()
	{
		var console = TestClient.Stub("\"a,b\"");
		var cloud = TestClient.Stub("\"a,b\"");
		using var consoleClient = TestClient.Create(console);
		using var cloudClient = TestClient.CreateCloud(cloud);

		await consoleClient.For<IProbe>().DownloadAsync("report", TestContext.Current.CancellationToken);
		await cloudClient.For<IProbe>().DownloadAsync("report", TestContext.Current.CancellationToken);

		console.Calls[0].Headers.Accept.ToString().Should().Be("text/csv");
		cloud.Calls[0].Headers.Accept.ToString().Should().Be("text/csv");
	}

	[Fact]
	public async Task AnExistingApiKeyHeader_IsReplaced()
	{
		var stub = TestClient.Stub("{}");
		using var invoker = new HttpMessageInvoker(new Handlers.ApiKeyAuthenticationHandler("fake-api-key") { InnerHandler = stub });
		using var request = new HttpRequestMessage(HttpMethod.Get, TestClient.PlatformUrl + "v4/integration/assets");
		request.Headers.Add("X-Api-Key", "caller-supplied");

		using var response = await invoker.SendAsync(request, TestContext.Current.CancellationToken);

		stub.Calls[0].Headers.GetValues("X-Api-Key").Should().Equal("fake-api-key");
	}

	[Fact]
	public async Task AnExistingTokenHeader_IsReplaced()
	{
		var stub = TestClient.Stub("{}");
		using var invoker = new HttpMessageInvoker(new Handlers.BasicAuthenticationHandler("u", "p", "654321") { InnerHandler = stub });
		using var request = new HttpRequestMessage(HttpMethod.Get, TestClient.ConsoleUrl + "api/3");
		request.Headers.Add("Token", "stale");

		using var response = await invoker.SendAsync(request, TestContext.Current.CancellationToken);

		stub.Calls[0].Headers.GetValues("Token").Should().Equal("654321");
	}

	[Fact]
	public void UrlParameters_AreFormattedForTheWire_InTheInvariantCulture()
	{
		var formatter = new Serialization.Rapid7UrlParameterFormatter();
		var previous = CultureInfo.CurrentCulture;
		CultureInfo.CurrentCulture = new CultureInfo("de-DE");
		try
		{
			string? Format(object? value) => formatter.Format(value, typeof(RequestEncodingTests), value?.GetType() ?? typeof(object));

			Format(null).Should().BeNull();
			Format(true).Should().Be("true");
			Format(false).Should().Be("false");
			Format(DayOfWeek.Monday).Should().Be("Monday");
			Format(new DateTimeOffset(2026, 1, 2, 3, 4, 5, 6, TimeSpan.FromHours(1))).Should().Be("2026-01-02T03:04:05.006+01:00");
			Format(new DateTime(2026, 1, 2, 3, 4, 5, 6, DateTimeKind.Utc)).Should().Be("2026-01-02T03:04:05.006Z");
			Format(new DateOnly(2026, 1, 2)).Should().Be("2026-01-02");
			Format(1.5).Should().Be("1.5");
			Format("text").Should().Be("text");
		}
		finally
		{
			CultureInfo.CurrentCulture = previous;
		}
	}
}
