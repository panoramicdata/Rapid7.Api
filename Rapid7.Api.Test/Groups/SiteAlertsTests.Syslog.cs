using Rapid7.Api.Models.Sites;
using Rapid7.Api.Test.Support;

namespace Rapid7.Api.Test.Groups;

public partial class SiteAlertsTests
{
	private const string SyslogJson = $$"""
		{
			"id": 8,
			"name": "Log to the SIEM",
			"notification": "Syslog",
			{{CommonJson}},
			"server": "syslog.example.test",
			"links": [ { "href": "https://console.test:3780/api/3/sites/7/alerts/syslog/8", "rel": "self" } ]
		}
		""";

	private static SyslogAlert Syslog => new()
	{
		Name = "Scan failures",
		Enabled = true,
		MaximumAlerts = 10,
		EnabledScanEvents = ScanEvents,
		EnabledVulnerabilityEvents = VulnerabilityEvents,
		Server = "syslog.example.test"
	};

	private static string SyslogBody => "{\"server\":\"syslog.example.test\"" + CommonBodyFor("Syslog");

	[Fact]
	public async Task ListSyslogAsync_SendsGetToTheSyslogAlerts()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteAlerts.ListSyslogAsync(SiteFixtures.SiteId, ct), ListOf(SyslogJson));

		call.ShouldBe(HttpMethod.Get, "/api/3/sites/7/alerts/syslog");
	}

	[Fact]
	public async Task ListSyslogAsync_MapsEveryAlert()
	{
		var list = await TestClient.ReadAsync((c, ct) => c.SiteAlerts.ListSyslogAsync(SiteFixtures.SiteId, ct), ListOf(SyslogJson));

		ShouldBeLogToTheSiem(list.Resources.Should().ContainSingle().Subject);
	}

	[Fact]
	public async Task ReplaceSyslogAsync_PutsTheAlertArray()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteAlerts.ReplaceSyslogAsync(SiteFixtures.SiteId, [Syslog], ct), SiteFixtures.LinksJson);

		call.ShouldBe(HttpMethod.Put, "/api/3/sites/7/alerts/syslog", body: $"[{SyslogBody}]");
	}

	[Fact]
	public async Task CreateSyslogAsync_PostsTheAlert()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteAlerts.CreateSyslogAsync(SiteFixtures.SiteId, Syslog, ct), SiteFixtures.CreatedJson);

		call.ShouldBe(HttpMethod.Post, "/api/3/sites/7/alerts/syslog", body: SyslogBody);
	}

	[Fact]
	public async Task DeleteAllSyslogAsync_SendsDeleteToTheSyslogAlerts()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteAlerts.DeleteAllSyslogAsync(SiteFixtures.SiteId, ct), SiteFixtures.LinksJson);

		call.ShouldBe(HttpMethod.Delete, "/api/3/sites/7/alerts/syslog");
	}

	[Fact]
	public async Task GetSyslogAsync_SendsGetToTheAlert()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteAlerts.GetSyslogAsync(SiteFixtures.SiteId, 8, ct), SyslogJson);

		call.ShouldBe(HttpMethod.Get, "/api/3/sites/7/alerts/syslog/8");
	}

	[Fact]
	public async Task GetSyslogAsync_MapsEveryField()
	{
		var alert = await TestClient.ReadAsync((c, ct) => c.SiteAlerts.GetSyslogAsync(SiteFixtures.SiteId, 8, ct), SyslogJson);

		ShouldBeLogToTheSiem(alert);
	}

	[Fact]
	public async Task UpdateSyslogAsync_PutsTheAlert()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteAlerts.UpdateSyslogAsync(SiteFixtures.SiteId, 8, Syslog, ct), SiteFixtures.LinksJson);

		call.ShouldBe(HttpMethod.Put, "/api/3/sites/7/alerts/syslog/8", body: SyslogBody);
	}

	[Fact]
	public async Task DeleteSyslogAsync_SendsDeleteToTheAlert()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteAlerts.DeleteSyslogAsync(SiteFixtures.SiteId, 8, ct), SiteFixtures.LinksJson);

		call.ShouldBe(HttpMethod.Delete, "/api/3/sites/7/alerts/syslog/8");
	}

	private static void ShouldBeLogToTheSiem(SyslogAlert alert)
	{
		alert.Id.Should().Be(8);
		alert.Name.Should().Be("Log to the SIEM");
		alert.Notification.Should().Be(AlertNotificationType.Syslog);
		ShouldHaveTheCommonSettings(alert);
		alert.Server.Should().Be("syslog.example.test");
	}
}
