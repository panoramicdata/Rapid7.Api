using Rapid7.Api.Models;
using Rapid7.Api.Models.AssetDiscovery;

namespace Rapid7.Api.IntegrationTest.AssetDiscovery;

/// <summary>
/// Reads discovery connections and round-trips a Sonar query the test creates. Reconnecting a discovery connection is
/// only covered by the unit tests: the connections are not the test's to disturb.
/// </summary>
[Collection(Rapid7TestGroup.Name)]
public class AssetDiscoveryIntegrationTests(Rapid7Fixture fixture)
{
	private static CancellationToken Ct => TestContext.Current.CancellationToken;

	private Rapid7Client Client => fixture.Client;

	private static SonarCriteria DomainCriteria(string domain) => new()
	{
		Filters =
		[
			new SonarCriterion { Type = SonarCriterionType.DomainContains, Domain = domain },
			new SonarCriterion { Type = SonarCriterionType.ScanDateWithinTheLast, Days = 30 },
		],
	};

	[Fact]
	public async Task DiscoveryConnections_ListAndRead()
	{
		var page = await Client.DiscoveryConnections.ListAsync(new PageOptions { Size = 10 }, Ct);

		foreach (var connection in page.Resources.Take(1))
		{
			var read = await Client.DiscoveryConnections.GetAsync(connection.Id!.Value, Ct);
			read.Name.Should().Be(connection.Name);
		}

		page.PageInfo.Should().NotBeNull();
	}

	[Fact]
	public async Task SonarQueries_List()
	{
		var list = await Client.SonarQueries.ListAsync(Ct);

		list.Resources.Should().OnlyContain(q => q.Id > 0);
	}

	[Fact]
	public async Task SonarQuery_RoundTrip()
	{
		var name = Rapid7Fixture.UniqueName("sonar");
		var created = await Client.SonarQueries.CreateAsync(new SonarQueryRequest { Name = name, Criteria = DomainCriteria("example.com") }, Ct);
		var id = created.Id;
		try
		{
			var read = await Client.SonarQueries.GetAsync(id, Ct);
			read.Name.Should().Be(name);
			read.Criteria!.Filters.Should().Contain(f => f.Type == SonarCriterionType.DomainContains && f.Domain == "example.com");

			await Client.SonarQueries.UpdateAsync(id, new SonarQueryRequest { Name = name + "-renamed", Criteria = DomainCriteria("example.org") }, Ct);
			var updated = await Client.SonarQueries.GetAsync(id, Ct);
			updated.Name.Should().Be(name + "-renamed");

			var list = await Client.SonarQueries.ListAsync(Ct);
			list.Resources.Should().Contain(q => q.Id == id);

			var assets = await Client.SonarQueries.ListAssetsAsync(id, Ct);
			assets.Resources.Should().OnlyContain(a => a.Address != null || a.Name != null);
		}
		finally
		{
			await Client.SonarQueries.DeleteAsync(id, CancellationToken.None);
		}

		var act = () => Client.SonarQueries.GetAsync(id, Ct);
		(await act.Should().ThrowAsync<Rapid7ApiException>()).Which.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
	}

	[Fact]
	public async Task SonarQueries_SearchWithoutSaving()
	{
		var assets = await Client.SonarQueries.SearchAsync(DomainCriteria("example.com"), Ct);

		assets.Should().OnlyContain(a => a.Address != null || a.Name != null);
	}
}
