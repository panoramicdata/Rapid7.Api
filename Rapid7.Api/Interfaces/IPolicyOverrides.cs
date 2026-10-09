using Rapid7.Api.Models;
using Rapid7.Api.Models.PolicyOverrides;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>
/// Policy overrides: requests to treat a policy rule's result differently, their review and expiry
/// (<c>api/3/policy_overrides</c>, <c>api/3/assets/{id}/policy_overrides</c>).
/// </summary>
public interface IPolicyOverrides
{
	/// <summary>Lists every policy override (<c>GET api/3/policy_overrides</c>).</summary>
	/// <param name="paging">Paging and sorting; <see langword="null"/> for the defaults.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>One page of policy overrides.</returns>
	[Get("api/3/policy_overrides")]
	Task<Page<PolicyOverride>> GetPolicyOverridesAsync([Query] PageOptions? paging, CancellationToken cancellationToken);

	/// <summary>Lists the policy overrides that apply to an asset (<c>GET api/3/assets/{id}/policy_overrides</c>).</summary>
	/// <param name="assetId">The identifier of the asset.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Every policy override for the asset.</returns>
	[Get("api/3/assets/{assetId}/policy_overrides")]
	Task<ResourceList<PolicyOverride>> GetForAssetAsync(long assetId, CancellationToken cancellationToken);

	/// <summary>Submits a policy override (<c>POST api/3/policy_overrides</c>).</summary>
	/// <param name="request">The rule, scope, substitute result and reason.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The identifier of the new policy override.</returns>
	/// <remarks>Set <see cref="PolicyOverrideRequest.State"/> to <see cref="PolicyOverrideState.Approved"/> to submit and approve in one request, when allowed to review overrides.</remarks>
	[Post("api/3/policy_overrides")]
	Task<CreatedReference<long>> CreateAsync([Body] PolicyOverrideRequest request, CancellationToken cancellationToken);

	/// <summary>Gets one policy override (<c>GET api/3/policy_overrides/{id}</c>).</summary>
	/// <param name="id">The identifier of the policy override.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The policy override.</returns>
	[Get("api/3/policy_overrides/{id}")]
	Task<PolicyOverride> GetAsync(long id, CancellationToken cancellationToken);

	/// <summary>Deletes a policy override (<c>DELETE api/3/policy_overrides/{id}</c>).</summary>
	/// <param name="id">The identifier of the policy override.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to related resources.</returns>
	[Delete("api/3/policy_overrides/{id}")]
	Task<Links> DeleteAsync(long id, CancellationToken cancellationToken);

	/// <summary>
	/// Recalls, approves or rejects a policy override, without a comment (<c>POST api/3/policy_overrides/{id}/{status}</c>).
	/// </summary>
	/// <param name="id">The identifier of the policy override.</param>
	/// <param name="status">The review action, sent in the path.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>A task that completes when the console has applied the change.</returns>
	[Post("api/3/policy_overrides/{id}/{status}")]
	Task SetStatusAsync(long id, PolicyOverrideStatusChange status, CancellationToken cancellationToken);

	/// <summary>
	/// Recalls, approves or rejects a policy override, with a comment (<c>POST api/3/policy_overrides/{id}/{status}</c>).
	/// </summary>
	/// <param name="id">The identifier of the policy override.</param>
	/// <param name="status">The review action, sent in the path.</param>
	/// <param name="comment">Why the status changed, sent as the request body (a JSON string).</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>A task that completes when the console has applied the change.</returns>
	[Post("api/3/policy_overrides/{id}/{status}")]
	Task SetStatusAsync(long id, PolicyOverrideStatusChange status, [Body(BodySerializationMethod.Serialized)] string comment, CancellationToken cancellationToken);

	/// <summary>Gets when a policy override expires (<c>GET api/3/policy_overrides/{id}/expires</c>).</summary>
	/// <param name="id">The identifier of the policy override.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The expiry date, or <see langword="null"/> when the console returns none.</returns>
	[Get("api/3/policy_overrides/{id}/expires")]
	Task<DateTimeOffset?> GetExpirationAsync(long id, CancellationToken cancellationToken);

	/// <summary>Sets when a policy override expires (<c>PUT api/3/policy_overrides/{id}/expires</c>).</summary>
	/// <param name="id">The identifier of the policy override.</param>
	/// <param name="expires">The new expiry date, sent as an ISO 8601 JSON string.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to related resources.</returns>
	[Put("api/3/policy_overrides/{id}/expires")]
	Task<Links> SetExpirationAsync(long id, [Body] DateTimeOffset expires, CancellationToken cancellationToken);
}
