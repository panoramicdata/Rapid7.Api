using Rapid7.Api.Models.Credentials;
using Rapid7.Api.Test.Support;
using System.Net;

namespace Rapid7.Api.Test.Groups;

public class SharedCredentialsTests
{
	// A shared SSH-key credential as the console returns it: the private key and passwords are write-only and absent.
	private const string CredentialJson = """
		{
			"id": 17,
			"name": "Linux estate",
			"description": "Root via sudo",
			"hostRestriction": "10.0.0.0/8",
			"portRestriction": 22,
			"siteAssignment": "specific-sites",
			"sites": [3, 5],
			"account": {
				"service": "ssh-key",
				"username": "scanner",
				"permissionElevation": "sudo",
				"permissionElevationUsername": "root"
			}
		}
		""";

	private const string CredentialsJson = $$"""
		{
			"resources": [{{CredentialJson}}],
			"links": [{ "href": "https://console.test:3780/api/3/shared_credentials", "rel": "self" }]
		}
		""";

	// Every account field at once, so the flat account model is pinned in both directions.
	private const string FullAccountJson = """{"service":"snmpv3","username":"u","password":"fake-p","domain":"d","database":"db","realm":"r","useWindowsAuthentication":true,"ntlmHash":"fake-h","notesIDPassword":"fake-n","sid":"s","enumerateSids":false,"oracleListenerPassword":"fake-l","serviceName":"sn","communityName":"fake-c","authenticationType":"sha","privacyType":"aes-256-with-3-des-key-extension","privacyPassword":"fake-pp","permissionElevation":"privileged-exec","permissionElevationUsername":"eu","permissionElevationPassword":"fake-ep","pemKey":"fake-k","privateKeyPassword":"fake-kp","pemCert":"cert","pemExpiration":"2030-01-01","pkcs12_data":"fake-pk"}""";

	private static readonly CredentialAccount FullAccount = new()
	{
		Service = CredentialService.SnmpV3,
		Username = "u",
		Password = "fake-p",
		Domain = "d",
		Database = "db",
		Realm = "r",
		UseWindowsAuthentication = true,
		NtlmHash = "fake-h",
		NotesIdPassword = "fake-n",
		Sid = "s",
		EnumerateSids = false,
		OracleListenerPassword = "fake-l",
		ServiceName = "sn",
		CommunityName = "fake-c",
		AuthenticationType = SnmpV3AuthenticationType.Sha,
		PrivacyType = SnmpV3PrivacyType.Aes256With3DesKeyExtension,
		PrivacyPassword = "fake-pp",
		PermissionElevation = PermissionElevation.PrivilegedExec,
		PermissionElevationUsername = "eu",
		PermissionElevationPassword = "fake-ep",
		PemKey = "fake-k",
		PrivateKeyPassword = "fake-kp",
		PemCert = "cert",
		PemExpiration = "2030-01-01",
		Pkcs12Data = "fake-pk"
	};

	private static readonly SharedCredentialRequest SshRequest = new()
	{
		Name = "Linux estate",
		SiteAssignment = CredentialSiteAssignment.SpecificSites,
		Sites = [3, 5],
		Description = "Root via sudo",
		HostRestriction = "10.0.0.0/8",
		PortRestriction = 22,
		Account = new CredentialAccount { Service = CredentialService.Ssh, Username = "scanner", Password = "fake-secret" }
	};

	private const string SshRequestJson = """{"name":"Linux estate","account":{"service":"ssh","username":"scanner","password":"fake-secret"},"siteAssignment":"specific-sites","sites":[3,5],"description":"Root via sudo","hostRestriction":"10.0.0.0/8","portRestriction":22}""";

	[Fact]
	public async Task ListAsync_SendsGetToSharedCredentials()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SharedCredentials.ListAsync(ct), CredentialsJson);

		call.ShouldBe(HttpMethod.Get, "/api/3/shared_credentials");
	}

	[Fact]
	public async Task ListAsync_MapsTheCredentials()
	{
		var credentials = await TestClient.ReadAsync((c, ct) => c.SharedCredentials.ListAsync(ct), CredentialsJson);

		ShouldBeTheLinuxCredential(credentials.Resources.Should().ContainSingle().Subject);
		credentials.Items.Should().ContainSingle().Which.Rel.Should().Be("self");
	}

	[Fact]
	public async Task CreateAsync_PostsTheCredential()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SharedCredentials.CreateAsync(SshRequest, ct), AccessJson.Created);

		call.ShouldBe(HttpMethod.Post, "/api/3/shared_credentials", body: SshRequestJson);
	}

	[Fact]
	public async Task CreateAsync_SendsEveryAccountField()
	{
		var request = new SharedCredentialRequest { Name = "n", SiteAssignment = CredentialSiteAssignment.AllSites, Account = FullAccount };

		var call = await TestClient.CaptureAsync((c, ct) => c.SharedCredentials.CreateAsync(request, ct), AccessJson.Created);

		call.ShouldBe(HttpMethod.Post, "/api/3/shared_credentials", body: $$"""{"name":"n","account":{{FullAccountJson}},"siteAssignment":"all-sites"}""");
	}

	[Fact]
	public async Task CreateAsync_ReturnsTheNewId()
	{
		var created = await TestClient.ReadAsync((c, ct) => c.SharedCredentials.CreateAsync(SshRequest, ct), AccessJson.Created);

		created.Id.Should().Be(9);
	}

	[Fact]
	public async Task DeleteAllAsync_SendsDeleteToSharedCredentials()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SharedCredentials.DeleteAllAsync(ct), AccessJson.LinksOnly);

		call.ShouldBe(HttpMethod.Delete, "/api/3/shared_credentials");
	}

	[Fact]
	public async Task GetAsync_SendsGetToTheCredential()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SharedCredentials.GetAsync(17, ct), CredentialJson);

		call.ShouldBe(HttpMethod.Get, "/api/3/shared_credentials/17");
	}

	[Fact]
	public async Task GetAsync_MapsEveryField()
		=> ShouldBeTheLinuxCredential(await TestClient.ReadAsync((c, ct) => c.SharedCredentials.GetAsync(17, ct), CredentialJson));

	[Fact]
	public async Task GetAsync_MapsEveryAccountField()
	{
		var credential = await TestClient.ReadAsync(
			(c, ct) => c.SharedCredentials.GetAsync(17, ct),
			$$"""{"id":17,"name":"n","siteAssignment":"all-sites","sites":null,"account":{{FullAccountJson}}}""");

		credential.Account.Should().BeEquivalentTo(FullAccount);
		credential.SiteAssignment.Should().Be(CredentialSiteAssignment.AllSites);
		credential.Sites.Should().BeEmpty();
	}

	[Fact]
	public async Task UpdateAsync_PutsTheCredential()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SharedCredentials.UpdateAsync(17, SshRequest, ct), AccessJson.LinksOnly);

		call.ShouldBe(HttpMethod.Put, "/api/3/shared_credentials/17", body: SshRequestJson);
	}

	[Fact]
	public async Task DeleteAsync_SendsDeleteToTheCredential()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SharedCredentials.DeleteAsync(17, ct), AccessJson.LinksOnly);

		call.ShouldBe(HttpMethod.Delete, "/api/3/shared_credentials/17");
	}

	[Fact]
	public async Task DeleteAsync_ReturnsTheLinks()
		=> AccessJson.ShouldBeTheSelfLink(await TestClient.ReadAsync((c, ct) => c.SharedCredentials.DeleteAsync(17, ct), AccessJson.LinksOnly));

	[Fact]
	public Task CreateAsync_BadRequest_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync(
			(c, ct) => c.SharedCredentials.CreateAsync(SshRequest, ct),
			HttpStatusCode.BadRequest,
			AccessJson.Error("BAD_REQUEST", "The credential name is already in use."),
			"The credential name is already in use.");

	private static void ShouldBeTheLinuxCredential(SharedCredential credential)
	{
		credential.Id.Should().Be(17);
		credential.Name.Should().Be("Linux estate");
		credential.Description.Should().Be("Root via sudo");
		credential.HostRestriction.Should().Be("10.0.0.0/8");
		credential.PortRestriction.Should().Be(22);
		credential.SiteAssignment.Should().Be(CredentialSiteAssignment.SpecificSites);
		credential.Sites.Should().Equal(3, 5);
		credential.Account!.Service.Should().Be(CredentialService.SshKey);
		credential.Account.Username.Should().Be("scanner");
		credential.Account.PermissionElevation.Should().Be(PermissionElevation.Sudo);
		credential.Account.PermissionElevationUsername.Should().Be("root");
		credential.Account.PemKey.Should().BeNull("secrets are write-only");
		credential.Account.Password.Should().BeNull("secrets are write-only");
	}
}
