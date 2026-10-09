using Rapid7.Api.Interfaces;

namespace Rapid7.Api;

public sealed partial class Rapid7Client
{
	/// <summary>Compliance policies and their results by asset (<c>api/3/policies</c>, <c>api/3/policy/summary</c>).</summary>
	public IPolicies Policies => field ??= For<IPolicies>();

	/// <summary>The rules of a compliance policy (<c>api/3/policies/{policyId}/rules</c>).</summary>
	public IPolicyRules PolicyRules => field ??= For<IPolicyRules>();

	/// <summary>The groups of a compliance policy (<c>api/3/policies/{policyId}/groups</c>).</summary>
	public IPolicyGroups PolicyGroups => field ??= For<IPolicyGroups>();

	/// <summary>One asset's compliance with policies (<c>api/3/assets/{assetId}/policies</c>).</summary>
	public IAssetPolicies AssetPolicies => field ??= For<IAssetPolicies>();
}
