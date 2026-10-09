# Asset endpoints (Security Console API v3)

Source: https://help.rapid7.com/insightvm/en-us/api/index.html

Paths are relative to the Security Console base address (`https://<host>:<port>/`). One row per operation. `Client method` is `IInterface.Method`; `Test` is `TestClass.TestMethod`. Deprecated operations are marked and not implemented.

| Method | Path | Client method | Test |
|---|---|---|---|
| GET | `api/3/assets` | IAssets.ListAsync | AssetsTests.ListAsync_SendsGetWithPaging |
| DELETE | `api/3/assets/{id}` | IAssets.DeleteAsync | AssetsTests.DeleteAsync_SendsDelete |
| GET | `api/3/assets/{id}` | IAssets.GetAsync | AssetsTests.GetAsync_SendsGet |
| GET | `api/3/assets/{id}/databases` | IAssetDetails.ListDatabasesAsync | AssetDetailsTests.ListDatabasesAsync_SendsGet |
| GET | `api/3/assets/{id}/files` | IAssetDetails.ListFilesAsync | AssetDetailsTests.ListFilesAsync_SendsGet |
| GET | `api/3/assets/{id}/services` | IAssetServices.ListAsync | AssetServicesTests.ListAsync_SendsGet |
| GET | `api/3/assets/{id}/services/{protocol}/{port}` | IAssetServices.GetAsync | AssetServicesTests.GetAsync_SendsGetWithTheNic |
| GET | `api/3/assets/{id}/services/{protocol}/{port}/configurations` | IAssetServices.ListConfigurationsAsync | AssetServicesTests.ListConfigurationsAsync_SendsGet |
| GET | `api/3/assets/{id}/services/{protocol}/{port}/databases` | IAssetServices.ListDatabasesAsync | AssetServicesTests.ListDatabasesAsync_SendsGet |
| GET | `api/3/assets/{id}/services/{protocol}/{port}/user_groups` | IAssetServices.ListUserGroupsAsync | AssetServicesTests.ListUserGroupsAsync_SendsGet |
| GET | `api/3/assets/{id}/services/{protocol}/{port}/users` | IAssetServices.ListUsersAsync | AssetServicesTests.ListUsersAsync_SendsGet |
| GET | `api/3/assets/{id}/services/{protocol}/{port}/web_applications` | IAssetServices.ListWebApplicationsAsync | AssetServicesTests.ListWebApplicationsAsync_SendsGet |
| GET | `api/3/assets/{id}/services/{protocol}/{port}/web_applications/{webApplicationId}` | IAssetServices.GetWebApplicationAsync | AssetServicesTests.GetWebApplicationAsync_SendsGet |
| GET | `api/3/assets/{id}/software` | IAssetDetails.ListSoftwareAsync | AssetDetailsTests.ListSoftwareAsync_SendsGet |
| GET | `api/3/assets/{id}/tags` | IAssetDetails.ListTagsAsync | AssetDetailsTests.ListTagsAsync_SendsGet |
| DELETE | `api/3/assets/{id}/tags/{tagId}` | IAssetDetails.RemoveTagAsync | AssetDetailsTests.RemoveTagAsync_SendsDelete |
| PUT | `api/3/assets/{id}/tags/{tagId}` | IAssetDetails.AddTagAsync | AssetDetailsTests.AddTagAsync_SendsPutWithoutABody |
| GET | `api/3/assets/{id}/user_groups` | IAssetDetails.ListUserGroupsAsync | AssetDetailsTests.ListUserGroupsAsync_SendsGet |
| GET | `api/3/assets/{id}/users` | IAssetDetails.ListUsersAsync | AssetDetailsTests.ListUsersAsync_SendsGet |
| POST | `api/3/assets/search` | IAssets.SearchAsync | AssetsTests.SearchAsync_PostsTheCriteriaWithPaging |
| GET | `api/3/operating_systems` | IAssetCatalog.ListOperatingSystemsAsync | AssetCatalogTests.ListOperatingSystemsAsync_SendsGetWithPaging |
| GET | `api/3/operating_systems/{id}` | IAssetCatalog.GetOperatingSystemAsync | AssetCatalogTests.GetOperatingSystemAsync_SendsGet |
| POST | `api/3/sites/{id}/assets` | IAssets.CreateAsync | AssetsTests.CreateAsync_PostsTheAssetToTheSite |
| GET | `api/3/software` | IAssetCatalog.ListSoftwareAsync | AssetCatalogTests.ListSoftwareAsync_SendsGetWithPaging |
| GET | `api/3/software/{id}` | IAssetCatalog.GetSoftwareAsync | AssetCatalogTests.GetSoftwareAsync_SendsGet |
