# Policy Override endpoints (Security Console API v3)

Source: https://help.rapid7.com/insightvm/en-us/api/index.html

Paths are relative to the Security Console base address (`https://<host>:<port>/`). One row per operation. `Client method` is `IInterface.Method`; `Test` is `TestClass.TestMethod`. Deprecated operations are marked and not implemented.

| Method | Path | Client method | Test |
|---|---|---|---|
| GET | `api/3/assets/{id}/policy_overrides` | IPolicyOverrides.GetForAssetAsync | PolicyOverridesTests.GetForAssetAsync_SendsGet |
| GET | `api/3/policy_overrides` | IPolicyOverrides.GetPolicyOverridesAsync | PolicyOverridesTests.GetPolicyOverridesAsync_SendsPaging |
| POST | `api/3/policy_overrides` | IPolicyOverrides.CreateAsync | PolicyOverridesTests.CreateAsync_PostsTheOverride |
| DELETE | `api/3/policy_overrides/{id}` | IPolicyOverrides.DeleteAsync | PolicyOverridesTests.DeleteAsync_SendsDelete |
| GET | `api/3/policy_overrides/{id}` | IPolicyOverrides.GetAsync | PolicyOverridesTests.GetAsync_SendsGet |
| POST | `api/3/policy_overrides/{id}/{status}` | IPolicyOverrides.SetStatusAsync | PolicyOverridesTests.SetStatusAsync_PostsTheCommentToTheStatusPath, PolicyOverridesTests.SetStatusAsync_WithoutAComment_SendsNoBody |
| GET | `api/3/policy_overrides/{id}/expires` | IPolicyOverrides.GetExpirationAsync | PolicyOverridesTests.GetExpirationAsync_SendsGet |
| PUT | `api/3/policy_overrides/{id}/expires` | IPolicyOverrides.SetExpirationAsync | PolicyOverridesTests.SetExpirationAsync_PutsTheDateAsAJsonString |
