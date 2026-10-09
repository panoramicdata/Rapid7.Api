using Rapid7.Api.Models.Administration;
using Rapid7.Api.Test.Support;

namespace Rapid7.Api.Test.Groups;

public partial class AdministrationTests
{
	[Fact]
	public async Task ExecuteCommandAsync_MapsTheOutput()
	{
		var output = await TestClient.ReadAsync((c, ct) => c.Administration.ExecuteCommandAsync("ver", ct), AdministrationJson.CommandOutput);

		output.Output.Should().Be("Security Console version 6.6.250");
		output.Items.Should().ContainSingle().Which.Rel.Should().Be("self");
	}

	[Fact]
	public async Task GetInfoAsync_MapsTheHost()
	{
		var info = await TestClient.ReadAsync((c, ct) => c.Administration.GetInfoAsync(ct), AdministrationJson.Info);

		info.Host.Should().Be("CONSOLE");
		info.Fqdn.Should().Be("console.example.test");
		info.Ip.Should().Be("192.0.2.10");
		info.OperatingSystem.Should().Be("Ubuntu Linux 22.04");
		info.User.Should().Be("root");
		info.Superuser.Should().BeTrue();
		info.Serial.Should().Be("0000SERIAL");
		info.DistinguishedName.Should().Be("CN=Rapid7 Security Console/ O=Rapid7");
		info.Cpu!.Count.Should().Be(8);
		info.Cpu.ClockSpeed.Should().Be(2600);
		info.Memory!.Free!.Bytes.Should().Be(45006848);
		info.Memory.Free.Formatted.Should().Be("42.9 MB");
		info.Memory.Total!.Bytes.Should().Be(17179869184);
		info.Items.Should().ContainSingle().Which.Rel.Should().Be("self");
	}

	[Fact]
	public async Task GetInfoAsync_MapsTheDiskJvmAndVersion()
	{
		var info = await TestClient.ReadAsync((c, ct) => c.Administration.GetInfoAsync(ct), AdministrationJson.Info);

		info.Disk!.Free!.Formatted.Should().Be("155.1 GB");
		info.Disk.Total!.Bytes.Should().Be(499004735488);
		var installation = info.Disk.Installation!;
		installation.Directory.Should().Be("/opt/rapid7/nexpose");
		installation.Total!.Bytes.Should().Be(12125933077);
		installation.Database!.Bytes.Should().Be(5364047843);
		installation.Scans!.Formatted.Should().Be("1.3 GB");
		installation.Reports!.Bytes.Should().Be(24789050);
		installation.Backups!.Bytes.Should().Be(0);
		info.Jvm!.Name.Should().Be("OpenJDK 64-Bit Server VM");
		info.Jvm.Vendor.Should().Be("Azul Systems, Inc.");
		info.Jvm.Version.Should().Be("17.0.9");
		info.Jvm.StartTime.Should().Be(new DateTimeOffset(2026, 2, 13, 20, 35, 35, 76, TimeSpan.Zero));
		info.Jvm.Uptime.Should().Be("PT8H21M7.978S");
		info.Version!.Semantic.Should().Be("6.6.250");
		info.Version.Build.Should().Be("2026-01-10-14-11");
		info.Version.Changeset.Should().Be("0000changeset");
		info.Version.Platform.Should().Be("Linux64");
		info.Version.Update!.Product.Should().Be("2200922472");
		info.Version.Update.Content.Should().Be("3192129162");
		info.Version.Update.ContentPartial.Should().Be("723680177");
		info.Version.Update.Id!.ProductId.Should().Be("281474976711146");
		info.Version.Update.Id.VersionId.Should().Be("490");
	}

	[Fact]
	public async Task GetLicenseAsync_MapsEveryField()
	{
		var license = await TestClient.ReadAsync((c, ct) => c.Administration.GetLicenseAsync(ct), AdministrationJson.License);

		license.Status.Should().Be(LicenseStatus.EvaluationMode);
		license.Edition.Should().Be("InsightVM");
		license.Evaluation.Should().BeTrue();
		license.Perpetual.Should().BeFalse();
		license.Expires.Should().Be(new DateTimeOffset(2026, 12, 31, 23, 59, 59, 999, TimeSpan.Zero));
		license.Limits.Should().BeEquivalentTo(new LicenseLimits { Assets = 100000, AssetsWithHostedEngine = 1000, ScanEngines = 100, Users = 1000 });
		license.Features.Should().BeEquivalentTo(new LicenseFeatures
		{
			AdaptiveSecurity = false,
			Agents = true,
			DynamicDiscovery = true,
			EarlyAccess = false,
			EnginePool = true,
			InsightPlatform = true,
			Mobile = true,
			Multitenancy = false,
			PolicyEditor = true,
			PolicyManager = true,
			RemediationAnalytics = true,
			Reporting = new LicenseReporting { Advanced = true, CustomizableCsvExport = true, Pci = false },
			Scanning = new LicenseScanning
			{
				Discovery = true,
				Scada = false,
				Virtual = true,
				WebApplication = true,
				Policy = new LicensePolicyScanning
				{
					Scanning = true,
					Benchmarks = new LicensePolicyBenchmarks { Cis = true, Disa = false, Fdcc = true, Usgcb = false, Custom = true }
				}
			}
		});
		license.Items.Should().ContainSingle().Which.Rel.Should().Be("self");
	}

	[Theory]
	[InlineData("Activated", LicenseStatus.Activated)]
	[InlineData("Unlicensed", LicenseStatus.Unlicensed)]
	[InlineData("Expired", LicenseStatus.Expired)]
	[InlineData("Revoked", LicenseStatus.Revoked)]
	[InlineData("Unknown", LicenseStatus.Unknown)]
	[InlineData("Suspended", LicenseStatus.Unknown)]
	public async Task GetLicenseAsync_MapsEachStatus(string wire, LicenseStatus expected)
	{
		var license = await TestClient.ReadAsync((c, ct) => c.Administration.GetLicenseAsync(ct), $$"""{"status":"{{wire}}","links":[]}""");

		license.Status.Should().Be(expected);
	}

	[Fact]
	public async Task GetPropertiesAsync_MapsEachProperty()
	{
		var properties = await TestClient.ReadAsync((c, ct) => c.Administration.GetPropertiesAsync(ct), AdministrationJson.Properties);

		properties.Properties.Should().Equal(new Dictionary<string, string>
		{
			["java.version"] = "17.0.9",
			["os.name"] = "Linux",
			["nexpose.port"] = "3780"
		});
		properties.Items.Should().ContainSingle().Which.Rel.Should().Be("self");
	}

	[Fact]
	public async Task GetSettingsAsync_MapsTheTopLevelSettings()
	{
		var settings = await TestClient.ReadAsync((c, ct) => c.Administration.GetSettingsAsync(ct), AdministrationJson.Settings);

		settings.Uuid.Should().Be("00000000-0000-0000-0000-000000000001");
		settings.SerialNumber.Should().Be("0000SERIAL");
		settings.Directory.Should().Be("/opt/rapid7/nexpose");
		settings.AssetLinking.Should().BeTrue();
		settings.InsightPlatform.Should().BeTrue();
		settings.InsightPlatformRegion.Should().Be("us-east-1");
		settings.Authentication.Should().BeEquivalentTo(new AuthenticationSettings { TwoFactorAuthentication = true, LoginLockThreshold = 5 });
		settings.Smtp.Should().BeEquivalentTo(new SmtpSettings { Host = "mail.example.test", Port = 25, Sender = "security@example.test", DistributionId = "d-1" });
		settings.Updates.Should().BeEquivalentTo(new UpdateSettings { Enabled = true, ProductAutoUpdate = false, ContentAutoUpdate = true });
		settings.Web.Should().BeEquivalentTo(new WebSettings { Port = 3780, MinThreads = 10, MaxThreads = 100, SessionTimeout = "PT10M" });
		settings.Items.Should().ContainSingle().Which.Rel.Should().Be("self");
	}

	[Fact]
	public async Task GetSettingsAsync_MapsTheDatabaseRiskAndScanSettings()
	{
		var settings = await TestClient.ReadAsync((c, ct) => c.Administration.GetSettingsAsync(ct), AdministrationJson.Settings);

		settings.Database.Should().BeEquivalentTo(new DatabaseSettings
		{
			Vendor = "postgresql",
			Host = "127.0.0.1",
			Port = 5432,
			Url = "//127.0.0.1:5432/nexpose",
			User = "nxpgsql",
			MaintenanceThreadPoolSize = 20,
			Connection = new DatabaseConnectionSettings { MaximumPoolSize = -1, MaximumAdministrationPoolSize = 4, MaximumPreparedStatementPoolSize = 256 }
		});
		settings.Risk.Should().BeEquivalentTo(new RiskSettings
		{
			Model = "risk_score_v2",
			AdjustWithCriticality = true,
			CriticalityModifiers = new RiskModifierSettings { VeryHigh = 2, High = 1.5, Medium = 1, Low = 0.75, VeryLow = 0.5 }
		});
		settings.Scan.Should().BeEquivalentTo(new GlobalScanSettings
		{
			ConnectionTimeout = "PT15S",
			ReadTimeout = "PT15M",
			StatusIdleTimeout = "PT3M",
			StatusThreads = 3,
			MaximumThreads = -1,
			Incremental = true
		});
	}
}
