# Site endpoints (Security Console API v3)

Source: https://help.rapid7.com/insightvm/en-us/api/index.html

Paths are relative to the Security Console base address (`https://<host>:<port>/`). One row per operation. `Client method` is `IInterface.Method`; `Test` is `TestClass.TestMethod`. Deprecated operations are marked and not implemented.

| Method | Path | Client method | Test |
|---|---|---|---|
| GET | `api/3/sites` | ISites.ListAsync | SitesTests.ListAsync_WithPaging_SendsPageSizeAndSorts |
| POST | `api/3/sites` | ISites.CreateAsync | SitesTests.CreateAsync_PostsTheSiteWithItsScope |
| DELETE | `api/3/sites/{id}` | ISites.DeleteAsync | SitesTests.DeleteAsync_SendsDeleteToTheSite |
| GET | `api/3/sites/{id}` | ISites.GetAsync | SitesTests.GetAsync_SendsGetToTheSite |
| PUT | `api/3/sites/{id}` | ISites.UpdateAsync | SitesTests.UpdateAsync_PutsTheSettings |
| DELETE | `api/3/sites/{id}/alerts` | ISiteAlerts.DeleteAllAsync | SiteAlertsTests.DeleteAllAsync_SendsDeleteToTheAlerts |
| GET | `api/3/sites/{id}/alerts` | ISiteAlerts.ListAsync | SiteAlertsTests.ListAsync_SendsGetToTheAlerts |
| DELETE | `api/3/sites/{id}/alerts/smtp` | ISiteAlerts.DeleteAllSmtpAsync | SiteAlertsTests.DeleteAllSmtpAsync_SendsDeleteToTheSmtpAlerts |
| GET | `api/3/sites/{id}/alerts/smtp` | ISiteAlerts.ListSmtpAsync | SiteAlertsTests.ListSmtpAsync_SendsGetToTheSmtpAlerts |
| POST | `api/3/sites/{id}/alerts/smtp` | ISiteAlerts.CreateSmtpAsync | SiteAlertsTests.CreateSmtpAsync_PostsTheAlert |
| PUT | `api/3/sites/{id}/alerts/smtp` | ISiteAlerts.ReplaceSmtpAsync | SiteAlertsTests.ReplaceSmtpAsync_PutsTheAlertArray |
| DELETE | `api/3/sites/{id}/alerts/smtp/{alertId}` | ISiteAlerts.DeleteSmtpAsync | SiteAlertsTests.DeleteSmtpAsync_SendsDeleteToTheAlert |
| GET | `api/3/sites/{id}/alerts/smtp/{alertId}` | ISiteAlerts.GetSmtpAsync | SiteAlertsTests.GetSmtpAsync_SendsGetToTheAlert |
| PUT | `api/3/sites/{id}/alerts/smtp/{alertId}` | ISiteAlerts.UpdateSmtpAsync | SiteAlertsTests.UpdateSmtpAsync_PutsTheAlert |
| DELETE | `api/3/sites/{id}/alerts/snmp` | ISiteAlerts.DeleteAllSnmpAsync | SiteAlertsTests.DeleteAllSnmpAsync_SendsDeleteToTheSnmpAlerts |
| GET | `api/3/sites/{id}/alerts/snmp` | ISiteAlerts.ListSnmpAsync | SiteAlertsTests.ListSnmpAsync_SendsGetToTheSnmpAlerts |
| POST | `api/3/sites/{id}/alerts/snmp` | ISiteAlerts.CreateSnmpAsync | SiteAlertsTests.CreateSnmpAsync_PostsTheAlert |
| PUT | `api/3/sites/{id}/alerts/snmp` | ISiteAlerts.ReplaceSnmpAsync | SiteAlertsTests.ReplaceSnmpAsync_PutsTheAlertArray |
| DELETE | `api/3/sites/{id}/alerts/snmp/{alertId}` | ISiteAlerts.DeleteSnmpAsync | SiteAlertsTests.DeleteSnmpAsync_SendsDeleteToTheAlert |
| GET | `api/3/sites/{id}/alerts/snmp/{alertId}` | ISiteAlerts.GetSnmpAsync | SiteAlertsTests.GetSnmpAsync_SendsGetToTheAlert |
| PUT | `api/3/sites/{id}/alerts/snmp/{alertId}` | ISiteAlerts.UpdateSnmpAsync | SiteAlertsTests.UpdateSnmpAsync_PutsTheAlert |
| DELETE | `api/3/sites/{id}/alerts/syslog` | ISiteAlerts.DeleteAllSyslogAsync | SiteAlertsTests.DeleteAllSyslogAsync_SendsDeleteToTheSyslogAlerts |
| GET | `api/3/sites/{id}/alerts/syslog` | ISiteAlerts.ListSyslogAsync | SiteAlertsTests.ListSyslogAsync_SendsGetToTheSyslogAlerts |
| POST | `api/3/sites/{id}/alerts/syslog` | ISiteAlerts.CreateSyslogAsync | SiteAlertsTests.CreateSyslogAsync_PostsTheAlert |
| PUT | `api/3/sites/{id}/alerts/syslog` | ISiteAlerts.ReplaceSyslogAsync | SiteAlertsTests.ReplaceSyslogAsync_PutsTheAlertArray |
| DELETE | `api/3/sites/{id}/alerts/syslog/{alertId}` | ISiteAlerts.DeleteSyslogAsync | SiteAlertsTests.DeleteSyslogAsync_SendsDeleteToTheAlert |
| GET | `api/3/sites/{id}/alerts/syslog/{alertId}` | ISiteAlerts.GetSyslogAsync | SiteAlertsTests.GetSyslogAsync_SendsGetToTheAlert |
| PUT | `api/3/sites/{id}/alerts/syslog/{alertId}` | ISiteAlerts.UpdateSyslogAsync | SiteAlertsTests.UpdateSyslogAsync_PutsTheAlert |
| DELETE | `api/3/sites/{id}/assets` |  |  |
| GET | `api/3/sites/{id}/assets` |  |  |
| DELETE | `api/3/sites/{id}/assets/{assetId}` |  |  |
| GET | `api/3/sites/{id}/discovery_connection` |  |  |
| PUT | `api/3/sites/{id}/discovery_connection` |  |  |
| GET | `api/3/sites/{id}/discovery_search_criteria` |  |  |
| PUT | `api/3/sites/{id}/discovery_search_criteria` |  |  |
| DELETE | `api/3/sites/{id}/excluded_asset_groups` |  |  |
| GET | `api/3/sites/{id}/excluded_asset_groups` |  |  |
| PUT | `api/3/sites/{id}/excluded_asset_groups` |  |  |
| DELETE | `api/3/sites/{id}/excluded_asset_groups/{assetGroupId}` |  |  |
| DELETE | `api/3/sites/{id}/excluded_targets` |  |  |
| GET | `api/3/sites/{id}/excluded_targets` |  |  |
| POST | `api/3/sites/{id}/excluded_targets` |  |  |
| PUT | `api/3/sites/{id}/excluded_targets` |  |  |
| DELETE | `api/3/sites/{id}/included_asset_groups` |  |  |
| GET | `api/3/sites/{id}/included_asset_groups` |  |  |
| PUT | `api/3/sites/{id}/included_asset_groups` |  |  |
| DELETE | `api/3/sites/{id}/included_asset_groups/{assetGroupId}` |  |  |
| DELETE | `api/3/sites/{id}/included_targets` |  |  |
| GET | `api/3/sites/{id}/included_targets` |  |  |
| POST | `api/3/sites/{id}/included_targets` |  |  |
| PUT | `api/3/sites/{id}/included_targets` |  |  |
| GET | `api/3/sites/{id}/organization` | ISiteOrganization.GetAsync | SiteOrganizationTests.GetAsync_SendsGetToTheOrganization |
| PUT | `api/3/sites/{id}/organization` | ISiteOrganization.UpdateAsync | SiteOrganizationTests.UpdateAsync_PutsTheDetails |
| GET | `api/3/sites/{id}/scan_engine` | ISiteScanEngine.GetAsync | SiteScanEngineTests.GetAsync_SendsGetToTheSiteScanEngine |
| PUT | `api/3/sites/{id}/scan_engine` | ISiteScanEngine.SetAsync | SiteScanEngineTests.SetAsync_PutsTheBareEngineId |
| DELETE | `api/3/sites/{id}/scan_schedules` | ISiteScanSchedules.DeleteAllAsync | SiteScanSchedulesTests.DeleteAllAsync_SendsDeleteToTheSchedules |
| GET | `api/3/sites/{id}/scan_schedules` | ISiteScanSchedules.ListAsync | SiteScanSchedulesTests.ListAsync_SendsGetToTheSchedules |
| POST | `api/3/sites/{id}/scan_schedules` | ISiteScanSchedules.CreateAsync | SiteScanSchedulesTests.CreateAsync_PostsTheSchedule |
| PUT | `api/3/sites/{id}/scan_schedules` | ISiteScanSchedules.ReplaceAllAsync | SiteScanSchedulesTests.ReplaceAllAsync_PutsTheScheduleArray |
| DELETE | `api/3/sites/{id}/scan_schedules/{scheduleId}` | ISiteScanSchedules.DeleteAsync | SiteScanSchedulesTests.DeleteAsync_SendsDeleteToTheSchedule |
| GET | `api/3/sites/{id}/scan_schedules/{scheduleId}` | ISiteScanSchedules.GetAsync | SiteScanSchedulesTests.GetAsync_SendsGetToTheSchedule |
| PUT | `api/3/sites/{id}/scan_schedules/{scheduleId}` | ISiteScanSchedules.UpdateAsync | SiteScanSchedulesTests.UpdateAsync_PutsTheSchedule |
| GET | `api/3/sites/{id}/scan_template` | ISiteScanTemplate.GetAsync | SiteScanTemplateTests.GetAsync_SendsGetToTheSiteScanTemplate |
| PUT | `api/3/sites/{id}/scan_template` | ISiteScanTemplate.SetAsync | SiteScanTemplateTests.SetAsync_PutsTheTemplateIdAsAJsonString |
| GET | `api/3/sites/{id}/shared_credentials` |  |  |
| PUT | `api/3/sites/{id}/shared_credentials/{credentialId}/enabled` |  |  |
| DELETE | `api/3/sites/{id}/site_credentials` |  |  |
| GET | `api/3/sites/{id}/site_credentials` |  |  |
| POST | `api/3/sites/{id}/site_credentials` |  |  |
| PUT | `api/3/sites/{id}/site_credentials` |  |  |
| DELETE | `api/3/sites/{id}/site_credentials/{credentialId}` |  |  |
| GET | `api/3/sites/{id}/site_credentials/{credentialId}` |  |  |
| PUT | `api/3/sites/{id}/site_credentials/{credentialId}` |  |  |
| PUT | `api/3/sites/{id}/site_credentials/{credentialId}/enabled` |  |  |
| GET | `api/3/sites/{id}/tags` |  |  |
| PUT | `api/3/sites/{id}/tags` |  |  |
| DELETE | `api/3/sites/{id}/tags/{tagId}` |  |  |
| PUT | `api/3/sites/{id}/tags/{tagId}` |  |  |
| GET | `api/3/sites/{id}/users` |  |  |
| POST | `api/3/sites/{id}/users` |  |  |
| PUT | `api/3/sites/{id}/users` |  |  |
| DELETE | `api/3/sites/{id}/users/{userId}` |  |  |
| GET | `api/3/sites/{id}/web_authentication/html_forms` |  |  |
| GET | `api/3/sites/{id}/web_authentication/http_headers` |  |  |
