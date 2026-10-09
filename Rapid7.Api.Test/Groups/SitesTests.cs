using Rapid7.Api.Models;
using Rapid7.Api.Models.Sites;
using Rapid7.Api.Test.Support;

namespace Rapid7.Api.Test.Groups;

public class SitesTests
{
	private const string SiteJson = """
		{
			"id": 7,
			"name": "Head office",
			"description": "Servers and workstations at head office",
			"importance": "high",
			"type": "dynamic",
			"connectionType": "vsphere",
			"assets": 768,
			"riskScore": 4457823.78,
			"lastScanTime": "2026-09-30T22:15:03.123Z",
			"scanEngine": 3,
			"scanTemplate": "full-audit-without-web-spider",
			"vulnerabilities": { "critical": 16, "moderate": 3, "severe": 76, "total": 95 },
			"links": [ { "href": "https://console.test:3780/api/3/sites/7", "rel": "self" } ]
		}
		""";

	private const string PageJson = $$"""
		{
			"resources": [ {{SiteJson}}, { "id": 8, "name": "Branch", "importance": "very_low", "type": "agent", "links": [] } ],
			"page": { "number": 1, "size": 2, "totalPages": 4, "totalResources": 7 },
			"links": [ { "href": "https://console.test:3780/api/3/sites?page=1&size=2", "rel": "self" } ]
		}
		""";

	[Fact]
	public async Task ListAsync_WithoutPaging_SendsGetToSites()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.Sites.ListAsync(null, ct), PageJson);

		call.ShouldBe(HttpMethod.Get, "/api/3/sites");
	}

	[Fact]
	public async Task ListAsync_WithPaging_SendsPageSizeAndSorts()
	{
		var paging = new PageOptions { Page = 1, Size = 2, Sort = ["name,ASC", "id"] };

		var call = await TestClient.CaptureAsync((c, ct) => c.Sites.ListAsync(paging, ct), PageJson);

		call.ShouldBe(HttpMethod.Get, "/api/3/sites", "?page=1&size=2&sort=name%2CASC&sort=id");
	}

	[Fact]
	public async Task ListAsync_MapsThePage()
	{
		var page = await TestClient.ReadAsync((c, ct) => c.Sites.ListAsync(null, ct), PageJson);

		page.Resources.Should().HaveCount(2);
		ShouldBeHeadOffice(page.Resources[0]);
		page.Resources[1].Importance.Should().Be(SiteImportance.VeryLow);
		page.Resources[1].Type.Should().Be(SiteType.Agent);
		page.Resources[1].ConnectionType.Should().BeNull();
		page.Resources[1].Vulnerabilities.Should().BeNull();
		page.PageInfo!.Number.Should().Be(1);
		page.PageInfo.TotalResources.Should().Be(7);
		page.Items.Should().ContainSingle();
	}

	[Fact]
	public async Task CreateAsync_PostsTheSiteWithItsScope()
	{
		var request = new SiteCreateRequest
		{
			Name = "Head office",
			Description = "Servers",
			Importance = SiteImportance.VeryHigh,
			EngineId = 3,
			ScanTemplateId = "discovery",
			Scan = new ScanScope
			{
				Assets = new ScanScopeAssets
				{
					IncludedTargets = new ScanScopeTargets { Addresses = ["192.0.2.0/24", "server.example.test"] },
					ExcludedTargets = new ScanScopeTargets { Addresses = ["192.0.2.1"] },
					IncludedAssetGroups = new ScanScopeAssetGroups { AssetGroupIds = [61] },
					ExcludedAssetGroups = new ScanScopeAssetGroups { AssetGroupIds = [62, 63] }
				}
			}
		};

		var call = await TestClient.CaptureAsync((c, ct) => c.Sites.CreateAsync(request, ct), SiteFixtures.CreatedJson);

		call.ShouldBe(
			HttpMethod.Post,
			"/api/3/sites",
			body: """{"importance":"very_high","engineId":3,"scanTemplateId":"discovery","scan":{"assets":{"includedTargets":{"addresses":["192.0.2.0/24","server.example.test"]},"excludedTargets":{"addresses":["192.0.2.1"]},"includedAssetGroups":{"assetGroupIDs":[61]},"excludedAssetGroups":{"assetGroupIDs":[62,63]}}},"name":"Head office","description":"Servers"}""");
	}

	[Fact]
	public async Task CreateAsync_DynamicSite_SendsTheConnectionOnly()
	{
		var request = new SiteCreateRequest { Name = "Cloud", Scan = new ScanScope { Connection = new ScanScopeConnection { Id = 5 } } };

		var call = await TestClient.CaptureAsync((c, ct) => c.Sites.CreateAsync(request, ct), SiteFixtures.CreatedJson);

		call.ShouldBe(HttpMethod.Post, "/api/3/sites", body: """{"scan":{"connection":{"id":5}},"name":"Cloud"}""");
	}

	[Fact]
	public async Task CreateAsync_MapsTheNewSiteId()
	{
		var created = await TestClient.ReadAsync((c, ct) => c.Sites.CreateAsync(new SiteCreateRequest { Name = "x" }, ct), SiteFixtures.CreatedJson);

		created.ShouldBeCreated42();
	}

	[Fact]
	public async Task GetAsync_SendsGetToTheSite()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.Sites.GetAsync(SiteFixtures.SiteId, ct), SiteJson);

		call.ShouldBe(HttpMethod.Get, "/api/3/sites/7");
	}

	[Fact]
	public async Task GetAsync_MapsEveryField()
	{
		var site = await TestClient.ReadAsync((c, ct) => c.Sites.GetAsync(SiteFixtures.SiteId, ct), SiteJson);

		ShouldBeHeadOffice(site);
	}

	[Fact]
	public async Task UpdateAsync_PutsTheSettings()
	{
		var request = new SiteUpdateRequest
		{
			Name = "Head office",
			Description = "Renamed",
			Importance = SiteImportance.Low,
			EngineId = 4,
			ScanTemplateId = "full-audit"
		};

		var call = await TestClient.CaptureAsync((c, ct) => c.Sites.UpdateAsync(SiteFixtures.SiteId, request, ct), SiteFixtures.LinksJson);

		call.ShouldBe(
			HttpMethod.Put,
			"/api/3/sites/7",
			body: """{"importance":"low","engineId":4,"scanTemplateId":"full-audit","name":"Head office","description":"Renamed"}""");
	}

	[Fact]
	public async Task UpdateAsync_MapsTheLinks()
	{
		var request = new SiteUpdateRequest { Name = "x", Importance = SiteImportance.Normal, EngineId = 1, ScanTemplateId = "y" };

		var links = await TestClient.ReadAsync((c, ct) => c.Sites.UpdateAsync(SiteFixtures.SiteId, request, ct), SiteFixtures.LinksJson);

		links.ShouldBeSiteLinks();
	}

	[Fact]
	public async Task DeleteAsync_SendsDeleteToTheSite()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.Sites.DeleteAsync(SiteFixtures.SiteId, ct), SiteFixtures.LinksJson);

		call.ShouldBe(HttpMethod.Delete, "/api/3/sites/7");
	}

	[Fact]
	public Task GetAsync_NotFound_RaisesRapid7ApiException()
		=> SiteFixtures.ShouldRaiseNotFoundAsync((c, ct) => c.Sites.GetAsync(SiteFixtures.SiteId, ct));

	private static void ShouldBeHeadOffice(Site site)
	{
		site.Id.Should().Be(7);
		site.Name.Should().Be("Head office");
		site.Description.Should().Be("Servers and workstations at head office");
		site.Importance.Should().Be(SiteImportance.High);
		site.Type.Should().Be(SiteType.Dynamic);
		site.ConnectionType.Should().Be(SiteConnectionType.VSphere);
		site.Assets.Should().Be(768);
		site.RiskScore.Should().Be(4457823.78);
		site.LastScanTime.Should().Be(new DateTimeOffset(2026, 9, 30, 22, 15, 3, 123, TimeSpan.Zero));
		site.ScanEngine.Should().Be(3);
		site.ScanTemplate.Should().Be("full-audit-without-web-spider");
		site.Vulnerabilities!.Critical.Should().Be(16);
		site.Vulnerabilities.Moderate.Should().Be(3);
		site.Vulnerabilities.Severe.Should().Be(76);
		site.Vulnerabilities.Total.Should().Be(95);
		site.Items.Should().ContainSingle().Which.Href.Should().Be("https://console.test:3780/api/3/sites/7");
	}
}
