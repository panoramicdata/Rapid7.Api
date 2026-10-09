using Rapid7.Api.Models;
using Rapid7.Api.Models.Policies;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>
/// Compliance policies and their results across assets (<c>api/3/policies</c>, <c>api/3/policy/summary</c>). Results
/// cover only the assets the caller can access. Rules are in <see cref="IPolicyRules"/>, groups in
/// <see cref="IPolicyGroups"/>, and one asset's view in <see cref="IAssetPolicies"/>.
/// </summary>
public interface IPolicies
{
	/// <summary>Lists compliance policies with their results (<c>GET api/3/policies</c>).</summary>
	/// <param name="options">A title filter, whether to return only scanned policies, and paging; <see langword="null"/> for the defaults.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>One page of policies.</returns>
	[Get("api/3/policies")]
	Task<Page<Policy>> GetPoliciesAsync([Query] PolicyListOptions? options, CancellationToken cancellationToken);

	/// <summary>Gets one compliance policy with its results (<c>GET api/3/policies/{policyId}</c>).</summary>
	/// <param name="policyId">The identifier of the policy.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The policy.</returns>
	/// <remarks>
	/// The OpenAPI document names the scan template policy settings as this response; the console returns the compliance
	/// policy, so that is what this method reads.
	/// </remarks>
	[Get("api/3/policies/{policyId}")]
	Task<Policy> GetPolicyAsync(long policyId, CancellationToken cancellationToken);

	/// <summary>Lists the rules and groups directly under a policy (<c>GET api/3/policies/{policyId}/children</c>).</summary>
	/// <param name="policyId">The identifier of the policy.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The rules and groups at the top level of the policy.</returns>
	[Get("api/3/policies/{policyId}/children")]
	Task<Page<PolicyItem>> GetChildrenAsync(long policyId, CancellationToken cancellationToken);

	/// <summary>Lists how each asset fares against a policy (<c>GET api/3/policies/{policyId}/assets</c>).</summary>
	/// <param name="policyId">The identifier of the policy.</param>
	/// <param name="options">Whether to return only applicable results, and paging; <see langword="null"/> for the defaults.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>One page of asset results.</returns>
	[Get("api/3/policies/{policyId}/assets")]
	Task<Page<PolicyAsset>> GetAssetResultsAsync(long policyId, [Query] PolicyResultListOptions? options, CancellationToken cancellationToken);

	/// <summary>Gets how one asset fares against a policy (<c>GET api/3/policies/{policyId}/assets/{assetId}</c>).</summary>
	/// <param name="policyId">The identifier of the policy.</param>
	/// <param name="assetId">The identifier of the asset.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The asset's result.</returns>
	[Get("api/3/policies/{policyId}/assets/{assetId}")]
	Task<PolicyAsset> GetAssetResultAsync(long policyId, long assetId, CancellationToken cancellationToken);

	/// <summary>Gets compliance totals across every policy (<c>GET api/3/policy/summary</c>).</summary>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The totals.</returns>
	[Get("api/3/policy/summary")]
	Task<PolicySummary> GetSummaryAsync(CancellationToken cancellationToken);
}
