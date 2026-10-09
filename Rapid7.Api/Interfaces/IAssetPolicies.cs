using Rapid7.Api.Models;
using Rapid7.Api.Models.Policies;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>One asset's compliance with policies, policy groups and rules (<c>api/3/assets/{assetId}/policies</c>).</summary>
public interface IAssetPolicies
{
	/// <summary>Lists the policies evaluated against an asset, with the asset's results (<c>GET api/3/assets/{assetId}/policies</c>).</summary>
	/// <param name="assetId">The identifier of the asset.</param>
	/// <param name="options">Whether to return only applicable policies, and paging; <see langword="null"/> for the defaults.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>One page of policies, with statuses and counts for this asset.</returns>
	[Get("api/3/assets/{assetId}/policies")]
	Task<Page<Policy>> GetPoliciesAsync(long assetId, [Query] PolicyResultListOptions? options, CancellationToken cancellationToken);

	/// <summary>
	/// Lists the rules and groups directly under a policy, with an asset's results
	/// (<c>GET api/3/assets/{assetId}/policies/{policyId}/children</c>).
	/// </summary>
	/// <param name="assetId">The identifier of the asset.</param>
	/// <param name="policyId">The identifier of the policy.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The rules and groups at the top level of the policy.</returns>
	[Get("api/3/assets/{assetId}/policies/{policyId}/children")]
	Task<Page<PolicyItem>> GetChildrenAsync(long assetId, long policyId, CancellationToken cancellationToken);

	/// <summary>
	/// Lists the rules and groups directly under a policy group, with an asset's results
	/// (<c>GET api/3/assets/{assetId}/policies/{policyId}/groups/{groupId}/children</c>).
	/// </summary>
	/// <param name="assetId">The identifier of the asset.</param>
	/// <param name="policyId">The identifier of the policy.</param>
	/// <param name="groupId">The identifier of the group.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The rules and groups one level below the group.</returns>
	[Get("api/3/assets/{assetId}/policies/{policyId}/groups/{groupId}/children")]
	Task<Page<PolicyItem>> GetGroupChildrenAsync(long assetId, long policyId, long groupId, CancellationToken cancellationToken);

	/// <summary>Lists the rules of a policy with an asset's results (<c>GET api/3/assets/{assetId}/policies/{policyId}/rules</c>).</summary>
	/// <param name="assetId">The identifier of the asset.</param>
	/// <param name="policyId">The identifier of the policy.</param>
	/// <param name="paging">Paging and sorting; <see langword="null"/> for the defaults.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>One page of rules.</returns>
	[Get("api/3/assets/{assetId}/policies/{policyId}/rules")]
	Task<Page<PolicyRule>> GetRulesAsync(long assetId, long policyId, [Query] PageOptions? paging, CancellationToken cancellationToken);

	/// <summary>
	/// Lists the rules under a policy group with an asset's results
	/// (<c>GET api/3/assets/{assetId}/policies/{policyId}/groups/{groupId}/rules</c>).
	/// </summary>
	/// <param name="assetId">The identifier of the asset.</param>
	/// <param name="policyId">The identifier of the policy.</param>
	/// <param name="groupId">The identifier of the group.</param>
	/// <param name="paging">Paging and sorting; <see langword="null"/> for the defaults.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>One page of rules.</returns>
	[Get("api/3/assets/{assetId}/policies/{policyId}/groups/{groupId}/rules")]
	Task<Page<PolicyRule>> GetGroupRulesAsync(long assetId, long policyId, long groupId, [Query] PageOptions? paging, CancellationToken cancellationToken);
}
