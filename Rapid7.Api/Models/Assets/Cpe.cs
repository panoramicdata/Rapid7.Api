using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Assets;

/// <summary>A Common Platform Enumeration (CPE) name of an operating system or piece of software, with its components.</summary>
public sealed class Cpe
{
	/// <summary>The edition.</summary>
	[JsonPropertyName("edition")]
	public string? Edition { get; init; }

	/// <summary>The language.</summary>
	[JsonPropertyName("language")]
	public string? Language { get; init; }

	/// <summary>Any other component.</summary>
	[JsonPropertyName("other")]
	public string? Other { get; init; }

	/// <summary>The part: operating system, application or hardware.</summary>
	[JsonPropertyName("part")]
	public CpePart Part { get; init; }

	/// <summary>The product.</summary>
	[JsonPropertyName("product")]
	public string? Product { get; init; }

	/// <summary>The software edition.</summary>
	[JsonPropertyName("swEdition")]
	public string? SoftwareEdition { get; init; }

	/// <summary>The target hardware.</summary>
	[JsonPropertyName("targetHW")]
	public string? TargetHardware { get; init; }

	/// <summary>The target software.</summary>
	[JsonPropertyName("targetSW")]
	public string? TargetSoftware { get; init; }

	/// <summary>The update or service pack.</summary>
	[JsonPropertyName("update")]
	public string? Update { get; init; }

	/// <summary>The name in CPE 2.2 URI form, such as <c>cpe:/o:vendor:product</c>.</summary>
	[JsonPropertyName("v2.2")]
	public string? V22 { get; init; }

	/// <summary>The name in CPE 2.3 formatted-string form, such as <c>cpe:2.3:o:vendor:product:...</c>.</summary>
	[JsonPropertyName("v2.3")]
	public string? V23 { get; init; }

	/// <summary>The vendor.</summary>
	[JsonPropertyName("vendor")]
	public string? Vendor { get; init; }

	/// <summary>The version.</summary>
	[JsonPropertyName("version")]
	public string? Version { get; init; }
}
