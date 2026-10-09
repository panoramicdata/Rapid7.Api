using Rapid7.Api.Models.Credentials;

namespace Rapid7.Api.IntegrationTest.Credentials;

/// <summary>
/// Reads shared credentials, and creates, changes and deletes one test credential of its own (assigned to no site). It
/// never calls <c>DeleteAllAsync</c> and never changes another credential.
/// </summary>
[Collection(Rapid7TestGroup.Name)]
public class SharedCredentialsIntegrationTests(Rapid7Fixture fixture)
{
	private static CancellationToken Ct => TestContext.Current.CancellationToken;

	[Fact]
	public async Task ListAsync_ReadsWithoutSecrets()
	{
		var credentials = await fixture.Client.SharedCredentials.ListAsync(Ct);

		credentials.Links.Should().NotBeEmpty();
		credentials.Resources.Should().OnlyContain(c => c.Account == null || c.Account.Password == null);
	}

	[Fact]
	public async Task TestCredential_RoundTrip()
	{
		var name = Rapid7Fixture.UniqueName("credential");
		var request = new SharedCredentialRequest
		{
			Name = name,
			Description = "Rapid7.Api integration test",
			SiteAssignment = CredentialSiteAssignment.SpecificSites,
			Sites = [],
			HostRestriction = "192.0.2.1",
			Account = new CredentialAccount { Service = CredentialService.Ssh, Username = "rapid7api-test", Password = $"Rr7!{Guid.NewGuid():N}" }
		};
		var created = await fixture.Client.SharedCredentials.CreateAsync(request, Ct);
		try
		{
			var credential = await fixture.Client.SharedCredentials.GetAsync(created.Id, Ct);
			credential.Name.Should().Be(name);
			credential.Account!.Service.Should().Be(CredentialService.Ssh);
			credential.Account.Password.Should().BeNull("the console never returns secrets");

			await fixture.Client.SharedCredentials.UpdateAsync(
				created.Id,
				new SharedCredentialRequest
				{
					Name = name,
					Description = "Rapid7.Api integration test (updated)",
					SiteAssignment = CredentialSiteAssignment.SpecificSites,
					Sites = [],
					HostRestriction = "192.0.2.1",
					Account = new CredentialAccount { Service = CredentialService.Ssh, Username = "rapid7api-test", Password = $"Rr7!{Guid.NewGuid():N}" }
				},
				Ct);
			(await fixture.Client.SharedCredentials.GetAsync(created.Id, Ct)).Description.Should().Be("Rapid7.Api integration test (updated)");
		}
		finally
		{
			await fixture.Client.SharedCredentials.DeleteAsync(created.Id, CancellationToken.None);
		}
	}
}
