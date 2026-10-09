using Rapid7.Api.Models;
using Rapid7.Api.Models.Assets;
using Rapid7.Api.Test.Support;
using System.Net;
using System.Text.Json;
using static Rapid7.Api.Test.Groups.AssetSamples;

namespace Rapid7.Api.Test.Groups;

public class AssetsTests
{
	private static readonly PageOptions Paging = new() { Page = 1, Size = 50, Sort = ["riskScore,DESC"] };

	private const string PagingQuery = "?page=1&size=50&sort=riskScore%2CDESC";

	private const string CreatedJson = $$"""{"id":282,"links":[{{SelfLink}}]}""";

	[Fact]
	public Task ListAsync_SendsGetWithPaging()
		=> SendsAsync((c, ct) => c.Assets.ListAsync(Paging, ct), HttpMethod.Get, "/api/3/assets", PagingQuery, response: EmptyPage);

	[Fact]
	public Task SearchAsync_PostsTheCriteriaWithPaging()
		=> SendsAsync(
			(c, ct) => c.Assets.SearchAsync(
				new SearchCriteria
				{
					Match = SearchMatch.All,
					Filters =
					[
						new SearchFilter(SearchField.RiskScore, SearchOperator.IsGreaterThan) { Value = 5000 },
						new SearchFilter(SearchField.IpAddress, SearchOperator.InRange) { Lower = "192.0.2.1", Upper = "192.0.2.254" },
						new SearchFilter(SearchField.SiteId, SearchOperator.In) { Values = [1, 2] },
						new SearchFilter(SearchField.OperatingSystem, SearchOperator.IsNotEmpty),
					],
				},
				Paging,
				ct),
			HttpMethod.Post,
			"/api/3/assets/search",
			PagingQuery,
			"""{"match":"all","filters":[{"field":"risk-score","operator":"is-greater-than","value":5000},{"field":"ip-address","operator":"in-range","lower":"192.0.2.1","upper":"192.0.2.254"},{"field":"site-id","operator":"in","values":[1,2]},{"field":"operating-system","operator":"is-not-empty"}]}""",
			EmptyPage);

	[Fact]
	public Task GetAsync_SendsGet()
		=> SendsAsync((c, ct) => c.Assets.GetAsync(282, ct), HttpMethod.Get, "/api/3/assets/282", response: AssetJson);

	[Fact]
	public Task DeleteAsync_SendsDelete()
		=> SendsAsync((c, ct) => c.Assets.DeleteAsync(282, ct), HttpMethod.Delete, "/api/3/assets/282");

	[Fact]
	public Task CreateAsync_PostsTheAssetToTheSite()
		=> SendsAsync(
			(c, ct) => c.Assets.CreateAsync(
				7,
				new AssetCreateRequest
				{
					Date = new DateTimeOffset(2026, 10, 1, 12, 30, 0, TimeSpan.Zero),
					Description = "Imported from CMDB",
					HostName = "workstation-1.example.test",
					Ip = "192.0.2.10",
					Mac = "AB:12:CD:34:EF:56",
					Os = "Ubuntu Linux 24.04",
					Type = AssetType.Physical,
					Cpe = "cpe:/o:canonical:ubuntu_linux:24.04",
					Addresses = [new Address { Ip = "192.0.2.11" }],
					HostNames = [new HostName { Name = "ws1", Source = HostNameSource.User }],
					Ids = [new UniqueId { Id = "abc", Source = "CMDB" }],
					Configurations = [new Configuration { Name = "owner", Value = "it" }],
					Databases = [new Database { Name = "pg" }],
					Files = [new AssetFile { Name = "etc", Type = AssetFileType.Directory }],
					UserGroups = [new GroupAccount { Name = "wheel" }],
					Users = [new UserAccount { Name = "root" }],
					Services = [new Service { Port = 22, Protocol = ServiceProtocol.Tcp, Name = "SSH" }],
					Software = [new Software { Product = "OpenSSH" }],
				},
				ct),
			HttpMethod.Post,
			"/api/3/sites/7/assets",
			body: """{"date":"2026-10-01T12:30:00+00:00","description":"Imported from CMDB","cpe":"cpe:/o:canonical:ubuntu_linux:24.04","addresses":[{"ip":"192.0.2.11"}],"configurations":[{"name":"owner","value":"it"}],"databases":[{"name":"pg"}],"files":[{"attributes":[],"name":"etc","type":"directory"}],"hostNames":[{"name":"ws1","source":"user"}],"ids":[{"id":"abc","source":"CMDB"}],"services":[{"configurations":[],"databases":[],"name":"SSH","port":22,"protocol":"tcp","userGroups":[],"users":[],"webApplications":[],"links":[]}],"software":[{"configurations":[],"product":"OpenSSH"}],"userGroups":[{"name":"wheel"}],"users":[{"name":"root"}],"hostName":"workstation-1.example.test","ip":"192.0.2.10","mac":"AB:12:CD:34:EF:56","os":"Ubuntu Linux 24.04","type":"physical"}""",
			response: CreatedJson);

	[Fact]
	public async Task CreateAsync_ReturnsTheAssetId()
	{
		var created = await TestClient.ReadAsync(
			(c, ct) => c.Assets.CreateAsync(7, new AssetCreateRequest { Date = DateTimeOffset.UnixEpoch }, ct),
			CreatedJson);

		created.Id.Should().Be(282L);
		created.Items.Should().ContainSingle().Which.Rel.Should().Be("self");
	}

	[Fact]
	public async Task GetAsync_MapsEveryField()
	{
		var asset = await TestClient.ReadAsync((c, ct) => c.Assets.GetAsync(282, ct), AssetJson);

		ShouldRoundTrip(asset, AssetJson);
		asset.Id.Should().Be(282L);
		asset.Type.Should().Be(AssetType.Physical);
		asset.RiskScore.Should().Be(37457.16);
		asset.History[0].Date.Should().Be(new DateTimeOffset(2018, 4, 9, 6, 23, 49, TimeSpan.Zero));
		asset.HostNames[0].Source.Should().Be(HostNameSource.Dns);
		asset.Files[0].Type.Should().Be(AssetFileType.Directory);
		asset.OsFingerprint!.Cpe!.Part.Should().Be(CpePart.OperatingSystem);
		asset.Software[0].Cpe!.Part.Should().Be(CpePart.Application);
		asset.Services[0].Protocol.Should().Be(ServiceProtocol.Tcp);
		asset.Services[0].WebApplications[0].Pages[0].LinkType.Should().Be(WebPageLinkType.HtmlReference);
		asset.Vulnerabilities!.Exploits.Should().Be(4);
		asset.Links.Should().ContainSingle().Which.Rel.Should().Be("self");
	}

	[Fact]
	public async Task ListAsync_MapsThePage()
	{
		var page = await TestClient.ReadAsync((c, ct) => c.Assets.ListAsync(null, ct), Page(AssetJson));

		page.PageInfo!.TotalResources.Should().Be(1);
		ShouldRoundTrip(page.Resources.Should().ContainSingle().Subject, AssetJson);
	}

	[Fact]
	public async Task SearchAsync_ReadsEveryPageThroughRapid7Paging()
	{
		var stub = new StubHandler();
		stub.Enqueue(HttpStatusCode.OK, """{"resources":[{"id":1}],"page":{"number":0,"size":1,"totalPages":2,"totalResources":2},"links":[]}""");
		stub.Enqueue(HttpStatusCode.OK, """{"resources":[{"id":2}],"page":{"number":1,"size":1,"totalPages":2,"totalResources":2},"links":[]}""");
		using var client = TestClient.Create(stub);
		var criteria = new SearchCriteria { Match = SearchMatch.Any };

		var ids = await Rapid7Paging.ReadAllAsync((page, ct) => client.Assets.SearchAsync(criteria, page, ct), 1, TestContext.Current.CancellationToken)
			.Select(a => a.Id)
			.ToListAsync(TestContext.Current.CancellationToken);

		ids.Should().Equal(1L, 2L);
		stub.Calls.Select(c => c.Uri.Query).Should().Equal("?page=0&size=1", "?page=1&size=1");
	}

	[Fact]
	public async Task SearchAsync_IsAllowedOnAReadOnlyClient()
	{
		var stub = TestClient.Stub(EmptyPage);
		using var client = TestClient.Create(stub, o => o.ReadOnly = true);

		await client.Assets.SearchAsync(new SearchCriteria(), null, TestContext.Current.CancellationToken);

		stub.Calls.Should().ContainSingle().Which.Body.Should().Be("""{"filters":[]}""");
	}

	[Fact]
	public async Task DeleteAsync_IsRefusedOnAReadOnlyClient()
	{
		var stub = TestClient.Stub(LinksJson);
		using var client = TestClient.Create(stub, o => o.ReadOnly = true);

		var act = () => client.Assets.DeleteAsync(282, TestContext.Current.CancellationToken);

		await act.Should().ThrowAsync<Rapid7ReadOnlyException>();
		stub.Calls.Should().BeEmpty();
	}

	[Fact]
	public void SearchFilter_ReadsOperandsAsJsonElements()
	{
		var criteria = JsonSerializer.Deserialize<SearchCriteria>(SearchCriteriaJson, Rapid7Json.Options)!;

		criteria.Match.Should().Be(SearchMatch.All);
		criteria.Filters[0].Field.Should().Be(SearchField.RiskScore);
		criteria.Filters[0].Operator.Should().Be(SearchOperator.IsGreaterThan);
		criteria.Filters[0].Value.Should().BeOfType<JsonElement>().Which.GetInt32().Should().Be(5000);
		criteria.Filters[1].Lower.Should().BeOfType<JsonElement>().Which.GetString().Should().Be("192.0.2.1");
		criteria.Filters[1].Upper.Should().BeOfType<JsonElement>().Which.GetString().Should().Be("192.0.2.254");
		criteria.Filters[2].Values.Should().HaveCount(2);
	}

	[Fact]
	public Task GetAsync_NotFound_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync((c, ct) => c.Assets.GetAsync(999, ct), HttpStatusCode.NotFound, NotFound, NotFoundMessage);
}
