using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Assets;

/// <summary>
/// The details of an asset to create in a site, or to merge into the asset the console matches them to (an asset import
/// from an external source). Give the operating system as <see cref="AssetProfile.OsFingerprint"/> (most precise),
/// <see cref="AssetProfile.Os"/> or <see cref="Cpe"/>; the console uses the first one present, in that order.
/// </summary>
public sealed class AssetCreateRequest : AssetProfile
{
	/// <summary>When the data was collected from the asset.</summary>
	[JsonPropertyName("date")]
	public required DateTimeOffset Date { get; init; }

	/// <summary>A description of the source of the data, recorded in the history of the asset for auditing.</summary>
	[JsonPropertyName("description")]
	public string? Description { get; init; }

	/// <summary>The CPE name of the operating system, the least precise way to give it.</summary>
	[JsonPropertyName("cpe")]
	public string? Cpe { get; init; }

	/// <summary>Every address of the asset.</summary>
	[JsonPropertyName("addresses")]
	public IReadOnlyList<Address>? Addresses { get; init; }

	/// <summary>Settings enumerated on the asset.</summary>
	[JsonPropertyName("configurations")]
	public IReadOnlyList<Configuration>? Configurations { get; init; }

	/// <summary>The databases enumerated on the asset.</summary>
	[JsonPropertyName("databases")]
	public IReadOnlyList<Database>? Databases { get; init; }

	/// <summary>The files and directories discovered on the asset.</summary>
	[JsonPropertyName("files")]
	public IReadOnlyList<AssetFile>? Files { get; init; }

	/// <summary>Additional host names of the asset.</summary>
	[JsonPropertyName("hostNames")]
	public IReadOnlyList<HostName>? HostNames { get; init; }

	/// <summary>Unique identifiers found on the asset.</summary>
	[JsonPropertyName("ids")]
	public IReadOnlyList<UniqueId>? Ids { get; init; }

	/// <summary>The services discovered on the asset.</summary>
	[JsonPropertyName("services")]
	public IReadOnlyList<Service>? Services { get; init; }

	/// <summary>The software discovered on the asset.</summary>
	[JsonPropertyName("software")]
	public IReadOnlyList<Software>? Software { get; init; }

	/// <summary>The group accounts enumerated on the asset.</summary>
	[JsonPropertyName("userGroups")]
	public IReadOnlyList<GroupAccount>? UserGroups { get; init; }

	/// <summary>The user accounts enumerated on the asset.</summary>
	[JsonPropertyName("users")]
	public IReadOnlyList<UserAccount>? Users { get; init; }
}
