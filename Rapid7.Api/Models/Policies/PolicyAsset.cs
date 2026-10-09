using Rapid7.Api.Models.Assets;
using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Policies;

/// <summary>An asset's compliance with a policy, policy group or policy rule.</summary>
public sealed class PolicyAsset : LinksResource
{
	/// <summary>The identifier of the asset.</summary>
	[JsonPropertyName("id")]
	public long? Id { get; init; }

	/// <summary>The primary host name of the asset.</summary>
	[JsonPropertyName("hostname")]
	public string? Hostname { get; init; }

	/// <summary>The primary IPv4 or IPv6 address of the asset.</summary>
	[JsonPropertyName("ip")]
	public string? Ip { get; init; }

	/// <summary>The operating system of the asset.</summary>
	[JsonPropertyName("os")]
	public OperatingSystemFingerprint? Os { get; init; }

	/// <summary>The asset's overall compliance status.</summary>
	[JsonPropertyName("status")]
	public PolicyAssetStatus? Status { get; init; }
}
