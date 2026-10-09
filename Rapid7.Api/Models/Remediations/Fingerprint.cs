using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Remediations;

/// <summary>A fingerprint (operating system, service or software) the console matched a solution against.</summary>
public sealed class Fingerprint
{
	/// <summary>A description of the fingerprint.</summary>
	[JsonPropertyName("description")]
	public string? Description { get; init; }

	/// <summary>The product family.</summary>
	[JsonPropertyName("family")]
	public string? Family { get; init; }

	/// <summary>The product.</summary>
	[JsonPropertyName("product")]
	public string? Product { get; init; }

	/// <summary>The vendor.</summary>
	[JsonPropertyName("vendor")]
	public string? Vendor { get; init; }

	/// <summary>The version.</summary>
	[JsonPropertyName("version")]
	public string? Version { get; init; }
}
