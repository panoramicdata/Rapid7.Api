using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Assets;

/// <summary>
/// An operating system, as fingerprinted on an asset or listed in the console's operating system catalogue
/// (<c>api/3/operating_systems</c>).
/// </summary>
public sealed class OperatingSystemFingerprint : CatalogEntry
{
	/// <summary>The processor architecture, such as <c>x86</c> or <c>x86_64</c>.</summary>
	[JsonPropertyName("architecture")]
	public string? Architecture { get; init; }

	/// <summary>The system name, such as <c>Microsoft Windows</c>.</summary>
	[JsonPropertyName("systemName")]
	public string? SystemName { get; init; }
}
