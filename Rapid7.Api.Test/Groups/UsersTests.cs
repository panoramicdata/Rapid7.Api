using System.Net;
using Rapid7.Api.Models;
using Rapid7.Api.Models.Users;
using Rapid7.Api.Test.Support;

namespace Rapid7.Api.Test.Groups;

public class UsersTests
{
	private static readonly UserRoleAssignment UserRole = new() { Id = "user", AllSites = true, AllAssetGroups = false, Superuser = false };

	[Fact]
	public async Task ListAsync_SendsGetWithPaging()
	{
		var call = await TestClient.CaptureAsync(
			(c, ct) => c.Users.ListAsync(new PageOptions { Page = 1, Size = 50, Sort = ["login,ASC"] }, ct),
			AccessJson.LinksOnly);

		call.ShouldBe(HttpMethod.Get, "/api/3/users", "?page=1&size=50&sort=login%2CASC");
	}

	[Fact]
	public async Task ListAsync_MapsThePage()
	{
		var page = await TestClient.ReadAsync((c, ct) => c.Users.ListAsync(null, ct), UserJson.UserPage);

		page.PageInfo!.TotalResources.Should().Be(1);
		ShouldBeJane(page.Resources.Should().ContainSingle().Subject);
	}

	[Fact]
	public async Task CreateAsync_PostsTheAccount()
	{
		var request = new UserCreateRequest
		{
			Login = "jsmith",
			Name = "Jane Smith",
			Password = "fake-secret",
			Role = UserRole,
			Email = "jsmith@example.test",
			Enabled = true,
			PasswordResetOnLogin = true,
			Authentication = new AuthenticationSourceAssignment { Type = AuthenticationSourceType.Ldap, Id = 4 },
			Locale = new LocalePreferences { Default = "en-US", Reports = "en-GB" }
		};

		var call = await TestClient.CaptureAsync((c, ct) => c.Users.CreateAsync(request, ct), AccessJson.Created);

		call.ShouldBe(
			HttpMethod.Post,
			"/api/3/users",
			body: """{"password":"fake-secret","login":"jsmith","name":"Jane Smith","role":{"id":"user","allAssetGroups":false,"allSites":true,"superuser":false},"email":"jsmith@example.test","enabled":true,"passwordResetOnLogin":true,"authentication":{"type":"ldap","id":4},"locale":{"default":"en-US","reports":"en-GB"}}""");
	}

	[Fact]
	public async Task CreateAsync_ReturnsTheNewId()
	{
		var request = new UserCreateRequest { Login = "jsmith", Name = "Jane Smith", Password = "fake-secret", Role = new UserRoleAssignment { Id = "user" } };

		var created = await TestClient.ReadAsync((c, ct) => c.Users.CreateAsync(request, ct), AccessJson.Created);

		created.Id.Should().Be(9);
		AccessJson.ShouldBeTheSelfLink(created);
	}

	[Fact]
	public async Task GetAsync_SendsGetToTheUser()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.Users.GetAsync(9, ct), AccessJson.LinksOnly);

		call.ShouldBe(HttpMethod.Get, "/api/3/users/9");
	}

	[Fact]
	public async Task GetAsync_MapsEveryField()
		=> ShouldBeJane(await TestClient.ReadAsync((c, ct) => c.Users.GetAsync(9, ct), UserJson.User));

	[Theory]
	[InlineData(null, """{"login":"jsmith","name":"Jane Smith","role":{"id":"user"}}""")]
	[InlineData("new-fake-secret", """{"password":"new-fake-secret","login":"jsmith","name":"Jane Smith","role":{"id":"user"}}""")]
	public async Task UpdateAsync_PutsOnlyTheSetFields(string? password, string body)
	{
		var request = new UserUpdateRequest { Login = "jsmith", Name = "Jane Smith", Role = new UserRoleAssignment { Id = "user" }, Password = password };

		var call = await TestClient.CaptureAsync((c, ct) => c.Users.UpdateAsync(9, request, ct), AccessJson.LinksOnly);

		call.ShouldBe(HttpMethod.Put, "/api/3/users/9", body: body);
	}

	[Fact]
	public async Task DeleteAsync_SendsDeleteToTheUser()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.Users.DeleteAsync(9, ct), AccessJson.LinksOnly);

		call.ShouldBe(HttpMethod.Delete, "/api/3/users/9");
	}

	[Fact]
	public async Task DeleteAsync_ReturnsTheLinks()
		=> AccessJson.ShouldBeTheSelfLink(await TestClient.ReadAsync((c, ct) => c.Users.DeleteAsync(9, ct), AccessJson.LinksOnly));

	[Fact]
	public async Task UnlockAsync_DeletesTheLock()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.Users.UnlockAsync(9, ct), AccessJson.LinksOnly);

		call.ShouldBe(HttpMethod.Delete, "/api/3/users/9/lock");
	}

	[Fact]
	public async Task ResetPasswordAsync_PutsThePassword()
	{
		var call = await TestClient.CaptureAsync(
			(c, ct) => c.Users.ResetPasswordAsync(9, new PasswordChange { Password = "fake-secret" }, ct),
			AccessJson.LinksOnly);

		call.ShouldBe(HttpMethod.Put, "/api/3/users/9/password", body: """{"password":"fake-secret"}""");
	}

	[Fact]
	public async Task ListPrivilegesAsync_SendsGetToTheUsersPrivileges()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.Users.ListPrivilegesAsync(9, ct), AccessJson.LinksOnly);

		call.ShouldBe(HttpMethod.Get, "/api/3/users/9/privileges");
	}

	[Fact]
	public async Task ListPrivilegesAsync_MapsThePrivileges()
	{
		var privileges = await TestClient.ReadAsync((c, ct) => c.Users.ListPrivilegesAsync(9, ct), UserJson.Privileges);

		privileges.Resources.Should().Equal("all-permissions", "manage-sites");
	}

	[Fact]
	public Task GetAsync_NotFound_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync(
			(c, ct) => c.Users.GetAsync(404, ct),
			HttpStatusCode.NotFound,
			AccessJson.Error("NOT_FOUND", "The resource cannot be found."),
			"The resource cannot be found.");

	[Fact]
	public Task CreateAsync_BadRequest_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync(
			(c, ct) => c.Users.CreateAsync(new UserCreateRequest { Login = "x", Name = "x", Password = "x", Role = UserRole }, ct),
			HttpStatusCode.BadRequest,
			AccessJson.Error("BAD_REQUEST", "The password does not meet the policy."),
			"The password does not meet the policy.");

	private static void ShouldBeJane(User user)
	{
		user.Id.Should().Be(9);
		user.Login.Should().Be("jsmith");
		user.Name.Should().Be("Jane Smith");
		user.Email.Should().Be("jsmith@example.test");
		user.Enabled.Should().BeTrue();
		user.Locked.Should().BeFalse();
		user.Links.Should().ContainSingle().Which.Href.Should().Be("https://console.test:3780/api/3/users/9");
		user.Authentication!.Id.Should().Be(1);
		user.Authentication.Name.Should().Be("Builtin Users");
		user.Authentication.Type.Should().Be(AuthenticationSourceType.Normal);
		user.Authentication.External.Should().BeFalse();
		user.Locale!.Default.Should().Be("en-US");
		user.Locale.Reports.Should().Be("en-GB");
		user.Role!.Id.Should().Be("user");
		user.Role.Name.Should().Be("User");
		user.Role.AllAssetGroups.Should().BeFalse();
		user.Role.AllSites.Should().BeTrue();
		user.Role.Superuser.Should().BeFalse();
		user.Role.Privileges.Should().Equal("view-site-asset-data", "create-reports");
	}
}
