using Rapid7.Api.Models;
using Rapid7.Api.Models.Sites;

namespace Rapid7.Api.IntegrationTest.Sites;

[Collection(Rapid7TestGroup.Name)]
public class SitesIntegrationTests(Rapid7Fixture fixture)
{
	[Fact]
	public async Task ListAsync_ReadsAPageOfSites()
	{
		var page = await fixture.Client.Sites.ListAsync(new PageOptions { Size = 5, Sort = ["id,ASC"] }, TestContext.Current.CancellationToken);

		page.PageInfo.Should().NotBeNull();
		page.Resources.Should().HaveCountLessThanOrEqualTo(5);
		page.Resources.Should().OnlyContain(s => s.Id > 0 && !string.IsNullOrEmpty(s.Name));
	}

	[Fact]
	public Task CreateGetUpdateDelete_RoundTripsATestSite()
		=> ScratchSite.RunAsync(fixture, async (siteId, ct) =>
		{
			var site = await fixture.Client.Sites.GetAsync(siteId, ct);
			site.Name.Should().StartWith(Rapid7Fixture.Prefix);
			site.Type.Should().Be(SiteType.Static);
			site.Importance.Should().Be(SiteImportance.VeryLow);

			var newName = Rapid7Fixture.UniqueName("site-renamed");
			await fixture.Client.Sites.UpdateAsync(
				siteId,
				new SiteUpdateRequest
				{
					Name = newName,
					Description = "Updated by the Rapid7.Api integration tests.",
					Importance = SiteImportance.Low,
					EngineId = site.ScanEngine!.Value,
					ScanTemplateId = site.ScanTemplate!
				},
				ct);

			var updated = await fixture.Client.Sites.GetAsync(siteId, ct);
			updated.Name.Should().Be(newName);
			updated.Importance.Should().Be(SiteImportance.Low);
		});

	[Fact]
	public async Task DeleteAsync_RemovesTheSite()
	{
		var ct = TestContext.Current.CancellationToken;
		var deletedId = 0;
		await ScratchSite.RunAsync(fixture, (siteId, _) =>
		{
			deletedId = siteId;
			return Task.CompletedTask;
		});

		var act = () => fixture.Client.Sites.GetAsync(deletedId, ct);

		(await act.Should().ThrowAsync<Rapid7ApiException>()).Which.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
	}
}
