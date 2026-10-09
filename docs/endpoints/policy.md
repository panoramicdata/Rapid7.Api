# Policy endpoints (Security Console API v3)

Source: https://help.rapid7.com/insightvm/en-us/api/index.html

Paths are relative to the Security Console base address (`https://<host>:<port>/`). One row per operation. `Client method` is `IInterface.Method`; `Test` is `TestClass.TestMethod`. Deprecated operations are marked and not implemented.

| Method | Path | Client method | Test |
|---|---|---|---|
| GET | `api/3/assets/{assetId}/policies` |  |  |
| GET | `api/3/assets/{assetId}/policies/{policyId}/children` |  |  |
| GET | `api/3/assets/{assetId}/policies/{policyId}/groups/{groupId}/children` |  |  |
| GET | `api/3/assets/{assetId}/policies/{policyId}/groups/{groupId}/rules` |  |  |
| GET | `api/3/assets/{assetId}/policies/{policyId}/rules` |  |  |
| GET | `api/3/policies` |  |  |
| GET | `api/3/policies/{id}/children` |  |  |
| GET | `api/3/policies/{policyId}` |  |  |
| GET | `api/3/policies/{policyId}/assets` |  |  |
| GET | `api/3/policies/{policyId}/assets/{assetId}` |  |  |
| GET | `api/3/policies/{policyId}/groups` |  |  |
| GET | `api/3/policies/{policyId}/groups/{groupId}` |  |  |
| GET | `api/3/policies/{policyId}/groups/{groupId}/assets` |  |  |
| GET | `api/3/policies/{policyId}/groups/{groupId}/assets/{assetId}` |  |  |
| GET | `api/3/policies/{policyId}/groups/{groupId}/children` |  |  |
| GET | `api/3/policies/{policyId}/groups/{groupId}/rules` |  |  |
| GET | `api/3/policies/{policyId}/rules` |  |  |
| GET | `api/3/policies/{policyId}/rules/{ruleId}` |  |  |
| GET | `api/3/policies/{policyId}/rules/{ruleId}/assets` |  |  |
| GET | `api/3/policies/{policyId}/rules/{ruleId}/assets/{assetId}` |  |  |
| GET | `api/3/policies/{policyId}/rules/{ruleId}/assets/{assetId}/proof` |  |  |
| GET | `api/3/policies/{policyId}/rules/{ruleId}/controls` |  |  |
| GET | `api/3/policies/{policyId}/rules/{ruleId}/rationale` |  |  |
| GET | `api/3/policies/{policyId}/rules/{ruleId}/remediation` |  |  |
| GET | `api/3/policies/{policyId}/rules/disabled` |  |  |
| GET | `api/3/policy/summary` |  |  |
