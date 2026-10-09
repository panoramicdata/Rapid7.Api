using Rapid7.Api.Models.Credentials;
using Rapid7.Api.Models.Sites;

namespace Rapid7.Api.IntegrationTest.Sites;

[Collection(Rapid7TestGroup.Name)]
public class SiteCredentialsIntegrationTests(Rapid7Fixture fixture)
{
	private static SiteCredential Credential(string name, string? description = null) => new()
	{
		Account = new CredentialAccount { Service = CredentialService.Cifs, Username = "rapid7api-test", Password = "not-a-real-password" },
		Description = description,
		Name = name,
		HostRestriction = "192.0.2.10"
	};

	[Fact]
	public async Task SiteCredential_RoundTrip()
	{
		var ct = TestContext.Current.CancellationToken;
		await using var site = await TestSite.CreateAsync(fixture.Client, ct);
		var credentials = fixture.Client.SiteCredentials;

		(await credentials.ListAsync(site.Id, ct)).Resources.Should().BeEmpty();
		var created = await credentials.CreateAsync(site.Id, Credential(Rapid7Fixture.UniqueName("credential")), ct);
		created.Id.Should().BePositive();

		await credentials.UpdateAsync(site.Id, created.Id, Credential(site.Name, "updated"), ct);
		await credentials.SetEnabledAsync(site.Id, created.Id, false, ct);
		var read = await credentials.GetAsync(site.Id, created.Id, ct);
		read.Name.Should().Be(site.Name);
		read.Description.Should().Be("updated");
		read.Enabled.Should().BeFalse();
		read.HostRestriction.Should().Be("192.0.2.10");

		await credentials.DeleteAsync(site.Id, created.Id, ct);
		(await credentials.ListAsync(site.Id, ct)).Resources.Should().BeEmpty();
	}

	[Fact]
	public async Task SiteCredentials_ReplaceAllAndDeleteAll()
	{
		var ct = TestContext.Current.CancellationToken;
		await using var site = await TestSite.CreateAsync(fixture.Client, ct);
		var credentials = fixture.Client.SiteCredentials;

		await credentials.ReplaceAllAsync(site.Id, [Credential(Rapid7Fixture.UniqueName("a")), Credential(Rapid7Fixture.UniqueName("b"))], ct);
		(await credentials.ListAsync(site.Id, ct)).Resources.Should().HaveCount(2);

		await credentials.DeleteAllAsync(site.Id, ct);
		(await credentials.ListAsync(site.Id, ct)).Resources.Should().BeEmpty();
	}

	[Fact]
	public async Task ListSharedAsync_ReadsTheSharedCredentialsOfANewSite()
	{
		var ct = TestContext.Current.CancellationToken;
		await using var site = await TestSite.CreateAsync(fixture.Client, ct);

		var shared = await fixture.Client.SiteCredentials.ListSharedAsync(site.Id, ct);

		// Shared credentials assigned to all sites appear here; the test only reads them and never toggles one.
		shared.Resources.Should().AllSatisfy(c => c.Id.Should().NotBeNull());
	}
}
