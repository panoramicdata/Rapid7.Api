namespace Rapid7.Api.Models.BulkExport;

/// <summary>The names of the datasets bulk exports produce, as used in <see cref="ExportResult.Prefix"/>.</summary>
public static class ExportDatasets
{
	/// <summary>Assets (<c>asset</c>), from policy and vulnerability exports.</summary>
	public const string Asset = "asset";

	/// <summary>Policy rule results per asset (<c>asset_policy</c>), from policy exports.</summary>
	public const string AssetPolicy = "asset_policy";

	/// <summary>Policy rule results per asset from scans (<c>asset_scan_policy</c>), from policy exports.</summary>
	public const string AssetScanPolicy = "asset_scan_policy";

	/// <summary>Vulnerability findings per asset (<c>asset_vulnerability</c>), from vulnerability exports.</summary>
	public const string AssetVulnerability = "asset_vulnerability";

	/// <summary>Findings covered by exceptions (<c>vulnerability_exception</c>), from vulnerability exports.</summary>
	public const string VulnerabilityException = "vulnerability_exception";

	/// <summary>Remediated findings (<c>vulnerability_remediation</c>), from remediation exports.</summary>
	public const string VulnerabilityRemediation = "vulnerability_remediation";

	/// <summary>Installed software per asset (<c>asset_software</c>), from software exports.</summary>
	public const string AssetSoftware = "asset_software";
}
