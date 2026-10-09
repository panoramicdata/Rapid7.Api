using Rapid7.Api.Models.Assets;
using Rapid7.Api.Test.Support;
using System.Net;
using static Rapid7.Api.Test.Groups.AssetSamples;

namespace Rapid7.Api.Test.Groups;

public class AssetServicesTests
{
	private const string ServicePath = "/api/3/assets/282/services/tcp/139";

	private const string ServiceReference = $$"""{"links":[{{SelfLink}}],"nic":"eth0","port":22,"protocol":"tcp"}""";

	[Fact]
	public Task ListAsync_SendsGet()
		=> SendsAsync((c, ct) => c.AssetServices.ListAsync(282, ct), HttpMethod.Get, "/api/3/assets/282/services", response: EmptyList);

	[Fact]
	public Task GetAsync_SendsGetWithTheNic()
		=> SendsAsync(
			(c, ct) => c.AssetServices.GetAsync(282, ServiceProtocol.Tcp, 139, new AssetServiceOptions { Nic = "eth0" }, ct),
			HttpMethod.Get,
			ServicePath,
			"?nic=eth0",
			response: ServiceJson);

	[Fact]
	public Task GetAsync_WithoutOptions_SendsNoQuery()
		=> SendsAsync((c, ct) => c.AssetServices.GetAsync(282, ServiceProtocol.Udp, 161, null, ct), HttpMethod.Get, "/api/3/assets/282/services/udp/161", response: ServiceJson);

	[Fact]
	public Task ListConfigurationsAsync_SendsGet()
		=> SendsAsync((c, ct) => c.AssetServices.ListConfigurationsAsync(282, ServiceProtocol.Tcp, 139, ct), HttpMethod.Get, ServicePath + "/configurations", response: EmptyList);

	[Fact]
	public Task ListDatabasesAsync_SendsGet()
		=> SendsAsync((c, ct) => c.AssetServices.ListDatabasesAsync(282, ServiceProtocol.Tcp, 139, ct), HttpMethod.Get, ServicePath + "/databases", response: EmptyList);

	[Fact]
	public Task ListUserGroupsAsync_SendsGet()
		=> SendsAsync((c, ct) => c.AssetServices.ListUserGroupsAsync(282, ServiceProtocol.Tcp, 139, ct), HttpMethod.Get, ServicePath + "/user_groups", response: EmptyList);

	[Fact]
	public Task ListUsersAsync_SendsGet()
		=> SendsAsync((c, ct) => c.AssetServices.ListUsersAsync(282, ServiceProtocol.Tcp, 139, ct), HttpMethod.Get, ServicePath + "/users", response: EmptyList);

	[Fact]
	public Task ListWebApplicationsAsync_SendsGet()
		=> SendsAsync((c, ct) => c.AssetServices.ListWebApplicationsAsync(282, ServiceProtocol.Tcp, 139, ct), HttpMethod.Get, ServicePath + "/web_applications", response: EmptyList);

	[Fact]
	public Task GetWebApplicationAsync_SendsGet()
		=> SendsAsync(
			(c, ct) => c.AssetServices.GetWebApplicationAsync(282, ServiceProtocol.Tcp, 139, 30712, ct),
			HttpMethod.Get,
			ServicePath + "/web_applications/30712",
			response: WebApplicationJson);

	[Fact]
	public async Task ListAsync_MapsEveryField()
	{
		var list = await TestClient.ReadAsync((c, ct) => c.AssetServices.ListAsync(282, ct), List(ServiceReference));

		var service = list.Resources.Should().ContainSingle().Subject;
		ShouldRoundTrip(service, ServiceReference);
		service.Protocol.Should().Be(ServiceProtocol.Tcp);
		service.Port.Should().Be(22);
	}

	[Fact]
	public async Task GetAsync_MapsEveryField()
	{
		var service = await TestClient.ReadAsync((c, ct) => c.AssetServices.GetAsync(282, ServiceProtocol.Tcp, 139, null, ct), ServiceJson);

		ShouldRoundTrip(service, ServiceJson);
		service.Port.Should().Be(139);
		service.Users[0].FullName.Should().Be("Smith, John");
	}

	[Fact]
	public async Task ListConfigurationsAsync_MapsEveryField()
	{
		var list = await TestClient.ReadAsync((c, ct) => c.AssetServices.ListConfigurationsAsync(282, ServiceProtocol.Tcp, 139, ct), List(ConfigurationJson));

		ShouldRoundTrip(list.Resources.Should().ContainSingle().Subject, ConfigurationJson);
	}

	[Fact]
	public async Task ListDatabasesAsync_MapsEveryField()
	{
		var list = await TestClient.ReadAsync((c, ct) => c.AssetServices.ListDatabasesAsync(282, ServiceProtocol.Tcp, 139, ct), List(DatabaseJson));

		list.Resources.Should().ContainSingle().Which.Name.Should().Be("MSSQL");
	}

	[Fact]
	public async Task ListUserGroupsAsync_MapsEveryField()
	{
		var list = await TestClient.ReadAsync((c, ct) => c.AssetServices.ListUserGroupsAsync(282, ServiceProtocol.Tcp, 139, ct), List(GroupAccountJson));

		list.Resources.Should().ContainSingle().Which.Id.Should().Be(972);
	}

	[Fact]
	public async Task ListUsersAsync_MapsEveryField()
	{
		var list = await TestClient.ReadAsync((c, ct) => c.AssetServices.ListUsersAsync(282, ServiceProtocol.Tcp, 139, ct), List(UserAccountJson));

		list.Resources.Should().ContainSingle().Which.Name.Should().Be("john_smith");
	}

	[Fact]
	public async Task ListWebApplicationsAsync_MapsEveryField()
	{
		var list = await TestClient.ReadAsync((c, ct) => c.AssetServices.ListWebApplicationsAsync(282, ServiceProtocol.Tcp, 139, ct), List("""{"id":30712}"""));

		list.Resources.Should().ContainSingle().Which.Id.Should().Be(30712);
	}

	[Fact]
	public async Task GetWebApplicationAsync_MapsEveryField()
	{
		var application = await TestClient.ReadAsync((c, ct) => c.AssetServices.GetWebApplicationAsync(282, ServiceProtocol.Tcp, 139, 30712, ct), WebApplicationJson);

		ShouldRoundTrip(application, WebApplicationJson);
		application.Pages[0].Response.Should().Be(200);
	}

	[Fact]
	public Task GetAsync_NotFound_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync((c, ct) => c.AssetServices.GetAsync(999, ServiceProtocol.Tcp, 1, null, ct), HttpStatusCode.NotFound, NotFound, NotFoundMessage);
}
