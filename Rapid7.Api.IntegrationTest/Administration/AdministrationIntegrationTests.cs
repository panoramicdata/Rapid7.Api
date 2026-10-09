using Rapid7.Api.Models.Administration;

namespace Rapid7.Api.IntegrationTest.Administration;

/// <summary>
/// Reads console administration details. It never runs console commands, changes the licence or changes settings.
/// </summary>
[Collection(Rapid7TestGroup.Name)]
public class AdministrationIntegrationTests(Rapid7Fixture fixture)
{
	private static CancellationToken Ct => TestContext.Current.CancellationToken;

	[Fact]
	public async Task GetInfoAsync_ReadsTheHostAndVersion()
	{
		var info = await fixture.Client.Administration.GetInfoAsync(Ct);

		info.Host.Should().NotBeNullOrEmpty();
		info.Version!.Semantic.Should().NotBeNullOrEmpty();
		info.Memory!.Total!.Bytes.Should().BePositive();
	}

	[Fact]
	public async Task GetLicenseAsync_ReadsTheStatus()
	{
		var license = await fixture.Client.Administration.GetLicenseAsync(Ct);

		license.Status.Should().NotBeNull();
		license.Limits.Should().NotBeNull();
	}

	[Fact]
	public async Task GetPropertiesAsync_ReadsTheJavaVersion()
	{
		var properties = await fixture.Client.Administration.GetPropertiesAsync(Ct);

		properties.Properties.Should().ContainKey("java.version");
	}

	[Fact]
	public async Task GetSettingsAsync_ReadsTheWebPort()
	{
		var settings = await fixture.Client.Administration.GetSettingsAsync(Ct);

		settings.Web!.Port.Should().BePositive();
		settings.Database.Should().NotBeNull();
	}

	[Fact]
	public async Task GetLogsAsync_DownloadsAZip()
	{
		using var content = await fixture.Client.Administration.GetLogsAsync([ConsoleLog.Auth], Ct);

		var bytes = await content.ReadAsByteArrayAsync(Ct);
		bytes.Should().HaveCountGreaterThan(4);
		bytes.Take(2).Should().Equal((byte)'P', (byte)'K');
	}
}
