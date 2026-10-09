using System.Net;
using Rapid7.Api.Models;
using Rapid7.Api.Test.Support;
using static Rapid7.Api.Test.Groups.AssetSamples;

namespace Rapid7.Api.Test.Groups;

public class DiscoveryConnectionsTests
{
	private const string Connection = $$"""
		{
			"accessKeyId":"AKIAEXAMPLE",
			"address":"vcenter.example.test",
			"arn":"arn:aws:iam::123456789012:role/discovery",
			"awsSessionName":"insightvm",
			"connectionType":"aws",
			"eventSource":"infoblox-trinzic",
			"exchangeServerHostname":"mail.example.test",
			"exchangeUser":"svc-exchange",
			"folderPath":"/var/log/dhcp",
			"id":4,
			"ldapServer":"ldap.example.test",
			"links":[{{SelfLink}}],
			"name":"Connection 1",
			"port":443,
			"protocol":"https",
			"region":"us-east-1",
			"scanEngineIsInsideAWS":true,
			"secretAccessKey":"redacted",
			"status":"connected",
			"username":"svc-discovery",
			"winRMServer":"winrm.example.test"
		}
		""";

	[Fact]
	public Task ListAsync_SendsGetWithPaging()
		=> SendsAsync(
			(c, ct) => c.DiscoveryConnections.ListAsync(new PageOptions { Size = 100 }, ct),
			HttpMethod.Get,
			"/api/3/discovery_connections",
			"?size=100",
			response: EmptyPage);

	[Fact]
	public Task GetAsync_SendsGet()
		=> SendsAsync((c, ct) => c.DiscoveryConnections.GetAsync(4, ct), HttpMethod.Get, "/api/3/discovery_connections/4", response: Connection);

	[Fact]
	public Task ReconnectAsync_SendsPostWithoutABody()
		=> SendsAsync((c, ct) => c.DiscoveryConnections.ReconnectAsync(4, ct), HttpMethod.Post, "/api/3/discovery_connections/4/connect", response: string.Empty);

	[Fact]
	public async Task ListAsync_MapsEveryField()
	{
		var page = await TestClient.ReadAsync((c, ct) => c.DiscoveryConnections.ListAsync(null, ct), Page(Connection));

		var connection = page.Resources.Should().ContainSingle().Subject;
		ShouldRoundTrip(connection, Connection);
		connection.ScanEngineIsInsideAws.Should().BeTrue();
		connection.Port.Should().Be(443);
	}

	[Fact]
	public async Task GetAsync_MapsEveryField()
	{
		var connection = await TestClient.ReadAsync((c, ct) => c.DiscoveryConnections.GetAsync(4, ct), Connection);

		ShouldRoundTrip(connection, Connection);
		connection.Id.Should().Be(4);
	}

	[Fact]
	public Task ReconnectAsync_NotFound_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync((c, ct) => c.DiscoveryConnections.ReconnectAsync(999, ct), HttpStatusCode.NotFound, NotFound, NotFoundMessage);
}
