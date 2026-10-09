# Cloud Integrations API v4 endpoints

Source: https://help.rapid7.com/insightvm/en-us/api/integrations.html

Paths are relative to the regional Insight platform base address (`https://<region>.api.insight.rapid7.com/vm/`). One row per operation.

| Method | Path | Client method | Test |
|---|---|---|---|
| GET | `admin/health` |  |  |
| POST | `v4/integration/assets` |  |  |
| GET | `v4/integration/assets/{id}` |  |  |
| GET | `v4/integration/scan` |  |  |
| POST | `v4/integration/scan` |  |  |
| GET | `v4/integration/scan/{id}` |  |  |
| POST | `v4/integration/scan/{id}/stop` |  |  |
| GET | `v4/integration/scan/engine` |  |  |
| GET | `v4/integration/scan/engine/{id}` |  |  |
| DELETE | `v4/integration/scan/engine/{id}/configuration` |  |  |
| POST | `v4/integration/scan/engine/{id}/configuration` |  |  |
| POST | `v4/integration/sites` |  |  |
| POST | `v4/integration/vulnerabilities` |  |  |
