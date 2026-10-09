using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Vulnerabilities;

/// <summary>How a vulnerability affects PCI compliance.</summary>
public sealed class Pci
{
	/// <summary>The CVSS score after PCI rules and exceptions, from 0 to 10.</summary>
	[JsonPropertyName("adjustedCVSSScore")]
	public int? AdjustedCvssScore { get; init; }

	/// <summary>The severity score after PCI rules and exceptions, from 0 to 10.</summary>
	[JsonPropertyName("adjustedSeverityScore")]
	public int? AdjustedSeverityScore { get; init; }

	/// <summary>Whether the vulnerability causes a PCI failure on an asset that has it.</summary>
	[JsonPropertyName("fail")]
	public bool? Fail { get; init; }

	/// <summary>Notes on the vulnerability that concern PCI compliance.</summary>
	[JsonPropertyName("specialNotes")]
	public string? SpecialNotes { get; init; }

	/// <summary>The PCI status.</summary>
	[JsonPropertyName("status")]
	public PciStatus? Status { get; init; }
}
