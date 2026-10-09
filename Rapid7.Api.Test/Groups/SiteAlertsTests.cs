using Rapid7.Api.Models.Sites;
using Rapid7.Api.Test.Support;

namespace Rapid7.Api.Test.Groups;

/// <summary>The generic site alert endpoints, the shared alert settings, and SMTP alerts (SNMP and syslog in partial files).</summary>
public partial class SiteAlertsTests
{
	// The settings every alert kind shares, as the console returns them.
	private const string CommonJson = """
		"enabled": true,
		"maximumAlerts": 10,
		"enabledScanEvents": { "started": true, "stopped": false, "failed": true, "paused": false, "resumed": true },
		"enabledVulnerabilityEvents": { "vulnerabilitySeverity": "severe_and_critical", "confirmedVulnerabilities": true, "unconfirmedVulnerabilities": false, "potentialVulnerabilities": true }
		""";

	private const string SmtpJson = $$"""
		{
			"id": 5,
			"name": "Email the team",
			"notification": "SMTP",
			{{CommonJson}},
			"relayServer": "smtp.example.test",
			"senderEmailAddress": "insightvm@example.test",
			"recipients": [ "secops@example.test", "it@example.test" ],
			"limitAlertText": true,
			"links": [ { "href": "https://console.test:3780/api/3/sites/7/alerts/smtp/5", "rel": "self" } ]
		}
		""";

	private const string AllAlertsJson = """
		{
			"resources": [
				{ "id": 5, "name": "Email", "enabled": true, "notification": "SMTP", "recipients": [ "secops@example.test" ], "relayServer": "smtp.example.test", "senderEmailAddress": "insightvm@example.test", "limitAlertText": false },
				{ "id": 6, "name": "Trap", "enabled": false, "notification": "SNMP", "server": "snmp.example.test", "community": "public", "maximumAlerts": 3 },
				{ "id": 8, "name": "Log", "enabled": true, "notification": "Syslog", "server": "syslog.example.test" }
			],
			"links": [ { "href": "https://console.test:3780/api/3/sites/7/alerts", "rel": "self" } ]
		}
		""";

	// What the typed alerts below send: the derived settings, then the shared ones (links are always sent, and ignored).
	private const string CommonBody = ""","name":"Scan failures","enabled":true,"notification":"{0}","maximumAlerts":10,"enabledScanEvents":{"started":true,"stopped":false,"failed":true,"paused":false,"resumed":true},"enabledVulnerabilityEvents":{"vulnerabilitySeverity":"only_critical","confirmedVulnerabilities":true,"unconfirmedVulnerabilities":false,"potentialVulnerabilities":false},"links":[]}""";

	private static string SmtpBody => """{"relayServer":"smtp.example.test","senderEmailAddress":"insightvm@example.test","recipients":["secops@example.test"],"limitAlertText":true""" + CommonBodyFor("SMTP");

	private static readonly AlertScanEvents ScanEvents = new() { Started = true, Stopped = false, Failed = true, Paused = false, Resumed = true };

	private static readonly AlertVulnerabilityEvents VulnerabilityEvents = new()
	{
		VulnerabilitySeverity = AlertVulnerabilitySeverity.OnlyCritical,
		ConfirmedVulnerabilities = true
	};

	private static SmtpAlert Smtp => new()
	{
		Name = "Scan failures",
		Enabled = true,
		MaximumAlerts = 10,
		EnabledScanEvents = ScanEvents,
		EnabledVulnerabilityEvents = VulnerabilityEvents,
		RelayServer = "smtp.example.test",
		SenderEmailAddress = "insightvm@example.test",
		Recipients = ["secops@example.test"],
		LimitAlertText = true
	};

	[Fact]
	public async Task ListAsync_SendsGetToTheAlerts()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteAlerts.ListAsync(SiteFixtures.SiteId, ct), AllAlertsJson);

		call.ShouldBe(HttpMethod.Get, "/api/3/sites/7/alerts");
	}

	[Fact]
	public async Task ListAsync_MapsEveryKindOfAlert()
	{
		var list = await TestClient.ReadAsync((c, ct) => c.SiteAlerts.ListAsync(SiteFixtures.SiteId, ct), AllAlertsJson);

		list.Resources.Should().HaveCount(3);
		var smtp = list.Resources[0];
		smtp.Id.Should().Be(5);
		smtp.Name.Should().Be("Email");
		smtp.Enabled.Should().BeTrue();
		smtp.Notification.Should().Be(AlertNotificationType.Smtp);
		smtp.Recipients.Should().Equal("secops@example.test");
		smtp.RelayServer.Should().Be("smtp.example.test");
		smtp.SenderEmailAddress.Should().Be("insightvm@example.test");
		smtp.LimitAlertText.Should().BeFalse();
		smtp.Server.Should().BeNull();
		var snmp = list.Resources[1];
		snmp.Notification.Should().Be(AlertNotificationType.Snmp);
		snmp.Server.Should().Be("snmp.example.test");
		snmp.Community.Should().Be("public");
		snmp.MaximumAlerts.Should().Be(3);
		snmp.Recipients.Should().BeEmpty();
		list.Resources[2].Notification.Should().Be(AlertNotificationType.Syslog);
		list.Items.Should().ContainSingle();
	}

	[Fact]
	public async Task ListAsync_AlertWithoutANotification_IsUnknown()
	{
		var list = await TestClient.ReadAsync((c, ct) => c.SiteAlerts.ListAsync(SiteFixtures.SiteId, ct), """{"resources":[{"id":1,"name":"x","enabled":true}],"links":[]}""");

		list.Resources[0].Notification.Should().Be(AlertNotificationType.Unknown);
	}

	[Fact]
	public async Task DeleteAllAsync_SendsDeleteToTheAlerts()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteAlerts.DeleteAllAsync(SiteFixtures.SiteId, ct), SiteFixtures.LinksJson);

		call.ShouldBe(HttpMethod.Delete, "/api/3/sites/7/alerts");
	}

	[Fact]
	public async Task DeleteAllAsync_MapsTheLinks()
	{
		var links = await TestClient.ReadAsync((c, ct) => c.SiteAlerts.DeleteAllAsync(SiteFixtures.SiteId, ct), SiteFixtures.LinksJson);

		links.ShouldBeSiteLinks();
	}

	[Fact]
	public async Task ListSmtpAsync_SendsGetToTheSmtpAlerts()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteAlerts.ListSmtpAsync(SiteFixtures.SiteId, ct), ListOf(SmtpJson));

		call.ShouldBe(HttpMethod.Get, "/api/3/sites/7/alerts/smtp");
	}

	[Fact]
	public async Task ListSmtpAsync_MapsEveryAlert()
	{
		var list = await TestClient.ReadAsync((c, ct) => c.SiteAlerts.ListSmtpAsync(SiteFixtures.SiteId, ct), ListOf(SmtpJson));

		ShouldBeEmailTheTeam(list.Resources.Should().ContainSingle().Subject);
	}

	[Fact]
	public async Task ReplaceSmtpAsync_PutsTheAlertArray()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteAlerts.ReplaceSmtpAsync(SiteFixtures.SiteId, [Smtp], ct), SiteFixtures.LinksJson);

		call.ShouldBe(HttpMethod.Put, "/api/3/sites/7/alerts/smtp", body: $"[{SmtpBody}]");
	}

	[Fact]
	public async Task CreateSmtpAsync_PostsTheAlert()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteAlerts.CreateSmtpAsync(SiteFixtures.SiteId, Smtp, ct), SiteFixtures.CreatedJson);

		call.ShouldBe(HttpMethod.Post, "/api/3/sites/7/alerts/smtp", body: SmtpBody);
	}

	[Fact]
	public async Task CreateSmtpAsync_MapsTheNewAlertId()
	{
		var created = await TestClient.ReadAsync((c, ct) => c.SiteAlerts.CreateSmtpAsync(SiteFixtures.SiteId, Smtp, ct), SiteFixtures.CreatedJson);

		created.ShouldBeCreated42();
	}

	[Fact]
	public async Task DeleteAllSmtpAsync_SendsDeleteToTheSmtpAlerts()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteAlerts.DeleteAllSmtpAsync(SiteFixtures.SiteId, ct), SiteFixtures.LinksJson);

		call.ShouldBe(HttpMethod.Delete, "/api/3/sites/7/alerts/smtp");
	}

	[Fact]
	public async Task GetSmtpAsync_SendsGetToTheAlert()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteAlerts.GetSmtpAsync(SiteFixtures.SiteId, 5, ct), SmtpJson);

		call.ShouldBe(HttpMethod.Get, "/api/3/sites/7/alerts/smtp/5");
	}

	[Fact]
	public async Task GetSmtpAsync_MapsEveryField()
	{
		var alert = await TestClient.ReadAsync((c, ct) => c.SiteAlerts.GetSmtpAsync(SiteFixtures.SiteId, 5, ct), SmtpJson);

		ShouldBeEmailTheTeam(alert);
	}

	[Fact]
	public async Task UpdateSmtpAsync_PutsTheAlert()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteAlerts.UpdateSmtpAsync(SiteFixtures.SiteId, 5, Smtp, ct), SiteFixtures.LinksJson);

		call.ShouldBe(HttpMethod.Put, "/api/3/sites/7/alerts/smtp/5", body: SmtpBody);
	}

	[Fact]
	public async Task DeleteSmtpAsync_SendsDeleteToTheAlert()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteAlerts.DeleteSmtpAsync(SiteFixtures.SiteId, 5, ct), SiteFixtures.LinksJson);

		call.ShouldBe(HttpMethod.Delete, "/api/3/sites/7/alerts/smtp/5");
	}

	[Fact]
	public Task GetSmtpAsync_NotFound_RaisesRapid7ApiException()
		=> SiteFixtures.ShouldRaiseNotFoundAsync((c, ct) => c.SiteAlerts.GetSmtpAsync(SiteFixtures.SiteId, 5, ct));

	[Theory]
	[InlineData("any_severity", AlertVulnerabilitySeverity.AnySeverity)]
	[InlineData("only_critical", AlertVulnerabilitySeverity.OnlyCritical)]
	public async Task GetSmtpAsync_MapsTheOtherSeverities(string wire, AlertVulnerabilitySeverity expected)
	{
		var alert = await TestClient.ReadAsync(
			(c, ct) => c.SiteAlerts.GetSmtpAsync(SiteFixtures.SiteId, 5, ct),
			$$"""{"id":5,"name":"x","enabled":true,"notification":"SMTP","enabledVulnerabilityEvents":{"vulnerabilitySeverity":"{{wire}}"},"links":[]}""");

		alert.EnabledVulnerabilityEvents!.VulnerabilitySeverity.Should().Be(expected);
		alert.EnabledScanEvents.Should().BeNull();
	}

	/// <summary>The body every typed alert sends for the shared settings, for <paramref name="notification"/>.</summary>
	private static string CommonBodyFor(string notification) => CommonBody.Replace("{0}", notification, StringComparison.Ordinal);

	private static string ListOf(string alertJson) => $$"""{"resources":[{{alertJson}}],"links":[]}""";

	private static void ShouldHaveTheCommonSettings(SiteAlertBase alert)
	{
		alert.Enabled.Should().BeTrue();
		alert.MaximumAlerts.Should().Be(10);
		alert.EnabledScanEvents!.Started.Should().BeTrue();
		alert.EnabledScanEvents.Stopped.Should().BeFalse();
		alert.EnabledScanEvents.Failed.Should().BeTrue();
		alert.EnabledScanEvents.Paused.Should().BeFalse();
		alert.EnabledScanEvents.Resumed.Should().BeTrue();
		alert.EnabledVulnerabilityEvents!.VulnerabilitySeverity.Should().Be(AlertVulnerabilitySeverity.SevereAndCritical);
		alert.EnabledVulnerabilityEvents.ConfirmedVulnerabilities.Should().BeTrue();
		alert.EnabledVulnerabilityEvents.UnconfirmedVulnerabilities.Should().BeFalse();
		alert.EnabledVulnerabilityEvents.PotentialVulnerabilities.Should().BeTrue();
		alert.Items.Should().ContainSingle();
	}

	private static void ShouldBeEmailTheTeam(SmtpAlert alert)
	{
		alert.Id.Should().Be(5);
		alert.Name.Should().Be("Email the team");
		alert.Notification.Should().Be(AlertNotificationType.Smtp);
		ShouldHaveTheCommonSettings(alert);
		alert.RelayServer.Should().Be("smtp.example.test");
		alert.SenderEmailAddress.Should().Be("insightvm@example.test");
		alert.Recipients.Should().Equal("secops@example.test", "it@example.test");
		alert.LimitAlertText.Should().BeTrue();
	}
}
