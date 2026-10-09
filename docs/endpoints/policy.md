# Policy endpoints (Security Console API v3)

Source: https://help.rapid7.com/insightvm/en-us/api/index.html

Paths are relative to the Security Console base address (`https://<host>:<port>/`). One row per operation. `Client method` is `IInterface.Method`; `Test` is `TestClass.TestMethod`. Deprecated operations are marked and not implemented.

| Method | Path | Client method | Test |
|---|---|---|---|
| GET | `api/3/assets/{assetId}/policies` | IAssetPolicies.GetPoliciesAsync | AssetPoliciesTests.GetPoliciesAsync_SendsApplicableOnlyAndPaging |
| GET | `api/3/assets/{assetId}/policies/{policyId}/children` | IAssetPolicies.GetChildrenAsync | AssetPoliciesTests.GetChildrenAsync_SendsGet |
| GET | `api/3/assets/{assetId}/policies/{policyId}/groups/{groupId}/children` | IAssetPolicies.GetGroupChildrenAsync | AssetPoliciesTests.GetGroupChildrenAsync_SendsGet |
| GET | `api/3/assets/{assetId}/policies/{policyId}/groups/{groupId}/rules` | IAssetPolicies.GetGroupRulesAsync | AssetPoliciesTests.GetGroupRulesAsync_SendsPaging |
| GET | `api/3/assets/{assetId}/policies/{policyId}/rules` | IAssetPolicies.GetRulesAsync | AssetPoliciesTests.GetRulesAsync_SendsPaging |
| GET | `api/3/policies` | IPolicies.GetPoliciesAsync | PoliciesTests.GetPoliciesAsync_SendsTheFilterAndPaging |
| GET | `api/3/policies/{id}/children` | IPolicies.GetChildrenAsync | PoliciesTests.GetChildrenAsync_SendsGet |
| GET | `api/3/policies/{policyId}` | IPolicies.GetPolicyAsync | PoliciesTests.GetPolicyAsync_SendsGet |
| GET | `api/3/policies/{policyId}/assets` | IPolicies.GetAssetResultsAsync | PoliciesTests.GetAssetResultsAsync_SendsApplicableOnlyAndPaging |
| GET | `api/3/policies/{policyId}/assets/{assetId}` | IPolicies.GetAssetResultAsync | PoliciesTests.GetAssetResultAsync_SendsGet |
| GET | `api/3/policies/{policyId}/groups` | IPolicyGroups.GetGroupsAsync | PolicyGroupsTests.GetGroupsAsync_SendsPaging |
| GET | `api/3/policies/{policyId}/groups/{groupId}` | IPolicyGroups.GetGroupAsync | PolicyGroupsTests.GetGroupAsync_SendsGet |
| GET | `api/3/policies/{policyId}/groups/{groupId}/assets` | IPolicyGroups.GetAssetResultsAsync | PolicyGroupsTests.GetAssetResultsAsync_SendsApplicableOnly |
| GET | `api/3/policies/{policyId}/groups/{groupId}/assets/{assetId}` | IPolicyGroups.GetAssetResultAsync | PolicyGroupsTests.GetAssetResultAsync_SendsGet |
| GET | `api/3/policies/{policyId}/groups/{groupId}/children` | IPolicyGroups.GetChildrenAsync | PolicyGroupsTests.GetChildrenAsync_SendsGet |
| GET | `api/3/policies/{policyId}/groups/{groupId}/rules` | IPolicyGroups.GetRulesAsync | PolicyGroupsTests.GetRulesAsync_SendsPaging |
| GET | `api/3/policies/{policyId}/rules` | IPolicyRules.GetRulesAsync | PolicyRulesTests.GetRulesAsync_SendsPaging |
| GET | `api/3/policies/{policyId}/rules/{ruleId}` | IPolicyRules.GetRuleAsync | PolicyRulesTests.GetRuleAsync_SendsGet |
| GET | `api/3/policies/{policyId}/rules/{ruleId}/assets` | IPolicyRules.GetAssetResultsAsync | PolicyRulesTests.GetAssetResultsAsync_SendsApplicableOnly |
| GET | `api/3/policies/{policyId}/rules/{ruleId}/assets/{assetId}` | IPolicyRules.GetAssetResultAsync | PolicyRulesTests.GetAssetResultAsync_SendsGet |
| GET | `api/3/policies/{policyId}/rules/{ruleId}/assets/{assetId}/proof` | IPolicyRules.GetProofAsync | PolicyRulesTests.GetProofAsync_AsksForHtml |
| GET | `api/3/policies/{policyId}/rules/{ruleId}/controls` | IPolicyRules.GetControlsAsync | PolicyRulesTests.GetControlsAsync_SendsPaging |
| GET | `api/3/policies/{policyId}/rules/{ruleId}/rationale` | IPolicyRules.GetRationaleAsync | PolicyRulesTests.GetRationaleAsync_AsksForHtml |
| GET | `api/3/policies/{policyId}/rules/{ruleId}/remediation` | IPolicyRules.GetRemediationAsync | PolicyRulesTests.GetRemediationAsync_AsksForHtml |
| GET | `api/3/policies/{policyId}/rules/disabled` | IPolicyRules.GetDisabledRulesAsync | PolicyRulesTests.GetDisabledRulesAsync_SendsPaging |
| GET | `api/3/policy/summary` | IPolicies.GetSummaryAsync | PoliciesTests.GetSummaryAsync_SendsGet |
