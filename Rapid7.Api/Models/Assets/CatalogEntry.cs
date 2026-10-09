using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Assets;

/// <summary>The fields an operating system fingerprint and a piece of software share.</summary>
public abstract class CatalogEntry
{
	/// <summary>Settings enumerated for the entry, as name and value pairs.</summary>
	[JsonPropertyName("configurations")]
	public IReadOnlyList<Configuration> Configurations { get; init; } = [];

	/// <summary>The Common Platform Enumeration name of the entry.</summary>
	[JsonPropertyName("cpe")]
	public Cpe? Cpe { get; init; }

	/// <summary>A description of the entry, typically vendor, product and version together.</summary>
	[JsonPropertyName("description")]
	public string? Description { get; init; }

	/// <summary>The family, such as <c>Windows</c> or <c>Office 2013</c>.</summary>
	[JsonPropertyName("family")]
	public string? Family { get; init; }

	/// <summary>The identifier of the entry in the console's catalogue.</summary>
	[JsonPropertyName("id")]
	public long? Id { get; init; }

	/// <summary>The product name.</summary>
	[JsonPropertyName("product")]
	public string? Product { get; init; }

	/// <summary>The type, such as <c>Workstation</c> or <c>Productivity</c>.</summary>
	[JsonPropertyName("type")]
	public string? Type { get; init; }

	/// <summary>The vendor.</summary>
	[JsonPropertyName("vendor")]
	public string? Vendor { get; init; }

	/// <summary>The version.</summary>
	[JsonPropertyName("version")]
	public string? Version { get; init; }
}
