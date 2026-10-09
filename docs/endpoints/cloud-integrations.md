# Cloud Integrations API v4 endpoints

Source: https://help.rapid7.com/insightvm/en-us/api/integrations.html

Paths are relative to the regional Insight platform base address (`https://<region>.api.insight.rapid7.com/vm/`). One row per operation.

| Method | Path | Client method | Test |
|---|---|---|---|
| GET | `admin/health` | `ICloudHealth.GetAsync` | `CloudHealthTests.GetAsync_SendsGetToAdminHealth` |
| POST | `v4/integration/assets` | `ICloudAssets.SearchAsync` | `CloudAssetsTests.SearchAsync_PostsTheFilters_WithEveryQueryOption` |
| GET | `v4/integration/assets/{id}` | `ICloudAssets.GetAsync` | `CloudAssetsTests.GetAsync_SendsGetWithTheEscapedId_AndEveryQueryOption` |
| GET | `v4/integration/scan` | `ICloudScans.ListAsync` | `CloudScansTests.ListAsync_SendsGet_WithPagingAndDetails` |
| POST | `v4/integration/scan` | `ICloudScans.StartAsync` | `CloudScansTests.StartAsync_PostsTheScanForm` |
| GET | `v4/integration/scan/{id}` | `ICloudScans.GetAsync` | `CloudScansTests.GetAsync_SendsGetWithTheDetailsFlag` |
| POST | `v4/integration/scan/{id}/stop` | `ICloudScans.StopAsync` | `CloudScansTests.StopAsync_PostsToStopWithoutABody` |
| GET | `v4/integration/scan/engine` | `ICloudScanEngines.ListAsync` | `CloudScanEnginesTests.ListAsync_SendsGetWithPaging` |
| GET | `v4/integration/scan/engine/{id}` | `ICloudScanEngines.GetAsync` | `CloudScanEnginesTests.GetAsync_SendsGetWithTheId` |
| DELETE | `v4/integration/scan/engine/{id}/configuration` | `ICloudScanEngines.RemoveConfigurationAsync` | `CloudScanEnginesTests.RemoveConfigurationAsync_SendsDeleteWithTheNames` |
| POST | `v4/integration/scan/engine/{id}/configuration` | `ICloudScanEngines.UpdateConfigurationAsync` | `CloudScanEnginesTests.UpdateConfigurationAsync_PostsTheProperties` |
| POST | `v4/integration/sites` | `ICloudSites.ListAsync` | `CloudSitesTests.ListAsync_PostsWithoutABody_AndSendsThePaging` |
| POST | `v4/integration/vulnerabilities` | `ICloudVulnerabilities.SearchAsync` | `CloudVulnerabilitiesTests.SearchAsync_PostsTheFilter_AndSendsThePaging` |
