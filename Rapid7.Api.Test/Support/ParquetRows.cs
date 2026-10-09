using Parquet;
using Parquet.Serialization;
using System.Text.Json.Serialization;

namespace Rapid7.Api.Test.Support;

/// <summary>Writes small Parquet files in memory, shaped like Rapid7's Bulk Export datasets.</summary>
internal static class ParquetFile
{
	/// <summary>Writes <paramref name="rows"/> as a Parquet file with <paramref name="rowGroupSize"/> rows per row group.</summary>
	public static async Task<byte[]> WriteAsync<TRow>(IEnumerable<TRow> rows, int rowGroupSize = 1000)
	{
		using var stream = new MemoryStream();
		await ParquetSerializer.SerializeAsync(rows, stream, new ParquetOptions { RowGroupSize = rowGroupSize }, null, TestContext.Current.CancellationToken);
		return stream.ToArray();
	}

	public static readonly DateTime When = new(2026, 9, 1, 12, 30, 45, DateTimeKind.Utc);

	public static readonly DateTimeOffset WhenOffset = new(2026, 9, 1, 12, 30, 45, TimeSpan.Zero);
}

/// <summary>Columns every dataset shares.</summary>
internal abstract class RowBase
{
	[JsonPropertyName("orgId")]
	public string? OrgId { get; set; } = "org-1";

	[JsonPropertyName("assetId")]
	public string? AssetId { get; set; } = "asset-1";
}

/// <summary>An <c>asset</c> row.</summary>
internal sealed class AssetRow : RowBase
{
	[JsonPropertyName("agentId")] public string? AgentId { get; set; } = "agent-1";
	[JsonPropertyName("awsInstanceId")] public string? AwsInstanceId { get; set; } = "i-0abc";
	[JsonPropertyName("azureResourceId")] public string? AzureResourceId { get; set; } = "/subscriptions/1/vm";
	[JsonPropertyName("gcpObjectId")] public string? GcpObjectId { get; set; } = "gcp-1";
	[JsonPropertyName("mac")] public string? Mac { get; set; } = "00:50:56:8B:62:45";
	[JsonPropertyName("ip")] public string? Ip { get; set; } = "10.0.0.1";
	[JsonPropertyName("hostName")] public string? HostName { get; set; } = "host.example.test";
	[JsonPropertyName("osArchitecture")] public string? OsArchitecture { get; set; } = "x86_64";
	[JsonPropertyName("osFamily")] public string? OsFamily { get; set; } = "Linux";
	[JsonPropertyName("osProduct")] public string? OsProduct { get; set; } = "Ubuntu Linux";
	[JsonPropertyName("osVendor")] public string? OsVendor { get; set; } = "Canonical";
	[JsonPropertyName("osVersion")] public string? OsVersion { get; set; } = "24.04";
	[JsonPropertyName("osType")] public string? OsType { get; set; } = "General";
	[JsonPropertyName("osDescription")] public string? OsDescription { get; set; } = "Ubuntu Linux 24.04";
	[JsonPropertyName("riskScore")] public double? RiskScore { get; set; } = 1234.5;
	[JsonPropertyName("sites")] public List<string>? Sites { get; set; } = ["lab", "dmz"];
	[JsonPropertyName("assetGroups")] public List<string>? AssetGroups { get; set; } = ["linux"];
	[JsonPropertyName("tags")] public List<string>? Tags { get; set; } = ["owner:ops"];
}

/// <summary>An <c>asset_policy</c> row.</summary>
internal sealed class PolicyRow : RowBase
{
	[JsonPropertyName("benchmarkNaturalId")] public string? BenchmarkNaturalId { get; set; } = "cis-ubuntu";
	[JsonPropertyName("profileNaturalId")] public string? ProfileNaturalId { get; set; } = "level-1";
	[JsonPropertyName("benchmarkVersion")] public string? BenchmarkVersion { get; set; } = "2.0.0";
	[JsonPropertyName("ruleNaturalId")] public string? RuleNaturalId { get; set; } = "rule-1.1";
	[JsonPropertyName("ruleTitle")] public string? RuleTitle { get; set; } = "Ensure tmp is a partition";
	[JsonPropertyName("finalStatus")] public string? FinalStatus { get; set; } = "fail";
	[JsonPropertyName("proof")] public string? Proof { get; set; } = "tmp is not mounted";
	[JsonPropertyName("lastAssessmentTimestamp")] public DateTime? LastAssessmentTimestamp { get; set; } = ParquetFile.When;
	[JsonPropertyName("benchmarkTitle")] public string? BenchmarkTitle { get; set; } = "CIS Ubuntu";
	[JsonPropertyName("profileTitle")] public string? ProfileTitle { get; set; } = "Level 1";
	[JsonPropertyName("publisher")] public string? Publisher { get; set; } = "CIS";
	[JsonPropertyName("fixTexts")] public List<string>? FixTexts { get; set; } = ["Mount tmp separately"];
	[JsonPropertyName("rationales")] public List<string>? Rationales { get; set; } = ["Limits attacks", "Eases cleanup"];
}

/// <summary>A <c>vulnerability_exception</c> row.</summary>
internal sealed class ExceptionRow : RowBase
{
	[JsonPropertyName("vulnId")] public string? VulnId { get; set; } = "ssh-weak-cipher";
	[JsonPropertyName("checkId")] public string? CheckId { get; set; } = "ssh-check";
	[JsonPropertyName("key")] public string? Key { get; set; } = "/etc/ssh/sshd_config";
	[JsonPropertyName("port")] public int? Port { get; set; } = 22;
	[JsonPropertyName("protocol")] public string? Protocol { get; set; } = "TCP";
	[JsonPropertyName("nic")] public string? Nic { get; set; } = "eth0";
	[JsonPropertyName("proof")] public string? Proof { get; set; } = "<p>cipher</p>";
	[JsonPropertyName("firstFoundTimestamp")] public DateTime? FirstFoundTimestamp { get; set; } = ParquetFile.When;
	[JsonPropertyName("reintroducedTimestamp")] public DateTime? ReintroducedTimestamp { get; set; } = ParquetFile.When.AddDays(1);
	[JsonPropertyName("exceptionDetails")] public List<string>? ExceptionDetails { get; set; } = ["scope:asset", "reason:compensating control"];
}

/// <summary>A <c>vulnerability_remediation</c> row.</summary>
internal sealed class RemediationRow : RowBase
{
	[JsonPropertyName("cveId")] public string? CveId { get; set; } = "CVE-2024-0001";
	[JsonPropertyName("vulnId")] public string? VulnId { get; set; } = "openssl-cve-2024-0001";
	[JsonPropertyName("proof")] public string? Proof { get; set; } = "<p>fixed</p>";
	[JsonPropertyName("firstFoundTimestamp")] public DateTime? FirstFoundTimestamp { get; set; } = ParquetFile.When;
	[JsonPropertyName("reintroducedTimestamp")] public DateTime? ReintroducedTimestamp { get; set; } = ParquetFile.When.AddDays(1);
	[JsonPropertyName("lastDetected")] public DateTime? LastDetected { get; set; } = ParquetFile.When.AddDays(2);
	[JsonPropertyName("lastRemoved")] public DateTime? LastRemoved { get; set; } = ParquetFile.When.AddDays(3);
	[JsonPropertyName("title")] public string? Title { get; set; } = "OpenSSL: CVE-2024-0001";
	[JsonPropertyName("description")] public string? Description { get; set; } = "<p>An issue.</p>";
	[JsonPropertyName("cvssV2Score")] public double? CvssV2Score { get; set; } = 5.0;
	[JsonPropertyName("cvssV3Score")] public double? CvssV3Score { get; set; } = 7.5;
	[JsonPropertyName("cvssV2Severity")] public string? CvssV2Severity { get; set; } = "Medium";
	[JsonPropertyName("cvssV3Severity")] public string? CvssV3Severity { get; set; } = "High";
	[JsonPropertyName("cvssV2AttackVector")] public string? CvssV2AttackVector { get; set; } = "NETWORK";
	[JsonPropertyName("cvssV3AttackVector")] public string? CvssV3AttackVector { get; set; } = "NETWORK";
	[JsonPropertyName("riskScoreV2_0")] public int? RiskScoreV2 { get; set; } = 650;
	[JsonPropertyName("datePublished")] public DateTime? DatePublished { get; set; } = ParquetFile.When.AddDays(-30);
	[JsonPropertyName("dateAdded")] public DateTime? DateAdded { get; set; } = ParquetFile.When.AddDays(-20);
	[JsonPropertyName("dateModified")] public DateTime? DateModified { get; set; } = ParquetFile.When.AddDays(-10);
	[JsonPropertyName("epssscore")] public double? EpssScore { get; set; } = 0.25;
	[JsonPropertyName("epsspercentile")] public double? EpssPercentile { get; set; } = 0.9;
}

/// <summary>An <c>asset_software</c> row, with required (non-nullable) columns and a 64-bit number where a double is expected.</summary>
internal sealed class SoftwareRow : RowBase
{
	[JsonPropertyName("product")] public string Product { get; set; } = "OpenSSL";
	[JsonPropertyName("family")] public string Family { get; set; } = "Crypto";
	[JsonPropertyName("vendor")] public string Vendor { get; set; } = "OpenSSL Project";
	[JsonPropertyName("version")] public string Version { get; set; } = "3.0.2";
	[JsonPropertyName("type")] public string Type { get; set; } = "Library";
	[JsonPropertyName("certainty")] public long Certainty { get; set; } = 1;
	[JsonPropertyName("unknownColumn")] public string Unknown { get; set; } = "ignored";
}
