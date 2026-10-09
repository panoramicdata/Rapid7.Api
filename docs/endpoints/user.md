# User endpoints (Security Console API v3)

Source: https://help.rapid7.com/insightvm/en-us/api/index.html

Paths are relative to the Security Console base address (`https://<host>:<port>/`). One row per operation. `Client method` is `IInterface.Method`; `Test` is `TestClass.TestMethod`. Deprecated operations are marked and not implemented.

| Method | Path | Client method | Test |
|---|---|---|---|
| GET | `api/3/authentication_sources` |  |  |
| GET | `api/3/authentication_sources/{id}` |  |  |
| GET | `api/3/authentication_sources/{id}/users` |  |  |
| GET | `api/3/privileges` |  |  |
| GET | `api/3/privileges/{id}` |  |  |
| GET | `api/3/privileges/{id}/users` |  |  |
| GET | `api/3/roles` |  |  |
| DELETE | `api/3/roles/{id}` |  |  |
| GET | `api/3/roles/{id}` |  |  |
| PUT | `api/3/roles/{id}` |  |  |
| GET | `api/3/roles/{id}/users` |  |  |
| GET | `api/3/users` |  |  |
| POST | `api/3/users` |  |  |
| DELETE | `api/3/users/{id}` |  |  |
| GET | `api/3/users/{id}` |  |  |
| PUT | `api/3/users/{id}` |  |  |
| GET | `api/3/users/{id}/2FA` |  |  |
| POST | `api/3/users/{id}/2FA` |  |  |
| PUT | `api/3/users/{id}/2FA` |  |  |
| DELETE | `api/3/users/{id}/asset_groups` |  |  |
| GET | `api/3/users/{id}/asset_groups` |  |  |
| PUT | `api/3/users/{id}/asset_groups` |  |  |
| DELETE | `api/3/users/{id}/asset_groups/{assetGroupId}` |  |  |
| PUT | `api/3/users/{id}/asset_groups/{assetGroupId}` |  |  |
| DELETE | `api/3/users/{id}/lock` |  |  |
| PUT | `api/3/users/{id}/password` |  |  |
| GET | `api/3/users/{id}/privileges` |  |  |
| DELETE | `api/3/users/{id}/sites` |  |  |
| GET | `api/3/users/{id}/sites` |  |  |
| PUT | `api/3/users/{id}/sites` |  |  |
| DELETE | `api/3/users/{id}/sites/{siteId}` |  |  |
| PUT | `api/3/users/{id}/sites/{siteId}` |  |  |
