# Administration endpoints (Security Console API v3)

Source: https://help.rapid7.com/insightvm/en-us/api/index.html

Paths are relative to the Security Console base address (`https://<host>:<port>/`). One row per operation. `Client method` is `IInterface.Method`; `Test` is `TestClass.TestMethod`. Deprecated operations are marked and not implemented.

| Method | Path | Client method | Test |
|---|---|---|---|
| POST | `api/3/administration/commands` | `IAdministration.ExecuteCommandAsync` | `AdministrationTests.ExecuteCommandAsync_PostsTheCommandAsPlainText` |
| GET | `api/3/administration/info` | `IAdministration.GetInfoAsync` | `AdministrationTests.GetInfoAsync_SendsGetToInfo` |
| GET | `api/3/administration/license` | `IAdministration.GetLicenseAsync` | `AdministrationTests.GetLicenseAsync_SendsGetToLicense` |
| POST | `api/3/administration/license` | `IAdministration.ActivateLicenseAsync`, `IAdministration.UploadLicenseAsync` | `AdministrationTests.ActivateLicenseAsync_PostsTheKeyInTheQueryWithoutABody`, `AdministrationTests.UploadLicenseAsync_PostsTheFileAsTheLicensePart` |
| GET | `api/3/administration/logs` | `IAdministration.GetLogsAsync` | `AdministrationTests.GetLogsAsync_AsksForAZipOfTheNamedLogs` |
| GET | `api/3/administration/properties` | `IAdministration.GetPropertiesAsync` | `AdministrationTests.GetPropertiesAsync_SendsGetToProperties` |
| GET | `api/3/administration/settings` | `IAdministration.GetSettingsAsync` | `AdministrationTests.GetSettingsAsync_SendsGetToSettings` |
