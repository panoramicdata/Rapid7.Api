namespace Rapid7.Api.Models.BulkExport;

/// <summary>
/// Starts an asset software export (the <c>createAssetSoftwareExport</c> mutation, an early-access operation), producing
/// the <c>asset_software</c> dataset.
/// </summary>
public sealed class CreateAssetSoftwareExportRequest() : CreateExportRequest("CreateAssetSoftwareExport", "createAssetSoftwareExport");
