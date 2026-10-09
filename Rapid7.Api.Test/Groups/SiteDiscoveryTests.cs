using Rapid7.Api.Models.Sites;
using Rapid7.Api.Test.Support;
using System.Net;
using System.Text.Json;

namespace Rapid7.Api.Test.Groups;

public class SiteDiscoveryTests
{
	private const string ConnectionJson = """
		{
			"id": 3,
			"name": "Corporate vCenter",
			"type": "vsphere",
			"links": [ { "href": "https://console.test:3780/api/3/discovery_connections/3", "rel": "self" } ]
		}
		""";

	private const string CriteriaJson = """
		{
			"connectionType": "aws",
			"match": "any",
			"filters": [
				{ "field": "AWS_REGION", "operator": "IN", "values": [ "eu-west-2", "us-east-1" ] },
				{ "field": "IP_ADDRESS", "operator": "IN_RANGE", "lower": "192.0.2.1", "upper": "192.0.2.9" },
				{ "field": "AWS_INSTANCE_COUNT", "operator": "IS", "value": 4 }
			]
		}
		""";

	[Fact]
	public async Task GetConnectionAsync_SendsGet()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteDiscovery.GetConnectionAsync(7, ct), ConnectionJson);

		call.ShouldBe(HttpMethod.Get, "/api/3/sites/7/discovery_connection");
	}

	[Fact]
	public async Task GetConnectionAsync_MapsEveryField()
	{
		var connection = await TestClient.ReadAsync((c, ct) => c.SiteDiscovery.GetConnectionAsync(7, ct), ConnectionJson);

		connection.Id.Should().Be(3);
		connection.Name.Should().Be("Corporate vCenter");
		connection.Type.Should().Be(DiscoveryConnectionType.VSphere);
		connection.Links.Should().ContainSingle().Which.Href.Should().EndWith("/discovery_connections/3");
	}

	[Theory]
	[InlineData("activesync-ldap", DiscoveryConnectionType.ActiveSyncLdap)]
	[InlineData("activesync-office365", DiscoveryConnectionType.ActiveSyncOffice365)]
	[InlineData("activesync-powershell", DiscoveryConnectionType.ActiveSyncPowerShell)]
	[InlineData("aws", DiscoveryConnectionType.Aws)]
	[InlineData("dhcp", DiscoveryConnectionType.Dhcp)]
	[InlineData("sonar", DiscoveryConnectionType.Sonar)]
	[InlineData("vsphere", DiscoveryConnectionType.VSphere)]
	[InlineData("something-new", DiscoveryConnectionType.Unknown)]
	public async Task GetConnectionAsync_MapsEveryConnectionType(string wire, DiscoveryConnectionType expected)
	{
		var connection = await TestClient.ReadAsync((c, ct) => c.SiteDiscovery.GetConnectionAsync(7, ct), $$"""{"id":3,"type":"{{wire}}","links":[]}""");

		connection.Type.Should().Be(expected);
	}

	[Fact]
	public async Task SetConnectionAsync_SendsPutWithTheBareConnectionId()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteDiscovery.SetConnectionAsync(7, 3, ct), SiteMembershipJson.Links);

		call.ShouldBe(HttpMethod.Put, "/api/3/sites/7/discovery_connection", body: "3");
	}

	[Fact]
	public async Task GetSearchCriteriaAsync_SendsGet()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteDiscovery.GetSearchCriteriaAsync(7, ct), CriteriaJson);

		call.ShouldBe(HttpMethod.Get, "/api/3/sites/7/discovery_search_criteria");
	}

	[Fact]
	public async Task GetSearchCriteriaAsync_MapsEveryField()
	{
		var criteria = await TestClient.ReadAsync((c, ct) => c.SiteDiscovery.GetSearchCriteriaAsync(7, ct), CriteriaJson);

		criteria.ConnectionType.Should().Be(DiscoveryConnectionType.Aws);
		criteria.Match.Should().Be(DiscoverySearchMatch.Any);
		criteria.Filters.Should().HaveCount(3);
		var region = criteria.Filters[0];
		region.Field.Should().Be("AWS_REGION");
		region.Operator.Should().Be("IN");
		region.Values!.Cast<JsonElement>().Select(v => v.GetString()).Should().Equal("eu-west-2", "us-east-1");
		var range = criteria.Filters[1];
		((JsonElement)range.Lower!).GetString().Should().Be("192.0.2.1");
		((JsonElement)range.Upper!).GetString().Should().Be("192.0.2.9");
		((JsonElement)criteria.Filters[2].Value!).GetInt32().Should().Be(4);
	}

	[Fact]
	public async Task SetSearchCriteriaAsync_SendsPutWithTheCriteria()
	{
		var criteria = new DiscoverySearchCriteria
		{
			ConnectionType = DiscoveryConnectionType.VSphere,
			Match = DiscoverySearchMatch.All,
			Filters =
			[
				new DiscoverySearchCriteriaFilter { Field = "VSPHERE_POWER_STATE", Operator = "IS", Value = "on" },
				new DiscoverySearchCriteriaFilter { Field = "VSPHERE_HOST", Operator = "IN", Values = ["esx-01", "esx-02"] },
				new DiscoverySearchCriteriaFilter { Field = "IP_ADDRESS", Operator = "IN_RANGE", Lower = "192.0.2.1", Upper = "192.0.2.9" }
			]
		};

		var call = await TestClient.CaptureAsync((c, ct) => c.SiteDiscovery.SetSearchCriteriaAsync(7, criteria, ct), SiteMembershipJson.Links);

		call.ShouldBe(
			HttpMethod.Put,
			"/api/3/sites/7/discovery_search_criteria",
			body: """{"connectionType":"vsphere","filters":[{"field":"VSPHERE_POWER_STATE","operator":"IS","value":"on"},{"field":"VSPHERE_HOST","operator":"IN","values":["esx-01","esx-02"]},{"field":"IP_ADDRESS","lower":"192.0.2.1","operator":"IN_RANGE","upper":"192.0.2.9"}],"match":"all"}""");
	}

	[Fact]
	public async Task SetSearchCriteriaAsync_ReturnsTheLinks()
	{
		var links = await TestClient.ReadAsync(
			(c, ct) => c.SiteDiscovery.SetSearchCriteriaAsync(7, new DiscoverySearchCriteria(), ct),
			SiteMembershipJson.Links);

		links.ShouldLinkToSite();
	}

	[Fact]
	public Task GetSearchCriteriaAsync_StaticSite_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync(
			(c, ct) => c.SiteDiscovery.GetSearchCriteriaAsync(7, ct),
			HttpStatusCode.BadRequest,
			"""{"status":"BAD_REQUEST","message":"The site is not a dynamic site.","links":[]}""",
			"The site is not a dynamic site.");
}
