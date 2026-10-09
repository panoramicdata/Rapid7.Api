using Rapid7.Api.Models;
using Rapid7.Api.Models.Remediations;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>
/// Asset remediation (<c>api/3/assets/{id}/vulnerabilities/{vulnerabilityId}/solution</c>): the solutions that best fit a
/// vulnerability on a particular asset, given what the console fingerprinted there. The user must have access to the asset.
/// </summary>
public interface IRemediations
{
	/// <summary>
	/// Lists the solutions matched to a vulnerability on an asset, with how each was matched
	/// (<c>GET api/3/assets/{id}/vulnerabilities/{vulnerabilityId}/solution</c>).
	/// </summary>
	/// <param name="assetId">The asset identifier.</param>
	/// <param name="vulnerabilityId">The vulnerability identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The matched solutions, unpaged.</returns>
	[Get("api/3/assets/{assetId}/vulnerabilities/{vulnerabilityId}/solution")]
	Task<ResourceList<MatchedSolution>> ListSolutionsAsync(long assetId, string vulnerabilityId, CancellationToken cancellationToken);
}
