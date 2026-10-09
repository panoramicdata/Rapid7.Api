using Rapid7.Api.Models;
using Rapid7.Api.Models.Policies;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>
/// The rules of a compliance policy, their results by asset, controls, rationale, remediation and proof
/// (<c>api/3/policies/{policyId}/rules</c>).
/// </summary>
public interface IPolicyRules
{
	/// <summary>Lists every rule in a policy, at any depth (<c>GET api/3/policies/{policyId}/rules</c>).</summary>
	/// <param name="policyId">The identifier of the policy.</param>
	/// <param name="paging">Paging and sorting; <see langword="null"/> for the defaults.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>One page of rules.</returns>
	[Get("api/3/policies/{policyId}/rules")]
	Task<Page<PolicyRule>> GetRulesAsync(long policyId, [Query] PageOptions? paging, CancellationToken cancellationToken);

	/// <summary>Lists the rules of a policy that are disabled (<c>GET api/3/policies/{policyId}/rules/disabled</c>).</summary>
	/// <param name="policyId">The identifier of the policy.</param>
	/// <param name="paging">Paging and sorting; <see langword="null"/> for the defaults.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>One page of disabled rules.</returns>
	[Get("api/3/policies/{policyId}/rules/disabled")]
	Task<Page<PolicyRule>> GetDisabledRulesAsync(long policyId, [Query] PageOptions? paging, CancellationToken cancellationToken);

	/// <summary>Gets one rule of a policy (<c>GET api/3/policies/{policyId}/rules/{ruleId}</c>).</summary>
	/// <param name="policyId">The identifier of the policy.</param>
	/// <param name="ruleId">The identifier of the rule.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The rule.</returns>
	[Get("api/3/policies/{policyId}/rules/{ruleId}")]
	Task<PolicyRule> GetRuleAsync(long policyId, long ruleId, CancellationToken cancellationToken);

	/// <summary>Lists how each asset fares against a rule (<c>GET api/3/policies/{policyId}/rules/{ruleId}/assets</c>).</summary>
	/// <param name="policyId">The identifier of the policy.</param>
	/// <param name="ruleId">The identifier of the rule.</param>
	/// <param name="options">Whether to return only applicable results, and paging; <see langword="null"/> for the defaults.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>One page of asset results.</returns>
	[Get("api/3/policies/{policyId}/rules/{ruleId}/assets")]
	Task<Page<PolicyAsset>> GetAssetResultsAsync(long policyId, long ruleId, [Query] PolicyResultListOptions? options, CancellationToken cancellationToken);

	/// <summary>Gets how one asset fares against a rule (<c>GET api/3/policies/{policyId}/rules/{ruleId}/assets/{assetId}</c>).</summary>
	/// <param name="policyId">The identifier of the policy.</param>
	/// <param name="ruleId">The identifier of the rule.</param>
	/// <param name="assetId">The identifier of the asset.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The asset's result.</returns>
	[Get("api/3/policies/{policyId}/rules/{ruleId}/assets/{assetId}")]
	Task<PolicyAsset> GetAssetResultAsync(long policyId, long ruleId, long assetId, CancellationToken cancellationToken);

	/// <summary>
	/// Gets the evidence the scan collected when checking a rule on an asset
	/// (<c>GET api/3/policies/{policyId}/rules/{ruleId}/assets/{assetId}/proof</c>).
	/// </summary>
	/// <param name="policyId">The identifier of the policy.</param>
	/// <param name="ruleId">The identifier of the rule.</param>
	/// <param name="assetId">The identifier of the asset.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The proof, as an HTML fragment.</returns>
	[Get("api/3/policies/{policyId}/rules/{ruleId}/assets/{assetId}/proof")]
	[Headers("Accept: text/html")]
	Task<string> GetProofAsync(long policyId, long ruleId, long assetId, CancellationToken cancellationToken);

	/// <summary>Lists the NIST SP 800-53 control mappings of each CCE in a rule (<c>GET api/3/policies/{policyId}/rules/{ruleId}/controls</c>).</summary>
	/// <param name="policyId">The identifier of the policy.</param>
	/// <param name="ruleId">The identifier of the rule.</param>
	/// <param name="paging">Paging and sorting; <see langword="null"/> for the defaults.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>One page of controls.</returns>
	[Get("api/3/policies/{policyId}/rules/{ruleId}/controls")]
	Task<Page<PolicyControl>> GetControlsAsync(long policyId, long ruleId, [Query] PageOptions? paging, CancellationToken cancellationToken);

	/// <summary>Gets why a rule matters (<c>GET api/3/policies/{policyId}/rules/{ruleId}/rationale</c>).</summary>
	/// <param name="policyId">The identifier of the policy.</param>
	/// <param name="ruleId">The identifier of the rule.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The rationale, as an HTML fragment.</returns>
	[Get("api/3/policies/{policyId}/rules/{ruleId}/rationale")]
	[Headers("Accept: text/html")]
	Task<string> GetRationaleAsync(long policyId, long ruleId, CancellationToken cancellationToken);

	/// <summary>Gets how to make assets comply with a rule (<c>GET api/3/policies/{policyId}/rules/{ruleId}/remediation</c>).</summary>
	/// <param name="policyId">The identifier of the policy.</param>
	/// <param name="ruleId">The identifier of the rule.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The remediation steps, as an HTML fragment.</returns>
	[Get("api/3/policies/{policyId}/rules/{ruleId}/remediation")]
	[Headers("Accept: text/html")]
	Task<string> GetRemediationAsync(long policyId, long ruleId, CancellationToken cancellationToken);
}
