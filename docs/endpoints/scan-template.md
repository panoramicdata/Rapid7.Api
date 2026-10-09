# Scan Template endpoints (Security Console API v3)

Source: https://help.rapid7.com/insightvm/en-us/api/index.html

Paths are relative to the Security Console base address (`https://<host>:<port>/`). One row per operation. `Client method` is `IInterface.Method`; `Test` is `TestClass.TestMethod`. Deprecated operations are marked and not implemented.

| Method | Path | Client method | Test |
|---|---|---|---|
| GET | `api/3/scan_templates` | IScanTemplates.ListAsync | ScanTemplatesTests.ListAsync_SendsGet |
| POST | `api/3/scan_templates` | IScanTemplates.CreateAsync | ScanTemplatesTests.CreateAsync_PostsOnlyWhatWasSet |
| DELETE | `api/3/scan_templates/{id}` | IScanTemplates.DeleteAsync | ScanTemplatesTests.DeleteAsync_SendsDelete |
| GET | `api/3/scan_templates/{id}` | IScanTemplates.GetAsync | ScanTemplatesTests.GetAsync_SendsGet |
| PUT | `api/3/scan_templates/{id}` | IScanTemplates.UpdateAsync | ScanTemplatesTests.UpdateAsync_PutsTheTemplate |
