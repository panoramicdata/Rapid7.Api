using System.Net;
using Rapid7.Api.Test.Support;
using static Rapid7.Api.Test.Groups.VulnerabilityResultSamples;
using static Rapid7.Api.Test.Groups.VulnerabilitySamples;

namespace Rapid7.Api.Test.Groups;

public class RemediationsTests
{
	[Fact]
	public async Task ListSolutionsAsync_SendsGetToTheAssetVulnerabilitySolution()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.Remediations.ListSolutionsAsync(282, "ubuntu-cve-2017-9233", ct), List(MatchedSolutionJson));

		call.ShouldBe(HttpMethod.Get, "/api/3/assets/282/vulnerabilities/ubuntu-cve-2017-9233/solution");
	}

	[Fact]
	public async Task ListSolutionsAsync_MapsEveryField()
	{
		var list = await TestClient.ReadAsync((c, ct) => c.Remediations.ListSolutionsAsync(282, "ubuntu-cve-2017-9233", ct), List(MatchedSolutionJson));

		AssertMatchedSolution(list.Resources.Should().ContainSingle().Subject);
	}

	[Fact]
	public Task ListSolutionsAsync_NotFound_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync((c, ct) => c.Remediations.ListSolutionsAsync(282, "missing", ct), HttpStatusCode.NotFound, NotFound, NotFoundMessage);
}
