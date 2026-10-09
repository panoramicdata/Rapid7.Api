using Rapid7.Api.Interfaces;

namespace Rapid7.Api;

public sealed partial class Rapid7Client
{
	/// <summary>The solutions matched to a vulnerability on an asset (<c>api/3/assets/{id}/vulnerabilities/{vulnerabilityId}/solution</c>).</summary>
	public IRemediations Remediations => field ??= For<IRemediations>();
}
