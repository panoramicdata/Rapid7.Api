using Rapid7.Api.Models.Cloud;
using Rapid7.Api.Test.Support;
using System.Net;

namespace Rapid7.Api.Test.Groups;

public class CloudHealthTests
{
	private const string HealthJson = """
		{
			"status": "UP",
			"duration": "PT0.012S",
			"components": {
				"database": { "description": "PostgreSQL", "status": "UP" },
				"search": { "description": "Search index", "status": "OUT-OF-SERVICE" }
			},
			"summary": { "up": ["database"], "down": ["cache"], "outOfService": ["search"], "unknown": ["queue"] }
		}
		""";

	[Fact]
	public async Task GetAsync_SendsGetToAdminHealth()
	{
		var call = await TestClient.CaptureAsync(TestClient.CreateCloud, (c, ct) => c.Health.GetAsync(ct), HealthJson);

		call.ShouldBe(HttpMethod.Get, "/vm/admin/health");
	}

	[Fact]
	public async Task GetAsync_MapsEveryField()
	{
		var health = await TestClient.ReadAsync(TestClient.CreateCloud, (c, ct) => c.Health.GetAsync(ct), HealthJson);

		health.Status.Should().Be(CloudHealthStatus.Up);
		health.Duration.Should().Be("PT0.012S");
		health.Components.Should().HaveCount(2);
		health.Components["database"].Description.Should().Be("PostgreSQL");
		health.Components["search"].Status.Should().Be("OUT-OF-SERVICE");
		health.Summary!.Up.Should().Equal("database");
		health.Summary.Down.Should().Equal("cache");
		health.Summary.OutOfService.Should().Equal("search");
		health.Summary.Unknown.Should().Equal("queue");
	}

	[Theory]
	[InlineData("DOWN", CloudHealthStatus.Down)]
	[InlineData("OUT-OF-SERVICE", CloudHealthStatus.OutOfService)]
	[InlineData("UNKNOWN", CloudHealthStatus.Unknown)]
	public async Task GetAsync_MapsTheSpecificationExample(string status, CloudHealthStatus expected)
	{
		var health = await TestClient.ReadAsync(
			TestClient.CreateCloud,
			(c, ct) => c.Health.GetAsync(ct),
			$$"""{"components":null,"status":"{{status}}","summary":null}""");

		health.Status.Should().Be(expected);
		health.Components.Should().BeEmpty();
		health.Summary.Should().BeNull();
	}

	[Fact]
	public Task GetAsync_Unauthorized_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync(
			TestClient.CreateCloud,
			(c, ct) => c.Health.GetAsync(ct),
			HttpStatusCode.Unauthorized,
			"""{"status":401,"localized_message":"Authorization is required.","message":"Authorization is required to interact with this resource."}""",
			"Authorization is required to interact with this resource.");
}
