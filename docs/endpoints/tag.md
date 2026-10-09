# Tag endpoints (Security Console API v3)

Source: https://help.rapid7.com/insightvm/en-us/api/index.html

Paths are relative to the Security Console base address (`https://<host>:<port>/`). One row per operation. `Client method` is `IInterface.Method`; `Test` is `TestClass.TestMethod`. Deprecated operations are marked and not implemented.

| Method | Path | Client method | Test |
|---|---|---|---|
| GET | `api/3/tags` | ITags.ListAsync | TagsTests.ListAsync_SendsFiltersAndPaging |
| POST | `api/3/tags` | ITags.CreateAsync | TagsTests.CreateAsync_PostsTheTag |
| DELETE | `api/3/tags/{id}` | ITags.DeleteAsync | TagsTests.DeleteAsync_SendsDelete |
| GET | `api/3/tags/{id}` | ITags.GetAsync | TagsTests.GetAsync_SendsGet |
| PUT | `api/3/tags/{id}` | ITags.UpdateAsync | TagsTests.UpdateAsync_PutsTheTag_LeavingOutNulls |
| DELETE | `api/3/tags/{id}/asset_groups` | ITagMembers.RemoveAllAssetGroupsAsync | TagMembersTests.RemoveAllAssetGroupsAsync_SendsDelete |
| GET | `api/3/tags/{id}/asset_groups` | ITagMembers.ListAssetGroupsAsync | TagMembersTests.ListAssetGroupsAsync_SendsGet |
| PUT | `api/3/tags/{id}/asset_groups` | ITagMembers.SetAssetGroupsAsync | TagMembersTests.SetAssetGroupsAsync_PutsTheIdentifiers |
| DELETE | `api/3/tags/{id}/asset_groups/{assetGroupId}` | ITagMembers.RemoveAssetGroupAsync | TagMembersTests.RemoveAssetGroupAsync_SendsDelete |
| PUT | `api/3/tags/{id}/asset_groups/{assetGroupId}` | ITagMembers.AddAssetGroupAsync | TagMembersTests.AddAssetGroupAsync_SendsPut |
| GET | `api/3/tags/{id}/assets` | ITagMembers.ListAssetsAsync | TagMembersTests.ListAssetsAsync_SendsGet |
| DELETE | `api/3/tags/{id}/assets/{assetId}` | ITagMembers.RemoveAssetAsync | TagMembersTests.RemoveAssetAsync_SendsDelete |
| PUT | `api/3/tags/{id}/assets/{assetId}` | ITagMembers.AddAssetAsync | TagMembersTests.AddAssetAsync_SendsPut |
| DELETE | `api/3/tags/{id}/search_criteria` | ITags.DeleteSearchCriteriaAsync | TagsTests.DeleteSearchCriteriaAsync_SendsDelete |
| GET | `api/3/tags/{id}/search_criteria` | ITags.GetSearchCriteriaAsync | TagsTests.GetSearchCriteriaAsync_SendsGet |
| PUT | `api/3/tags/{id}/search_criteria` | ITags.UpdateSearchCriteriaAsync | TagsTests.UpdateSearchCriteriaAsync_PutsTheCriteria |
| DELETE | `api/3/tags/{id}/sites` | ITagMembers.RemoveAllSitesAsync | TagMembersTests.RemoveAllSitesAsync_SendsDelete |
| GET | `api/3/tags/{id}/sites` | ITagMembers.ListSitesAsync | TagMembersTests.ListSitesAsync_SendsGet |
| PUT | `api/3/tags/{id}/sites` | ITagMembers.SetSitesAsync | TagMembersTests.SetSitesAsync_PutsTheIdentifiers |
| DELETE | `api/3/tags/{id}/sites/{siteId}` | ITagMembers.RemoveSiteAsync | TagMembersTests.RemoveSiteAsync_SendsDelete |
| PUT | `api/3/tags/{id}/sites/{siteId}` | ITagMembers.AddSiteAsync | TagMembersTests.AddSiteAsync_SendsPut |
