namespace Rapid7.Api.IntegrationTest.Users;

/// <summary>Reads roles, privileges and authentication sources. Nothing is changed.</summary>
[Collection(Rapid7TestGroup.Name)]
public class RolesAndPrivilegesIntegrationTests(Rapid7Fixture fixture)
{
	private static CancellationToken Ct => TestContext.Current.CancellationToken;

	[Fact]
	public async Task Roles_ListGetAndUsers()
	{
		var roles = await fixture.Client.Roles.ListAsync(Ct);
		var globalAdmin = roles.Resources.Should().Contain(r => r.Id == "global-admin").Subject;

		var role = await fixture.Client.Roles.GetAsync("global-admin", Ct);
		var users = await fixture.Client.Roles.ListUsersAsync("global-admin", Ct);

		role.Name!.ToString().Should().NotBeNullOrEmpty();
		role.Privileges.Should().NotBeEmpty();
		globalAdmin.Privileges.Should().BeEquivalentTo(role.Privileges);
		users.Resources.Should().NotBeEmpty("the console has at least one global administrator");
	}

	[Fact]
	public async Task Privileges_ListGetAndUsers()
	{
		var privileges = await fixture.Client.Privileges.ListAsync(Ct);

		var links = await fixture.Client.Privileges.GetAsync("all-permissions", Ct);
		var users = await fixture.Client.Privileges.ListUsersAsync("all-permissions", Ct);

		privileges.Resources.Should().Contain("all-permissions");
		links.Items.Should().NotBeEmpty();
		users.Items.Should().NotBeEmpty();
	}

	[Fact]
	public async Task AuthenticationSources_ListGetAndUsers()
	{
		var sources = await fixture.Client.AuthenticationSources.ListAsync(Ct);
		var first = sources.Resources.Should().NotBeEmpty().And.Subject.First();

		var source = await fixture.Client.AuthenticationSources.GetAsync(first.Id!.Value, Ct);
		var users = await fixture.Client.AuthenticationSources.ListUsersAsync(first.Id.Value, Ct);

		source.Name.Should().Be(first.Name);
		source.Type.Should().NotBe(Models.Users.AuthenticationSourceType.Unknown);
		users.Items.Should().NotBeEmpty();
	}
}
