using Rapid7.Api.Models.BulkExport;
using Rapid7.Api.Serialization;
using Rapid7.Api.Test.Support;

namespace Rapid7.Api.Test.Core;

/// <summary>Reads Parquet files shaped like each Bulk Export dataset into its record type.</summary>
public class ParquetTests
{
	private static async Task<List<T>> ReadAsync<T>(byte[] file, bool seekable = true) where T : BulkExportRecord, new()
	{
		await using Stream stream = seekable ? new MemoryStream(file) : new NonSeekableStream(file);
		var records = new List<T>();
		await foreach (var record in Rapid7Parquet.ReadAsync<T>(stream, TestContext.Current.CancellationToken))
		{
			records.Add(record);
		}

		return records;
	}

	private static async Task<AssetVulnerabilityRecord> ReadAssetVulnerabilityAsync()
		=> (await ReadAsync<AssetVulnerabilityRecord>(await ParquetFile.WriteAsync([new AssetVulnerabilityRow()]))).Single();

	/// <summary>Asserts the columns <see cref="DetailRowBase"/> writes.</summary>
	private static void ShouldHaveTheDetailColumns(VulnerabilityDetailRecord record)
	{
		record.VulnId.Should().Be("openssl-cve-2024-0001");
		record.FirstFoundTimestamp.Should().Be(ParquetFile.WhenOffset);
		record.ReintroducedTimestamp.Should().Be(ParquetFile.WhenOffset.AddDays(1));
		record.Title.Should().Be("OpenSSL: CVE-2024-0001");
		record.Description.Should().Be("<p>An issue.</p>");
		record.DatePublished.Should().Be(ParquetFile.WhenOffset.AddDays(-30));
		record.DateAdded.Should().Be(ParquetFile.WhenOffset.AddDays(-20));
		record.DateModified.Should().Be(ParquetFile.WhenOffset.AddDays(-10));
		record.EpssScore.Should().Be(0.25);
		record.EpssPercentile.Should().Be(0.9);
	}

	[Fact]
	public async Task Asset_MapsEveryColumn()
	{
		var record = (await ReadAsync<AssetRecord>(await ParquetFile.WriteAsync([new AssetRow()]))).Should().ContainSingle().Subject;

		record.OrgId.Should().Be("org-1");
		record.AssetId.Should().Be("asset-1");
		record.AgentId.Should().Be("agent-1");
		record.AwsInstanceId.Should().Be("i-0abc");
		record.AzureResourceId.Should().Be("/subscriptions/1/vm");
		record.GcpObjectId.Should().Be("gcp-1");
		record.Mac.Should().Be("00:50:56:8B:62:45");
		record.Ip.Should().Be("192.0.2.1");
		record.HostName.Should().Be("host.example.test");
		record.OsArchitecture.Should().Be("x86_64");
		record.OsFamily.Should().Be("Linux");
		record.OsProduct.Should().Be("Ubuntu Linux");
		record.OsVendor.Should().Be("Canonical");
		record.OsVersion.Should().Be("24.04");
		record.OsType.Should().Be("General");
		record.OsDescription.Should().Be("Ubuntu Linux 24.04");
		record.RiskScore.Should().Be(1234.5);
		record.Sites.Should().Equal("lab", "dmz");
		record.AssetGroups.Should().Equal("linux");
		record.Tags.Should().Equal("owner:ops");
	}

	[Fact]
	public async Task Asset_LeavesNullColumnsNullAndEmpty()
	{
		var row = new AssetRow { AgentId = null, RiskScore = null, Sites = null };

		var record = (await ReadAsync<AssetRecord>(await ParquetFile.WriteAsync([row]))).Single();

		record.AgentId.Should().BeNull();
		record.RiskScore.Should().BeNull();
		record.Sites.Should().BeEmpty();
	}

	[Fact]
	public async Task AssetPolicy_MapsEveryColumn()
	{
		var record = (await ReadAsync<AssetPolicyRecord>(await ParquetFile.WriteAsync([new PolicyRow()]))).Single();

		record.OrgId.Should().Be("org-1");
		record.BenchmarkNaturalId.Should().Be("cis-ubuntu");
		record.ProfileNaturalId.Should().Be("level-1");
		record.BenchmarkVersion.Should().Be("2.0.0");
		record.RuleNaturalId.Should().Be("rule-1.1");
		record.RuleTitle.Should().Be("Ensure tmp is a partition");
		record.FinalStatus.Should().Be("fail");
		record.Proof.Should().Be("tmp is not mounted");
		record.LastAssessmentTimestamp.Should().Be(ParquetFile.WhenOffset);
		record.BenchmarkTitle.Should().Be("CIS Ubuntu");
		record.ProfileTitle.Should().Be("Level 1");
		record.Publisher.Should().Be("CIS");
		record.FixTexts.Should().Equal("Mount tmp separately");
		record.Rationales.Should().Equal("Limits attacks", "Eases cleanup");
	}

	[Fact]
	public async Task VulnerabilityException_MapsEveryColumn()
	{
		var record = (await ReadAsync<VulnerabilityExceptionRecord>(await ParquetFile.WriteAsync([new ExceptionRow()]))).Single();

		record.AssetId.Should().Be("asset-1");
		record.VulnId.Should().Be("ssh-weak-cipher");
		record.CheckId.Should().Be("ssh-check");
		record.Key.Should().Be("/etc/ssh/sshd_config");
		record.Port.Should().Be(22);
		record.Protocol.Should().Be("TCP");
		record.Nic.Should().Be("eth0");
		record.Proof.Should().Be("<p>cipher</p>");
		record.FirstFoundTimestamp.Should().Be(ParquetFile.WhenOffset);
		record.ReintroducedTimestamp.Should().Be(ParquetFile.WhenOffset.AddDays(1));
		record.ExceptionDetails.Should().Equal("scope:asset", "reason:compensating control");
	}

	[Fact]
	public async Task VulnerabilityRemediation_MapsEveryColumn()
	{
		var record = (await ReadAsync<VulnerabilityRemediationRecord>(await ParquetFile.WriteAsync([new RemediationRow()]))).Single();

		ShouldHaveTheDetailColumns(record);
		record.CveId.Should().Be("CVE-2024-0001");
		record.Proof.Should().Be("<p>fixed</p>");
		record.LastDetected.Should().Be(ParquetFile.WhenOffset.AddDays(2));
		record.LastRemoved.Should().Be(ParquetFile.WhenOffset.AddDays(3));
		record.CvssV2Score.Should().Be(5.0);
		record.CvssV3Score.Should().Be(7.5);
		record.CvssV2Severity.Should().Be("Medium");
		record.CvssV3Severity.Should().Be("High");
		record.CvssV2AttackVector.Should().Be("NETWORK");
		record.CvssV3AttackVector.Should().Be("NETWORK");
		record.RiskScoreV2.Should().Be(650);
	}

	[Fact]
	public async Task AssetVulnerability_MapsEveryColumn_ConvertingWideIntegers()
	{
		var r = await ReadAssetVulnerabilityAsync();

		r.OrgId.Should().Be("org-1");
		ShouldHaveTheDetailColumns(r);
		r.Port.Should().Be(443);
		r.Protocol.Should().Be("TCP");
		r.Nic.Should().Be("eth0");
		r.Proof.Should().Be("<p>version 3.0.2</p>");
		r.SkillLevel.Should().Be("Novice");
		r.SkillLevelRank.Should().Be(1);
		r.Severity.Should().Be("Severe");
		r.SeverityRank.Should().Be(2);
		r.SeverityScore.Should().Be(7);
		r.HasExploits.Should().BeTrue();
		r.ThreatFeedExists.Should().BeFalse();
		r.PciCompliant.Should().BeFalse();
		r.PciSeverity.Should().Be(4);
		r.RiskScore.Should().Be(612.5);
		r.RiskScoreV2.Should().Be(700);
		r.Cves.Should().Equal("CVE-2024-0001", "CVE-2024-0002");
		r.Tags.Should().Equal("openssl");
		r.CheckId.Should().Be("openssl-check");
		r.BestSolutionSummary.Should().Be("Upgrade OpenSSL");
		r.BestSolutionFix.Should().Be("<p>Upgrade to 3.0.13.</p>");
		r.BestSolutionType.Should().Be("patch");
	}

	[Fact]
	public async Task AssetVulnerability_MapsTheCvssColumns()
	{
		var r = await ReadAssetVulnerabilityAsync();

		r.CvssAccessComplexity.Should().Be("L");
		r.CvssAccessVector.Should().Be("N");
		r.CvssAuthentication.Should().Be("N");
		r.CvssAvailabilityImpact.Should().Be("P");
		r.CvssConfidentialityImpact.Should().Be("N");
		r.CvssIntegrityImpact.Should().Be("C");
		r.CvssScore.Should().Be(5.0);
		r.CvssV3AttackVector.Should().Be("NETWORK");
		r.CvssV3AttackComplexity.Should().Be("LOW");
		r.CvssV3PrivilegesRequired.Should().Be("NONE");
		r.CvssV3UserInteraction.Should().Be("REQUIRED");
		r.CvssV3Scope.Should().Be("UNCHANGED");
		r.CvssV3Confidentiality.Should().Be("HIGH");
		r.CvssV3Integrity.Should().Be("LOW");
		r.CvssV3Availability.Should().Be("NONE");
		r.CvssV3Score.Should().Be(7.5);
		r.CvssV3Severity.Should().Be("High");
		r.CvssV3SeverityRank.Should().Be(3);
	}

	[Fact]
	public async Task AssetSoftware_ReadsRequiredColumns_AndIgnoresUnknownOnes()
	{
		var record = (await ReadAsync<AssetSoftwareRecord>(await ParquetFile.WriteAsync([new SoftwareRow()]))).Single();

		record.OrgId.Should().Be("org-1");
		record.Product.Should().Be("OpenSSL");
		record.Family.Should().Be("Crypto");
		record.Vendor.Should().Be("OpenSSL Project");
		record.Version.Should().Be("3.0.2");
		record.Type.Should().Be("Library");
		record.Certainty.Should().Be(1.0);
	}

	[Fact]
	public async Task ReadAsync_ReadsEveryRowGroup_FromANonSeekableStream()
	{
		var rows = Enumerable.Range(1, 5).Select(i => new SoftwareRow { Product = $"p{i}" });
		var file = await ParquetFile.WriteAsync(rows, rowGroupSize: 2);

		var records = await ReadAsync<AssetSoftwareRecord>(file, seekable: false);

		records.Select(r => r.Product).Should().Equal("p1", "p2", "p3", "p4", "p5");
	}

	[Fact]
	public async Task ReadAsync_ChecksCancellationBeforeEachRowGroup()
	{
		var file = await ParquetFile.WriteAsync(Enumerable.Range(1, 3).Select(_ => new SoftwareRow()), rowGroupSize: 1);
		using var cts = new CancellationTokenSource();
		var read = 0;

		var act = async () =>
		{
			await foreach (var _ in Rapid7Parquet.ReadAsync<AssetSoftwareRecord>(new MemoryStream(file), cts.Token))
			{
				read++;
				await cts.CancelAsync();
			}
		};

		await act.Should().ThrowAsync<OperationCanceledException>();
		read.Should().Be(1);
	}

	[Fact]
	public void ReadAsync_RejectsANullStream_WhenCalled()
	{
		var act = () => Rapid7Parquet.ReadAsync<AssetRecord>(null!, CancellationToken.None);

		act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("parquet");
	}

	[Fact]
	public void Values_ConvertTheShapesParquetNetReads()
	{
		ParquetValues.Convert(42, typeof(string)).Should().Be("42");
		ParquetValues.Convert("x".AsMemory(), typeof(string)).Should().Be("x");
		ParquetValues.Convert(ParquetFile.WhenOffset, typeof(DateTimeOffset?)).Should().Be(ParquetFile.WhenOffset);
		ParquetValues.Convert(new DateOnly(2026, 9, 1), typeof(DateTimeOffset?)).Should().Be(new DateTimeOffset(2026, 9, 1, 0, 0, 0, TimeSpan.Zero));
		ParquetValues.Convert(new DateTime(2026, 9, 1, 12, 30, 45, DateTimeKind.Unspecified), typeof(DateTimeOffset?)).Should().Be(ParquetFile.WhenOffset);
		((IReadOnlyList<string>)ParquetValues.Convert("single", typeof(IReadOnlyList<string>))).Should().Equal("single");
		((IReadOnlyList<string>)ParquetValues.Convert(new object?[] { "a", null, 3 }, typeof(IReadOnlyList<string>))).Should().Equal("a", "3");
		ParquetValues.Convert(7L, typeof(int?)).Should().Be(7);
		ParquetValues.Convert(1, typeof(bool?)).Should().Be(true);
	}

	[Fact]
	public void Values_RejectANonTimestampForATimestamp()
	{
		var act = () => ParquetValues.Convert("yesterday", typeof(DateTimeOffset?));

		act.Should().Throw<InvalidCastException>().WithMessage("Cannot read a String as a timestamp.");
	}
}
