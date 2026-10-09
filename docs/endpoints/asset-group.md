# Asset Group endpoints (Security Console API v3)

Source: https://help.rapid7.com/insightvm/en-us/api/index.html

Paths are relative to the Security Console base address (`https://<host>:<port>/`). One row per operation. `Client method` is `IInterface.Method`; `Test` is `TestClass.TestMethod`. Deprecated operations are marked and not implemented.

| Method | Path | Client method | Test |
|---|---|---|---|
| GET | `api/3/agents` | IAgents.ListAsync | AgentsTests.ListAsync_SendsGetWithPaging |
| GET | `api/3/asset_groups` | IAssetGroups.ListAsync | AssetGroupsTests.ListAsync_SendsGetWithFiltersAndPaging |
| POST | `api/3/asset_groups` | IAssetGroups.CreateAsync | AssetGroupsTests.CreateAsync_PostsTheGroup |
| DELETE | `api/3/asset_groups/{id}` | IAssetGroups.DeleteAsync | AssetGroupsTests.DeleteAsync_SendsDelete |
| GET | `api/3/asset_groups/{id}` | IAssetGroups.GetAsync | AssetGroupsTests.GetAsync_SendsGet |
| PUT | `api/3/asset_groups/{id}` | IAssetGroups.UpdateAsync | AssetGroupsTests.UpdateAsync_PutsTheGroup |
| DELETE | `api/3/asset_groups/{id}/assets` | IAssetGroupMembers.RemoveAllAssetsAsync | AssetGroupMembersTests.RemoveAllAssetsAsync_SendsDelete |
| GET | `api/3/asset_groups/{id}/assets` | IAssetGroupMembers.ListAssetsAsync | AssetGroupMembersTests.ListAssetsAsync_SendsGet |
| PUT | `api/3/asset_groups/{id}/assets` | IAssetGroupMembers.SetAssetsAsync | AssetGroupMembersTests.SetAssetsAsync_PutsTheAssetIds |
| DELETE | `api/3/asset_groups/{id}/assets/{assetId}` | IAssetGroupMembers.RemoveAssetAsync | AssetGroupMembersTests.RemoveAssetAsync_SendsDelete |
| PUT | `api/3/asset_groups/{id}/assets/{assetId}` | IAssetGroupMembers.AddAssetAsync | AssetGroupMembersTests.AddAssetAsync_SendsPutWithoutABody |
| GET | `api/3/asset_groups/{id}/search_criteria` | IAssetGroups.GetSearchCriteriaAsync | AssetGroupsTests.GetSearchCriteriaAsync_SendsGet |
| PUT | `api/3/asset_groups/{id}/search_criteria` | IAssetGroups.SetSearchCriteriaAsync | AssetGroupsTests.SetSearchCriteriaAsync_PutsTheCriteria |
| DELETE | `api/3/asset_groups/{id}/tags` | IAssetGroupTags.RemoveAllAsync | AssetGroupTagsTests.RemoveAllAsync_SendsDelete |
| GET | `api/3/asset_groups/{id}/tags` | IAssetGroupTags.ListAsync | AssetGroupTagsTests.ListAsync_SendsGet |
| PUT | `api/3/asset_groups/{id}/tags` | IAssetGroupTags.SetAsync | AssetGroupTagsTests.SetAsync_PutsTheTagIds |
| DELETE | `api/3/asset_groups/{id}/tags/{tagId}` | IAssetGroupTags.RemoveAsync | AssetGroupTagsTests.RemoveAsync_SendsDelete |
| PUT | `api/3/asset_groups/{id}/tags/{tagId}` | IAssetGroupTags.AddAsync | AssetGroupTagsTests.AddAsync_SendsPutWithoutABody |
| GET | `api/3/asset_groups/{id}/users` | IAssetGroupMembers.ListUsersAsync | AssetGroupMembersTests.ListUsersAsync_SendsGet |
| PUT | `api/3/asset_groups/{id}/users` | IAssetGroupMembers.SetUsersAsync | AssetGroupMembersTests.SetUsersAsync_PutsTheUserIds |
| DELETE | `api/3/asset_groups/{id}/users/{userId}` | IAssetGroupMembers.RemoveUserAsync | AssetGroupMembersTests.RemoveUserAsync_SendsDelete |
| PUT | `api/3/asset_groups/{id}/users/{userId}` | IAssetGroupMembers.AddUserAsync | AssetGroupMembersTests.AddUserAsync_SendsPutWithoutABody |
