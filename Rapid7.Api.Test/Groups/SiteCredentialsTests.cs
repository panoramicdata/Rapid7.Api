using Rapid7.Api.Models.Credentials;
using Rapid7.Api.Models.Sites;
using Rapid7.Api.Test.Support;
using System.Net;
using System.Text.Json;

namespace Rapid7.Api.Test.Groups;

public class SiteCredentialsTests
{
	// CredentialAccount belongs to the credential category; build and write it through its own JSON contract.
	private const string AccountJson = """{"service":"ssh","username":"scanner","password":"fake-secret"}""";

	private static readonly string ExpectedAccount = JsonSerializer.Serialize(Account(), Rapid7Json.Options);

	private static readonly string ExpectedCredential =
		$$"""{"account":{{ExpectedAccount}},"description":"Linux scan account","enabled":true,"hostRestriction":"10.0.0.5","name":"Scanner SSH","portRestriction":22}""";

	private const string CredentialJson = """
		{
			"account": { "service": "ssh", "username": "scanner" },
			"description": "Linux scan account",
			"enabled": true,
			"hostRestriction": "10.0.0.5",
			"id": 17,
			"name": "Scanner SSH",
			"portRestriction": 22
		}
		""";

	private const string CredentialListJson = $$"""
		{
			"resources": [ {{CredentialJson}} ],
			"links": [ { "href": "https://console.test:3780/api/3/sites/7/site_credentials", "rel": "self" } ]
		}
		""";

	private const string SharedListJson = """
		{
			"resources": [
				{
					"enabled": false,
					"id": 21,
					"name": "Domain admin",
					"service": "cifs",
					"links": [ { "href": "https://console.test:3780/api/3/shared_credentials/21", "rel": "self" } ]
				}
			],
			"links": [ { "href": "https://console.test:3780/api/3/sites/7/shared_credentials", "rel": "self" } ]
		}
		""";

	private static CredentialAccount Account() => JsonSerializer.Deserialize<CredentialAccount>(AccountJson, Rapid7Json.Options)!;

	private static SiteCredential Credential() => new()
	{
		Account = Account(),
		Description = "Linux scan account",
		Enabled = true,
		HostRestriction = "10.0.0.5",
		Name = "Scanner SSH",
		PortRestriction = 22
	};

	[Fact]
	public async Task ListAsync_SendsGetToTheSiteCredentials()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteCredentials.ListAsync(7, ct), CredentialListJson);

		call.ShouldBe(HttpMethod.Get, "/api/3/sites/7/site_credentials");
	}

	[Fact]
	public async Task ListAsync_MapsEveryField()
	{
		var list = await TestClient.ReadAsync((c, ct) => c.SiteCredentials.ListAsync(7, ct), CredentialListJson);

		var credential = list.Resources.Should().ContainSingle().Subject;
		ShouldBeTheScannerCredential(credential);
		list.Items.Should().ContainSingle().Which.Href.Should().EndWith("/sites/7/site_credentials");
	}

	[Fact]
	public async Task ReplaceAllAsync_SendsPutWithTheCredentialArray()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteCredentials.ReplaceAllAsync(7, [Credential()], ct), SiteMembershipJson.Links);

		call.ShouldBe(HttpMethod.Put, "/api/3/sites/7/site_credentials", body: $"[{ExpectedCredential}]");
	}

	[Fact]
	public async Task CreateAsync_SendsPostWithTheCredential()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteCredentials.CreateAsync(7, Credential(), ct), SiteMembershipJson.Reference);

		call.ShouldBe(HttpMethod.Post, "/api/3/sites/7/site_credentials", body: ExpectedCredential);
	}

	[Fact]
	public async Task CreateAsync_ReturnsTheNewIdentifier()
	{
		var created = await TestClient.ReadAsync(
			(c, ct) => c.SiteCredentials.CreateAsync(7, Credential(), ct),
			"""{"id":17,"links":[{"href":"https://console.test:3780/api/3/sites/7/site_credentials/17","rel":"self"}]}""");

		created.Id.Should().Be(17);
		created.Items.Should().ContainSingle().Which.Href.Should().EndWith("/site_credentials/17");
	}

	[Fact]
	public async Task DeleteAllAsync_SendsDeleteToTheSiteCredentials()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteCredentials.DeleteAllAsync(7, ct), SiteMembershipJson.Links);

		call.ShouldBe(HttpMethod.Delete, "/api/3/sites/7/site_credentials");
	}

	[Fact]
	public async Task GetAsync_SendsGetToTheCredential()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteCredentials.GetAsync(7, 17, ct), CredentialJson);

		call.ShouldBe(HttpMethod.Get, "/api/3/sites/7/site_credentials/17");
	}

	[Fact]
	public async Task GetAsync_MapsEveryField()
	{
		var credential = await TestClient.ReadAsync((c, ct) => c.SiteCredentials.GetAsync(7, 17, ct), CredentialJson);

		ShouldBeTheScannerCredential(credential);
	}

	[Fact]
	public async Task UpdateAsync_SendsPutWithTheCredential()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteCredentials.UpdateAsync(7, 17, Credential(), ct), SiteMembershipJson.Links);

		call.ShouldBe(HttpMethod.Put, "/api/3/sites/7/site_credentials/17", body: ExpectedCredential);
	}

	[Fact]
	public async Task DeleteAsync_SendsDeleteToTheCredential()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteCredentials.DeleteAsync(7, 17, ct), SiteMembershipJson.Links);

		call.ShouldBe(HttpMethod.Delete, "/api/3/sites/7/site_credentials/17");
	}

	[Fact]
	public async Task SetEnabledAsync_SendsPutWithABareBoolean()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteCredentials.SetEnabledAsync(7, 17, false, ct), SiteMembershipJson.Links);

		call.ShouldBe(HttpMethod.Put, "/api/3/sites/7/site_credentials/17/enabled", body: "false");
	}

	[Fact]
	public async Task ListSharedAsync_SendsGetToTheSharedCredentials()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteCredentials.ListSharedAsync(7, ct), SharedListJson);

		call.ShouldBe(HttpMethod.Get, "/api/3/sites/7/shared_credentials");
	}

	[Fact]
	public async Task ListSharedAsync_MapsEveryField()
	{
		var list = await TestClient.ReadAsync((c, ct) => c.SiteCredentials.ListSharedAsync(7, ct), SharedListJson);

		var shared = list.Resources.Should().ContainSingle().Subject;
		shared.Enabled.Should().BeFalse();
		shared.Id.Should().Be(21);
		shared.Name.Should().Be("Domain admin");
		shared.Service.Should().Be(CredentialService.Cifs);
		shared.Items.Should().ContainSingle().Which.Href.Should().EndWith("/shared_credentials/21");
		list.Items.Should().ContainSingle();
	}

	[Fact]
	public async Task SetSharedEnabledAsync_SendsPutWithABareBoolean()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteCredentials.SetSharedEnabledAsync(7, 21, true, ct), SiteMembershipJson.Links);

		call.ShouldBe(HttpMethod.Put, "/api/3/sites/7/shared_credentials/21/enabled", body: "true");
	}

	[Fact]
	public async Task SetSharedEnabledAsync_ReturnsTheLinks()
	{
		var links = await TestClient.ReadAsync((c, ct) => c.SiteCredentials.SetSharedEnabledAsync(7, 21, true, ct), SiteMembershipJson.Links);

		links.ShouldLinkToSite();
	}

	[Fact]
	public Task GetAsync_MissingSite_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync(
			(c, ct) => c.SiteCredentials.GetAsync(404, 17, ct),
			HttpStatusCode.NotFound,
			SiteMembershipJson.NotFound,
			SiteMembershipJson.NotFoundMessage);

	private static void ShouldBeTheScannerCredential(SiteCredential credential)
	{
		credential.Account.Should().NotBeNull();
		credential.Description.Should().Be("Linux scan account");
		credential.Enabled.Should().BeTrue();
		credential.HostRestriction.Should().Be("10.0.0.5");
		credential.Id.Should().Be(17);
		credential.Name.Should().Be("Scanner SSH");
		credential.PortRestriction.Should().Be(22);
	}
}
