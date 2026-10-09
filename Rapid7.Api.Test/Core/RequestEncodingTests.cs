using System.Net;
using System.Text.Json.Serialization;
using Rapid7.Api.Models;
using Rapid7.Api.Test.Support;
using Refit;

namespace Rapid7.Api.Test.Core;

/// <summary>Pins how the clients encode query strings, path values, bodies and authentication.</summary>
public class RequestEncodingTests
{
	public interface IProbe
	{
		[Get("api/3/probe")]
		Task<Page<Probe>> ListAsync([Query] PageOptions? options, CancellationToken cancellationToken);

		[Post("api/3/probe/{name}")]
		Task<CreatedReference<int>> CreateAsync(string name, [Body] Probe body, CancellationToken cancellationToken);
	}

	public sealed class Probe
	{
		[JsonPropertyName("name")]
		public string? Name { get; init; }

		[JsonPropertyName("enabled")]
		public bool? Enabled { get; init; }

		[JsonPropertyName("note")]
		public string? Note { get; init; }
	}

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

	[Fact]
	public async Task PlatformClients_SendTheApiKey_AndAcceptJson()
	{
		var stub = new StubHandler();
		stub.Enqueue(HttpStatusCode.OK, """{"links":[]}""");
		using var cloud = TestClient.CreateCloud(stub);
		var probe = cloud.For<IProbe>();

		await probe.ListAsync(null, TestContext.Current.CancellationToken);

		var call = stub.Calls[0];
		call.Uri.ToString().Should().Be("https://us.api.insight.test/vm/api/3/probe");
		call.Headers.GetValues("X-Api-Key").Should().Equal("fake-api-key");
		call.Headers.Accept.ToString().Should().Be("application/json");
	}
}
