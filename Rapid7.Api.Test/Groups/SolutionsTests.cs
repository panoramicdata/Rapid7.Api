using System.Net;
using Rapid7.Api.Models;
using Rapid7.Api.Models.Vulnerabilities;
using Rapid7.Api.Test.Support;
using static Rapid7.Api.Test.Groups.VulnerabilitySamples;

namespace Rapid7.Api.Test.Groups;

public class SolutionsTests
{
	private const string Id = "ubuntu-upgrade-libexpat1";

	[Fact]
	public async Task ListAsync_SendsGetWithPaging()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.Solutions.ListAsync(new PageOptions { Page = 3 }, ct), Page(SolutionJson));

		call.ShouldBe(HttpMethod.Get, "/api/3/solutions", "?page=3");
	}

	[Fact]
	public async Task ListAsync_MapsThePage()
	{
		var page = await TestClient.ReadAsync((c, ct) => c.Solutions.ListAsync(null, ct), Page(SolutionJson));

		AssertPage(page);
		AssertSolution(page.Resources[0]);
	}

	[Fact]
	public async Task GetAsync_SendsGet_AndMapsTheSolution()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.Solutions.GetAsync(Id, ct), SolutionJson);

		call.ShouldBe(HttpMethod.Get, $"/api/3/solutions/{Id}");
		AssertSolution(await TestClient.ReadAsync((c, ct) => c.Solutions.GetAsync(Id, ct), SolutionJson));
	}

	[Fact]
	public async Task ListPrerequisitesAsync_SendsGet_AndMapsSolutionIds()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.Solutions.ListPrerequisitesAsync(Id, ct), StringIds);

		call.ShouldBe(HttpMethod.Get, $"/api/3/solutions/{Id}/prerequisites");
		AssertStringIds(await TestClient.ReadAsync((c, ct) => c.Solutions.ListPrerequisitesAsync(Id, ct), StringIds));
	}

	[Fact]
	public async Task ListSupersededAsync_SendsGet_AndMapsSolutions()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.Solutions.ListSupersededAsync(Id, ct), List(SolutionJson));
		var list = await TestClient.ReadAsync((c, ct) => c.Solutions.ListSupersededAsync(Id, ct), List(SolutionJson));

		call.ShouldBe(HttpMethod.Get, $"/api/3/solutions/{Id}/supersedes");
		AssertSolution(list.Resources.Should().ContainSingle().Subject);
	}

	[Theory]
	[InlineData(null, "")]
	[InlineData(true, "?rollup=true")]
	[InlineData(false, "?rollup=false")]
	public async Task ListSupersedingAsync_SendsGetWithTheRollupFlag(bool? rollup, string query)
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.Solutions.ListSupersedingAsync(Id, rollup, ct), List(SolutionJson));

		call.ShouldBe(HttpMethod.Get, $"/api/3/solutions/{Id}/superseding", query);
	}

	[Fact]
	public async Task ListSupersedingAsync_MapsSolutions()
	{
		var list = await TestClient.ReadAsync((c, ct) => c.Solutions.ListSupersedingAsync(Id, null, ct), List(SolutionJson));

		AssertSolution(list.Resources.Should().ContainSingle().Subject);
	}

	[Theory]
	[InlineData("configuration", SolutionType.Configuration)]
	[InlineData("patch", SolutionType.Patch)]
	[InlineData("unknown", SolutionType.Unknown)]
	public async Task GetAsync_ReadsEverySolutionType(string wire, SolutionType expected)
	{
		var solution = await TestClient.ReadAsync((c, ct) => c.Solutions.GetAsync(Id, ct), $$"""{"id":"x","type":"{{wire}}"}""");

		solution.Type.Should().Be(expected);
	}

	[Fact]
	public Task GetAsync_NotFound_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync((c, ct) => c.Solutions.GetAsync("missing", ct), HttpStatusCode.NotFound, NotFound, NotFoundMessage);
}
