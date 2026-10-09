using System.Net;
using Rapid7.Api.Test.Support;

namespace Rapid7.Api.Test.Groups;

public class RootTests
{
	// The shape the Security Console returns for GET /api/3 (abbreviated; host replaced).
	private const string RootJson = """
		{
			"links": [
				{ "href": "https://console.test:3780/api/3", "rel": "self" },
				{ "href": "https://console.test:3780/api/3/assets", "rel": "Assets" },
				{ "href": "https://console.test:3780/api/3/sites", "rel": "Sites" },
				{ "href": "https://console.test:3780/api/3/vulnerabilities", "rel": "Vulnerabilities", "title": "Vulnerabilities", "type": "application/json" }
			]
		}
		""";

	[Fact]
	public async Task GetAsync_SendsGetToTheApiRoot()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.Root.GetAsync(ct), RootJson);

		call.ShouldBe(HttpMethod.Get, "/api/3");
	}

	[Fact]
	public async Task GetAsync_MapsEveryLink()
	{
		var links = await TestClient.ReadAsync((c, ct) => c.Root.GetAsync(ct), RootJson);

		links.Links.Should().HaveCount(4);
		links.Links[0].Rel.Should().Be("self");
		links.Links[1].Href.Should().Be("https://console.test:3780/api/3/assets");
		links.Links[3].Title.Should().Be("Vulnerabilities");
		links.Links[3].Type.Should().Be("application/json");
		links.Links[3].ToString().Should().Be("Vulnerabilities: https://console.test:3780/api/3/vulnerabilities");
	}

	[Fact]
	public Task GetAsync_Unauthorized_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync(
			(c, ct) => c.Root.GetAsync(ct),
			HttpStatusCode.Unauthorized,
			"""{"status":"UNAUTHORIZED","message":"Full authentication is required to access this resource.","links":[]}""",
			"Full authentication is required to access this resource.");
}
