using Rapid7.Api.Interfaces;

namespace Rapid7.Api;

public sealed partial class Rapid7Client
{
	/// <summary>The vulnerability catalogue and each vulnerability's exploits, malware kits, references, solutions and affected assets (<c>api/3/vulnerabilities</c>).</summary>
	public IVulnerabilities Vulnerabilities => field ??= For<IVulnerabilities>();

	/// <summary>Vulnerability categories (<c>api/3/vulnerability_categories</c>).</summary>
	public IVulnerabilityCategories VulnerabilityCategories => field ??= For<IVulnerabilityCategories>();

	/// <summary>External vulnerability references (<c>api/3/vulnerability_references</c>).</summary>
	public IVulnerabilityReferences VulnerabilityReferences => field ??= For<IVulnerabilityReferences>();

	/// <summary>Known exploits (<c>api/3/exploits</c>).</summary>
	public IExploits Exploits => field ??= For<IExploits>();

	/// <summary>Malware kits (<c>api/3/malware_kits</c>).</summary>
	public IMalwareKits MalwareKits => field ??= For<IMalwareKits>();

	/// <summary>Vulnerability solutions, their prerequisites and supersedence (<c>api/3/solutions</c>).</summary>
	public ISolutions Solutions => field ??= For<ISolutions>();
}
