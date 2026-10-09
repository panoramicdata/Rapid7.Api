namespace Rapid7.Api.Models.Assets;

/// <summary>
/// The documented asset fields a <see cref="SearchFilter"/> can filter on. The console may accept others; pass any name
/// as a string.
/// </summary>
public static class SearchField
{
	/// <summary><c>alternate-address-type</c>: <c>0</c> for IPv4, <c>1</c> for IPv6.</summary>
	public static string AlternateAddressType { get; } = "alternate-address-type";

	/// <summary><c>container-image</c>: the image of a container on the asset.</summary>
	public static string ContainerImage { get; } = "container-image";

	/// <summary><c>container-status</c>: such as <c>running</c> or <c>exited</c>.</summary>
	public static string ContainerStatus { get; } = "container-status";

	/// <summary><c>containers</c>: <c>0</c> when present, <c>1</c> when not.</summary>
	public static string Containers { get; } = "containers";

	/// <summary><c>criticality-tag</c>: the criticality tag applied.</summary>
	public static string CriticalityTag { get; } = "criticality-tag";

	/// <summary><c>custom-tag</c>: a custom tag applied.</summary>
	public static string CustomTag { get; } = "custom-tag";

	/// <summary><c>cve</c>: a CVE identifier of a vulnerability on the asset.</summary>
	public static string Cve { get; } = "cve";

	/// <summary><c>cvss-access-complexity</c>: <c>L</c>, <c>M</c> or <c>H</c>.</summary>
	public static string CvssAccessComplexity { get; } = "cvss-access-complexity";

	/// <summary><c>cvss-authentication-required</c>: <c>N</c>, <c>S</c> or <c>M</c>.</summary>
	public static string CvssAuthenticationRequired { get; } = "cvss-authentication-required";

	/// <summary><c>cvss-access-vector</c>: <c>L</c>, <c>A</c> or <c>N</c>.</summary>
	public static string CvssAccessVector { get; } = "cvss-access-vector";

	/// <summary><c>cvss-availability-impact</c>: <c>N</c>, <c>P</c> or <c>C</c>.</summary>
	public static string CvssAvailabilityImpact { get; } = "cvss-availability-impact";

	/// <summary><c>cvss-confidentiality-impact</c>: <c>N</c>, <c>P</c> or <c>C</c>.</summary>
	public static string CvssConfidentialityImpact { get; } = "cvss-confidentiality-impact";

	/// <summary><c>cvss-integrity-impact</c>: <c>N</c>, <c>P</c> or <c>C</c>.</summary>
	public static string CvssIntegrityImpact { get; } = "cvss-integrity-impact";

	/// <summary><c>cvss-v3-confidentiality-impact</c>: <c>N</c>, <c>L</c> or <c>H</c>.</summary>
	public static string CvssV3ConfidentialityImpact { get; } = "cvss-v3-confidentiality-impact";

	/// <summary><c>cvss-v3-integrity-impact</c>: <c>N</c>, <c>L</c> or <c>H</c>.</summary>
	public static string CvssV3IntegrityImpact { get; } = "cvss-v3-integrity-impact";

	/// <summary><c>cvss-v3-availability-impact</c>: <c>N</c>, <c>L</c> or <c>H</c>.</summary>
	public static string CvssV3AvailabilityImpact { get; } = "cvss-v3-availability-impact";

	/// <summary><c>cvss-v3-attack-vector</c>: <c>N</c>, <c>A</c>, <c>L</c> or <c>P</c>.</summary>
	public static string CvssV3AttackVector { get; } = "cvss-v3-attack-vector";

	/// <summary><c>cvss-v3-attack-complexity</c>: <c>L</c> or <c>H</c>.</summary>
	public static string CvssV3AttackComplexity { get; } = "cvss-v3-attack-complexity";

	/// <summary><c>cvss-v3-user-interaction</c>: <c>N</c> or <c>R</c>.</summary>
	public static string CvssV3UserInteraction { get; } = "cvss-v3-user-interaction";

	/// <summary><c>cvss-v3-privileges-required</c>: <c>N</c>, <c>L</c> or <c>H</c>.</summary>
	public static string CvssV3PrivilegesRequired { get; } = "cvss-v3-privileges-required";

	/// <summary><c>host-name</c>: the host name of the asset.</summary>
	public static string HostName { get; } = "host-name";

	/// <summary><c>host-type</c>: <c>0</c> unknown, <c>1</c> guest, <c>2</c> hypervisor, <c>3</c> physical, <c>4</c> mobile.</summary>
	public static string HostType { get; } = "host-type";

	/// <summary><c>ip-address</c>: an address of the asset.</summary>
	public static string IpAddress { get; } = "ip-address";

	/// <summary><c>ip-address-type</c>: <c>0</c> for IPv4, <c>1</c> for IPv6.</summary>
	public static string IpAddressType { get; } = "ip-address-type";

	/// <summary><c>last-scan-date</c>: when the asset was last scanned.</summary>
	public static string LastScanDate { get; } = "last-scan-date";

	/// <summary><c>location-tag</c>: a location tag applied.</summary>
	public static string LocationTag { get; } = "location-tag";

	/// <summary><c>mobile-device-last-sync-time</c>: when a mobile device last synchronised.</summary>
	public static string MobileDeviceLastSyncTime { get; } = "mobile-device-last-sync-time";

	/// <summary><c>open-ports</c>: an open port on the asset.</summary>
	public static string OpenPorts { get; } = "open-ports";

	/// <summary><c>operating-system</c>: the operating system of the asset.</summary>
	public static string OperatingSystem { get; } = "operating-system";

	/// <summary><c>owner-tag</c>: an owner tag applied.</summary>
	public static string OwnerTag { get; } = "owner-tag";

	/// <summary><c>pci-compliance</c>: <c>0</c> fail, <c>1</c> pass.</summary>
	public static string PciCompliance { get; } = "pci-compliance";

	/// <summary><c>risk-score</c>: the risk score of the asset.</summary>
	public static string RiskScore { get; } = "risk-score";

	/// <summary><c>service-name</c>: the name of a service on the asset.</summary>
	public static string ServiceName { get; } = "service-name";

	/// <summary><c>site-id</c>: a site the asset belongs to.</summary>
	public static string SiteId { get; } = "site-id";

	/// <summary><c>software</c>: software installed on the asset.</summary>
	public static string Software { get; } = "software";

	/// <summary><c>vAsset-cluster</c>: the virtualisation cluster of the asset.</summary>
	public static string VirtualAssetCluster { get; } = "vAsset-cluster";

	/// <summary><c>vAsset-datacenter</c>: the virtualisation data centre of the asset.</summary>
	public static string VirtualAssetDatacenter { get; } = "vAsset-datacenter";

	/// <summary><c>vAsset-host-name</c>: the virtualisation host of the asset.</summary>
	public static string VirtualAssetHostName { get; } = "vAsset-host-name";

	/// <summary><c>vAsset-power-state</c>: the power state of a virtual asset.</summary>
	public static string VirtualAssetPowerState { get; } = "vAsset-power-state";

	/// <summary><c>vAsset-resource-pool-path</c>: the resource pool path of a virtual asset.</summary>
	public static string VirtualAssetResourcePoolPath { get; } = "vAsset-resource-pool-path";

	/// <summary><c>vulnerability-assessed</c>: when the asset was last assessed for vulnerabilities.</summary>
	public static string VulnerabilityAssessed { get; } = "vulnerability-assessed";

	/// <summary><c>vulnerability-category</c>: the category of a vulnerability on the asset.</summary>
	public static string VulnerabilityCategory { get; } = "vulnerability-category";

	/// <summary><c>vulnerability-cvss-v3-score</c>: the CVSS v3 score of a vulnerability on the asset.</summary>
	public static string VulnerabilityCvssV3Score { get; } = "vulnerability-cvss-v3-score";

	/// <summary><c>vulnerability-cvss-score</c>: the CVSS v2 score of a vulnerability on the asset.</summary>
	public static string VulnerabilityCvssScore { get; } = "vulnerability-cvss-score";

	/// <summary><c>vulnerability-exposures</c>: the exposures (such as exploits or malware kits) of vulnerabilities on the asset.</summary>
	public static string VulnerabilityExposures { get; } = "vulnerability-exposures";

	/// <summary><c>vulnerability-title</c>: the title of a vulnerability on the asset.</summary>
	public static string VulnerabilityTitle { get; } = "vulnerability-title";

	/// <summary><c>vulnerability-validated-status</c>: <c>0</c> when present, <c>1</c> when not.</summary>
	public static string VulnerabilityValidatedStatus { get; } = "vulnerability-validated-status";
}
