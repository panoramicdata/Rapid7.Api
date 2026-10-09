using Rapid7.Api.Models.Cloud;
using Rapid7.Api.Test.Support;
using System.Net;

namespace Rapid7.Api.Test.Groups;

public class CloudVulnerabilitiesTests
{
	[Fact]
	public async Task SearchAsync_PostsTheFilter_AndSendsThePaging()
	{
		var call = await TestClient.CaptureAsync(
			TestClient.CreateCloud,
			(c, ct) => c.Vulnerabilities.SearchAsync(
				new CloudVulnerabilitySearch { Vulnerability = "modified > 2020-01-01T00:00:00Z" },
				new CursorPageOptions { Page = 0, Size = 2, Sort = ["id,asc"] },
				ct),
			CloudJson.VulnerabilityPage);

		call.ShouldBe(
			HttpMethod.Post,
			"/vm/v4/integration/vulnerabilities",
			"?page=0&size=2&sort=id%2Casc",
			CloudJson.Escaped("""{"vulnerability":"modified > 2020-01-01T00:00:00Z"}"""));
	}

	[Fact]
	public async Task SearchAsync_WithoutPaging_SendsNoQuery()
	{
		var call = await TestClient.CaptureAsync(
			TestClient.CreateCloud,
			(c, ct) => c.Vulnerabilities.SearchAsync(new CloudVulnerabilitySearch(), null, ct),
			CloudJson.VulnerabilityPage);

		call.ShouldBe(HttpMethod.Post, "/vm/v4/integration/vulnerabilities", body: "{}");
	}

	[Fact]
	public async Task SearchAsync_MapsEveryField()
	{
		var page = await TestClient.ReadAsync(
			TestClient.CreateCloud,
			(c, ct) => c.Vulnerabilities.SearchAsync(new CloudVulnerabilitySearch(), null, ct),
			CloudJson.VulnerabilityPage);

		page.Metadata!.Cursor.Should().Be("-37745434:::_S:::7-zip-cve-2016-2334");
		page.Metadata.TotalResources.Should().Be(81631);
		var v = page.Data.Should().ContainSingle().Subject;
		v.Id.Should().Be("7-zip-cve-2016-2334");
		v.Title.Should().Be("7-Zip: CVE-2016-2334: Heap-based buffer overflow vulnerability");
		v.Description.Should().Be("Heap-based buffer overflow in 7zip before 16.00.");
		v.Categories.Should().Be("7-Zip,Remote Execution");
		v.Cves.Should().Be("CVE-2016-2334");
		v.References.Should().Be("bid:90531,cve:CVE-2016-2334");
		v.Added.Should().Be(new DateTimeOffset(2018, 5, 16, 0, 0, 0, TimeSpan.Zero));
		v.Modified.Should().Be(new DateTimeOffset(2018, 6, 8, 0, 0, 0, TimeSpan.Zero));
		v.Published.Should().Be(new DateTimeOffset(2016, 12, 13, 0, 0, 0, TimeSpan.Zero));
		v.Severity.Should().Be(CloudVulnerabilitySeverity.Critical);
		v.SeverityScore.Should().Be(9);
		v.RiskScore.Should().Be(582.82);
		v.DenialOfService.Should().BeFalse();
		AssertCvss(v);
		v.PciCvssScore.Should().Be(9.3);
		v.PciSeverityScore.Should().Be(5);
		v.PciFail.Should().BeTrue();
		v.PciStatus.Should().Be("fail");
		v.PciSpecialNotes.Should().Be("note");
		var exploit = v.Exploits.Should().ContainSingle().Subject;
		exploit.Id.Should().Be("12345");
		exploit.Name.Should().Be("7-Zip HFS+ overflow");
		exploit.Description.Should().Be("An exploit.");
		exploit.Rank.Should().Be(CloudExploitRank.Excellent);
		exploit.SkillLevel.Should().Be(CloudExploitSkillLevel.Expert);
		exploit.Source.Should().Be(CloudExploitSource.Metasploit);
		var kit = v.MalwareKits.Should().ContainSingle().Subject;
		kit.Name.Should().Be("Kit");
		kit.Description.Should().Be("A kit.");
		kit.Popularity.Should().Be(CloudMalwareKitPopularity.Favored);
		var link = v.Links.Should().ContainSingle().Subject;
		link.Href.Should().Be("http://nvd.example.test/CVE-2016-2334");
		link.Id.Should().Be("CVE-2016-2334");
		link.Rel.Should().Be("advisory");
		link.Source.Should().Be("cve");
	}

	[Fact]
	public Task SearchAsync_BadRequest_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync(
			TestClient.CreateCloud,
			(c, ct) => c.Vulnerabilities.SearchAsync(new CloudVulnerabilitySearch { Vulnerability = "nonsense" }, null, ct),
			HttpStatusCode.BadRequest,
			"""{"status":400,"localized_message":"The search expression is invalid.","message":"Invalid search."}""",
			"Invalid search.");

	private static void AssertCvss(CloudVulnerability v)
	{
		v.CvssV2Vector.Should().Be("AV:N/AC:M/Au:N/C:C/I:C/A:C");
		v.CvssV2Score.Should().Be(9.3);
		v.CvssV2ExploitScore.Should().Be(8.5888);
		v.CvssV2ImpactScore.Should().Be(10.000845);
		v.CvssV2AccessVector.Should().Be("network");
		v.CvssV2AccessComplexity.Should().Be("medium");
		v.CvssV2Authentication.Should().Be("none");
		v.CvssV2ConfidentialityImpact.Should().Be("complete");
		v.CvssV2IntegrityImpact.Should().Be("complete");
		v.CvssV2AvailabilityImpact.Should().Be("partial");
		v.CvssV3Vector.Should().Be("CVSS:3.0/AV:L/AC:L/PR:N/UI:R/S:U/C:H/I:H/A:H");
		v.CvssV3Score.Should().Be(7.8);
		v.CvssV3ExploitScore.Should().Be(1.8345766);
		v.CvssV3ImpactScore.Should().Be(5.873119);
		v.CvssV3AttackVector.Should().Be("local");
		v.CvssV3AttackComplexity.Should().Be("low");
		v.CvssV3PrivilegesRequired.Should().Be("none");
		v.CvssV3UserInteraction.Should().Be("required");
		v.CvssV3Scope.Should().Be("unchanged");
		v.CvssV3ConfidentialityImpact.Should().Be("high");
		v.CvssV3IntegrityImpact.Should().Be("high");
		v.CvssV3AvailabilityImpact.Should().Be("low");
	}
}
