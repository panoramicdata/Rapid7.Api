using System.Net;
using Rapid7.Api.Models;
using Rapid7.Api.Test.Support;
using static Rapid7.Api.Test.Groups.AssetSamples;

namespace Rapid7.Api.Test.Groups;

public class AgentsTests
{
	private const string Agent = $$"""{"agentId":"fe1708451f8c78c3a20a8a79818878e1","lastAssessedForVulnerabilities":"2019-09-11T10:39:51.288Z",{{AssetFields}}}""";

	[Fact]
	public Task ListAsync_SendsGetWithPaging()
		=> SendsAsync(
			(c, ct) => c.Agents.ListAsync(new PageOptions { Page = 3, Sort = ["id,ASC", "riskScore,DESC"] }, ct),
			HttpMethod.Get,
			"/api/3/agents",
			"?page=3&sort=id%2CASC&sort=riskScore%2CDESC",
			response: EmptyPage);

	[Fact]
	public async Task ListAsync_MapsEveryField()
	{
		var page = await TestClient.ReadAsync((c, ct) => c.Agents.ListAsync(null, ct), Page(Agent));

		var agent = page.Resources.Should().ContainSingle().Subject;
		ShouldRoundTrip(agent, Agent);
		agent.AgentId.Should().Be("fe1708451f8c78c3a20a8a79818878e1");
		agent.LastAssessedForVulnerabilities.Should().Be(new DateTimeOffset(2019, 9, 11, 10, 39, 51, 288, TimeSpan.Zero));
		agent.Id.Should().Be(282);
	}

	[Fact]
	public Task ListAsync_Unauthorized_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync(
			(c, ct) => c.Agents.ListAsync(null, ct),
			HttpStatusCode.Unauthorized,
			"""{"status":"UNAUTHORIZED","message":"Full authentication is required to access this resource.","links":[]}""",
			"Full authentication is required to access this resource.");
}
