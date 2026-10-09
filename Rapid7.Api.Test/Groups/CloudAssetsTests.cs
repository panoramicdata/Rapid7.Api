using Rapid7.Api.Models.Cloud;
using Rapid7.Api.Test.Support;
using System.Net;
using System.Text.Json;

namespace Rapid7.Api.Test.Groups;

public class CloudAssetsTests
{
	private static readonly CloudAssetOptions EveryOption = new()
	{
		CurrentTime = new DateTimeOffset(2019, 4, 2, 8, 17, 56, 321, TimeSpan.Zero),
		ComparisonTime = new DateTimeOffset(2018, 11, 9, 8, 17, 56, 321, TimeSpan.Zero),
		IncludeSame = true,
		IncludeUniqueIdentifiers = false,
		IncludeInterSnapshotRemediations = true
	};

	private const string EveryOptionQuery
		= "currentTime=2019-04-02T08%3A17%3A56.321%2B00%3A00&comparisonTime=2018-11-09T08%3A17%3A56.321%2B00%3A00"
		+ "&includeSame=true&includeUniqueIdentifiers=false&includeInterSnapshotRemediations=true";

	[Fact]
	public async Task SearchAsync_PostsTheFilters_WithEveryQueryOption()
	{
		var call = await TestClient.CaptureAsync(
			TestClient.CreateCloud,
			(c, ct) => c.Assets.SearchAsync(
				new CloudAssetSearch { Asset = "last_scan_end > 2019-09-04T23:16:57.903Z", Vulnerability = "severity IN ['Critical', 'Severe']" },
				EveryOption,
				new CursorPageOptions { Page = 1, Size = 50, Sort = ["id,ASC", "risk_score,DESC"], Cursor = "cursor-1" },
				ct),
			CloudJson.AssetPage);

		call.ShouldBe(
			HttpMethod.Post,
			"/vm/v4/integration/assets",
			"?" + EveryOptionQuery + "&cursor=cursor-1&page=1&size=50&sort=id%2CASC&sort=risk_score%2CDESC",
			CloudJson.Escaped("""{"asset":"last_scan_end > 2019-09-04T23:16:57.903Z","vulnerability":"severity IN ['Critical', 'Severe']"}"""));
	}

	[Fact]
	public async Task SearchAsync_WithoutOptions_SendsAnEmptyFilterAndNoQuery()
	{
		var call = await TestClient.CaptureAsync(
			TestClient.CreateCloud,
			(c, ct) => c.Assets.SearchAsync(new CloudAssetSearch(), null, null, ct),
			CloudJson.AssetPage);

		call.ShouldBe(HttpMethod.Post, "/vm/v4/integration/assets", body: "{}");
	}

	[Fact]
	public async Task SearchAsync_MapsThePage()
	{
		var page = await TestClient.ReadAsync(
			TestClient.CreateCloud,
			(c, ct) => c.Assets.SearchAsync(new CloudAssetSearch(), null, null, ct),
			CloudJson.AssetPage);

		page.Data.Should().HaveCount(2);
		page.Metadata!.Number.Should().Be(0);
		page.Metadata.Size.Should().Be(2);
		page.Metadata.TotalResources.Should().Be(2195);
		page.Metadata.TotalPages.Should().Be(1098);
		page.Metadata.Cursor.Should().Be("cursor-1");
		page.Metadata.EffectiveTime.Should().Be(new DateTimeOffset(2024, 1, 25, 0, 0, 0, TimeSpan.Zero));
		page.EffectiveTime.Should().Be(new DateTimeOffset(2024, 1, 26, 0, 0, 0, TimeSpan.Zero));
		page.Links.Should().HaveCount(2);
		page.Links[1].Rel.Should().Be("next");
		AssertAsset(page.Data[0]);
	}

	[Fact]
	public async Task SearchAsync_ReadsASingleUniqueIdentifierObjectAsAList()
	{
		var page = await TestClient.ReadAsync(
			TestClient.CreateCloud,
			(c, ct) => c.Assets.SearchAsync(new CloudAssetSearch(), null, null, ct),
			CloudJson.AssetPage);

		var identifier = page.Data[1].UniqueIdentifiers.Should().ContainSingle().Subject;
		identifier.Id.Should().Be("4421d73d");
		identifier.Source.Should().Be("R7 Agent");
	}

	[Fact]
	public void UniqueIdentifiers_AreWrittenAsAnArray()
	{
		var asset = new CloudAsset { UniqueIdentifiers = [new CloudUniqueIdentifier { Id = "a", Source = "b" }] };

		var json = JsonSerializer.Serialize(asset, Rapid7Json.Options);

		json.Should().Contain("\"unique_identifiers\":[{\"id\":\"a\",\"source\":\"b\"}]");
	}

	[Fact]
	public async Task GetAsync_SendsGetWithTheEscapedId_AndEveryQueryOption()
	{
		var call = await TestClient.CaptureAsync(
			TestClient.CreateCloud,
			(c, ct) => c.Assets.GetAsync("org-1/asset 7", EveryOption, ct),
			CloudJson.Asset);

		call.ShouldBe(HttpMethod.Get, "/vm/v4/integration/assets/org-1%2Fasset%207", "?" + EveryOptionQuery);
	}

	[Fact]
	public async Task GetAsync_WithoutOptions_SendsNoQuery()
	{
		var call = await TestClient.CaptureAsync(
			TestClient.CreateCloud,
			(c, ct) => c.Assets.GetAsync("org-1-default-asset-7912", null, ct),
			CloudJson.Asset);

		call.ShouldBe(HttpMethod.Get, "/vm/v4/integration/assets/org-1-default-asset-7912");
	}

	[Fact]
	public async Task GetAsync_MapsEveryField()
	{
		var asset = await TestClient.ReadAsync(
			TestClient.CreateCloud,
			(c, ct) => c.Assets.GetAsync("org-1-default-asset-7912", null, ct),
			CloudJson.Asset);

		AssertAsset(asset);
	}

	[Fact]
	public Task GetAsync_NotFound_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync(
			TestClient.CreateCloud,
			(c, ct) => c.Assets.GetAsync("missing", null, ct),
			HttpStatusCode.NotFound,
			CloudJson.NotFound,
			"The requested resource does not exist.");

	private static void AssertAsset(CloudAsset asset)
	{
		asset.Id.Should().Be("org-1-default-asset-7912");
		asset.Type.Should().Be(CloudAssetType.Guest);
		asset.HostName.Should().Be("host.example.test");
		asset.Ip.Should().Be("10.1.0.128");
		asset.Mac.Should().Be("00:50:56:8B:62:45");
		asset.OsDescription.Should().Be("Microsoft Windows Server 2008 R2, Standard Edition SP1");
		asset.OsArchitecture.Should().Be("x86_64");
		asset.OsFamily.Should().Be("Windows");
		asset.OsName.Should().Be("Windows Server 2008 R2, Standard Edition");
		asset.OsSystemName.Should().Be("Microsoft Windows");
		asset.OsType.Should().Be("General");
		asset.OsVendor.Should().Be("Microsoft");
		asset.OsVersion.Should().Be("SP1");
		asset.AssessedForPolicies.Should().BeFalse();
		asset.AssessedForVulnerabilities.Should().BeTrue();
		asset.LastAssessedForVulnerabilities.Should().Be(new DateTimeOffset(2019, 2, 14, 21, 19, 41, 90, TimeSpan.Zero));
		asset.LastScanStart.Should().Be(new DateTimeOffset(2019, 2, 14, 21, 5, 35, 14, TimeSpan.Zero));
		asset.LastScanEnd.Should().Be(new DateTimeOffset(2019, 2, 14, 21, 19, 41, 90, TimeSpan.Zero));
		asset.RiskScore.Should().Be(74621.5625);
		asset.TotalVulnerabilities.Should().Be(276);
		asset.CriticalVulnerabilities.Should().Be(16);
		asset.SevereVulnerabilities.Should().Be(229);
		asset.ModerateVulnerabilities.Should().Be(31);
		asset.Exploits.Should().Be(24);
		asset.MalwareKits.Should().Be(1);
		asset.Tags.Should().HaveCount(2);
		asset.Tags[0].Name.Should().Be("lab");
		asset.Tags[0].Type.Should().Be("SITE");
		asset.UniqueIdentifiers.Should().ContainSingle().Which.Source.Should().Be("Endpoint Agent");
		var credential = asset.CredentialAssessments.Should().ContainSingle().Subject;
		credential.Port.Should().Be(22);
		credential.Protocol.Should().Be("TCP");
		credential.Status.Should().Be("SUCCESS");
		AssertFinding(asset.New.Should().ContainSingle().Subject);
		asset.Remediated.Should().ContainSingle().Which.Status.Should().Be(CloudFindingStatus.NotVulnerable);
		asset.Same.Should().ContainSingle().Which.VulnerabilityId.Should().Be("acrobat-cve-2018-16015");
	}

	private static void AssertFinding(CloudVulnerabilityFinding finding)
	{
		finding.VulnerabilityId.Should().Be("acrobat-cve-2018-16030");
		finding.CheckId.Should().Be("acrobat-check");
		finding.Status.Should().Be(CloudFindingStatus.VulnerableExploited);
		finding.Key.Should().Be("key-123");
		finding.Endpoint!.Port.Should().Be(443);
		finding.Endpoint.Protocol.Should().Be(CloudEndpointProtocol.Tcp);
		finding.Port.Should().Be(443);
		finding.Protocol.Should().Be("TCP");
		finding.Nic.Should().Be("eth0");
		finding.Proof.Should().Be("<p>Proof</p>");
		finding.FirstFound.Should().Be(new DateTimeOffset(2024, 1, 25, 10, 46, 15, TimeSpan.Zero));
		finding.LastFound.Should().Be(new DateTimeOffset(2024, 2, 1, 0, 0, 0, TimeSpan.Zero));
		finding.Reintroduced.Should().Be(new DateTimeOffset(2024, 1, 30, 0, 0, 0, TimeSpan.Zero));
		finding.RemediationDate.Should().Be(new DateTimeOffset(2024, 12, 25, 0, 0, 0, TimeSpan.Zero));
		finding.SolutionId.Should().Be("unknown-acrobat-cve-2018-16030");
		finding.SolutionSummary.Should().Be("The solution is unknown");
		finding.SolutionFix.Should().Be("Take a look at all possible solutions");
		finding.SolutionType.Should().Be("workaround");
	}
}
