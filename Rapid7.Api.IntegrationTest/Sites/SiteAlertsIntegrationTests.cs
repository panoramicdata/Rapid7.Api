using Rapid7.Api.Models.Sites;

namespace Rapid7.Api.IntegrationTest.Sites;

/// <summary>
/// Disabled alerts on a never-scanned test site, pointed at reserved example.test hosts: nothing is ever sent.
/// </summary>
[Collection(Rapid7TestGroup.Name)]
public class SiteAlertsIntegrationTests(Rapid7Fixture fixture)
{
	private static readonly AlertScanEvents FailuresOnly = new() { Failed = true };

	private static SmtpAlert Smtp(string name) => new()
	{
		Name = name,
		Enabled = false,
		EnabledScanEvents = FailuresOnly,
		RelayServer = "smtp.example.test",
		Recipients = ["nobody@example.test"]
	};

	private static SnmpAlert Snmp(string name) => new()
	{
		Name = name,
		Enabled = false,
		EnabledScanEvents = FailuresOnly,
		Server = "snmp.example.test",
		Community = "rapid7api-test"
	};

	private static SyslogAlert Syslog(string name) => new()
	{
		Name = name,
		Enabled = false,
		EnabledScanEvents = FailuresOnly,
		Server = "syslog.example.test"
	};

	[Fact]
	public Task SmtpAlert_CreateGetUpdateListDelete()
		=> ScratchSite.RunAsync(fixture, async (siteId, ct) =>
		{
			var alerts = fixture.Client.SiteAlerts;
			var created = await alerts.CreateSmtpAsync(siteId, Smtp("rapid7api-test-smtp"), ct);
			(await alerts.GetSmtpAsync(siteId, created.Id, ct)).Recipients.Should().Equal("nobody@example.test");

			await alerts.UpdateSmtpAsync(siteId, created.Id, Smtp("rapid7api-test-smtp-renamed"), ct);
			(await alerts.ListSmtpAsync(siteId, ct)).Resources.Should().ContainSingle(a => a.Name == "rapid7api-test-smtp-renamed");

			await alerts.DeleteSmtpAsync(siteId, created.Id, ct);
			(await alerts.ListSmtpAsync(siteId, ct)).Resources.Should().BeEmpty();
		});

	[Fact]
	public Task SnmpAlert_CreateGetUpdateListDelete()
		=> ScratchSite.RunAsync(fixture, async (siteId, ct) =>
		{
			var alerts = fixture.Client.SiteAlerts;
			var created = await alerts.CreateSnmpAsync(siteId, Snmp("rapid7api-test-snmp"), ct);
			(await alerts.GetSnmpAsync(siteId, created.Id, ct)).Community.Should().Be("rapid7api-test");

			await alerts.UpdateSnmpAsync(siteId, created.Id, Snmp("rapid7api-test-snmp-renamed"), ct);
			(await alerts.ListSnmpAsync(siteId, ct)).Resources.Should().ContainSingle(a => a.Name == "rapid7api-test-snmp-renamed");

			await alerts.DeleteSnmpAsync(siteId, created.Id, ct);
			(await alerts.ListSnmpAsync(siteId, ct)).Resources.Should().BeEmpty();
		});

	[Fact]
	public Task SyslogAlert_CreateGetUpdateListDelete()
		=> ScratchSite.RunAsync(fixture, async (siteId, ct) =>
		{
			var alerts = fixture.Client.SiteAlerts;
			var created = await alerts.CreateSyslogAsync(siteId, Syslog("rapid7api-test-syslog"), ct);
			(await alerts.GetSyslogAsync(siteId, created.Id, ct)).Server.Should().Be("syslog.example.test");

			await alerts.UpdateSyslogAsync(siteId, created.Id, Syslog("rapid7api-test-syslog-renamed"), ct);
			(await alerts.ListSyslogAsync(siteId, ct)).Resources.Should().ContainSingle(a => a.Name == "rapid7api-test-syslog-renamed");

			await alerts.DeleteSyslogAsync(siteId, created.Id, ct);
			(await alerts.ListSyslogAsync(siteId, ct)).Resources.Should().BeEmpty();
		});

	[Fact]
	public Task Alerts_ReplaceListAndDeleteAll()
		=> ScratchSite.RunAsync(fixture, async (siteId, ct) =>
		{
			var alerts = fixture.Client.SiteAlerts;
			await alerts.ReplaceSmtpAsync(siteId, [Smtp("rapid7api-test-smtp-a"), Smtp("rapid7api-test-smtp-b")], ct);
			await alerts.ReplaceSnmpAsync(siteId, [Snmp("rapid7api-test-snmp-a")], ct);
			await alerts.ReplaceSyslogAsync(siteId, [Syslog("rapid7api-test-syslog-a")], ct);

			var all = await alerts.ListAsync(siteId, ct);
			all.Resources.Should().HaveCount(4);
			all.Resources.Select(a => a.Notification).Should().Contain([AlertNotificationType.Smtp, AlertNotificationType.Snmp, AlertNotificationType.Syslog]);

			await alerts.DeleteAllSmtpAsync(siteId, ct);
			await alerts.DeleteAllSnmpAsync(siteId, ct);
			(await alerts.ListAsync(siteId, ct)).Resources.Should().ContainSingle();

			await alerts.DeleteAllSyslogAsync(siteId, ct);
			await alerts.ReplaceSyslogAsync(siteId, [Syslog("rapid7api-test-syslog-b")], ct);
			await alerts.DeleteAllAsync(siteId, ct);
			(await alerts.ListAsync(siteId, ct)).Resources.Should().BeEmpty();
		});
}
