# Scan Engine endpoints (Security Console API v3)

Source: https://help.rapid7.com/insightvm/en-us/api/index.html

Paths are relative to the Security Console base address (`https://<host>:<port>/`). One row per operation. `Client method` is `IInterface.Method`; `Test` is `TestClass.TestMethod`. Deprecated operations are marked and not implemented.

| Method | Path | Client method | Test |
|---|---|---|---|
| GET | `api/3/scan_engine_pools` |  |  |
| POST | `api/3/scan_engine_pools` |  |  |
| DELETE | `api/3/scan_engine_pools/{id}` |  |  |
| GET | `api/3/scan_engine_pools/{id}` |  |  |
| PUT | `api/3/scan_engine_pools/{id}` |  |  |
| GET | `api/3/scan_engine_pools/{id}/engines` |  |  |
| PUT | `api/3/scan_engine_pools/{id}/engines` |  |  |
| DELETE | `api/3/scan_engine_pools/{id}/engines/{engineId}` |  |  |
| PUT | `api/3/scan_engine_pools/{id}/engines/{engineId}` |  |  |
| GET | `api/3/scan_engine_pools/{id}/sites` |  |  |
| GET | `api/3/scan_engines` |  |  |
| POST | `api/3/scan_engines` |  |  |
| DELETE | `api/3/scan_engines/{id}` |  |  |
| GET | `api/3/scan_engines/{id}` |  |  |
| PUT | `api/3/scan_engines/{id}` |  |  |
| GET | `api/3/scan_engines/{id}/scan_engine_pools` |  |  |
| GET | `api/3/scan_engines/{id}/scans` |  |  |
| GET | `api/3/scan_engines/{id}/sites` |  |  |
| DELETE | `api/3/scan_engines/shared_secret` |  |  |
| GET | `api/3/scan_engines/shared_secret` |  |  |
| POST | `api/3/scan_engines/shared_secret` |  |  |
| GET | `api/3/scan_engines/shared_secret/time_to_live` |  |  |
