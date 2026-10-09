using Rapid7.Api.Models;
using Rapid7.Api.Models.Policies;

namespace Rapid7.Api.IntegrationTest.Policies;

/// <summary>
/// Reads policy compliance results from a live console. Everything here is read-only. Tests that need a policy, rule,
/// group or asset use the first one the console lists, and stop early when it lists none.
/// </summary>
[Collection(Rapid7TestGroup.Name)]
public class PolicyIntegrationTests(Rapid7Fixture fixture)
{
	private static readonly PageOptions FirstOnly = new() { Size = 1 };

	private static CancellationToken Ct => TestContext.Current.CancellationToken;

	private Rapid7Client Client => fixture.Client;

	[Fact]
	public async Task GetSummaryAsync_ReadsTheTotals()
	{
		var summary = await Client.Policies.GetSummaryAsync(Ct);

		summary.NumberOfPolicies.Should().NotBeNull();
		summary.ScannedPolicies.Should().BeLessThanOrEqualTo(summary.NumberOfPolicies.Value);
	}

	[Fact]
	public async Task Policy_ReadsThePolicyItsChildrenRulesAndGroups()
	{
		var policyId = await FirstPolicyIdAsync(scannedOnly: false);
		if (policyId is not long id)
		{
			return;
		}

		var policy = await Client.Policies.GetPolicyAsync(id, Ct);
		var children = await Client.Policies.GetChildrenAsync(id, Ct);
		var rules = await Client.PolicyRules.GetRulesAsync(id, FirstOnly, Ct);
		var disabled = await Client.PolicyRules.GetDisabledRulesAsync(id, FirstOnly, Ct);
		var groups = await Client.PolicyGroups.GetGroupsAsync(id, FirstOnly, Ct);

		policy.SurrogateId.Should().Be(id);
		children.Resources.Should().OnlyContain(c => c.Type == PolicyItemType.Rule || c.Type == PolicyItemType.Group);
		rules.Resources.Should().OnlyContain(r => r.SurrogateId != null);
		disabled.Resources.Should().OnlyContain(r => r.SurrogateId != null);
		groups.Resources.Should().OnlyContain(g => g.SurrogateId != null);
	}

	[Fact]
	public async Task Rule_ReadsTheRuleItsControlsRationaleAndRemediation()
	{
		var ids = await FirstRuleAsync();
		if (ids is not var (policyId, ruleId))
		{
			return;
		}

		var rule = await Client.PolicyRules.GetRuleAsync(policyId, ruleId, Ct);
		var controls = await Client.PolicyRules.GetControlsAsync(policyId, ruleId, FirstOnly, Ct);
		var rationale = await Client.PolicyRules.GetRationaleAsync(policyId, ruleId, Ct);
		var remediation = await Client.PolicyRules.GetRemediationAsync(policyId, ruleId, Ct);

		rule.SurrogateId.Should().Be(ruleId);
		controls.Resources.Should().NotBeNull();
		rationale.Should().NotBeNull();
		remediation.Should().NotBeNull();
	}

	[Fact]
	public async Task Group_ReadsTheGroupItsChildrenAndRules()
	{
		var policyId = await FirstPolicyIdAsync(scannedOnly: false);
		if (policyId is not long id)
		{
			return;
		}

		var groups = await Client.PolicyGroups.GetGroupsAsync(id, FirstOnly, Ct);
		if (First(groups.Resources)?.SurrogateId is not long groupId)
		{
			return;
		}

		var group = await Client.PolicyGroups.GetGroupAsync(id, groupId, Ct);
		var children = await Client.PolicyGroups.GetChildrenAsync(id, groupId, Ct);
		var rules = await Client.PolicyGroups.GetRulesAsync(id, groupId, FirstOnly, Ct);

		group.SurrogateId.Should().Be(groupId);
		children.Resources.Should().NotBeNull();
		rules.Resources.Should().NotBeNull();
	}

	[Fact]
	public async Task AssetResults_ReadEachLevelForAScannedAsset()
	{
		var policyId = await FirstPolicyIdAsync(scannedOnly: true);
		if (policyId is not long id)
		{
			return;
		}

		var assets = await Client.Policies.GetAssetResultsAsync(id, new PolicyResultListOptions { ApplicableOnly = true, Size = 1 }, Ct);
		if (First(assets.Resources)?.Id is not long assetId)
		{
			return;
		}

		var result = await Client.Policies.GetAssetResultAsync(id, assetId, Ct);
		var assetPolicies = await Client.AssetPolicies.GetPoliciesAsync(assetId, null, Ct);
		var children = await Client.AssetPolicies.GetChildrenAsync(assetId, id, Ct);
		var rules = await Client.AssetPolicies.GetRulesAsync(assetId, id, FirstOnly, Ct);

		result.Id.Should().Be(assetId);
		assetPolicies.Resources.Should().Contain(p => p.SurrogateId == id);
		children.Resources.Should().NotBeNull();
		rules.Resources.Should().NotBeNull();

		await ReadRuleResultsAsync(id, assetId, First(rules.Resources)?.SurrogateId);
		await ReadGroupResultsAsync(id, assetId);
	}

	private async Task ReadRuleResultsAsync(long policyId, long assetId, long? ruleId)
	{
		if (ruleId is not long id)
		{
			return;
		}

		var results = await Client.PolicyRules.GetAssetResultsAsync(policyId, id, new PolicyResultListOptions { Size = 1 }, Ct);
		var result = await Client.PolicyRules.GetAssetResultAsync(policyId, id, assetId, Ct);
		var proof = await Client.PolicyRules.GetProofAsync(policyId, id, assetId, Ct);

		results.Resources.Should().NotBeNull();
		result.Id.Should().Be(assetId);
		proof.Should().NotBeNull();
	}

	private async Task ReadGroupResultsAsync(long policyId, long assetId)
	{
		var groups = await Client.PolicyGroups.GetGroupsAsync(policyId, FirstOnly, Ct);
		if (First(groups.Resources)?.SurrogateId is not long groupId)
		{
			return;
		}

		var results = await Client.PolicyGroups.GetAssetResultsAsync(policyId, groupId, new PolicyResultListOptions { Size = 1 }, Ct);
		var result = await Client.PolicyGroups.GetAssetResultAsync(policyId, groupId, assetId, Ct);
		var children = await Client.AssetPolicies.GetGroupChildrenAsync(assetId, policyId, groupId, Ct);
		var rules = await Client.AssetPolicies.GetGroupRulesAsync(assetId, policyId, groupId, FirstOnly, Ct);

		results.Resources.Should().NotBeNull();
		result.Id.Should().Be(assetId);
		children.Resources.Should().NotBeNull();
		rules.Resources.Should().NotBeNull();
	}

	private static T? First<T>(IReadOnlyList<T> items) where T : class => items.Count > 0 ? items[0] : null;

	private async Task<long?> FirstPolicyIdAsync(bool scannedOnly)
	{
		var page = await Client.Policies.GetPoliciesAsync(new PolicyListOptions { ScannedOnly = scannedOnly, Size = 1 }, Ct);
		return First(page.Resources)?.SurrogateId;
	}

	private async Task<(long PolicyId, long RuleId)?> FirstRuleAsync()
	{
		if (await FirstPolicyIdAsync(scannedOnly: false) is not long policyId)
		{
			return null;
		}

		var rules = await Client.PolicyRules.GetRulesAsync(policyId, FirstOnly, Ct);
		return First(rules.Resources)?.SurrogateId is long ruleId ? (policyId, ruleId) : null;
	}
}
