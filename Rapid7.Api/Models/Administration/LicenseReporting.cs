using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Administration;

/// <summary>The reporting features a licence enables.</summary>
public sealed class LicenseReporting
{
	/// <summary>Whether advanced reporting is available.</summary>
	[JsonPropertyName("advanced")]
	public bool? Advanced { get; init; }

	/// <summary>Whether customisable CSV export is available.</summary>
	[JsonPropertyName("customizableCSVExport")]
	public bool? CustomizableCsvExport { get; init; }

	/// <summary>Whether PCI reporting is available.</summary>
	[JsonPropertyName("pci")]
	public bool? Pci { get; init; }
}
