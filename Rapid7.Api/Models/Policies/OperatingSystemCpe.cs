using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Policies;

/// <summary>A Common Platform Enumeration (CPE) name, split into its components.</summary>
public sealed class OperatingSystemCpe
{
	/// <summary>The kind of platform.</summary>
	[JsonPropertyName("part")]
	public CpePart Part { get; init; }

	/// <summary>The vendor.</summary>
	[JsonPropertyName("vendor")]
	public string? Vendor { get; init; }

	/// <summary>The product.</summary>
	[JsonPropertyName("product")]
	public string? Product { get; init; }

	/// <summary>The release version.</summary>
	[JsonPropertyName("version")]
	public string? Version { get; init; }

	/// <summary>The update, service pack or point release.</summary>
	[JsonPropertyName("update")]
	public string? Update { get; init; }

	/// <summary>The edition.</summary>
	[JsonPropertyName("edition")]
	public string? Edition { get; init; }

	/// <summary>The user interface language, as an RFC 5646 tag.</summary>
	[JsonPropertyName("language")]
	public string? Language { get; init; }

	/// <summary>The market or class of users the product is tailored to.</summary>
	[JsonPropertyName("swEdition")]
	public string? SwEdition { get; init; }

	/// <summary>The software environment the product runs in.</summary>
	[JsonPropertyName("targetSW")]
	public string? TargetSoftware { get; init; }

	/// <summary>The instruction set architecture the product runs on.</summary>
	[JsonPropertyName("targetHW")]
	public string? TargetHardware { get; init; }

	/// <summary>Any other identifying information.</summary>
	[JsonPropertyName("other")]
	public string? Other { get; init; }

	/// <summary>The full name in CPE 2.2 format.</summary>
	[JsonPropertyName("v2.2")]
	public string? V22 { get; init; }

	/// <summary>The full name in CPE 2.3 formatted string binding.</summary>
	[JsonPropertyName("v2.3")]
	public string? V23 { get; init; }
}
