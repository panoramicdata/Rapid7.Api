using System.Net;
using System.Text.Json;
using Rapid7.Api.Models.Users;
using Rapid7.Api.Test.Support;

namespace Rapid7.Api.Test.Groups;

public class RolesTests
{
	[Fact]
	public async Task ListAsync_SendsGetToRoles()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.Roles.ListAsync(ct), UserJson.Roles);

		call.ShouldBe(HttpMethod.Get, "/api/3/roles");
	}

	[Fact]
	public async Task ListAsync_MapsTheRoles()
	{
		var roles = await TestClient.ReadAsync((c, ct) => c.Roles.ListAsync(ct), UserJson.Roles);

		ShouldBeTheAuditor(roles.Resources.Should().ContainSingle().Subject);
		roles.Items.Should().ContainSingle().Which.Href.Should().Be("https://console.test:3780/api/3/roles");
	}

	[Fact]
	public async Task GetAsync_SendsGetToTheEscapedRole()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.Roles.GetAsync("custom auditor", ct), UserJson.Role);

		call.ShouldBe(HttpMethod.Get, "/api/3/roles/custom%20auditor");
	}

	[Fact]
	public async Task GetAsync_MapsEveryField()
		=> ShouldBeTheAuditor(await TestClient.ReadAsync((c, ct) => c.Roles.GetAsync("custom-auditor", ct), UserJson.Role));

	[Fact]
	public async Task UpdateAsync_PutsTheRole()
	{
		var request = new RoleRequest
		{
			Id = "custom-auditor",
			Name = new LocalizedMessage { DefaultValue = "Auditor" },
			Description = new LocalizedMessage { Key = "role.auditor.description", DefaultValue = "Reads everything", Arguments = [JsonSerializer.SerializeToElement(1)] },
			Privileges = ["view-site-asset-data"]
		};

		var call = await TestClient.CaptureAsync((c, ct) => c.Roles.UpdateAsync("custom-auditor", request, ct), AccessJson.LinksOnly);

		call.ShouldBe(
			HttpMethod.Put,
			"/api/3/roles/custom-auditor",
			body: """{"id":"custom-auditor","name":{"defaultValue":"Auditor","arguments":[]},"description":{"key":"role.auditor.description","defaultValue":"Reads everything","arguments":[1]},"privileges":["view-site-asset-data"]}""");
	}

	[Fact]
	public async Task DeleteAsync_SendsDeleteToTheRole()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.Roles.DeleteAsync("custom-auditor", ct), AccessJson.LinksOnly);

		call.ShouldBe(HttpMethod.Delete, "/api/3/roles/custom-auditor");
	}

	[Fact]
	public async Task ListUsersAsync_SendsGetToTheRolesUsers()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.Roles.ListUsersAsync("user", ct), AccessJson.Ids);

		call.ShouldBe(HttpMethod.Get, "/api/3/roles/user/users");
	}

	[Fact]
	public async Task ListUsersAsync_MapsTheUserIds()
		=> AccessJson.ShouldBeTheIds(await TestClient.ReadAsync((c, ct) => c.Roles.ListUsersAsync("user", ct), AccessJson.Ids));

	[Fact]
	public async Task GetAsync_RoleNameOfTheWrongShape_FailsToDeserialize()
	{
		var act = () => TestClient.ReadAsync((c, ct) => c.Roles.GetAsync("x", ct), """{"id":"x","name":42}""");

		(await act.Should().ThrowAsync<Refit.ApiException>()).WithInnerException<JsonException>();
	}

	[Fact]
	public Task DeleteAsync_BuiltInRole_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync(
			(c, ct) => c.Roles.DeleteAsync("global-admin", ct),
			HttpStatusCode.BadRequest,
			AccessJson.Error("BAD_REQUEST", "Built-in roles cannot be deleted."),
			"Built-in roles cannot be deleted.");

	[Fact]
	public void LocalizedMessage_ToString_PrefersTheDefaultText()
	{
		new LocalizedMessage { Key = "k", DefaultValue = "Text" }.ToString().Should().Be("Text");
		new LocalizedMessage { Key = "k" }.ToString().Should().Be("k");
	}

	private static void ShouldBeTheAuditor(Role role)
	{
		role.Id.Should().Be("custom-auditor");
		role.Name!.Key.Should().Be("role.auditor.name");
		role.Name.DefaultValue.Should().Be("Auditor");
		role.Name.Arguments.Select(a => a.ToString()).Should().Equal("a", "2");
		role.Description!.DefaultValue.Should().Be("Reads everything");
		role.Description.Key.Should().BeNull();
		role.Description.Arguments.Should().BeEmpty();
		role.Privileges.Should().Equal("view-site-asset-data");
		role.Items.Should().ContainSingle().Which.Rel.Should().Be("self");
	}
}
