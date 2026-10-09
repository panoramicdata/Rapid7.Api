# Site endpoints (Security Console API v3)

Source: https://help.rapid7.com/insightvm/en-us/api/index.html

Paths are relative to the Security Console base address (`https://<host>:<port>/`). One row per operation. `Client method` is `IInterface.Method`; `Test` is `TestClass.TestMethod`. Deprecated operations are marked and not implemented.

| Method | Path | Client method | Test |
|---|---|---|---|
| GET | `api/3/sites` |  |  |
| POST | `api/3/sites` |  |  |
| DELETE | `api/3/sites/{id}` |  |  |
| GET | `api/3/sites/{id}` |  |  |
| PUT | `api/3/sites/{id}` |  |  |
| DELETE | `api/3/sites/{id}/alerts` |  |  |
| GET | `api/3/sites/{id}/alerts` |  |  |
| DELETE | `api/3/sites/{id}/alerts/smtp` |  |  |
| GET | `api/3/sites/{id}/alerts/smtp` |  |  |
| POST | `api/3/sites/{id}/alerts/smtp` |  |  |
| PUT | `api/3/sites/{id}/alerts/smtp` |  |  |
| DELETE | `api/3/sites/{id}/alerts/smtp/{alertId}` |  |  |
| GET | `api/3/sites/{id}/alerts/smtp/{alertId}` |  |  |
| PUT | `api/3/sites/{id}/alerts/smtp/{alertId}` |  |  |
| DELETE | `api/3/sites/{id}/alerts/snmp` |  |  |
| GET | `api/3/sites/{id}/alerts/snmp` |  |  |
| POST | `api/3/sites/{id}/alerts/snmp` |  |  |
| PUT | `api/3/sites/{id}/alerts/snmp` |  |  |
| DELETE | `api/3/sites/{id}/alerts/snmp/{alertId}` |  |  |
| GET | `api/3/sites/{id}/alerts/snmp/{alertId}` |  |  |
| PUT | `api/3/sites/{id}/alerts/snmp/{alertId}` |  |  |
| DELETE | `api/3/sites/{id}/alerts/syslog` |  |  |
| GET | `api/3/sites/{id}/alerts/syslog` |  |  |
| POST | `api/3/sites/{id}/alerts/syslog` |  |  |
| PUT | `api/3/sites/{id}/alerts/syslog` |  |  |
| DELETE | `api/3/sites/{id}/alerts/syslog/{alertId}` |  |  |
| GET | `api/3/sites/{id}/alerts/syslog/{alertId}` |  |  |
| PUT | `api/3/sites/{id}/alerts/syslog/{alertId}` |  |  |
| DELETE | `api/3/sites/{id}/assets` | ISiteAssets.RemoveAllAsync | SiteAssetsTests.RemoveAllAsync_SendsDeleteToTheSiteAssets |
| GET | `api/3/sites/{id}/assets` | ISiteAssets.ListAsync | SiteAssetsTests.ListAsync_WithPaging_SendsPageSizeAndSort |
| DELETE | `api/3/sites/{id}/assets/{assetId}` | ISiteAssets.RemoveAsync | SiteAssetsTests.RemoveAsync_SendsDeleteToTheAsset |
| GET | `api/3/sites/{id}/discovery_connection` | ISiteDiscovery.GetConnectionAsync | SiteDiscoveryTests.GetConnectionAsync_SendsGet |
| PUT | `api/3/sites/{id}/discovery_connection` | ISiteDiscovery.SetConnectionAsync | SiteDiscoveryTests.SetConnectionAsync_SendsPutWithTheBareConnectionId |
| GET | `api/3/sites/{id}/discovery_search_criteria` | ISiteDiscovery.GetSearchCriteriaAsync | SiteDiscoveryTests.GetSearchCriteriaAsync_SendsGet |
| PUT | `api/3/sites/{id}/discovery_search_criteria` | ISiteDiscovery.SetSearchCriteriaAsync | SiteDiscoveryTests.SetSearchCriteriaAsync_SendsPutWithTheCriteria |
| DELETE | `api/3/sites/{id}/excluded_asset_groups` | ISiteTargets.RemoveAllExcludedAssetGroupsAsync | SiteTargetsTests.RemoveAllExcludedAssetGroupsAsync_SendsDelete |
| GET | `api/3/sites/{id}/excluded_asset_groups` | ISiteTargets.ListExcludedAssetGroupsAsync | SiteTargetsTests.ListExcludedAssetGroupsAsync_SendsGet |
| PUT | `api/3/sites/{id}/excluded_asset_groups` | ISiteTargets.ReplaceExcludedAssetGroupsAsync | SiteTargetsTests.ReplaceExcludedAssetGroupsAsync_SendsPutWithABareIdArray |
| DELETE | `api/3/sites/{id}/excluded_asset_groups/{assetGroupId}` | ISiteTargets.RemoveExcludedAssetGroupAsync | SiteTargetsTests.RemoveExcludedAssetGroupAsync_SendsDeleteToTheGroup |
| DELETE | `api/3/sites/{id}/excluded_targets` | ISiteTargets.RemoveExcludedTargetsAsync | SiteTargetsTests.RemoveExcludedTargetsAsync_SendsDeleteWithABareArray |
| GET | `api/3/sites/{id}/excluded_targets` | ISiteTargets.GetExcludedTargetsAsync | SiteTargetsTests.GetExcludedTargetsAsync_SendsGet |
| POST | `api/3/sites/{id}/excluded_targets` | ISiteTargets.AddExcludedTargetsAsync | SiteTargetsTests.AddExcludedTargetsAsync_SendsPostWithABareArray |
| PUT | `api/3/sites/{id}/excluded_targets` | ISiteTargets.ReplaceExcludedTargetsAsync | SiteTargetsTests.ReplaceExcludedTargetsAsync_SendsPutWithABareArray |
| DELETE | `api/3/sites/{id}/included_asset_groups` | ISiteTargets.RemoveAllIncludedAssetGroupsAsync | SiteTargetsTests.RemoveAllIncludedAssetGroupsAsync_SendsDelete |
| GET | `api/3/sites/{id}/included_asset_groups` | ISiteTargets.ListIncludedAssetGroupsAsync | SiteTargetsTests.ListIncludedAssetGroupsAsync_SendsGet |
| PUT | `api/3/sites/{id}/included_asset_groups` | ISiteTargets.ReplaceIncludedAssetGroupsAsync | SiteTargetsTests.ReplaceIncludedAssetGroupsAsync_SendsPutWithABareIdArray |
| DELETE | `api/3/sites/{id}/included_asset_groups/{assetGroupId}` | ISiteTargets.RemoveIncludedAssetGroupAsync | SiteTargetsTests.RemoveIncludedAssetGroupAsync_SendsDeleteToTheGroup |
| DELETE | `api/3/sites/{id}/included_targets` | ISiteTargets.RemoveIncludedTargetsAsync | SiteTargetsTests.RemoveIncludedTargetsAsync_SendsDeleteWithABareArray |
| GET | `api/3/sites/{id}/included_targets` | ISiteTargets.GetIncludedTargetsAsync | SiteTargetsTests.GetIncludedTargetsAsync_SendsGet |
| POST | `api/3/sites/{id}/included_targets` | ISiteTargets.AddIncludedTargetsAsync | SiteTargetsTests.AddIncludedTargetsAsync_SendsPostWithABareArray |
| PUT | `api/3/sites/{id}/included_targets` | ISiteTargets.ReplaceIncludedTargetsAsync | SiteTargetsTests.ReplaceIncludedTargetsAsync_SendsPutWithABareArray |
| GET | `api/3/sites/{id}/organization` |  |  |
| PUT | `api/3/sites/{id}/organization` |  |  |
| GET | `api/3/sites/{id}/scan_engine` |  |  |
| PUT | `api/3/sites/{id}/scan_engine` |  |  |
| DELETE | `api/3/sites/{id}/scan_schedules` |  |  |
| GET | `api/3/sites/{id}/scan_schedules` |  |  |
| POST | `api/3/sites/{id}/scan_schedules` |  |  |
| PUT | `api/3/sites/{id}/scan_schedules` |  |  |
| DELETE | `api/3/sites/{id}/scan_schedules/{scheduleId}` |  |  |
| GET | `api/3/sites/{id}/scan_schedules/{scheduleId}` |  |  |
| PUT | `api/3/sites/{id}/scan_schedules/{scheduleId}` |  |  |
| GET | `api/3/sites/{id}/scan_template` |  |  |
| PUT | `api/3/sites/{id}/scan_template` |  |  |
| GET | `api/3/sites/{id}/shared_credentials` | ISiteCredentials.ListSharedAsync | SiteCredentialsTests.ListSharedAsync_SendsGetToTheSharedCredentials |
| PUT | `api/3/sites/{id}/shared_credentials/{credentialId}/enabled` | ISiteCredentials.SetSharedEnabledAsync | SiteCredentialsTests.SetSharedEnabledAsync_SendsPutWithABareBoolean |
| DELETE | `api/3/sites/{id}/site_credentials` | ISiteCredentials.DeleteAllAsync | SiteCredentialsTests.DeleteAllAsync_SendsDeleteToTheSiteCredentials |
| GET | `api/3/sites/{id}/site_credentials` | ISiteCredentials.ListAsync | SiteCredentialsTests.ListAsync_SendsGetToTheSiteCredentials |
| POST | `api/3/sites/{id}/site_credentials` | ISiteCredentials.CreateAsync | SiteCredentialsTests.CreateAsync_SendsPostWithTheCredential |
| PUT | `api/3/sites/{id}/site_credentials` | ISiteCredentials.ReplaceAllAsync | SiteCredentialsTests.ReplaceAllAsync_SendsPutWithTheCredentialArray |
| DELETE | `api/3/sites/{id}/site_credentials/{credentialId}` | ISiteCredentials.DeleteAsync | SiteCredentialsTests.DeleteAsync_SendsDeleteToTheCredential |
| GET | `api/3/sites/{id}/site_credentials/{credentialId}` | ISiteCredentials.GetAsync | SiteCredentialsTests.GetAsync_SendsGetToTheCredential |
| PUT | `api/3/sites/{id}/site_credentials/{credentialId}` | ISiteCredentials.UpdateAsync | SiteCredentialsTests.UpdateAsync_SendsPutWithTheCredential |
| PUT | `api/3/sites/{id}/site_credentials/{credentialId}/enabled` | ISiteCredentials.SetEnabledAsync | SiteCredentialsTests.SetEnabledAsync_SendsPutWithABareBoolean |
| GET | `api/3/sites/{id}/tags` | ISiteTags.ListAsync | SiteTagsTests.ListAsync_SendsGetToTheSiteTags |
| PUT | `api/3/sites/{id}/tags` | ISiteTags.ReplaceAllAsync | SiteTagsTests.ReplaceAllAsync_SendsPutWithABareIdArray |
| DELETE | `api/3/sites/{id}/tags/{tagId}` | ISiteTags.RemoveAsync | SiteTagsTests.RemoveAsync_SendsDeleteToTheTag |
| PUT | `api/3/sites/{id}/tags/{tagId}` | ISiteTags.AddAsync | SiteTagsTests.AddAsync_SendsPutWithoutABody |
| GET | `api/3/sites/{id}/users` | ISiteUsers.ListAsync | SiteUsersTests.ListAsync_SendsGetToTheSiteUsers |
| POST | `api/3/sites/{id}/users` | ISiteUsers.AddAsync | SiteUsersTests.AddAsync_SendsPostWithTheBareUserId |
| PUT | `api/3/sites/{id}/users` | ISiteUsers.ReplaceAllAsync | SiteUsersTests.ReplaceAllAsync_SendsPutWithABareIdArray |
| DELETE | `api/3/sites/{id}/users/{userId}` | ISiteUsers.RemoveAsync | SiteUsersTests.RemoveAsync_SendsDeleteToTheUser |
| GET | `api/3/sites/{id}/web_authentication/html_forms` | ISiteWebAuthentication.ListHtmlFormsAsync | SiteWebAuthenticationTests.ListHtmlFormsAsync_SendsGet |
| GET | `api/3/sites/{id}/web_authentication/http_headers` | ISiteWebAuthentication.ListHttpHeadersAsync | SiteWebAuthenticationTests.ListHttpHeadersAsync_SendsGet |
