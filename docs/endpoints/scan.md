# Scan endpoints (Security Console API v3)

Source: https://help.rapid7.com/insightvm/en-us/api/index.html

Paths are relative to the Security Console base address (`https://<host>:<port>/`). One row per operation. `Client method` is `IInterface.Method`; `Test` is `TestClass.TestMethod`. Deprecated operations are marked and not implemented.

| Method | Path | Client method | Test |
|---|---|---|---|
| GET | `api/3/scans` | IScans.ListAsync | ScansTests.ListAsync_SendsActiveAndPaging |
| GET | `api/3/scans/{id}` | IScans.GetAsync | ScansTests.GetAsync_SendsGet |
| POST | `api/3/scans/{id}/{status}` | IScans.SetStatusAsync | ScansTests.SetStatusAsync_PostsTheChangeInThePath |
| GET | `api/3/sites/{id}/scans` | IScans.ListForSiteAsync | ScansTests.ListForSiteAsync_SendsActiveFalse |
| POST | `api/3/sites/{id}/scans` | IScans.StartForSiteAsync | ScansTests.StartForSiteAsync_PostsTheOverrides_AndTheBlackoutFlag |
