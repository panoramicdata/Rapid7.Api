using Rapid7.Api.Models.Sites;

namespace Rapid7.Api.IntegrationTest.Sites;

/// <summary>A test site's organization details, scan engine and scan template.</summary>
[Collection(Rapid7TestGroup.Name)]
public class SiteSettingsIntegrationTests(Rapid7Fixture fixture)
{
	[Fact]
	public Task Organization_ReadsAndUpdates()
		=> ScratchSite.RunAsync(fixture, async (siteId, ct) =>
		{
			await fixture.Client.SiteOrganization.GetAsync(siteId, ct);

			await fixture.Client.SiteOrganization.UpdateAsync(
				siteId,
				new SiteOrganization { Name = "Rapid7.Api tests", Email = "tests@example.test", City = "London" },
				ct);

			var organization = await fixture.Client.SiteOrganization.GetAsync(siteId, ct);
			organization.Name.Should().Be("Rapid7.Api tests");
			organization.Email.Should().Be("tests@example.test");
			organization.City.Should().Be("London");
		});

	[Fact]
	public Task ScanEngine_ReadsAndSetsTheSameEngine()
		=> ScratchSite.RunAsync(fixture, async (siteId, ct) =>
		{
			var engine = await fixture.Client.SiteScanEngine.GetAsync(siteId, ct);
			engine.Id.Should().NotBeNull();

			await fixture.Client.SiteScanEngine.SetAsync(siteId, engine.Id.Value, ct);

			(await fixture.Client.SiteScanEngine.GetAsync(siteId, ct)).Id.Should().Be(engine.Id);
		});

	[Fact]
	public Task ScanTemplate_ReadsAndSetsTheSameTemplate()
		=> ScratchSite.RunAsync(fixture, async (siteId, ct) =>
		{
			var template = await fixture.Client.SiteScanTemplate.GetAsync(siteId, ct);
			template.Id.Should().NotBeNullOrEmpty();

			await fixture.Client.SiteScanTemplate.SetAsync(siteId, template.Id, ct);

			(await fixture.Client.SiteScanTemplate.GetAsync(siteId, ct)).Id.Should().Be(template.Id);
		});
}
