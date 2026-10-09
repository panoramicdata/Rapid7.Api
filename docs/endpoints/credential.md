# Credential endpoints (Security Console API v3)

Source: https://help.rapid7.com/insightvm/en-us/api/index.html

Paths are relative to the Security Console base address (`https://<host>:<port>/`). One row per operation. `Client method` is `IInterface.Method`; `Test` is `TestClass.TestMethod`. Deprecated operations are marked and not implemented.

| Method | Path | Client method | Test |
|---|---|---|---|
| DELETE | `api/3/shared_credentials` | `ISharedCredentials.DeleteAllAsync` | `SharedCredentialsTests.DeleteAllAsync_SendsDeleteToSharedCredentials` |
| GET | `api/3/shared_credentials` | `ISharedCredentials.ListAsync` | `SharedCredentialsTests.ListAsync_SendsGetToSharedCredentials` |
| POST | `api/3/shared_credentials` | `ISharedCredentials.CreateAsync` | `SharedCredentialsTests.CreateAsync_PostsTheCredential` |
| DELETE | `api/3/shared_credentials/{id}` | `ISharedCredentials.DeleteAsync` | `SharedCredentialsTests.DeleteAsync_SendsDeleteToTheCredential` |
| GET | `api/3/shared_credentials/{id}` | `ISharedCredentials.GetAsync` | `SharedCredentialsTests.GetAsync_SendsGetToTheCredential` |
| PUT | `api/3/shared_credentials/{id}` | `ISharedCredentials.UpdateAsync` | `SharedCredentialsTests.UpdateAsync_PutsTheCredential` |
