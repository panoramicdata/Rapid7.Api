using Rapid7.Api.Models.Sites;
using Rapid7.Api.Test.Support;

namespace Rapid7.Api.Test.Groups;

public partial class SiteAlertsTests
{
	private const string SnmpJson = $$"""
		{
			"id": 6,
			"name": "Trap the NOC",
			"notification": "SNMP",
			{{CommonJson}},
			"server": "snmp.example.test",
			"community": "monitoring",
			"links": [ { "href": "https://console.test:3780/api/3/sites/7/alerts/snmp/6", "rel": "self" } ]
		}
		""";

	private static SnmpAlert Snmp => new()
	{
		Name = "Scan failures",
		Enabled = true,
		MaximumAlerts = 10,
		EnabledScanEvents = ScanEvents,
		EnabledVulnerabilityEvents = VulnerabilityEvents,
		Server = "snmp.example.test",
		Community = "monitoring"
	};

	private static string SnmpBody => "{\"server\":\"snmp.example.test\",\"community\":\"monitoring\"" + CommonBodyFor("SNMP");

	[Fact]
	public async Task ListSnmpAsync_SendsGetToTheSnmpAlerts()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteAlerts.ListSnmpAsync(SiteFixtures.SiteId, ct), ListOf(SnmpJson));

		call.ShouldBe(HttpMethod.Get, "/api/3/sites/7/alerts/snmp");
	}

	[Fact]
	public async Task ListSnmpAsync_MapsEveryAlert()
	{
		var list = await TestClient.ReadAsync((c, ct) => c.SiteAlerts.ListSnmpAsync(SiteFixtures.SiteId, ct), ListOf(SnmpJson));

		ShouldBeTrapTheNoc(list.Resources.Should().ContainSingle().Subject);
	}

	[Fact]
	public async Task ReplaceSnmpAsync_PutsTheAlertArray()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteAlerts.ReplaceSnmpAsync(SiteFixtures.SiteId, [Snmp], ct), SiteFixtures.LinksJson);

		call.ShouldBe(HttpMethod.Put, "/api/3/sites/7/alerts/snmp", body: $"[{SnmpBody}]");
	}

	[Fact]
	public async Task CreateSnmpAsync_PostsTheAlert()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteAlerts.CreateSnmpAsync(SiteFixtures.SiteId, Snmp, ct), SiteFixtures.CreatedJson);

		call.ShouldBe(HttpMethod.Post, "/api/3/sites/7/alerts/snmp", body: SnmpBody);
	}

	[Fact]
	public async Task DeleteAllSnmpAsync_SendsDeleteToTheSnmpAlerts()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteAlerts.DeleteAllSnmpAsync(SiteFixtures.SiteId, ct), SiteFixtures.LinksJson);

		call.ShouldBe(HttpMethod.Delete, "/api/3/sites/7/alerts/snmp");
	}

	[Fact]
	public async Task GetSnmpAsync_SendsGetToTheAlert()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteAlerts.GetSnmpAsync(SiteFixtures.SiteId, 6, ct), SnmpJson);

		call.ShouldBe(HttpMethod.Get, "/api/3/sites/7/alerts/snmp/6");
	}

	[Fact]
	public async Task GetSnmpAsync_MapsEveryField()
	{
		var alert = await TestClient.ReadAsync((c, ct) => c.SiteAlerts.GetSnmpAsync(SiteFixtures.SiteId, 6, ct), SnmpJson);

		ShouldBeTrapTheNoc(alert);
	}

	[Fact]
	public async Task UpdateSnmpAsync_PutsTheAlert()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteAlerts.UpdateSnmpAsync(SiteFixtures.SiteId, 6, Snmp, ct), SiteFixtures.LinksJson);

		call.ShouldBe(HttpMethod.Put, "/api/3/sites/7/alerts/snmp/6", body: SnmpBody);
	}

	[Fact]
	public async Task DeleteSnmpAsync_SendsDeleteToTheAlert()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteAlerts.DeleteSnmpAsync(SiteFixtures.SiteId, 6, ct), SiteFixtures.LinksJson);

		call.ShouldBe(HttpMethod.Delete, "/api/3/sites/7/alerts/snmp/6");
	}

	private static void ShouldBeTrapTheNoc(SnmpAlert alert)
	{
		alert.Id.Should().Be(6);
		alert.Name.Should().Be("Trap the NOC");
		alert.Notification.Should().Be(AlertNotificationType.Snmp);
		ShouldHaveTheCommonSettings(alert);
		alert.Server.Should().Be("snmp.example.test");
		alert.Community.Should().Be("monitoring");
	}
}
