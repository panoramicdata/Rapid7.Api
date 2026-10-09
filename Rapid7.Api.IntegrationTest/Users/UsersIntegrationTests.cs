using Rapid7.Api.Models;
using Rapid7.Api.Models.Users;

namespace Rapid7.Api.IntegrationTest.Users;

/// <summary>
/// Reads user accounts, and creates, changes and deletes one test user of its own. It never changes any other user, role
/// or site.
/// </summary>
[Collection(Rapid7TestGroup.Name)]
public class UsersIntegrationTests(Rapid7Fixture fixture)
{
	private static CancellationToken Ct => TestContext.Current.CancellationToken;

	[Fact]
	public async Task ListAsync_ReadsTheFirstPage()
	{
		var page = await fixture.Client.Users.ListAsync(new PageOptions { Size = 5 }, Ct);

		page.PageInfo.Should().NotBeNull();
		page.Resources.Should().NotBeEmpty().And.OnlyContain(u => u.Id != null && u.Login != null);
	}

	[Fact]
	public async Task GetAsync_ReadsAUsersDetailsPrivilegesAndAccess()
	{
		var first = (await fixture.Client.Users.ListAsync(new PageOptions { Size = 1 }, Ct)).Resources.Should().ContainSingle().Subject;
		var id = first.Id!.Value;

		var user = await fixture.Client.Users.GetAsync(id, Ct);
		var privileges = await fixture.Client.Users.ListPrivilegesAsync(id, Ct);
		var sites = await fixture.Client.UserAccess.ListSitesAsync(id, Ct);
		var assetGroups = await fixture.Client.UserAccess.ListAssetGroupsAsync(id, Ct);

		user.Login.Should().Be(first.Login);
		user.Role!.Id.Should().NotBeNullOrEmpty();
		privileges.Items.Should().NotBeEmpty();
		sites.Items.Should().NotBeEmpty();
		assetGroups.Items.Should().NotBeEmpty();
	}

	[Fact]
	public async Task TestUser_RoundTrip()
	{
		var login = Rapid7Fixture.UniqueName("user");
		var created = await fixture.Client.Users.CreateAsync(
			new UserCreateRequest
			{
				Login = login,
				Name = "Rapid7.Api integration test",
				Password = $"Rr7!{Guid.NewGuid():N}",
				Role = new UserRoleAssignment { Id = "user" },
				Enabled = false
			},
			Ct);
		var id = created.Id;
		try
		{
			var user = await fixture.Client.Users.GetAsync(id, Ct);
			user.Login.Should().Be(login);
			user.Enabled.Should().BeFalse();

			await fixture.Client.Users.UpdateAsync(
				id,
				new UserUpdateRequest { Login = login, Name = "Rapid7.Api integration test (renamed)", Role = new UserRoleAssignment { Id = "user" }, Enabled = false },
				Ct);
			(await fixture.Client.Users.GetAsync(id, Ct)).Name.Should().Be("Rapid7.Api integration test (renamed)");

			await fixture.Client.UserAccess.SetSitesAsync(id, [], Ct);
			await fixture.Client.UserAccess.RevokeAllSitesAsync(id, Ct);
			await fixture.Client.UserAccess.SetAssetGroupsAsync(id, [], Ct);
			await fixture.Client.UserAccess.RevokeAllAssetGroupsAsync(id, Ct);
			(await fixture.Client.UserAccess.ListSitesAsync(id, Ct)).Resources.Should().BeEmpty();
			(await fixture.Client.UserAccess.ListAssetGroupsAsync(id, Ct)).Resources.Should().BeEmpty();

			// The key is a secret: only check that the call answers.
			(await fixture.Client.UserTwoFactor.GetKeyAsync(id, Ct)).Should().NotBeNull();
		}
		finally
		{
			await fixture.Client.Users.DeleteAsync(id, CancellationToken.None);
		}
	}
}
