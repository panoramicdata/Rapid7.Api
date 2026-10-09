using Rapid7.Api.Models.Users;
using Rapid7.Api.Test.Support;
using System.Net;

namespace Rapid7.Api.Test.Groups;

public class AuthenticationSourcesTests
{
	[Fact]
	public async Task ListAsync_SendsGetToAuthenticationSources()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.AuthenticationSources.ListAsync(ct), UserJson.AuthenticationSources);

		call.ShouldBe(HttpMethod.Get, "/api/3/authentication_sources");
	}

	[Fact]
	public async Task ListAsync_MapsTheSources()
	{
		var sources = await TestClient.ReadAsync((c, ct) => c.AuthenticationSources.ListAsync(ct), UserJson.AuthenticationSources);

		ShouldBeTheLdapSource(sources.Resources.Should().ContainSingle().Subject);
		sources.Items.Should().ContainSingle().Which.Href.Should().Be("https://console.test:3780/api/3/authentication_sources");
	}

	[Fact]
	public async Task GetAsync_SendsGetToTheSource()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.AuthenticationSources.GetAsync(4, ct), UserJson.AuthenticationSource);

		call.ShouldBe(HttpMethod.Get, "/api/3/authentication_sources/4");
	}

	[Fact]
	public async Task GetAsync_MapsEveryField()
		=> ShouldBeTheLdapSource(await TestClient.ReadAsync((c, ct) => c.AuthenticationSources.GetAsync(4, ct), UserJson.AuthenticationSource));

	[Fact]
	public async Task ListUsersAsync_SendsGetToTheSourcesUsers()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.AuthenticationSources.ListUsersAsync(4, ct), AccessJson.Ids);

		call.ShouldBe(HttpMethod.Get, "/api/3/authentication_sources/4/users");
	}

	[Fact]
	public async Task ListUsersAsync_MapsTheUserIds()
		=> AccessJson.ShouldBeTheIds(await TestClient.ReadAsync((c, ct) => c.AuthenticationSources.ListUsersAsync(4, ct), AccessJson.Ids));

	[Fact]
	public Task GetAsync_NotFound_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync(
			(c, ct) => c.AuthenticationSources.GetAsync(99, ct),
			HttpStatusCode.NotFound,
			AccessJson.Error("NOT_FOUND", "The resource cannot be found."),
			"The resource cannot be found.");

	private static void ShouldBeTheLdapSource(AuthenticationSource source)
	{
		source.Id.Should().Be(4);
		source.Name.Should().Be("Corporate LDAP");
		source.Type.Should().Be(AuthenticationSourceType.Ldap);
		source.External.Should().BeTrue();
		source.Items.Should().ContainSingle().Which.Rel.Should().Be("self");
	}
}
