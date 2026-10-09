using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Assets;

/// <summary>The identifying details an asset and an asset import (<see cref="AssetCreateRequest"/>) share.</summary>
public abstract class AssetProfile
{
	/// <summary>The primary host name (local or fully qualified).</summary>
	[JsonPropertyName("hostName")]
	public string? HostName { get; init; }

	/// <summary>The primary IPv4 or IPv6 address.</summary>
	[JsonPropertyName("ip")]
	public string? Ip { get; init; }

	/// <summary>The primary Media Access Control (MAC) address, as six colon-separated pairs of hexadecimal digits.</summary>
	[JsonPropertyName("mac")]
	public string? Mac { get; init; }

	/// <summary>A free-form description of the operating system, as a fingerprinting source reported it.</summary>
	[JsonPropertyName("os")]
	public string? Os { get; init; }

	/// <summary>The certainty of the operating system fingerprint, from <c>0</c> to <c>1</c>, as text.</summary>
	[JsonPropertyName("osCertainty")]
	public string? OsCertainty { get; init; }

	/// <summary>The operating system fingerprint.</summary>
	[JsonPropertyName("osFingerprint")]
	public OperatingSystemFingerprint? OsFingerprint { get; init; }

	/// <summary>The kind of machine.</summary>
	[JsonPropertyName("type")]
	public AssetType? Type { get; init; }
}
