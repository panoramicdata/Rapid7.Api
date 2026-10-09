using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Policies;

/// <summary>The operating system of an asset.</summary>
public sealed class OperatingSystem
{
	/// <summary>The identifier of the operating system.</summary>
	[JsonPropertyName("id")]
	public long? Id { get; init; }

	/// <summary>The full description: vendor, family, product, version and architecture.</summary>
	[JsonPropertyName("description")]
	public string? Description { get; init; }

	/// <summary>The vendor.</summary>
	[JsonPropertyName("vendor")]
	public string? Vendor { get; init; }

	/// <summary>The family, such as Windows or Linux.</summary>
	[JsonPropertyName("family")]
	public string? Family { get; init; }

	/// <summary>The product name.</summary>
	[JsonPropertyName("product")]
	public string? Product { get; init; }

	/// <summary>The version.</summary>
	[JsonPropertyName("version")]
	public string? Version { get; init; }

	/// <summary>The processor architecture.</summary>
	[JsonPropertyName("architecture")]
	public string? Architecture { get; init; }

	/// <summary>The vendor and family combined, for grouping.</summary>
	[JsonPropertyName("systemName")]
	public string? SystemName { get; init; }

	/// <summary>The type of system, such as Workstation.</summary>
	[JsonPropertyName("type")]
	public string? Type { get; init; }

	/// <summary>The CPE name of the operating system.</summary>
	[JsonPropertyName("cpe")]
	public OperatingSystemCpe? Cpe { get; init; }

	/// <summary>Configuration name and value pairs found on the system.</summary>
	[JsonPropertyName("configurations")]
	public IReadOnlyList<OperatingSystemConfiguration> Configurations { get; init; } = [];
}
