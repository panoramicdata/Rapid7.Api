using Rapid7.Api.Models;
using Rapid7.Api.Test.Support;
using System.Net;

namespace Rapid7.Api.Test.Groups;

public class SiteAssetsTests
{
	private const string AssetPageJson = """
		{
			"resources": [
				{
					"id": 282,
					"ip": "10.20.30.40",
					"hostName": "workstation-01.example.test",
					"mac": "AB:12:CD:34:EF:56",
					"os": "Microsoft Windows Server 2019",
					"riskScore": 37457.16,
					"links": [ { "href": "https://console.test:3780/api/3/assets/282", "rel": "self" } ]
				}
			],
			"page": { "number": 1, "size": 1, "totalPages": 3, "totalResources": 3 },
			"links": [ { "href": "https://console.test:3780/api/3/sites/7/assets?page=1&size=1", "rel": "self" } ]
		}
		""";

	[Fact]
	public async Task ListAsync_WithoutPaging_SendsGetWithNoQuery()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteAssets.ListAsync(7, null, ct), AssetPageJson);

		call.ShouldBe(HttpMethod.Get, "/api/3/sites/7/assets");
	}

	[Fact]
	public async Task ListAsync_WithPaging_SendsPageSizeAndSort()
	{
		var paging = new PageOptions { Page = 1, Size = 1, Sort = ["riskScore,DESC"] };

		var call = await TestClient.CaptureAsync((c, ct) => c.SiteAssets.ListAsync(7, paging, ct), AssetPageJson);

		call.ShouldBe(HttpMethod.Get, "/api/3/sites/7/assets", "?page=1&size=1&sort=riskScore%2CDESC");
	}

	[Fact]
	public async Task ListAsync_MapsTheAssetsAndThePage()
	{
		var page = await TestClient.ReadAsync((c, ct) => c.SiteAssets.ListAsync(7, null, ct), AssetPageJson);

		var asset = page.Resources.Should().ContainSingle().Subject;
		asset.Id.Should().Be(282);
		asset.Ip.Should().Be("10.20.30.40");
		asset.HostName.Should().Be("workstation-01.example.test");
		asset.Items.Should().ContainSingle().Which.Href.Should().EndWith("/assets/282");
		page.PageInfo!.Number.Should().Be(1);
		page.PageInfo.TotalPages.Should().Be(3);
		page.PageInfo.TotalResources.Should().Be(3);
		page.Items.Should().ContainSingle();
	}

	[Fact]
	public async Task RemoveAllAsync_SendsDeleteToTheSiteAssets()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteAssets.RemoveAllAsync(7, ct), SiteMembershipJson.Links);

		call.ShouldBe(HttpMethod.Delete, "/api/3/sites/7/assets");
	}

	[Fact]
	public async Task RemoveAsync_SendsDeleteToTheAsset()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteAssets.RemoveAsync(7, 9_000_000_001L, ct), SiteMembershipJson.Links);

		call.ShouldBe(HttpMethod.Delete, "/api/3/sites/7/assets/9000000001");
	}

	[Fact]
	public async Task RemoveAsync_ReturnsTheLinks()
	{
		var links = await TestClient.ReadAsync((c, ct) => c.SiteAssets.RemoveAsync(7, 282, ct), SiteMembershipJson.Links);

		links.ShouldLinkToSite();
	}

	[Fact]
	public Task ListAsync_MissingSite_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync(
			(c, ct) => c.SiteAssets.ListAsync(404, null, ct),
			HttpStatusCode.NotFound,
			SiteMembershipJson.NotFound,
			SiteMembershipJson.NotFoundMessage);
}
