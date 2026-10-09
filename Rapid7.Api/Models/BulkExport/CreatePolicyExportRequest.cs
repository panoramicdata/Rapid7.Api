namespace Rapid7.Api.Models.BulkExport;

/// <summary>
/// Starts a policy export (the <c>createPolicyExport</c> mutation), producing the <c>asset</c>, <c>asset_policy</c> and
/// <c>asset_scan_policy</c> datasets.
/// </summary>
public sealed class CreatePolicyExportRequest() : CreateExportRequest("CreatePolicyExport", "createPolicyExport");
