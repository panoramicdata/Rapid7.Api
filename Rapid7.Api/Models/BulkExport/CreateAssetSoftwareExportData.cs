using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.BulkExport;

/// <summary>The data of a <c>createAssetSoftwareExport</c> response.</summary>
public sealed class CreateAssetSoftwareExportData : ICreatedExportData
{
	/// <summary>The export created.</summary>
	[JsonPropertyName("createAssetSoftwareExport")]
	public ExportReference? Export { get; init; }
}
