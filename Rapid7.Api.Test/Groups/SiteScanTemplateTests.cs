using Rapid7.Api.Test.Support;

namespace Rapid7.Api.Test.Groups;

public class SiteScanTemplateTests
{
	[Fact]
	public async Task GetAsync_SendsGetToTheSiteScanTemplate()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteScanTemplate.GetAsync(SiteFixtures.SiteId, ct), ScanTemplateJson.Full);

		call.ShouldBe(HttpMethod.Get, "/api/3/sites/7/scan_template");
	}

	[Fact]
	public async Task GetAsync_MapsTheTemplateLikeTheScanTemplatesEndpoint()
	{
		// ScanTemplatesTests asserts every field of this template; the site's template is the same type.
		var template = await TestClient.ReadAsync((c, ct) => c.SiteScanTemplate.GetAsync(SiteFixtures.SiteId, ct), ScanTemplateJson.Full);
		var expected = await TestClient.ReadAsync((c, ct) => c.ScanTemplates.GetAsync("full-audit-without-web-spider", ct), ScanTemplateJson.Full);

		template.Should().BeEquivalentTo(expected, options => options.Excluding(t => t.Web));
		template.Web!.Value.GetRawText().Should().Be(expected.Web!.Value.GetRawText());
		template.Id.Should().Be("full-audit-without-web-spider");
	}

	[Fact]
	public async Task SetAsync_PutsTheTemplateIdAsAJsonString()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteScanTemplate.SetAsync(SiteFixtures.SiteId, "discovery", ct), SiteFixtures.LinksJson);

		call.ShouldBe(HttpMethod.Put, "/api/3/sites/7/scan_template", body: "\"discovery\"");
	}

	[Fact]
	public async Task SetAsync_MapsTheLinks()
	{
		var links = await TestClient.ReadAsync((c, ct) => c.SiteScanTemplate.SetAsync(SiteFixtures.SiteId, "discovery", ct), SiteFixtures.LinksJson);

		links.ShouldBeSiteLinks();
	}

	[Fact]
	public Task GetAsync_NotFound_RaisesRapid7ApiException()
		=> SiteFixtures.ShouldRaiseNotFoundAsync((c, ct) => c.SiteScanTemplate.GetAsync(SiteFixtures.SiteId, ct));
}
