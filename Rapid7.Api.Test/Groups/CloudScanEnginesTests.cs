using Rapid7.Api.Models;
using Rapid7.Api.Models.Cloud;
using Rapid7.Api.Test.Support;
using System.Net;
using System.Text.Json;

namespace Rapid7.Api.Test.Groups;

public class CloudScanEnginesTests
{
	private const string EnginePage = $$"""
		{
			"data": [{{CloudJson.ScanEngine}}],
			"metadata": { "number": 0, "size": 10, "totalResources": 1, "totalPages": 1 },
			"links": []
		}
		""";

	[Fact]
	public async Task ListAsync_SendsGetWithPaging()
	{
		var call = await TestClient.CaptureAsync(
			TestClient.CreateCloud,
			(c, ct) => c.ScanEngines.ListAsync(new PageOptions { Page = 0, Size = 10 }, ct),
			EnginePage);

		call.ShouldBe(HttpMethod.Get, "/vm/v4/integration/scan/engine", "?page=0&size=10");
	}

	[Fact]
	public async Task ListAsync_MapsEveryField()
	{
		var page = await TestClient.ReadAsync(TestClient.CreateCloud, (c, ct) => c.ScanEngines.ListAsync(null, ct), EnginePage);

		AssertEngine(page.Data.Should().ContainSingle().Subject);
	}

	[Fact]
	public async Task GetAsync_SendsGetWithTheId()
	{
		var call = await TestClient.CaptureAsync(
			TestClient.CreateCloud,
			(c, ct) => c.ScanEngines.GetAsync("6c384978-3545-455b-a69a-afa6e8cbd2dd", ct),
			CloudJson.ScanEngine);

		call.ShouldBe(HttpMethod.Get, "/vm/v4/integration/scan/engine/6c384978-3545-455b-a69a-afa6e8cbd2dd");
	}

	[Fact]
	public async Task GetAsync_MapsEveryField()
	{
		var engine = await TestClient.ReadAsync(TestClient.CreateCloud, (c, ct) => c.ScanEngines.GetAsync("e", ct), CloudJson.ScanEngine);

		AssertEngine(engine);
	}

	[Fact]
	public async Task GetAsync_ReadsTheSpecificationSpellingOfLastRetrieved()
	{
		var engine = await TestClient.ReadAsync(
			TestClient.CreateCloud,
			(c, ct) => c.ScanEngines.GetAsync("e", ct),
			"""{"profile":{"configuration":{"lastRetrieved":"2023-08-13T13:23:17.433Z","properties":[]}}}""");

		engine.Profile!.Configuration!.LastRetrieved.Should().Be(new DateTimeOffset(2023, 8, 13, 13, 23, 17, 433, TimeSpan.Zero));
	}

	[Fact]
	public void Configuration_IsWrittenWithOneLastRetrieved()
	{
		var configuration = new CloudScanEngineConfiguration { LastRetrieved = new DateTimeOffset(2023, 8, 13, 0, 0, 0, TimeSpan.Zero) };

		var json = JsonSerializer.Serialize(configuration, Rapid7Json.Options);

		json.Should().Be("""{"last_retrieved":"2023-08-13T00:00:00+00:00","properties":[]}""");
	}

	[Fact]
	public Task GetAsync_NotFound_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync(
			TestClient.CreateCloud,
			(c, ct) => c.ScanEngines.GetAsync("missing", ct),
			HttpStatusCode.NotFound,
			CloudJson.NotFound,
			"The requested resource does not exist.");

	[Fact]
	public async Task UpdateConfigurationAsync_PostsTheProperties()
	{
		var update = new CloudScanEngineConfigurationUpdate
		{
			Properties =
			[
				new CloudScanEngineProperty { Name = "com.rapid7.property1", Value = "Example" },
				new CloudScanEngineProperty { Name = "com.rapid7.property2", Value = "0" }
			]
		};

		var call = await TestClient.CaptureAsync(
			TestClient.CreateCloud,
			(c, ct) => c.ScanEngines.UpdateConfigurationAsync("6c384978", update, ct),
			CloudJson.Message);

		call.ShouldBe(
			HttpMethod.Post,
			"/vm/v4/integration/scan/engine/6c384978/configuration",
			body: """{"properties":[{"name":"com.rapid7.property1","value":"Example"},{"name":"com.rapid7.property2","value":"0"}]}""");
	}

	[Fact]
	public async Task UpdateConfigurationAsync_MapsTheMessage()
	{
		var message = await TestClient.ReadAsync(
			TestClient.CreateCloud,
			(c, ct) => c.ScanEngines.UpdateConfigurationAsync("e", new CloudScanEngineConfigurationUpdate { Properties = [] }, ct),
			CloudJson.Message);

		message.Message.Should().Be("A response message");
	}

	[Fact]
	public async Task RemoveConfigurationAsync_SendsDeleteWithTheNames()
	{
		var removal = new CloudScanEngineConfigurationRemoval { Properties = ["com.rapid7.exampleProperty1", "com.rapid7.exampleProperty2"] };

		var call = await TestClient.CaptureAsync(
			TestClient.CreateCloud,
			(c, ct) => c.ScanEngines.RemoveConfigurationAsync("6c384978", removal, ct),
			CloudJson.Message);

		call.ShouldBe(
			HttpMethod.Delete,
			"/vm/v4/integration/scan/engine/6c384978/configuration",
			body: """{"properties":["com.rapid7.exampleProperty1","com.rapid7.exampleProperty2"]}""");
	}

	[Fact]
	public async Task RemoveConfigurationAsync_IsRefusedByAReadOnlyClient()
	{
		var stub = new StubHandler();
		using var client = TestClient.CreateCloud(stub, o => o.ReadOnly = true);

		var act = () => client.ScanEngines.RemoveConfigurationAsync(
			"e",
			new CloudScanEngineConfigurationRemoval { Properties = ["p"] },
			TestContext.Current.CancellationToken);

		await act.Should().ThrowAsync<Rapid7ReadOnlyException>();
		stub.Calls.Should().BeEmpty();
	}

	private static void AssertEngine(CloudScanEngine engine)
	{
		engine.Id.Should().Be("6c384978-3545-455b-a69a-afa6e8cbd2dd");
		engine.Name.Should().Be("Scan Engine Example Name");
		engine.HostName.Should().Be("127.0.0.1");
		engine.Status.Should().Be("HEALTHY");
		engine.LastSeen.Should().Be(new DateTimeOffset(2021, 3, 2, 22, 29, 5, 6, TimeSpan.Zero));
		engine.Registered.Should().Be(new DateTimeOffset(2020, 9, 11, 18, 36, 49, 973, TimeSpan.Zero));
		var configuration = engine.Profile!.Configuration!;
		configuration.LastRetrieved.Should().Be(new DateTimeOffset(2023, 8, 13, 13, 23, 17, 433, TimeSpan.Zero));
		configuration.Properties.Should().Equal("property1:value1", "property2:value2");
	}
}
