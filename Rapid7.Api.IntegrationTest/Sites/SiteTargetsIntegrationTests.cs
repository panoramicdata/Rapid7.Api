namespace Rapid7.Api.IntegrationTest.Sites;

/// <summary>Target round trips on a throwaway site, using documentation-only addresses (RFC 5737); nothing is scanned.</summary>
[Collection(Rapid7TestGroup.Name)]
public class SiteTargetsIntegrationTests(Rapid7Fixture fixture)
{
	[Fact]
	public Task IncludedTargets_RoundTrip()
		=> TestSite.UsingAsync(fixture.Client, async (site, ct) =>
		{
			var targets = fixture.Client.SiteTargets;

			await targets.AddIncludedTargetsAsync(site.Id, ["192.0.2.10", "192.0.2.11"], ct);
			(await targets.GetIncludedTargetsAsync(site.Id, ct)).Addresses.Should().BeEquivalentTo("192.0.2.10", "192.0.2.11");

			await targets.RemoveIncludedTargetsAsync(site.Id, ["192.0.2.11"], ct);
			(await targets.GetIncludedTargetsAsync(site.Id, ct)).Addresses.Should().Equal("192.0.2.10");

			await targets.ReplaceIncludedTargetsAsync(site.Id, ["198.51.100.0/30"], ct);
			(await targets.GetIncludedTargetsAsync(site.Id, ct)).Addresses.Should().Equal("198.51.100.0/30");
		});

	[Fact]
	public Task ExcludedTargets_RoundTrip()
		=> TestSite.UsingAsync(fixture.Client, async (site, ct) =>
		{
			var targets = fixture.Client.SiteTargets;

			await targets.AddExcludedTargetsAsync(site.Id, ["192.0.2.20", "192.0.2.21"], ct);
			(await targets.GetExcludedTargetsAsync(site.Id, ct)).Addresses.Should().BeEquivalentTo("192.0.2.20", "192.0.2.21");

			await targets.RemoveExcludedTargetsAsync(site.Id, ["192.0.2.21"], ct);
			(await targets.GetExcludedTargetsAsync(site.Id, ct)).Addresses.Should().Equal("192.0.2.20");

			await targets.ReplaceExcludedTargetsAsync(site.Id, ["203.0.113.5"], ct);
			(await targets.GetExcludedTargetsAsync(site.Id, ct)).Addresses.Should().Equal("203.0.113.5");
		});

	[Fact]
	public Task AssetGroups_OfANewSite_AreEmptyAndCanBeCleared()
		=> TestSite.UsingAsync(fixture.Client, async (site, ct) =>
		{
			var targets = fixture.Client.SiteTargets;

			// Only this site's own (empty) lists are changed; no asset group is created or referenced.
			await targets.ReplaceIncludedAssetGroupsAsync(site.Id, [], ct);
			await targets.ReplaceExcludedAssetGroupsAsync(site.Id, [], ct);
			await targets.RemoveAllIncludedAssetGroupsAsync(site.Id, ct);
			await targets.RemoveAllExcludedAssetGroupsAsync(site.Id, ct);

			(await targets.ListIncludedAssetGroupsAsync(site.Id, ct)).Resources.Should().BeEmpty();
			(await targets.ListExcludedAssetGroupsAsync(site.Id, ct)).Resources.Should().BeEmpty();
		});
}
