using Rapid7.Api.Interfaces;

namespace Rapid7.Api;

public sealed partial class Rapid7CloudClient
{
	/// <summary>The health of the Cloud Integrations API (<c>admin/health</c>).</summary>
	public ICloudHealth Health => field ??= For<ICloudHealth>();

	/// <summary>Asset search and details (<c>v4/integration/assets</c>).</summary>
	public ICloudAssets Assets => field ??= For<ICloudAssets>();

	/// <summary>Sites (<c>v4/integration/sites</c>).</summary>
	public ICloudSites Sites => field ??= For<ICloudSites>();

	/// <summary>Vulnerability search (<c>v4/integration/vulnerabilities</c>).</summary>
	public ICloudVulnerabilities Vulnerabilities => field ??= For<ICloudVulnerabilities>();

	/// <summary>Scans: list, get, start and stop (<c>v4/integration/scan</c>).</summary>
	public ICloudScans Scans => field ??= For<ICloudScans>();

	/// <summary>Scan engines and their custom configuration (<c>v4/integration/scan/engine</c>).</summary>
	public ICloudScanEngines ScanEngines => field ??= For<ICloudScanEngines>();
}
