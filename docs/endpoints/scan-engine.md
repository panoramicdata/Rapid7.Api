# Scan Engine endpoints (Security Console API v3)

Source: https://help.rapid7.com/insightvm/en-us/api/index.html

Paths are relative to the Security Console base address (`https://<host>:<port>/`). One row per operation. `Client method` is `IInterface.Method`; `Test` is `TestClass.TestMethod`. Deprecated operations are marked and not implemented.

| Method | Path | Client method | Test |
|---|---|---|---|
| GET | `api/3/scan_engine_pools` | IScanEnginePools.ListAsync | ScanEnginePoolsTests.ListAsync_SendsGet |
| POST | `api/3/scan_engine_pools` | IScanEnginePools.CreateAsync | ScanEnginePoolsTests.CreateAsync_PostsThePool |
| DELETE | `api/3/scan_engine_pools/{id}` | IScanEnginePools.DeleteAsync | ScanEnginePoolsTests.DeleteAsync_SendsDelete |
| GET | `api/3/scan_engine_pools/{id}` | IScanEnginePools.GetAsync | ScanEnginePoolsTests.GetAsync_SendsGet |
| PUT | `api/3/scan_engine_pools/{id}` | IScanEnginePools.UpdateAsync | ScanEnginePoolsTests.UpdateAsync_PutsThePool_LeavingOutNulls |
| GET | `api/3/scan_engine_pools/{id}/engines` | IScanEnginePools.ListEnginesAsync | ScanEnginePoolsTests.ListEnginesAsync_SendsGet |
| PUT | `api/3/scan_engine_pools/{id}/engines` | IScanEnginePools.SetEnginesAsync | ScanEnginePoolsTests.SetEnginesAsync_PutsTheIdentifiers |
| DELETE | `api/3/scan_engine_pools/{id}/engines/{engineId}` | IScanEnginePools.RemoveEngineAsync | ScanEnginePoolsTests.RemoveEngineAsync_SendsDelete |
| PUT | `api/3/scan_engine_pools/{id}/engines/{engineId}` | IScanEnginePools.AddEngineAsync | ScanEnginePoolsTests.AddEngineAsync_SendsPut |
| GET | `api/3/scan_engine_pools/{id}/sites` | IScanEnginePools.ListSitesAsync | ScanEnginePoolsTests.ListSitesAsync_SendsGet |
| GET | `api/3/scan_engines` | IScanEngines.ListAsync | ScanEnginesTests.ListAsync_SendsGet |
| POST | `api/3/scan_engines` | IScanEngines.CreateAsync | ScanEnginesTests.CreateAsync_PostsTheEngine |
| DELETE | `api/3/scan_engines/{id}` | IScanEngines.DeleteAsync | ScanEnginesTests.DeleteAsync_SendsDelete |
| GET | `api/3/scan_engines/{id}` | IScanEngines.GetAsync | ScanEnginesTests.GetAsync_SendsGet |
| PUT | `api/3/scan_engines/{id}` | IScanEngines.UpdateAsync | ScanEnginesTests.UpdateAsync_PutsTheEngine_LeavingOutNullSites |
| GET | `api/3/scan_engines/{id}/scan_engine_pools` | IScanEngines.ListPoolsAsync | ScanEnginesTests.ListPoolsAsync_SendsGet |
| GET | `api/3/scan_engines/{id}/scans` | IScanEngines.ListScansAsync | ScanEnginesTests.ListScansAsync_SendsPaging |
| GET | `api/3/scan_engines/{id}/sites` | IScanEngines.ListSitesAsync | ScanEnginesTests.ListSitesAsync_SendsGet_WithoutPaging |
| DELETE | `api/3/scan_engines/shared_secret` | IScanEngineSharedSecret.RevokeAsync | ScanEngineSharedSecretTests.RevokeAsync_SendsDelete |
| GET | `api/3/scan_engines/shared_secret` | IScanEngineSharedSecret.GetAsync | ScanEngineSharedSecretTests.GetAsync_SendsGet_AcceptingPlainText |
| POST | `api/3/scan_engines/shared_secret` | IScanEngineSharedSecret.GetOrCreateAsync | ScanEngineSharedSecretTests.GetOrCreateAsync_SendsPost_AcceptingPlainText |
| GET | `api/3/scan_engines/shared_secret/time_to_live` | IScanEngineSharedSecret.GetTimeToLiveAsync | ScanEngineSharedSecretTests.GetTimeToLiveAsync_SendsGet |
