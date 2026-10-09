using Rapid7.Api.Models.Cloud;
using Rapid7.Api.Test.Support;
using System.Net;

namespace Rapid7.Api.Test.Groups;

public class CloudSitesTests
{
	[Fact]
	public async Task ListAsync_PostsWithoutABody_AndSendsThePaging()
	{
		var call = await TestClient.CaptureAsync(
			TestClient.CreateCloud,
			(c, ct) => c.Sites.ListAsync(new CursorPageOptions { Page = 2, Size = 3, Sort = ["name,DESC"], Cursor = "c:1" }, ct),
			CloudJson.SitePage);

		call.ShouldBe(HttpMethod.Post, "/vm/v4/integration/sites", "?cursor=c%3A1&page=2&size=3&sort=name%2CDESC");
	}

	[Fact]
	public async Task ListAsync_WithoutPaging_SendsNoQuery()
	{
		var call = await TestClient.CaptureAsync(TestClient.CreateCloud, (c, ct) => c.Sites.ListAsync(null, ct), CloudJson.SitePage);

		call.ShouldBe(HttpMethod.Post, "/vm/v4/integration/sites");
	}

	[Fact]
	public async Task ListAsync_MapsThePage()
	{
		var page = await TestClient.ReadAsync(TestClient.CreateCloud, (c, ct) => c.Sites.ListAsync(null, ct), CloudJson.SitePage);

		page.Data.Select(s => s.Name).Should().Equal("docker hosts", "lab");
		page.Data.Should().OnlyContain(s => s.Type == "SITE");
		page.Metadata!.Cursor.Should().Be("-760687744:::_S:::lab");
		page.Metadata.TotalPages.Should().Be(1);
		page.Items.Should().ContainSingle().Which.Rel.Should().Be("self");
	}

	[Fact]
	public async Task ListAsync_IsAllowedOnAReadOnlyClient()
	{
		var stub = TestClient.Stub(CloudJson.SitePage);
		using var client = TestClient.CreateCloud(stub, o => o.ReadOnly = true);

		await client.Sites.ListAsync(null, TestContext.Current.CancellationToken);

		stub.Calls.Should().ContainSingle();
	}

	[Fact]
	public Task ListAsync_Forbidden_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync(
			TestClient.CreateCloud,
			(c, ct) => c.Sites.ListAsync(null, ct),
			HttpStatusCode.Forbidden,
			"""<ErrorResource><status>403</status><message>Access denied.</message></ErrorResource>""",
			"Access denied.");
}
