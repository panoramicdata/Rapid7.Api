using Rapid7.Api.Models.ScanTemplates;
using Rapid7.Api.Test.Support;

namespace Rapid7.Api.Test.Groups;

public class SiteScanTemplateTests
{
	private const string TemplateJson = """
		{
			"id": "full-audit-without-web-spider",
			"name": "Full audit without Web Spider",
			"description": "Audits every system with safe checks only.",
			"discoveryOnly": false,
			"vulnerabilityEnabled": true,
			"policyEnabled": true,
			"enableWindowsServices": false,
			"enhancedLogging": true,
			"maxParallelAssets": 10,
			"maxScanProcesses": 12,
			"webEnabled": false,
			"checks": {
				"categories": { "enabled": [ "Windows" ], "disabled": [ "Oracle" ], "links": [] },
				"types": { "enabled": [ "Local" ], "disabled": [ "Policy" ], "links": [] },
				"individual": { "enabled": [ "ssh-weak-ciphers" ], "disabled": [ "tls-v1_0-enabled" ], "links": [] },
				"correlate": true,
				"potential": false,
				"unsafe": false,
				"links": []
			},
			"database": { "db2": "database", "oracle": [ "default", "orcl" ], "postgres": "postgres", "links": [] },
			"discovery": {
				"asset": {
					"sendIcmpPings": true,
					"sendArpPings": true,
					"tcpPorts": [ 22, 443 ],
					"udpPorts": [ 161 ],
					"treatTcpResetAsAsset": true,
					"ipFingerprintingEnabled": true,
					"fingerprintRetries": 2,
					"fingerprintMinimumCertainty": 0.16,
					"collectWhoisInformation": false
				},
				"service": {
					"tcp": { "ports": "well-known", "additionalPorts": "3078,8000-8080", "excludedPorts": "1024", "method": "SYN+RST", "links": [] },
					"udp": { "ports": "custom", "additionalPorts": "4020-4032", "excludedPorts": "9899", "links": [] },
					"serviceNameFile": "custom-services.txt"
				},
				"performance": {
					"packetRate": { "minimum": 450, "maximum": 15000, "defeatRateLimit": true },
					"parallelism": { "minimum": 0, "maximum": 1000 },
					"scanDelay": { "minimum": "PT0S", "maximum": "PT1S" },
					"timeout": { "initial": "PT0.5S", "minimum": "PT0S", "maximum": "PT3S" },
					"retryLimit": 3
				}
			},
			"policy": { "enabled": [ 84, 85 ], "recursiveWindowsFSSearch": true, "storeSCAP": false, "links": [] },
			"telnet": {
				"characterSet": "ASCII",
				"loginRegex": "login:",
				"passwordPromptRegex": "password:",
				"failedLoginRegex": "incorrect",
				"questionableLoginRegex": "last login",
				"links": []
			},
			"links": [ { "href": "https://console.test:3780/api/3/scan_templates/full-audit-without-web-spider", "rel": "self" } ]
		}
		""";

	[Fact]
	public async Task GetAsync_SendsGetToTheSiteScanTemplate()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteScanTemplate.GetAsync(SiteFixtures.SiteId, ct), TemplateJson);

		call.ShouldBe(HttpMethod.Get, "/api/3/sites/7/scan_template");
	}

	[Fact]
	public async Task GetAsync_MapsTheTemplateSettings()
	{
		var template = await TestClient.ReadAsync((c, ct) => c.SiteScanTemplate.GetAsync(SiteFixtures.SiteId, ct), TemplateJson);

		template.Id.Should().Be("full-audit-without-web-spider");
		template.Name.Should().Be("Full audit without Web Spider");
		template.Description.Should().Be("Audits every system with safe checks only.");
		template.DiscoveryOnly.Should().BeFalse();
		template.VulnerabilityEnabled.Should().BeTrue();
		template.PolicyEnabled.Should().BeTrue();
		template.EnableWindowsServices.Should().BeFalse();
		template.EnhancedLogging.Should().BeTrue();
		template.MaxParallelAssets.Should().Be(10);
		template.MaxScanProcesses.Should().Be(12);
		template.Items.Should().ContainSingle();
	}

	[Fact]
	public async Task GetAsync_MapsChecksDatabasePolicyAndTelnet()
	{
		var template = await TestClient.ReadAsync((c, ct) => c.SiteScanTemplate.GetAsync(SiteFixtures.SiteId, ct), TemplateJson);

		var checks = template.Checks!;
		checks.Categories!.Enabled.Should().Equal("Windows");
		checks.Categories.Disabled.Should().Equal("Oracle");
		checks.Types!.Enabled.Should().Equal("Local");
		checks.Types.Disabled.Should().Equal("Policy");
		checks.Individual!.Enabled.Should().Equal("ssh-weak-ciphers");
		checks.Individual.Disabled.Should().Equal("tls-v1_0-enabled");
		checks.Correlate.Should().BeTrue();
		checks.Potential.Should().BeFalse();
		checks.Unsafe.Should().BeFalse();
		template.Database!.Db2.Should().Be("database");
		template.Database.Oracle.Should().Equal("default", "orcl");
		template.Database.Postgres.Should().Be("postgres");
		template.Policy!.Enabled.Should().Equal(84L, 85L);
		template.Policy.RecursiveWindowsFileSystemSearch.Should().BeTrue();
		template.Policy.StoreScap.Should().BeFalse();
		template.Telnet!.CharacterSet.Should().Be("ASCII");
		template.Telnet.LoginRegex.Should().Be("login:");
		template.Telnet.PasswordPromptRegex.Should().Be("password:");
		template.Telnet.FailedLoginRegex.Should().Be("incorrect");
		template.Telnet.QuestionableLoginRegex.Should().Be("last login");
	}

	[Fact]
	public async Task GetAsync_MapsDiscovery()
	{
		var template = await TestClient.ReadAsync((c, ct) => c.SiteScanTemplate.GetAsync(SiteFixtures.SiteId, ct), TemplateJson);

		var asset = template.Discovery!.Asset!;
		asset.SendIcmpPings.Should().BeTrue();
		asset.SendArpPings.Should().BeTrue();
		asset.TcpPorts.Should().Equal(22, 443);
		asset.UdpPorts.Should().Equal(161);
		asset.TreatTcpResetAsAsset.Should().BeTrue();
		asset.IpFingerprintingEnabled.Should().BeTrue();
		asset.FingerprintRetries.Should().Be(2);
		asset.FingerprintMinimumCertainty.Should().Be(0.16);
		asset.CollectWhoisInformation.Should().BeFalse();
		var service = template.Discovery.Service!;
		service.Tcp!.Ports.Should().Be(ScanTemplatePortSet.WellKnown);
		service.Tcp.AdditionalPorts.Should().Be("3078,8000-8080");
		service.Tcp.ExcludedPorts.Should().Be("1024");
		service.Tcp.Method.Should().Be(ScanTemplateTcpMethod.SynRst);
		service.Udp!.Ports.Should().Be(ScanTemplatePortSet.Custom);
		service.Udp.AdditionalPorts.Should().Be("4020-4032");
		service.Udp.ExcludedPorts.Should().Be("9899");
		service.ServiceNameFile.Should().Be("custom-services.txt");
	}

	[Fact]
	public async Task GetAsync_MapsDiscoveryPerformance()
	{
		var template = await TestClient.ReadAsync((c, ct) => c.SiteScanTemplate.GetAsync(SiteFixtures.SiteId, ct), TemplateJson);

		var performance = template.Discovery!.Performance!;
		performance.PacketRate!.Minimum.Should().Be(450);
		performance.PacketRate.Maximum.Should().Be(15000);
		performance.PacketRate.DefeatRateLimit.Should().BeTrue();
		performance.Parallelism!.Minimum.Should().Be(0);
		performance.Parallelism.Maximum.Should().Be(1000);
		performance.ScanDelay!.Minimum.Should().Be("PT0S");
		performance.ScanDelay.Maximum.Should().Be("PT1S");
		performance.Timeout!.Initial.Should().Be("PT0.5S");
		performance.Timeout.Minimum.Should().Be("PT0S");
		performance.Timeout.Maximum.Should().Be("PT3S");
		performance.RetryLimit.Should().Be(3);
	}

	[Fact]
	public async Task SetAsync_PutsTheTemplateIdAsAJsonString()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteScanTemplate.SetAsync(SiteFixtures.SiteId, "discovery", ct), SiteFixtures.LinksJson);

		call.ShouldBe(HttpMethod.Put, "/api/3/sites/7/scan_template", body: "\"discovery\"");
	}

	[Fact]
	public async Task SetAsync_MapsTheLinks()
	{
		var links = await TestClient.ReadAsync((c, ct) => c.SiteScanTemplate.SetAsync(SiteFixtures.SiteId, "discovery", ct), SiteFixtures.LinksJson);

		links.ShouldBeSiteLinks();
	}

	[Fact]
	public Task GetAsync_NotFound_RaisesRapid7ApiException()
		=> SiteFixtures.ShouldRaiseNotFoundAsync((c, ct) => c.SiteScanTemplate.GetAsync(SiteFixtures.SiteId, ct));
}
