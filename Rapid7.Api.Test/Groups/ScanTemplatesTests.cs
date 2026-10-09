using Rapid7.Api.Models.ScanTemplates;
using Rapid7.Api.Test.Support;
using System.Net;
using System.Text.Json.Nodes;

namespace Rapid7.Api.Test.Groups;

public class ScanTemplatesTests
{
	private const string Created = """{"id":"my-template","links":[]}""";

	private static readonly ScanTemplate Minimal = new()
	{
		Name = "My template",
		DiscoveryOnly = true,
		Discovery = new ScanTemplateDiscovery
		{
			Service = new ScanTemplateServiceDiscovery { Tcp = new ScanTemplateTcpDiscovery { Ports = ScanTemplatePortSelection.All, Method = TcpDiscoveryMethod.Full } }
		}
	};

	[Fact]
	public async Task ListAsync_SendsGet()
		=> (await TestClient.CaptureAsync((c, ct) => c.ScanTemplates.ListAsync(ct), ScanJson.Empty))
			.ShouldBe(HttpMethod.Get, "/api/3/scan_templates");

	[Fact]
	public async Task CreateAsync_PostsOnlyWhatWasSet()
		=> (await TestClient.CaptureAsync((c, ct) => c.ScanTemplates.CreateAsync(Minimal, ct), Created))
			.ShouldBe(
				HttpMethod.Post,
				"/api/3/scan_templates",
				body: """{"name":"My template","discoveryOnly":true,"discovery":{"service":{"tcp":{"method":"Full","ports":"all"}}}}""");

	[Fact]
	public async Task GetAsync_SendsGet()
		=> (await TestClient.CaptureAsync((c, ct) => c.ScanTemplates.GetAsync("full-audit-without-web-spider", ct), ScanTemplateJson.Full))
			.ShouldBe(HttpMethod.Get, "/api/3/scan_templates/full-audit-without-web-spider");

	[Fact]
	public async Task UpdateAsync_PutsTheTemplate()
		=> (await TestClient.CaptureAsync((c, ct) => c.ScanTemplates.UpdateAsync("my-template", Minimal with { MaxScanProcesses = 5 }, ct), ScanJson.LinksOnly))
			.ShouldBe(
				HttpMethod.Put,
				"/api/3/scan_templates/my-template",
				body: """{"name":"My template","discoveryOnly":true,"maxScanProcesses":5,"discovery":{"service":{"tcp":{"method":"Full","ports":"all"}}}}""");

	[Fact]
	public async Task DeleteAsync_SendsDelete()
		=> (await TestClient.CaptureAsync((c, ct) => c.ScanTemplates.DeleteAsync("my-template", ct), ScanJson.LinksOnly))
			.ShouldBe(HttpMethod.Delete, "/api/3/scan_templates/my-template");

	[Fact]
	public async Task CreateAsync_ReadsTheNewIdentifier()
		=> (await TestClient.ReadAsync((c, ct) => c.ScanTemplates.CreateAsync(Minimal, ct), Created)).Id.Should().Be("my-template");

	[Fact]
	public async Task ListAsync_MapsTemplates()
	{
		var templates = await TestClient.ReadAsync(
			(c, ct) => c.ScanTemplates.ListAsync(ct),
			$$"""{"resources":[{{ScanTemplateJson.Full}},{"id":"discovery","name":"Discovery Scan","links":[]}],"links":[]}""");

		templates.Resources.Should().HaveCount(2);
		templates.Resources[0].Checks!.Unsafe.Should().BeFalse();
		templates.Resources[1].Id.Should().Be("discovery");
		templates.Resources[1].Discovery.Should().BeNull();
		templates.Resources[1].Web.Should().BeNull();
	}

	[Fact]
	public async Task GetAsync_MapsTheTopLevelSettings()
	{
		var template = await ReadFullAsync();

		template.Id.Should().Be("full-audit-without-web-spider");
		template.Name.Should().Be("Full audit without Web Spider");
		template.Description.Should().Be("Audits every system with safe checks only.");
		template.DiscoveryOnly.Should().BeFalse();
		template.VulnerabilityEnabled.Should().BeTrue();
		template.PolicyEnabled.Should().BeTrue();
		template.WebEnabled.Should().BeFalse();
		template.EnableWindowsServices.Should().BeTrue();
		template.EnhancedLogging.Should().BeFalse();
		template.MaxParallelAssets.Should().Be(10);
		template.MaxScanProcesses.Should().Be(12);
		template.Web!.Value.GetProperty("maxPages").GetInt32().Should().Be(3000);
		template.Links.Should().ContainSingle().Which.Rel.Should().Be("self");
	}

	[Fact]
	public async Task GetAsync_MapsAssetDiscovery()
	{
		var asset = (await ReadFullAsync()).Discovery!.Asset!;

		asset.SendArpPings.Should().BeTrue();
		asset.SendIcmpPings.Should().BeFalse();
		asset.TcpPorts.Should().Equal(22, 443);
		asset.UdpPorts.Should().Equal(161);
		asset.TreatTcpResetAsAsset.Should().BeTrue();
		asset.IpFingerprintingEnabled.Should().BeTrue();
		asset.FingerprintRetries.Should().Be(4);
		asset.FingerprintMinimumCertainty.Should().Be(0.16);
		asset.CollectWhoisInformation.Should().BeFalse();
	}

	[Fact]
	public async Task GetAsync_MapsDiscoveryPerformance()
	{
		var performance = (await ReadFullAsync()).Discovery!.Performance!;

		performance.RetryLimit.Should().Be(3);
		performance.PacketRate!.Minimum.Should().Be(450);
		performance.PacketRate.Maximum.Should().Be(15000);
		performance.PacketRate.DefeatRateLimit.Should().BeTrue();
		performance.Parallelism!.Minimum.Should().Be(0);
		performance.Parallelism.Maximum.Should().Be(1000);
		performance.ScanDelay!.Minimum.Should().Be("PT0S");
		performance.ScanDelay.Maximum.Should().Be("PT0.1S");
		performance.Timeout!.Minimum.Should().Be("PT0S");
		performance.Timeout.Maximum.Should().Be("PT3S");
		performance.Timeout.Initial.Should().Be("PT0.5S");
	}

	[Fact]
	public async Task GetAsync_MapsServiceDiscovery()
	{
		var service = (await ReadFullAsync()).Discovery!.Service!;

		service.ServiceNameFile.Should().Be("custom-services.txt");
		service.Tcp!.Ports.Should().Be(ScanTemplatePortSelection.WellKnown);
		service.Tcp.AdditionalPorts.Should().Be("3078,8000-8080");
		service.Tcp.ExcludedPorts.Should().Be("1024");
		service.Tcp.Method.Should().Be(TcpDiscoveryMethod.SynRst);
		service.Tcp.Links.Should().ContainSingle();
		service.Udp!.Ports.Should().Be(ScanTemplatePortSelection.Custom);
		service.Udp.AdditionalPorts.Should().Be("4020-4032");
		service.Udp.ExcludedPorts.Should().Be("9899");
		service.Udp.Links.Should().BeEmpty();
	}

	[Fact]
	public async Task GetAsync_MapsChecks()
	{
		var checks = (await ReadFullAsync()).Checks!;

		checks.Categories!.Enabled.Should().Equal("Microsoft Windows");
		checks.Categories.Disabled.Should().Equal("Oracle");
		checks.Types!.Enabled.Should().Equal("Safe");
		checks.Types.Disabled.Should().Equal("Policy");
		checks.Individual!.Enabled.Should().Equal("WINDOWS-HOTFIX-MS14-009");
		checks.Individual.Disabled.Should().Equal("ssh-weak-ciphers");
		checks.Correlate.Should().BeTrue();
		checks.Potential.Should().BeFalse();
		checks.Unsafe.Should().BeFalse();
		checks.Links.Should().ContainSingle();
	}

	[Fact]
	public async Task GetAsync_MapsPolicyDatabaseAndTelnet()
	{
		var template = await ReadFullAsync();

		template.Policy!.Enabled.Should().Equal(1001L, 1002L);
		template.Policy.RecursiveWindowsFileSystemSearch.Should().BeTrue();
		template.Policy.StoreScap.Should().BeFalse();
		template.Database!.Db2.Should().Be("database");
		template.Database.Oracle.Should().Equal("ORCL", "XE");
		template.Database.Postgres.Should().Be("postgres");
		template.Telnet!.CharacterSet.Should().Be("ASCII");
		template.Telnet.LoginRegex.Should().Be(@"(?:[l,L]ogin) *\:");
		template.Telnet.PasswordPromptRegex.Should().Be(@"(?:[p,P]assword) *\:");
		template.Telnet.FailedLoginRegex.Should().Be("(?:[i,I]ncorrect|[f,F]ail)");
		template.Telnet.QuestionableLoginRegex.Should().Be(@"(?:[l,L]ast [l,L]ogin *\:)");
	}

	[Theory]
	[InlineData("SYN", TcpDiscoveryMethod.Syn)]
	[InlineData("SYN+FIN", TcpDiscoveryMethod.SynFin)]
	[InlineData("SYN+ECE", TcpDiscoveryMethod.SynEce)]
	[InlineData("Full", TcpDiscoveryMethod.Full)]
	[InlineData("XMAS", TcpDiscoveryMethod.Unknown)]
	public async Task GetAsync_MapsEveryTcpMethod(string wire, TcpDiscoveryMethod expected)
		=> (await TestClient.ReadAsync(
				(c, ct) => c.ScanTemplates.GetAsync("t", ct),
				"""{"discovery":{"service":{"tcp":{"method":""" + $"\"{wire}\"" + """},"udp":{"ports":"none"}}},"links":[]}"""))
			.Discovery!.Service!.Tcp!.Method.Should().Be(expected);

	[Fact]
	public async Task ACopiedTemplate_SendsEveryFieldBack_WithoutItsIdentity()
	{
		var original = await ReadFullAsync();
		var copy = original with { Id = null, Links = null, Name = "Copy of full audit" };

		var call = await TestClient.CaptureAsync((c, ct) => c.ScanTemplates.CreateAsync(copy, ct), Created);

		var expected = JsonNode.Parse(ScanTemplateJson.Full)!.AsObject();
		expected.Remove("id");
		expected.Remove("links");
		expected["name"] = "Copy of full audit";
		JsonNode.DeepEquals(JsonNode.Parse(call.Body!), expected).Should().BeTrue(call.Body);
	}

	[Fact]
	public Task DeleteAsync_BuiltInTemplate_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync(
			(c, ct) => c.ScanTemplates.DeleteAsync("full-audit", ct),
			HttpStatusCode.BadRequest,
			"""{"status":"BAD_REQUEST","message":"Built-in scan templates cannot be deleted.","links":[]}""",
			"Built-in scan templates cannot be deleted.");

	private static Task<ScanTemplate> ReadFullAsync()
		=> TestClient.ReadAsync((c, ct) => c.ScanTemplates.GetAsync("full-audit-without-web-spider", ct), ScanTemplateJson.Full);
}
