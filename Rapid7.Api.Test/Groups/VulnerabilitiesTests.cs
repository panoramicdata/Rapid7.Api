using System.Net;
using Rapid7.Api.Models;
using Rapid7.Api.Test.Support;
using static Rapid7.Api.Test.Groups.VulnerabilitySamples;

namespace Rapid7.Api.Test.Groups;

public class VulnerabilitiesTests
{
	private const string Id = "windows-hotfix-ms03-007";

	private static readonly PageOptions Paging = new() { Page = 2, Size = 50, Sort = ["riskScore,DESC"] };

	private const string PagingQuery = "?page=2&size=50&sort=riskScore%2CDESC";

	[Fact]
	public async Task ListAsync_SendsGetWithPaging()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.Vulnerabilities.ListAsync(Paging, ct), Page(VulnerabilityJson));

		call.ShouldBe(HttpMethod.Get, "/api/3/vulnerabilities", PagingQuery);
	}

	[Fact]
	public async Task ListAsync_MapsThePage()
	{
		var page = await TestClient.ReadAsync((c, ct) => c.Vulnerabilities.ListAsync(null, ct), Page(VulnerabilityJson));

		AssertPage(page);
		AssertVulnerability(page.Resources[0]);
	}

	[Fact]
	public async Task GetAsync_SendsGetToTheEscapedId()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.Vulnerabilities.GetAsync("a/b", ct), VulnerabilityJson);

		call.ShouldBe(HttpMethod.Get, "/api/3/vulnerabilities/a%2Fb");
	}

	[Fact]
	public async Task GetAsync_MapsEveryField()
		=> AssertVulnerability(await TestClient.ReadAsync((c, ct) => c.Vulnerabilities.GetAsync(Id, ct), VulnerabilityJson));

	[Fact]
	public async Task ListAffectedAssetsAsync_SendsGet_AndMapsAssetIds()
	{
		const string json = """{"links":[],"resources":[282,9007199254740993]}""";
		var call = await TestClient.CaptureAsync((c, ct) => c.Vulnerabilities.ListAffectedAssetsAsync(Id, ct), json);
		var assets = await TestClient.ReadAsync((c, ct) => c.Vulnerabilities.ListAffectedAssetsAsync(Id, ct), json);

		call.ShouldBe(HttpMethod.Get, $"/api/3/vulnerabilities/{Id}/assets");
		assets.Resources.Should().Equal(282L, 9007199254740993L);
	}

	[Fact]
	public async Task ListExploitsAsync_SendsGetWithPaging()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.Vulnerabilities.ListExploitsAsync(Id, Paging, ct), Page(ExploitJson));

		call.ShouldBe(HttpMethod.Get, $"/api/3/vulnerabilities/{Id}/exploits", PagingQuery);
	}

	[Fact]
	public async Task ListExploitsAsync_MapsThePage()
	{
		var page = await TestClient.ReadAsync((c, ct) => c.Vulnerabilities.ListExploitsAsync(Id, null, ct), Page(ExploitJson));

		AssertPage(page);
		AssertExploit(page.Resources[0]);
	}

	[Fact]
	public async Task ListMalwareKitsAsync_SendsGetWithPaging()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.Vulnerabilities.ListMalwareKitsAsync(Id, Paging, ct), Page(MalwareKitJson));

		call.ShouldBe(HttpMethod.Get, $"/api/3/vulnerabilities/{Id}/malware_kits", PagingQuery);
	}

	[Fact]
	public async Task ListMalwareKitsAsync_MapsThePage()
	{
		var page = await TestClient.ReadAsync((c, ct) => c.Vulnerabilities.ListMalwareKitsAsync(Id, null, ct), Page(MalwareKitJson));

		AssertPage(page);
		AssertMalwareKit(page.Resources[0]);
	}

	[Fact]
	public async Task ListReferencesAsync_SendsGetWithPaging()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.Vulnerabilities.ListReferencesAsync(Id, Paging, ct), Page(ReferenceJson));

		call.ShouldBe(HttpMethod.Get, $"/api/3/vulnerabilities/{Id}/references", PagingQuery);
	}

	[Fact]
	public async Task ListReferencesAsync_MapsThePage()
	{
		var page = await TestClient.ReadAsync((c, ct) => c.Vulnerabilities.ListReferencesAsync(Id, null, ct), Page(ReferenceJson));

		AssertPage(page);
		AssertReference(page.Resources[0]);
	}

	[Fact]
	public async Task ListSolutionsAsync_SendsGet_AndMapsSolutionIds()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.Vulnerabilities.ListSolutionsAsync(Id, ct), StringIds);

		call.ShouldBe(HttpMethod.Get, $"/api/3/vulnerabilities/{Id}/solutions");
		AssertStringIds(await TestClient.ReadAsync((c, ct) => c.Vulnerabilities.ListSolutionsAsync(Id, ct), StringIds));
	}

	[Fact]
	public void ClientProperties_ForEveryVulnerabilityFamily_AreCreatedOnce()
	{
		using var client = TestClient.Create(new StubHandler());

		client.Vulnerabilities.Should().BeSameAs(client.Vulnerabilities);
		client.VulnerabilityCategories.Should().BeSameAs(client.VulnerabilityCategories);
		client.VulnerabilityReferences.Should().BeSameAs(client.VulnerabilityReferences);
		client.Exploits.Should().BeSameAs(client.Exploits);
		client.MalwareKits.Should().BeSameAs(client.MalwareKits);
		client.Solutions.Should().BeSameAs(client.Solutions);
		client.VulnerabilityResults.Should().BeSameAs(client.VulnerabilityResults);
		client.VulnerabilityValidations.Should().BeSameAs(client.VulnerabilityValidations);
		client.VulnerabilityExceptions.Should().BeSameAs(client.VulnerabilityExceptions);
		client.VulnerabilityChecks.Should().BeSameAs(client.VulnerabilityChecks);
		client.Remediations.Should().BeSameAs(client.Remediations);
	}

	[Fact]
	public Task GetAsync_NotFound_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync((c, ct) => c.Vulnerabilities.GetAsync("missing", ct), HttpStatusCode.NotFound, NotFound, NotFoundMessage);

	[Fact]
	public async Task UnrecognisedEnumValues_ReadAsUnknown()
	{
		const string json = """{"id":"x","severity":"Apocalyptic","cvss":{"v2":{"accessVector":"Z"}},"pci":{"status":"Maybe"}}""";

		var v = await TestClient.ReadAsync((c, ct) => c.Vulnerabilities.GetAsync("x", ct), json);

		v.Severity.Should().Be(Models.Vulnerabilities.VulnerabilitySeverity.Unknown);
		v.Cvss!.V2!.AccessVector.Should().Be(Models.Vulnerabilities.CvssAccessVector.Unknown);
		v.Pci!.Status.Should().Be(Models.Vulnerabilities.PciStatus.Unknown);
		v.Categories.Should().BeEmpty();
	}
}
