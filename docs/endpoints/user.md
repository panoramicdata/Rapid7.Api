# User endpoints (Security Console API v3)

Source: https://help.rapid7.com/insightvm/en-us/api/index.html

Paths are relative to the Security Console base address (`https://<host>:<port>/`). One row per operation. `Client method` is `IInterface.Method`; `Test` is `TestClass.TestMethod`. Deprecated operations are marked and not implemented.

| Method | Path | Client method | Test |
|---|---|---|---|
| GET | `api/3/authentication_sources` | `IAuthenticationSources.ListAsync` | `AuthenticationSourcesTests.ListAsync_SendsGetToAuthenticationSources` |
| GET | `api/3/authentication_sources/{id}` | `IAuthenticationSources.GetAsync` | `AuthenticationSourcesTests.GetAsync_SendsGetToTheSource` |
| GET | `api/3/authentication_sources/{id}/users` | `IAuthenticationSources.ListUsersAsync` | `AuthenticationSourcesTests.ListUsersAsync_SendsGetToTheSourcesUsers` |
| GET | `api/3/privileges` | `IPrivileges.ListAsync` | `PrivilegesTests.ListAsync_SendsGetToPrivileges` |
| GET | `api/3/privileges/{id}` | `IPrivileges.GetAsync` | `PrivilegesTests.GetAsync_SendsGetToThePrivilege` |
| GET | `api/3/privileges/{id}/users` | `IPrivileges.ListUsersAsync` | `PrivilegesTests.ListUsersAsync_SendsGetToThePrivilegesUsers` |
| GET | `api/3/roles` | `IRoles.ListAsync` | `RolesTests.ListAsync_SendsGetToRoles` |
| DELETE | `api/3/roles/{id}` | `IRoles.DeleteAsync` | `RolesTests.DeleteAsync_SendsDeleteToTheRole` |
| GET | `api/3/roles/{id}` | `IRoles.GetAsync` | `RolesTests.GetAsync_SendsGetToTheEscapedRole` |
| PUT | `api/3/roles/{id}` | `IRoles.UpdateAsync` | `RolesTests.UpdateAsync_PutsTheRole` |
| GET | `api/3/roles/{id}/users` | `IRoles.ListUsersAsync` | `RolesTests.ListUsersAsync_SendsGetToTheRolesUsers` |
| GET | `api/3/users` | `IUsers.ListAsync` | `UsersTests.ListAsync_SendsGetWithPaging` |
| POST | `api/3/users` | `IUsers.CreateAsync` | `UsersTests.CreateAsync_PostsTheAccount` |
| DELETE | `api/3/users/{id}` | `IUsers.DeleteAsync` | `UsersTests.DeleteAsync_SendsDeleteToTheUser` |
| GET | `api/3/users/{id}` | `IUsers.GetAsync` | `UsersTests.GetAsync_SendsGetToTheUser` |
| PUT | `api/3/users/{id}` | `IUsers.UpdateAsync` | `UsersTests.UpdateAsync_PutsOnlyTheSetFields` |
| GET | `api/3/users/{id}/2FA` | `IUserTwoFactor.GetKeyAsync` | `UserTwoFactorTests.GetKeyAsync_SendsGetTo2FA` |
| POST | `api/3/users/{id}/2FA` | `IUserTwoFactor.RegenerateKeyAsync` | `UserTwoFactorTests.RegenerateKeyAsync_PostsTo2FAWithoutABody` |
| PUT | `api/3/users/{id}/2FA` | `IUserTwoFactor.SetKeyAsync` | `UserTwoFactorTests.SetKeyAsync_PutsTheKeyAsAJsonString` |
| DELETE | `api/3/users/{id}/asset_groups` | `IUserAccess.RevokeAllAssetGroupsAsync` | `UserAccessTests.RevokeAllAssetGroupsAsync_DeletesTheUsersAssetGroups` |
| GET | `api/3/users/{id}/asset_groups` | `IUserAccess.ListAssetGroupsAsync` | `UserAccessTests.ListAssetGroupsAsync_SendsGetToTheUsersAssetGroups` |
| PUT | `api/3/users/{id}/asset_groups` | `IUserAccess.SetAssetGroupsAsync` | `UserAccessTests.SetAssetGroupsAsync_PutsTheIdsAsAnArray` |
| DELETE | `api/3/users/{id}/asset_groups/{assetGroupId}` | `IUserAccess.RevokeAssetGroupAsync` | `UserAccessTests.RevokeAssetGroupAsync_DeletesTheAssetGroup` |
| PUT | `api/3/users/{id}/asset_groups/{assetGroupId}` | `IUserAccess.GrantAssetGroupAsync` | `UserAccessTests.GrantAssetGroupAsync_PutsTheAssetGroup` |
| DELETE | `api/3/users/{id}/lock` | `IUsers.UnlockAsync` | `UsersTests.UnlockAsync_DeletesTheLock` |
| PUT | `api/3/users/{id}/password` | `IUsers.ResetPasswordAsync` | `UsersTests.ResetPasswordAsync_PutsThePassword` |
| GET | `api/3/users/{id}/privileges` | `IUsers.ListPrivilegesAsync` | `UsersTests.ListPrivilegesAsync_SendsGetToTheUsersPrivileges` |
| DELETE | `api/3/users/{id}/sites` | `IUserAccess.RevokeAllSitesAsync` | `UserAccessTests.RevokeAllSitesAsync_DeletesTheUsersSites` |
| GET | `api/3/users/{id}/sites` | `IUserAccess.ListSitesAsync` | `UserAccessTests.ListSitesAsync_SendsGetToTheUsersSites` |
| PUT | `api/3/users/{id}/sites` | `IUserAccess.SetSitesAsync` | `UserAccessTests.SetSitesAsync_PutsTheIdsAsAnArray` |
| DELETE | `api/3/users/{id}/sites/{siteId}` | `IUserAccess.RevokeSiteAsync` | `UserAccessTests.RevokeSiteAsync_DeletesTheSite` |
| PUT | `api/3/users/{id}/sites/{siteId}` | `IUserAccess.GrantSiteAsync` | `UserAccessTests.GrantSiteAsync_PutsTheSite` |
