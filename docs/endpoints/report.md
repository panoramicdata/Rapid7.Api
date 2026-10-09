# Report endpoints (Security Console API v3)

Source: https://help.rapid7.com/insightvm/en-us/api/index.html

Paths are relative to the Security Console base address (`https://<host>:<port>/`). One row per operation. `Client method` is `IInterface.Method`; `Test` is `TestClass.TestMethod`. Deprecated operations are marked and not implemented.

| Method | Path | Client method | Test |
|---|---|---|---|
| GET | `api/3/report_formats` |  |  |
| GET | `api/3/report_templates` |  |  |
| GET | `api/3/report_templates/{id}` |  |  |
| GET | `api/3/reports` |  |  |
| POST | `api/3/reports` |  |  |
| DELETE | `api/3/reports/{id}` |  |  |
| GET | `api/3/reports/{id}` |  |  |
| PUT | `api/3/reports/{id}` |  |  |
| POST | `api/3/reports/{id}/generate` |  |  |
| GET | `api/3/reports/{id}/history` |  |  |
| DELETE | `api/3/reports/{id}/history/{instance}` |  |  |
| GET | `api/3/reports/{id}/history/{instance}` |  |  |
| GET | `api/3/reports/{id}/history/{instance}/output` |  |  |
