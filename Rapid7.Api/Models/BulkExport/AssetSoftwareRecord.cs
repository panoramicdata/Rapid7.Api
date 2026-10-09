using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.BulkExport;

/// <summary>A row of the <c>asset_software</c> dataset: one piece of software found on an asset.</summary>
public sealed class AssetSoftwareRecord : BulkExportRecord
{
	/// <summary>The product name (<c>product</c>).</summary>
	[JsonPropertyName("product")]
	public string? Product { get; init; }

	/// <summary>The software family (<c>family</c>).</summary>
	[JsonPropertyName("family")]
	public string? Family { get; init; }

	/// <summary>The software vendor (<c>vendor</c>).</summary>
	[JsonPropertyName("vendor")]
	public string? Vendor { get; init; }

	/// <summary>The software version (<c>version</c>).</summary>
	[JsonPropertyName("version")]
	public string? Version { get; init; }

	/// <summary>The kind of software (<c>type</c>).</summary>
	[JsonPropertyName("type")]
	public string? Type { get; init; }

	/// <summary>How certain InsightVM is that the software is present (<c>certainty</c>).</summary>
	[JsonPropertyName("certainty")]
	public double? Certainty { get; init; }
}
