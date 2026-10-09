using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Assets;

/// <summary>
/// An asset (a machine the console knows about) with everything discovered on it, its risk score and its vulnerability
/// counts.
/// </summary>
public class Asset : AssetProfile
{
	/// <summary>Every address discovered on the asset.</summary>
	[JsonPropertyName("addresses")]
	public IReadOnlyList<Address> Addresses { get; init; } = [];

	/// <summary>Whether the asset has been assessed for policies at least once.</summary>
	[JsonPropertyName("assessedForPolicies")]
	public bool? AssessedForPolicies { get; init; }

	/// <summary>Whether the asset has been assessed for vulnerabilities at least once.</summary>
	[JsonPropertyName("assessedForVulnerabilities")]
	public bool? AssessedForVulnerabilities { get; init; }

	/// <summary>Settings enumerated on the asset, as name and value pairs.</summary>
	[JsonPropertyName("configurations")]
	public IReadOnlyList<Configuration> Configurations { get; init; } = [];

	/// <summary>The databases enumerated on the asset.</summary>
	[JsonPropertyName("databases")]
	public IReadOnlyList<Database> Databases { get; init; } = [];

	/// <summary>The files and directories discovered on the asset.</summary>
	[JsonPropertyName("files")]
	public IReadOnlyList<AssetFile> Files { get; init; } = [];

	/// <summary>The changes to the asset over time.</summary>
	[JsonPropertyName("history")]
	public IReadOnlyList<AssetHistory> History { get; init; } = [];

	/// <summary>Every host name of the asset.</summary>
	[JsonPropertyName("hostNames")]
	public IReadOnlyList<HostName> HostNames { get; init; } = [];

	/// <summary>The identifier of the asset.</summary>
	[JsonPropertyName("id")]
	public long? Id { get; init; }

	/// <summary>Unique identifiers found on the asset, such as hardware or operating system identifiers.</summary>
	[JsonPropertyName("ids")]
	public IReadOnlyList<UniqueId> Ids { get; init; } = [];

	/// <summary>Links to the asset and its related resources.</summary>
	[JsonPropertyName("links")]
	public IReadOnlyList<Link> Links { get; init; } = [];

	/// <summary>The base risk score, before criticality adjustments.</summary>
	[JsonPropertyName("rawRiskScore")]
	public double? RawRiskScore { get; init; }

	/// <summary>The risk score, with criticality adjustments.</summary>
	[JsonPropertyName("riskScore")]
	public double? RiskScore { get; init; }

	/// <summary>The services discovered on the asset.</summary>
	[JsonPropertyName("services")]
	public IReadOnlyList<Service> Services { get; init; } = [];

	/// <summary>The software discovered on the asset.</summary>
	[JsonPropertyName("software")]
	public IReadOnlyList<Software> Software { get; init; } = [];

	/// <summary>The group accounts enumerated on the asset.</summary>
	[JsonPropertyName("userGroups")]
	public IReadOnlyList<GroupAccount> UserGroups { get; init; } = [];

	/// <summary>The user accounts enumerated on the asset.</summary>
	[JsonPropertyName("users")]
	public IReadOnlyList<UserAccount> Users { get; init; } = [];

	/// <summary>The vulnerabilities found on the asset, by severity.</summary>
	[JsonPropertyName("vulnerabilities")]
	public AssetVulnerabilities? Vulnerabilities { get; init; }
}
