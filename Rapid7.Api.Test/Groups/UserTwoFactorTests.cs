using System.Net;
using Rapid7.Api.Test.Support;

namespace Rapid7.Api.Test.Groups;

public class UserTwoFactorTests
{
	[Fact]
	public async Task GetKeyAsync_SendsGetTo2FA()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.UserTwoFactor.GetKeyAsync(9, ct), UserJson.TwoFactorKey);

		call.ShouldBe(HttpMethod.Get, "/api/3/users/9/2FA");
	}

	[Fact]
	public async Task GetKeyAsync_MapsTheKey()
	{
		var key = await TestClient.ReadAsync((c, ct) => c.UserTwoFactor.GetKeyAsync(9, ct), UserJson.TwoFactorKey);

		key.Key.Should().Be("FAKESEEDFAKESEED");
		key.Items.Should().ContainSingle().Which.Href.Should().Be("https://console.test:3780/api/3/users/9/2FA");
	}

	[Fact]
	public async Task RegenerateKeyAsync_PostsTo2FAWithoutABody()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.UserTwoFactor.RegenerateKeyAsync(9, ct), UserJson.TwoFactorKey);

		call.ShouldBe(HttpMethod.Post, "/api/3/users/9/2FA");
	}

	[Fact]
	public async Task RegenerateKeyAsync_ReturnsTheNewKey()
	{
		var key = await TestClient.ReadAsync((c, ct) => c.UserTwoFactor.RegenerateKeyAsync(9, ct), UserJson.TwoFactorKey);

		key.Key.Should().Be("FAKESEEDFAKESEED");
	}

	[Fact]
	public async Task SetKeyAsync_PutsTheKeyAsAJsonString()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.UserTwoFactor.SetKeyAsync(9, "FAKESEED", ct), AccessJson.LinksOnly);

		call.ShouldBe(HttpMethod.Put, "/api/3/users/9/2FA", body: "\"FAKESEED\"");
	}

	[Fact]
	public Task SetKeyAsync_BadRequest_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync(
			(c, ct) => c.UserTwoFactor.SetKeyAsync(9, "x", ct),
			HttpStatusCode.BadRequest,
			AccessJson.Error("BAD_REQUEST", "Two-factor authentication is not enabled."),
			"Two-factor authentication is not enabled.");
}
