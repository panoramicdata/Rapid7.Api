# Report endpoints (Security Console API v3)

Source: https://help.rapid7.com/insightvm/en-us/api/index.html

Paths are relative to the Security Console base address (`https://<host>:<port>/`). One row per operation. `Client method` is `IInterface.Method`; `Test` is `TestClass.TestMethod`. Deprecated operations are marked and not implemented.

| Method | Path | Client method | Test |
|---|---|---|---|
| GET | `api/3/report_formats` | IReportTemplates.GetFormatsAsync | ReportTemplatesTests.GetFormatsAsync_SendsGet |
| GET | `api/3/report_templates` | IReportTemplates.GetTemplatesAsync | ReportTemplatesTests.GetTemplatesAsync_SendsGet |
| GET | `api/3/report_templates/{id}` | IReportTemplates.GetTemplateAsync | ReportTemplatesTests.GetTemplateAsync_SendsGet_WithTheIdentifierAsOneSegment |
| GET | `api/3/reports` | IReports.GetReportsAsync | ReportsTests.GetReportsAsync_SendsPaging |
| POST | `api/3/reports` | IReports.CreateAsync | ReportsTests.CreateAsync_PostsTheConfiguration |
| DELETE | `api/3/reports/{id}` | IReports.DeleteAsync | ReportsTests.DeleteAsync_SendsDelete |
| GET | `api/3/reports/{id}` | IReports.GetAsync | ReportsTests.GetAsync_SendsGet |
| PUT | `api/3/reports/{id}` | IReports.UpdateAsync | ReportsTests.UpdateAsync_PutsTheConfiguration |
| POST | `api/3/reports/{id}/generate` | IReports.GenerateAsync | ReportsTests.GenerateAsync_PostsWithoutABody |
| GET | `api/3/reports/{id}/history` | IReportInstances.GetInstancesAsync | ReportInstancesTests.GetInstancesAsync_SendsGet |
| DELETE | `api/3/reports/{id}/history/{instance}` | IReportInstances.DeleteInstanceAsync | ReportInstancesTests.DeleteInstanceAsync_SendsDelete |
| GET | `api/3/reports/{id}/history/{instance}` | IReportInstances.GetInstanceAsync | ReportInstancesTests.GetInstanceAsync_SendsGet |
| GET | `api/3/reports/{id}/history/{instance}/output` | IReportInstances.DownloadAsync | ReportInstancesTests.DownloadAsync_SendsGet_AcceptingAnyMediaType |
