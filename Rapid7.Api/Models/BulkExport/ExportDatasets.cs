namespace Rapid7.Api.Models.BulkExport;

/// <summary>The names of the datasets bulk exports produce, as used in <see cref="ExportResult.Prefix"/>.</summary>
public static class ExportDatasets
{
	/// <summary>Assets (<c>asset</c>), from policy and vulnerability exports; read as <see cref="AssetRecord"/>.</summary>
	public static string Asset { get; } = "asset";

	/// <summary>Policy rule results per asset (<c>asset_policy</c>), from policy exports; read as <see cref="AssetPolicyRecord"/>.</summary>
	public static string AssetPolicy { get; } = "asset_policy";

	/// <summary>
	/// Policy rule results per asset from scans (<c>asset_scan_policy</c>), from policy exports; read as
	/// <see cref="AssetPolicyRecord"/>.
	/// </summary>
	public static string AssetScanPolicy { get; } = "asset_scan_policy";

	/// <summary>
	/// Vulnerability findings per asset (<c>asset_vulnerability</c>), from vulnerability exports; read as
	/// <see cref="AssetVulnerabilityRecord"/>.
	/// </summary>
	public static string AssetVulnerability { get; } = "asset_vulnerability";

	/// <summary>
	/// Findings covered by exceptions (<c>vulnerability_exception</c>), from vulnerability exports; read as
	/// <see cref="VulnerabilityExceptionRecord"/>.
	/// </summary>
	public static string VulnerabilityException { get; } = "vulnerability_exception";

	/// <summary>
	/// Remediated findings (<c>vulnerability_remediation</c>), from remediation exports; read as
	/// <see cref="VulnerabilityRemediationRecord"/>.
	/// </summary>
	public static string VulnerabilityRemediation { get; } = "vulnerability_remediation";

	/// <summary>Installed software per asset (<c>asset_software</c>), from software exports; read as <see cref="AssetSoftwareRecord"/>.</summary>
	public static string AssetSoftware { get; } = "asset_software";
}
