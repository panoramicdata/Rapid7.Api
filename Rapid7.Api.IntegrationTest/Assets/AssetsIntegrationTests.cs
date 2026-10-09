using Rapid7.Api.Models;
using Rapid7.Api.Models.Assets;

namespace Rapid7.Api.IntegrationTest.Assets;

/// <summary>
/// Reads assets, what was discovered on them and the catalogues. Nothing here changes an asset: importing, deleting and
/// tagging assets are only covered by the unit tests, because the console's assets are not the test's to change.
/// </summary>
[Collection(Rapid7TestGroup.Name)]
public class AssetsIntegrationTests(Rapid7Fixture fixture)
{
	private static CancellationToken Ct => TestContext.Current.CancellationToken;

	private Rapid7Client Client => fixture.Client;

	[Fact]
	public async Task ListAsync_ReturnsAPageOfAssets()
	{
		var page = await Client.Assets.ListAsync(new PageOptions { Size = 5, Sort = ["id,ASC"] }, Ct);

		page.PageInfo.Should().NotBeNull();
		page.Resources.Should().HaveCountLessThanOrEqualTo(5);
		page.Resources.Should().OnlyContain(a => a.Id > 0);
	}

	[Fact]
	public async Task ReadAllAsync_PagesThroughTheAssets()
	{
		var first = await Client.Assets.ListAsync(new PageOptions { Size = 1 }, Ct);

		var assets = await Rapid7Paging.ReadAllAsync((page, ct) => Client.Assets.ListAsync(page, ct), 2, Ct).Take(5).ToListAsync(Ct);

		assets.Should().HaveCount((int)Math.Min(5, first.PageInfo!.TotalResources));
		assets.Select(a => a.Id).Should().OnlyHaveUniqueItems();
	}

	[Fact]
	public async Task SearchAsync_FindsAssetsMatchingCriteria()
	{
		var criteria = new SearchCriteria
		{
			Match = SearchMatch.All,
			Filters = [new SearchFilter(SearchField.RiskScore, SearchOperator.IsGreaterThan) { Value = -1 }],
		};

		var everything = await Client.Assets.ListAsync(new PageOptions { Size = 1 }, Ct);
		var found = await Client.Assets.SearchAsync(criteria, new PageOptions { Size = 1 }, Ct);

		found.PageInfo!.TotalResources.Should().Be(everything.PageInfo!.TotalResources);
	}

	[Fact]
	public async Task SearchAsync_WithAnImpossibleCriterion_FindsNothing()
	{
		var criteria = new SearchCriteria
		{
			Match = SearchMatch.Any,
			Filters = [new SearchFilter(SearchField.HostName, SearchOperator.Is) { Value = Rapid7Fixture.UniqueName("no-such-host") }],
		};

		var found = await Client.Assets.SearchAsync(criteria, null, Ct);

		found.Resources.Should().BeEmpty();
	}

	[Fact]
	public async Task AssetDetails_ReadEverythingDiscoveredOnAnAsset()
	{
		var assetId = await FirstAssetIdAsync();

		var asset = await Client.Assets.GetAsync(assetId, Ct);
		var databases = await Client.AssetDetails.ListDatabasesAsync(assetId, Ct);
		var files = await Client.AssetDetails.ListFilesAsync(assetId, Ct);
		var software = await Client.AssetDetails.ListSoftwareAsync(assetId, Ct);
		var userGroups = await Client.AssetDetails.ListUserGroupsAsync(assetId, Ct);
		var users = await Client.AssetDetails.ListUsersAsync(assetId, Ct);
		var tags = await Client.AssetDetails.ListTagsAsync(assetId, Ct);

		asset.Id.Should().Be(assetId);
		databases.Resources.Should().OnlyContain(d => d.Name.Length > 0);
		files.Resources.Should().OnlyContain(f => f.Name.Length > 0);
		software.Resources.Should().OnlyContain(s => s.Id > 0);
		userGroups.Resources.Should().OnlyContain(g => g.Name.Length > 0);
		users.Should().NotBeNull();
		tags.Resources.Should().OnlyContain(t => t.Name.Length > 0);
	}

	[Fact]
	public async Task AssetServices_ReadAServiceAndWhatWasEnumeratedThroughIt()
	{
		var (assetId, service) = await FirstServiceAsync();
		var protocol = service.Protocol!.Value;
		var port = service.Port!.Value;

		var read = await Client.AssetServices.GetAsync(assetId, protocol, port, new AssetServiceOptions { Nic = service.Nic }, Ct);
		var configurations = await Client.AssetServices.ListConfigurationsAsync(assetId, protocol, port, Ct);
		var databases = await Client.AssetServices.ListDatabasesAsync(assetId, protocol, port, Ct);
		var userGroups = await Client.AssetServices.ListUserGroupsAsync(assetId, protocol, port, Ct);
		var users = await Client.AssetServices.ListUsersAsync(assetId, protocol, port, Ct);
		var applications = await Client.AssetServices.ListWebApplicationsAsync(assetId, protocol, port, Ct);

		read.Port.Should().Be(port);
		read.Protocol.Should().Be(protocol);
		configurations.Resources.Should().OnlyContain(c => c.Name.Length > 0);
		databases.Should().NotBeNull();
		userGroups.Should().NotBeNull();
		users.Should().NotBeNull();
		foreach (var reference in applications.Resources.Take(1))
		{
			var application = await Client.AssetServices.GetWebApplicationAsync(assetId, protocol, port, reference.Id!.Value, Ct);
			application.Id.Should().Be(reference.Id);
		}
	}

	[Fact]
	public async Task AssetCatalog_ListsAndReadsOperatingSystems()
	{
		var page = await Client.AssetCatalog.ListOperatingSystemsAsync(new PageOptions { Size = 5 }, Ct);

		foreach (var os in page.Resources.Take(1))
		{
			var read = await Client.AssetCatalog.GetOperatingSystemAsync(os.Id!.Value, Ct);
			read.Description.Should().Be(os.Description);
		}

		page.PageInfo.Should().NotBeNull();
	}

	[Fact]
	public async Task AssetCatalog_ListsAndReadsSoftware()
	{
		var page = await Client.AssetCatalog.ListSoftwareAsync(new PageOptions { Size = 5 }, Ct);

		foreach (var software in page.Resources.Take(1))
		{
			var read = await Client.AssetCatalog.GetSoftwareAsync(software.Id!.Value, Ct);
			read.Product.Should().Be(software.Product);
		}

		page.PageInfo.Should().NotBeNull();
	}

	[Fact]
	public async Task GetAsync_UnknownAsset_RaisesNotFound()
	{
		var act = () => Client.Assets.GetAsync(long.MaxValue, Ct);

		(await act.Should().ThrowAsync<Rapid7ApiException>()).Which.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
	}

	private async Task<long> FirstAssetIdAsync()
	{
		var page = await Client.Assets.ListAsync(new PageOptions { Size = 1 }, Ct);
		return page.Resources.Should().ContainSingle("the console must hold at least one asset").Subject.Id!.Value;
	}

	private async Task<(long AssetId, ServiceReference Service)> FirstServiceAsync()
	{
		var criteria = new SearchCriteria
		{
			Match = SearchMatch.All,
			Filters = [new SearchFilter(SearchField.OpenPorts, SearchOperator.InRange) { Lower = 1, Upper = 65535 }],
		};
		var page = await Client.Assets.SearchAsync(criteria, new PageOptions { Size = 1 }, Ct);
		var assetId = page.Resources.Should().ContainSingle("the console must hold an asset with an open port").Subject.Id!.Value;
		var services = await Client.AssetServices.ListAsync(assetId, Ct);
		return (assetId, services.Resources.Should().NotBeEmpty().And.Subject.First());
	}
}
