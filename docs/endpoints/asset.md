# Asset endpoints (Security Console API v3)

Source: https://help.rapid7.com/insightvm/en-us/api/index.html

Paths are relative to the Security Console base address (`https://<host>:<port>/`). One row per operation. `Client method` is `IInterface.Method`; `Test` is `TestClass.TestMethod`. Deprecated operations are marked and not implemented.

| Method | Path | Client method | Test |
|---|---|---|---|
| GET | `api/3/assets` |  |  |
| DELETE | `api/3/assets/{id}` |  |  |
| GET | `api/3/assets/{id}` |  |  |
| GET | `api/3/assets/{id}/databases` |  |  |
| GET | `api/3/assets/{id}/files` |  |  |
| GET | `api/3/assets/{id}/services` |  |  |
| GET | `api/3/assets/{id}/services/{protocol}/{port}` |  |  |
| GET | `api/3/assets/{id}/services/{protocol}/{port}/configurations` |  |  |
| GET | `api/3/assets/{id}/services/{protocol}/{port}/databases` |  |  |
| GET | `api/3/assets/{id}/services/{protocol}/{port}/user_groups` |  |  |
| GET | `api/3/assets/{id}/services/{protocol}/{port}/users` |  |  |
| GET | `api/3/assets/{id}/services/{protocol}/{port}/web_applications` |  |  |
| GET | `api/3/assets/{id}/services/{protocol}/{port}/web_applications/{webApplicationId}` |  |  |
| GET | `api/3/assets/{id}/software` |  |  |
| GET | `api/3/assets/{id}/tags` |  |  |
| DELETE | `api/3/assets/{id}/tags/{tagId}` |  |  |
| PUT | `api/3/assets/{id}/tags/{tagId}` |  |  |
| GET | `api/3/assets/{id}/user_groups` |  |  |
| GET | `api/3/assets/{id}/users` |  |  |
| POST | `api/3/assets/search` |  |  |
| GET | `api/3/operating_systems` |  |  |
| GET | `api/3/operating_systems/{id}` |  |  |
| POST | `api/3/sites/{id}/assets` |  |  |
| GET | `api/3/software` |  |  |
| GET | `api/3/software/{id}` |  |  |
