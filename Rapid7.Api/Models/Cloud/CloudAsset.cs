using Rapid7.Api.Serialization;
using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Cloud;

/// <summary>
/// An asset as the Cloud Integrations API describes it: identity, operating system, assessment state, vulnerability
/// counts, tags and, when a comparison time is given, the vulnerabilities that are new, remediated or unchanged since then.
/// </summary>
public sealed class CloudAsset
{
	/// <summary>The asset identifier, such as <c>452534235-25a7-40a3-9321-28ce0b5cc90e-default-asset-199</c>.</summary>
	[JsonPropertyName("id")]
	public string? Id { get; init; }

	/// <summary>The kind of machine.</summary>
	[JsonPropertyName("type")]
	public CloudAssetType Type { get; init; }

	/// <summary>The host name (local or fully qualified).</summary>
	[JsonPropertyName("host_name")]
	public string? HostName { get; init; }

	/// <summary>The IPv4 or IPv6 address.</summary>
	[JsonPropertyName("ip")]
	public string? Ip { get; init; }

	/// <summary>The MAC address, six colon-separated pairs of hexadecimal digits.</summary>
	[JsonPropertyName("mac")]
	public string? Mac { get; init; }

	/// <summary>The operating system: vendor, family, product, version and architecture in one description.</summary>
	[JsonPropertyName("os_description")]
	public string? OsDescription { get; init; }

	/// <summary>The operating system architecture, such as <c>x86_64</c>.</summary>
	[JsonPropertyName("os_architecture")]
	public string? OsArchitecture { get; init; }

	/// <summary>The operating system family, such as <c>Linux</c>.</summary>
	[JsonPropertyName("os_family")]
	public string? OsFamily { get; init; }

	/// <summary>The operating system name.</summary>
	[JsonPropertyName("os_name")]
	public string? OsName { get; init; }

	/// <summary>The vendor and family together, without repetition, suitable for grouping.</summary>
	[JsonPropertyName("os_system_name")]
	public string? OsSystemName { get; init; }

	/// <summary>The operating system type, such as <c>General</c>.</summary>
	[JsonPropertyName("os_type")]
	public string? OsType { get; init; }

	/// <summary>The operating system vendor.</summary>
	[JsonPropertyName("os_vendor")]
	public string? OsVendor { get; init; }

	/// <summary>The operating system version.</summary>
	[JsonPropertyName("os_version")]
	public string? OsVersion { get; init; }

	/// <summary>Whether the asset was assessed for policies.</summary>
	[JsonPropertyName("assessed_for_policies")]
	public bool? AssessedForPolicies { get; init; }

	/// <summary>Whether the asset was assessed for vulnerabilities.</summary>
	[JsonPropertyName("assessed_for_vulnerabilities")]
	public bool? AssessedForVulnerabilities { get; init; }

	/// <summary>When the asset was last assessed for vulnerabilities.</summary>
	[JsonPropertyName("last_assessed_for_vulnerabilities")]
	public DateTimeOffset? LastAssessedForVulnerabilities { get; init; }

	/// <summary>When the last scan of the asset started.</summary>
	[JsonPropertyName("last_scan_start")]
	public DateTimeOffset? LastScanStart { get; init; }

	/// <summary>When the last scan of the asset ended.</summary>
	[JsonPropertyName("last_scan_end")]
	public DateTimeOffset? LastScanEnd { get; init; }

	/// <summary>The risk score, with criticality adjustments.</summary>
	[JsonPropertyName("risk_score")]
	public double? RiskScore { get; init; }

	/// <summary>The number of vulnerability findings.</summary>
	[JsonPropertyName("total_vulnerabilities")]
	public int? TotalVulnerabilities { get; init; }

	/// <summary>The number of critical vulnerability findings.</summary>
	[JsonPropertyName("critical_vulnerabilities")]
	public int? CriticalVulnerabilities { get; init; }

	/// <summary>The number of severe vulnerability findings.</summary>
	[JsonPropertyName("severe_vulnerabilities")]
	public int? SevereVulnerabilities { get; init; }

	/// <summary>The number of moderate vulnerability findings.</summary>
	[JsonPropertyName("moderate_vulnerabilities")]
	public int? ModerateVulnerabilities { get; init; }

	/// <summary>The number of distinct known exploits for the vulnerabilities on the asset.</summary>
	[JsonPropertyName("exploits")]
	public int? Exploits { get; init; }

	/// <summary>The number of distinct known malware kits for the vulnerabilities on the asset.</summary>
	[JsonPropertyName("malware_kits")]
	public int? MalwareKits { get; init; }

	/// <summary>The tags (including sites, with type <c>SITE</c>) applied to the asset.</summary>
	[JsonPropertyName("tags")]
	public IReadOnlyList<CloudTag> Tags { get; init; } = [];

	/// <summary>
	/// Identifiers found on the asset, when unique identifiers are requested. A single object on the wire is read as a
	/// one-item list.
	/// </summary>
	[JsonPropertyName("unique_identifiers")]
	[JsonConverter(typeof(SingleOrArrayConverter<CloudUniqueIdentifier>))]
	public IReadOnlyList<CloudUniqueIdentifier> UniqueIdentifiers { get; init; } = [];

	/// <summary>How credentials fared on the asset in its last scan.</summary>
	[JsonPropertyName("credential_assessments")]
	public IReadOnlyList<CloudCredentialAssessment> CredentialAssessments { get; init; } = [];

	/// <summary>With a comparison time: the vulnerabilities found since then.</summary>
	[JsonPropertyName("new")]
	public IReadOnlyList<CloudVulnerabilityFinding> New { get; init; } = [];

	/// <summary>With a comparison time: the vulnerabilities remediated since then.</summary>
	[JsonPropertyName("remediated")]
	public IReadOnlyList<CloudVulnerabilityFinding> Remediated { get; init; } = [];

	/// <summary>With a comparison time and <c>includeSame</c>: the vulnerabilities present then and now.</summary>
	[JsonPropertyName("same")]
	public IReadOnlyList<CloudVulnerabilityFinding> Same { get; init; } = [];
}
