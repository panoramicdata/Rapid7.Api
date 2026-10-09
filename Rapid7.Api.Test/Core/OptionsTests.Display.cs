namespace Rapid7.Api.Test.Core;

public partial class OptionsTests
{
	[Fact]
	public void ConsoleToString_MasksThePasswordAndToken()
	{
		var options = new Rapid7ClientOptions
		{
			BaseUrl = "https://console.test:3780/",
			Username = "nxadmin",
			Password = "hunter2",
			TwoFactorToken = "123456",
			ReadOnly = true
		};

		options.ToString().Should().Be("Rapid7ClientOptions { BaseUrl = https://console.test:3780/, Username = nxadmin, Password = ***, TwoFactorToken = ***, ReadOnly = True }");
	}

	[Fact]
	public void ConsoleToString_ShowsUnsetValuesAsNone()
		=> new Rapid7ClientOptions().ToString().Should().Be("Rapid7ClientOptions { BaseUrl = (none), Username = , Password = (none), TwoFactorToken = (none), ReadOnly = False }");

	[Fact]
	public void PlatformToString_MasksTheApiKey()
	{
		new Rapid7PlatformOptions { Region = "eu", ApiKey = "secret-key" }.ToString()
			.Should().Be("Rapid7PlatformOptions { Region = eu, BaseUrl = (none), ApiKey = ***, ReadOnly = False }");
		new Rapid7PlatformOptions { BaseUrl = "https://k:secret@proxy.test/r7", ReadOnly = true }.ToString()
			.Should().Be("Rapid7PlatformOptions { Region = , BaseUrl = https://***@proxy.test/r7, ApiKey = (none), ReadOnly = True }");
	}

	[Theory]
	[InlineData("https://console.test:3780/api", "https://console.test:3780/api")]
	[InlineData("https://admin:secret@console.test:3780/", "https://***@console.test:3780/")]
	[InlineData("https://admin:se@cret@console.test:3780/x?y#z", "https://***@console.test:3780/x?y#z")]
	[InlineData("https://admin:se/cret@console.test:3780/", "https://***@console.test:3780/")]
	[InlineData("admin:secret@console.test:3780", "***@console.test:3780")]
	[InlineData("console.test:3780", "console.test:3780")]
	public void BaseUrlCredentials_AreMaskedInToString_EvenWhenTheUrlDoesNotParse(string baseUrl, string shown)
		=> new Rapid7ClientOptions { BaseUrl = baseUrl }.ToString().Should().Contain($"BaseUrl = {shown},").And.NotContain("secret");
}
