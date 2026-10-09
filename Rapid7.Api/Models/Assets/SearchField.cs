namespace Rapid7.Api.Models.Assets;

/// <summary>
/// The documented asset fields a <see cref="SearchFilter"/> can filter on. The console may accept others; pass any name
/// as a string.
/// </summary>
public static class SearchField
{
	/// <summary><c>alternate-address-type</c>: <c>0</c> for IPv4, <c>1</c> for IPv6.</summary>
	public const string AlternateAddressType = "alternate-address-type";

	/// <summary><c>container-image</c>: the image of a container on the asset.</summary>
	public const string ContainerImage = "container-image";

	/// <summary><c>container-status</c>: such as <c>running</c> or <c>exited</c>.</summary>
	public const string ContainerStatus = "container-status";

	/// <summary><c>containers</c>: <c>0</c> when present, <c>1</c> when not.</summary>
	public const string Containers = "containers";

	/// <summary><c>criticality-tag</c>: the criticality tag applied.</summary>
	public const string CriticalityTag = "criticality-tag";

	/// <summary><c>custom-tag</c>: a custom tag applied.</summary>
	public const string CustomTag = "custom-tag";

	/// <summary><c>cve</c>: a CVE identifier of a vulnerability on the asset.</summary>
	public const string Cve = "cve";

	/// <summary><c>cvss-access-complexity</c>: <c>L</c>, <c>M</c> or <c>H</c>.</summary>
	public const string CvssAccessComplexity = "cvss-access-complexity";

	/// <summary><c>cvss-authentication-required</c>: <c>N</c>, <c>S</c> or <c>M</c>.</summary>
	public const string CvssAuthenticationRequired = "cvss-authentication-required";

	/// <summary><c>cvss-access-vector</c>: <c>L</c>, <c>A</c> or <c>N</c>.</summary>
	public const string CvssAccessVector = "cvss-access-vector";

	/// <summary><c>cvss-availability-impact</c>: <c>N</c>, <c>P</c> or <c>C</c>.</summary>
	public const string CvssAvailabilityImpact = "cvss-availability-impact";

	/// <summary><c>cvss-confidentiality-impact</c>: <c>N</c>, <c>P</c> or <c>C</c>.</summary>
	public const string CvssConfidentialityImpact = "cvss-confidentiality-impact";

	/// <summary><c>cvss-integrity-impact</c>: <c>N</c>, <c>P</c> or <c>C</c>.</summary>
	public const string CvssIntegrityImpact = "cvss-integrity-impact";

	/// <summary><c>cvss-v3-confidentiality-impact</c>: <c>N</c>, <c>L</c> or <c>H</c>.</summary>
	public const string CvssV3ConfidentialityImpact = "cvss-v3-confidentiality-impact";

	/// <summary><c>cvss-v3-integrity-impact</c>: <c>N</c>, <c>L</c> or <c>H</c>.</summary>
	public const string CvssV3IntegrityImpact = "cvss-v3-integrity-impact";

	/// <summary><c>cvss-v3-availability-impact</c>: <c>N</c>, <c>L</c> or <c>H</c>.</summary>
	public const string CvssV3AvailabilityImpact = "cvss-v3-availability-impact";

	/// <summary><c>cvss-v3-attack-vector</c>: <c>N</c>, <c>A</c>, <c>L</c> or <c>P</c>.</summary>
	public const string CvssV3AttackVector = "cvss-v3-attack-vector";

	/// <summary><c>cvss-v3-attack-complexity</c>: <c>L</c> or <c>H</c>.</summary>
	public const string CvssV3AttackComplexity = "cvss-v3-attack-complexity";

	/// <summary><c>cvss-v3-user-interaction</c>: <c>N</c> or <c>R</c>.</summary>
	public const string CvssV3UserInteraction = "cvss-v3-user-interaction";

	/// <summary><c>cvss-v3-privileges-required</c>: <c>N</c>, <c>L</c> or <c>H</c>.</summary>
	public const string CvssV3PrivilegesRequired = "cvss-v3-privileges-required";

	/// <summary><c>host-name</c>: the host name of the asset.</summary>
	public const string HostName = "host-name";

	/// <summary><c>host-type</c>: <c>0</c> unknown, <c>1</c> guest, <c>2</c> hypervisor, <c>3</c> physical, <c>4</c> mobile.</summary>
	public const string HostType = "host-type";

	/// <summary><c>ip-address</c>: an address of the asset.</summary>
	public const string IpAddress = "ip-address";

	/// <summary><c>ip-address-type</c>: <c>0</c> for IPv4, <c>1</c> for IPv6.</summary>
	public const string IpAddressType = "ip-address-type";

	/// <summary><c>last-scan-date</c>: when the asset was last scanned.</summary>
	public const string LastScanDate = "last-scan-date";

	/// <summary><c>location-tag</c>: a location tag applied.</summary>
	public const string LocationTag = "location-tag";

	/// <summary><c>mobile-device-last-sync-time</c>: when a mobile device last synchronised.</summary>
	public const string MobileDeviceLastSyncTime = "mobile-device-last-sync-time";

	/// <summary><c>open-ports</c>: an open port on the asset.</summary>
	public const string OpenPorts = "open-ports";

	/// <summary><c>operating-system</c>: the operating system of the asset.</summary>
	public const string OperatingSystem = "operating-system";

	/// <summary><c>owner-tag</c>: an owner tag applied.</summary>
	public const string OwnerTag = "owner-tag";

	/// <summary><c>pci-compliance</c>: <c>0</c> fail, <c>1</c> pass.</summary>
	public const string PciCompliance = "pci-compliance";

	/// <summary><c>risk-score</c>: the risk score of the asset.</summary>
	public const string RiskScore = "risk-score";

	/// <summary><c>service-name</c>: the name of a service on the asset.</summary>
	public const string ServiceName = "service-name";

	/// <summary><c>site-id</c>: a site the asset belongs to.</summary>
	public const string SiteId = "site-id";

	/// <summary><c>software</c>: software installed on the asset.</summary>
	public const string Software = "software";

	/// <summary><c>vAsset-cluster</c>: the virtualisation cluster of the asset.</summary>
	public const string VirtualAssetCluster = "vAsset-cluster";

	/// <summary><c>vAsset-datacenter</c>: the virtualisation data centre of the asset.</summary>
	public const string VirtualAssetDatacenter = "vAsset-datacenter";

	/// <summary><c>vAsset-host-name</c>: the virtualisation host of the asset.</summary>
	public const string VirtualAssetHostName = "vAsset-host-name";

	/// <summary><c>vAsset-power-state</c>: the power state of a virtual asset.</summary>
	public const string VirtualAssetPowerState = "vAsset-power-state";

	/// <summary><c>vAsset-resource-pool-path</c>: the resource pool path of a virtual asset.</summary>
	public const string VirtualAssetResourcePoolPath = "vAsset-resource-pool-path";

	/// <summary><c>vulnerability-assessed</c>: when the asset was last assessed for vulnerabilities.</summary>
	public const string VulnerabilityAssessed = "vulnerability-assessed";

	/// <summary><c>vulnerability-category</c>: the category of a vulnerability on the asset.</summary>
	public const string VulnerabilityCategory = "vulnerability-category";

	/// <summary><c>vulnerability-cvss-v3-score</c>: the CVSS v3 score of a vulnerability on the asset.</summary>
	public const string VulnerabilityCvssV3Score = "vulnerability-cvss-v3-score";

	/// <summary><c>vulnerability-cvss-score</c>: the CVSS v2 score of a vulnerability on the asset.</summary>
	public const string VulnerabilityCvssScore = "vulnerability-cvss-score";

	/// <summary><c>vulnerability-exposures</c>: the exposures (such as exploits or malware kits) of vulnerabilities on the asset.</summary>
	public const string VulnerabilityExposures = "vulnerability-exposures";

	/// <summary><c>vulnerability-title</c>: the title of a vulnerability on the asset.</summary>
	public const string VulnerabilityTitle = "vulnerability-title";

	/// <summary><c>vulnerability-validated-status</c>: <c>0</c> when present, <c>1</c> when not.</summary>
	public const string VulnerabilityValidatedStatus = "vulnerability-validated-status";
}
