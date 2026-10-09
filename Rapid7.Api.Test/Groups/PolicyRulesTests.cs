using Rapid7.Api.Models;
using Rapid7.Api.Models.Policies;
using Rapid7.Api.Test.Support;
using System.Net;

namespace Rapid7.Api.Test.Groups;

public class PolicyRulesTests
{
	private const string Html = "<p>Set <code>PasswordHistorySize</code> to 24.</p>";

	private static readonly PageOptions Paging = new() { Page = 2, Size = 25 };

	private const string PagingQuery = "?page=2&size=25";

	[Fact]
	public async Task GetRulesAsync_SendsPaging()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.PolicyRules.GetRulesAsync(84, Paging, ct), PolicyJson.RulePage);

		call.ShouldBe(HttpMethod.Get, "/api/3/policies/84/rules", PagingQuery);
	}

	[Fact]
	public async Task GetRulesAsync_MapsEveryField()
	{
		var page = await TestClient.ReadAsync((c, ct) => c.PolicyRules.GetRulesAsync(84, null, ct), PolicyJson.RulePage);

		page.Resources.Should().ContainSingle().Which.ShouldBeExampleRule();
	}

	[Fact]
	public async Task GetDisabledRulesAsync_SendsPaging()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.PolicyRules.GetDisabledRulesAsync(84, Paging, ct), PolicyJson.RulePage);

		call.ShouldBe(HttpMethod.Get, "/api/3/policies/84/rules/disabled", PagingQuery);
	}

	[Fact]
	public async Task GetRuleAsync_SendsGet()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.PolicyRules.GetRuleAsync(84, 53, ct), PolicyJson.Rule);

		call.ShouldBe(HttpMethod.Get, "/api/3/policies/84/rules/53");
	}

	[Fact]
	public async Task GetRuleAsync_MapsEveryField()
		=> (await TestClient.ReadAsync((c, ct) => c.PolicyRules.GetRuleAsync(84, 53, ct), PolicyJson.Rule)).ShouldBeExampleRule();

	[Fact]
	public async Task GetAssetResultsAsync_SendsApplicableOnly()
	{
		var options = new PolicyResultListOptions { ApplicableOnly = false };

		var call = await TestClient.CaptureAsync((c, ct) => c.PolicyRules.GetAssetResultsAsync(84, 53, options, ct), PolicyJson.AssetPage);

		call.ShouldBe(HttpMethod.Get, "/api/3/policies/84/rules/53/assets", "?applicableOnly=false");
	}

	[Fact]
	public async Task GetAssetResultAsync_SendsGet()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.PolicyRules.GetAssetResultAsync(84, 53, 282, ct), PolicyJson.Asset);

		call.ShouldBe(HttpMethod.Get, "/api/3/policies/84/rules/53/assets/282");
	}

	[Fact]
	public async Task GetProofAsync_AsksForHtml()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.PolicyRules.GetProofAsync(84, 53, 282, ct), Html);

		call.ShouldBe(HttpMethod.Get, "/api/3/policies/84/rules/53/assets/282/proof");
		call.Headers.Accept.ToString().Should().Be("text/html");
	}

	[Fact]
	public async Task GetProofAsync_ReturnsTheHtml()
		=> (await TestClient.ReadAsync((c, ct) => c.PolicyRules.GetProofAsync(84, 53, 282, ct), Html)).Should().Be(Html);

	[Fact]
	public async Task GetControlsAsync_SendsPaging()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.PolicyRules.GetControlsAsync(84, 53, Paging, ct), PolicyJson.ControlPage);

		call.ShouldBe(HttpMethod.Get, "/api/3/policies/84/rules/53/controls", PagingQuery);
	}

	[Fact]
	public async Task GetControlsAsync_MapsEveryField()
	{
		var page = await TestClient.ReadAsync((c, ct) => c.PolicyRules.GetControlsAsync(84, 53, null, ct), PolicyJson.ControlPage);

		page.Resources.Should().ContainSingle().Which.Should().BeEquivalentTo(new
		{
			CceItemId = "CCE-35219-5",
			CcePlatform = "cpe:/o:example:example_server",
			ControlName = "AC-7",
			Id = "AC-7",
			Links = Array.Empty<object>(),
			PublishedDate = 1388534400000L
		});
	}

	[Fact]
	public async Task GetRationaleAsync_AsksForHtml()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.PolicyRules.GetRationaleAsync(84, 53, ct), Html);

		call.ShouldBe(HttpMethod.Get, "/api/3/policies/84/rules/53/rationale");
		call.Headers.Accept.ToString().Should().Be("text/html");
	}

	[Fact]
	public async Task GetRemediationAsync_AsksForHtml()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.PolicyRules.GetRemediationAsync(84, 53, ct), Html);

		call.ShouldBe(HttpMethod.Get, "/api/3/policies/84/rules/53/remediation");
		call.Headers.Accept.ToString().Should().Be("text/html");
	}

	[Fact]
	public Task GetRuleAsync_NotFound_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync((c, ct) => c.PolicyRules.GetRuleAsync(84, 999, ct), HttpStatusCode.NotFound, PolicyJson.NotFound, PolicyJson.NotFoundMessage);
}
