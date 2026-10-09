# Remediation endpoints (Security Console API v3)

Source: https://help.rapid7.com/insightvm/en-us/api/index.html

Paths are relative to the Security Console base address (`https://<host>:<port>/`). One row per operation. `Client method` is `IInterface.Method`; `Test` is `TestClass.TestMethod`. Deprecated operations are marked and not implemented.

| Method | Path | Client method | Test |
|---|---|---|---|
| GET | `api/3/assets/{id}/vulnerabilities/{vulnerabilityId}/solution` | IRemediations.ListSolutionsAsync | RemediationsTests.ListSolutionsAsync_SendsGetToTheAssetVulnerabilitySolution |
