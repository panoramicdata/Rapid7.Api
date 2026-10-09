using Rapid7.Api.Test.Support;

namespace Rapid7.Api.Test.Core;

/// <summary>Validation, defaults and display of <see cref="Rapid7ClientOptions"/> and <see cref="Rapid7PlatformOptions"/>.</summary>
public partial class OptionsTests
{
	private static void Construct(Rapid7ClientOptions options)
	{
		using var client = new Rapid7Client(options, new StubHandler());
	}

	private static void Construct(Rapid7PlatformOptions options)
	{
		using var cloud = new Rapid7CloudClient(options, new StubHandler());
		using var export = new Rapid7BulkExportClient(options, new StubHandler());
	}

	[Fact]
	public void Defaults_AreDocumented()
	{
		var options = new Rapid7ClientOptions();

		options.Timeout.Should().Be(TimeSpan.FromSeconds(100));
		options.MaxRetries.Should().Be(3);
		options.RetryBaseDelay.Should().Be(TimeSpan.FromSeconds(1));
		options.MaxRetryDelay.Should().Be(TimeSpan.FromSeconds(30));
		options.ReadOnly.Should().BeFalse();
		options.Logger.Should().BeNull();
		options.TrustedServerCertificateThumbprint.Should().BeNull();
		options.ServerCertificateValidationCallback.Should().BeNull();
		new Rapid7PlatformOptions().Region.Should().BeEmpty();
		Rapid7PlatformOptions.Regions.Should().Equal("us", "us2", "us3", "eu", "ca", "au", "ap", "aps2", "me1");
	}

	[Theory]
	[InlineData("")]
	[InlineData("console.test:3780")]
	[InlineData("/relative/path")]
	[InlineData("ftp://console.test/")]
	[InlineData("file:///c:/temp")]
	public void Console_RejectsANonHttpBaseUrl(string baseUrl)
		=> ShouldReject(() => Construct(TestClient.ConsoleOptions(o => o.BaseUrl = baseUrl)), "BaseUrl", "*absolute http or https*");

	[Theory]
	[InlineData("https://admin:secret@console.test:3780/", "*credentials*")]
	[InlineData("https://console.test:3780/?a=1", "*query string or fragment*")]
	[InlineData("https://console.test:3780/#top", "*query string or fragment*")]
	public void Console_RejectsCredentialsQueryOrFragmentInTheBaseUrl(string baseUrl, string message)
		=> ShouldReject(() => Construct(TestClient.ConsoleOptions(o => o.BaseUrl = baseUrl)), "BaseUrl", message);

	[Theory]
	[InlineData("", "pw")]
	[InlineData("  ", "pw")]
	[InlineData("nxadmin", "")]
	public void Console_RequiresUsernameAndPassword(string username, string password)
		=> ShouldReject(() => Construct(TestClient.ConsoleOptions(o => (o.Username, o.Password) = (username, password))), "Username", "Set both Username and Password.*");

	[Fact]
	public void Console_RejectsAColonInTheUsername()
		=> ShouldReject(() => Construct(TestClient.ConsoleOptions(o => o.Username = "domain:user")), "Username", "*colon*");

	[Theory]
	[InlineData("12\r\n3456")]
	[InlineData("123\t456")]
	public void Console_RejectsControlCharactersInTheTwoFactorToken(string token)
		=> ShouldReject(() => Construct(TestClient.ConsoleOptions(o => o.TwoFactorToken = token)), "TwoFactorToken", "*control characters*");

	[Fact]
	public void Console_AcceptsAPasswordWithAnyCharacters()
		=> FluentActions.Invoking(() => Construct(TestClient.ConsoleOptions(o => o.Password = "p:a@s/s\u00e9"))).Should().NotThrow();

	[Theory]
	[MemberData(nameof(RegionNames))]
	public void Platform_AcceptsEveryRegion(string region)
		=> FluentActions.Invoking(() => Construct(TestClient.PlatformOptions(o => (o.BaseUrl, o.Region) = (null, region)))).Should().NotThrow();

	public static TheoryData<string> RegionNames => [.. Rapid7PlatformOptions.Regions];

	[Theory]
	[InlineData("")]
	[InlineData("US")]
	[InlineData("uk")]
	[InlineData(" us")]
	public void Platform_RejectsAnUnknownRegion_WithoutABaseUrl(string region)
		=> ShouldReject(() => Construct(TestClient.PlatformOptions(o => (o.BaseUrl, o.Region) = ("  ", region))), "Region", "Region must be one of us, us2, us3, eu, ca, au, ap, aps2, me1, or set BaseUrl.*");

	[Theory]
	[InlineData("not a url", "*absolute http or https*")]
	[InlineData("https://key@us.api.insight.test/", "*credentials*")]
	[InlineData("https://us.api.insight.test/?region=us", "*query string or fragment*")]
	public void Platform_RejectsAnInvalidBaseUrl(string baseUrl, string message)
		=> ShouldReject(() => Construct(TestClient.PlatformOptions(o => o.BaseUrl = baseUrl)), "BaseUrl", message);

	[Theory]
	[InlineData("")]
	[InlineData("   ")]
	public void Platform_RequiresAnApiKey(string apiKey)
		=> ShouldReject(() => Construct(TestClient.PlatformOptions(o => o.ApiKey = apiKey)), "ApiKey", "Set ApiKey*");

	[Fact]
	public void Platform_RejectsControlCharactersInTheApiKey()
		=> ShouldReject(() => Construct(TestClient.PlatformOptions(o => o.ApiKey = "key\nX-Injected: 1")), "ApiKey", "*control characters*");

	private static void ShouldReject(Action act, string parameter, string message)
		=> act.Should().Throw<ArgumentException>().WithMessage(message).Which.ParamName.Should().Be(parameter);
}
