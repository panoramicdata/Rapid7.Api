# Asset Discovery endpoints (Security Console API v3)

Source: https://help.rapid7.com/insightvm/en-us/api/index.html

Paths are relative to the Security Console base address (`https://<host>:<port>/`). One row per operation. `Client method` is `IInterface.Method`; `Test` is `TestClass.TestMethod`. Deprecated operations are marked and not implemented.

| Method | Path | Client method | Test |
|---|---|---|---|
| GET | `api/3/discovery_connections` |  |  |
| GET | `api/3/discovery_connections/{id}` |  |  |
| POST | `api/3/discovery_connections/{id}/connect` |  |  |
| GET | `api/3/sonar_queries` |  |  |
| POST | `api/3/sonar_queries` |  |  |
| DELETE | `api/3/sonar_queries/{id}` |  |  |
| GET | `api/3/sonar_queries/{id}` |  |  |
| PUT | `api/3/sonar_queries/{id}` |  |  |
| GET | `api/3/sonar_queries/{id}/assets` |  |  |
| POST | `api/3/sonar_queries/search` |  |  |
