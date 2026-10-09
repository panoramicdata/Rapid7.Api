using Rapid7.Api.Models;
using Rapid7.Api.Models.Policies;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>The groups of a compliance policy, their rules, children and results by asset (<c>api/3/policies/{policyId}/groups</c>).</summary>
public interface IPolicyGroups
{
	/// <summary>Lists every group in a policy, at any depth (<c>GET api/3/policies/{policyId}/groups</c>).</summary>
	/// <param name="policyId">The identifier of the policy.</param>
	/// <param name="paging">Paging and sorting; <see langword="null"/> for the defaults.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>One page of groups.</returns>
	[Get("api/3/policies/{policyId}/groups")]
	Task<Page<PolicyGroup>> GetGroupsAsync(long policyId, [Query] PageOptions? paging, CancellationToken cancellationToken);

	/// <summary>Gets one group of a policy (<c>GET api/3/policies/{policyId}/groups/{groupId}</c>).</summary>
	/// <param name="policyId">The identifier of the policy.</param>
	/// <param name="groupId">The identifier of the group.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The group.</returns>
	[Get("api/3/policies/{policyId}/groups/{groupId}")]
	Task<PolicyGroup> GetGroupAsync(long policyId, long groupId, CancellationToken cancellationToken);

	/// <summary>Lists the rules and groups directly under a group (<c>GET api/3/policies/{policyId}/groups/{groupId}/children</c>).</summary>
	/// <param name="policyId">The identifier of the policy.</param>
	/// <param name="groupId">The identifier of the group.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The rules and groups one level below the group.</returns>
	[Get("api/3/policies/{policyId}/groups/{groupId}/children")]
	Task<Page<PolicyItem>> GetChildrenAsync(long policyId, long groupId, CancellationToken cancellationToken);

	/// <summary>Lists every rule under a group, at any depth (<c>GET api/3/policies/{policyId}/groups/{groupId}/rules</c>).</summary>
	/// <param name="policyId">The identifier of the policy.</param>
	/// <param name="groupId">The identifier of the group.</param>
	/// <param name="paging">Paging and sorting; <see langword="null"/> for the defaults.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>One page of rules.</returns>
	[Get("api/3/policies/{policyId}/groups/{groupId}/rules")]
	Task<Page<PolicyRule>> GetRulesAsync(long policyId, long groupId, [Query] PageOptions? paging, CancellationToken cancellationToken);

	/// <summary>
	/// Lists how each asset fares against the rules under a group (<c>GET api/3/policies/{policyId}/groups/{groupId}/assets</c>).
	/// </summary>
	/// <param name="policyId">The identifier of the policy.</param>
	/// <param name="groupId">The identifier of the group.</param>
	/// <param name="options">Whether to return only applicable results, and paging; <see langword="null"/> for the defaults.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>One page of asset results.</returns>
	[Get("api/3/policies/{policyId}/groups/{groupId}/assets")]
	Task<Page<PolicyAsset>> GetAssetResultsAsync(long policyId, long groupId, [Query] PolicyResultListOptions? options, CancellationToken cancellationToken);

	/// <summary>
	/// Gets how one asset fares against the rules under a group
	/// (<c>GET api/3/policies/{policyId}/groups/{groupId}/assets/{assetId}</c>).
	/// </summary>
	/// <param name="policyId">The identifier of the policy.</param>
	/// <param name="groupId">The identifier of the group.</param>
	/// <param name="assetId">The identifier of the asset.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The asset's result.</returns>
	[Get("api/3/policies/{policyId}/groups/{groupId}/assets/{assetId}")]
	Task<PolicyAsset> GetAssetResultAsync(long policyId, long groupId, long assetId, CancellationToken cancellationToken);
}
