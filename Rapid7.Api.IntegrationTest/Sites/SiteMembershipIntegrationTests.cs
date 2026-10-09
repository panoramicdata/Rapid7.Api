using System.Net;

namespace Rapid7.Api.IntegrationTest.Sites;

/// <summary>
/// Reads (and empty-list updates) of a throwaway site's assets, tags, users, discovery settings and web authentications.
/// Tags, users and assets are owned by other tests' categories, so none is created, added or removed here.
/// </summary>
[Collection(Rapid7TestGroup.Name)]
public class SiteMembershipIntegrationTests(Rapid7Fixture fixture)
{
	[Fact]
	public async Task Assets_OfANewSite_AreEmptyAndCanBeCleared()
	{
		var ct = TestContext.Current.CancellationToken;
		await using var site = await TestSite.CreateAsync(fixture.Client, ct);

		var page = await fixture.Client.SiteAssets.ListAsync(site.Id, new Models.PageOptions { Size = 10 }, ct);
		await fixture.Client.SiteAssets.RemoveAllAsync(site.Id, ct);

		page.Resources.Should().BeEmpty();
		page.PageInfo!.TotalResources.Should().Be(0);
	}

	[Fact]
	public async Task Tags_OfANewSite_AreEmptyAndCanBeReplaced()
	{
		var ct = TestContext.Current.CancellationToken;
		await using var site = await TestSite.CreateAsync(fixture.Client, ct);

		await fixture.Client.SiteTags.ReplaceAllAsync(site.Id, [], ct);

		(await fixture.Client.SiteTags.ListAsync(site.Id, ct)).Resources.Should().BeEmpty();
	}

	[Fact]
	public async Task Users_OfANewSite_CanBeReplacedWithNone()
	{
		var ct = TestContext.Current.CancellationToken;
		await using var site = await TestSite.CreateAsync(fixture.Client, ct);

		await fixture.Client.SiteUsers.ReplaceAllAsync(site.Id, [], ct);

		(await fixture.Client.SiteUsers.ListAsync(site.Id, ct)).Resources.Should().BeEmpty();
	}

	[Fact]
	public async Task WebAuthentication_OfANewSite_IsEmpty()
	{
		var ct = TestContext.Current.CancellationToken;
		await using var site = await TestSite.CreateAsync(fixture.Client, ct);

		(await fixture.Client.SiteWebAuthentication.ListHtmlFormsAsync(site.Id, ct)).Resources.Should().BeEmpty();
		(await fixture.Client.SiteWebAuthentication.ListHttpHeadersAsync(site.Id, ct)).Resources.Should().BeEmpty();
	}

	[Fact]
	public async Task DiscoveryConnection_OfAStaticSite_IsRead()
	{
		var ct = TestContext.Current.CancellationToken;
		await using var site = await TestSite.CreateAsync(fixture.Client, ct);

		var connection = await fixture.Client.SiteDiscovery.GetConnectionAsync(site.Id, ct);

		connection.Items.Should().NotBeEmpty();
	}

	[Fact]
	public async Task DiscoverySearchCriteria_OfAStaticSite_IsRefused()
	{
		var ct = TestContext.Current.CancellationToken;
		await using var site = await TestSite.CreateAsync(fixture.Client, ct);

		// Search criteria exist only for dynamic sites, which need a discovery connection this suite does not own.
		var act = () => fixture.Client.SiteDiscovery.GetSearchCriteriaAsync(site.Id, ct);

		(await act.Should().ThrowAsync<Rapid7ApiException>()).Which.StatusCode.Should().BeOneOf(HttpStatusCode.BadRequest, HttpStatusCode.NotFound);
	}
}
