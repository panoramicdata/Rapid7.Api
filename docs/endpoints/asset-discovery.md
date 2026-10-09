# Asset Discovery endpoints (Security Console API v3)

Source: https://help.rapid7.com/insightvm/en-us/api/index.html

Paths are relative to the Security Console base address (`https://<host>:<port>/`). One row per operation. `Client method` is `IInterface.Method`; `Test` is `TestClass.TestMethod`. Deprecated operations are marked and not implemented.

| Method | Path | Client method | Test |
|---|---|---|---|
| GET | `api/3/discovery_connections` | IDiscoveryConnections.ListAsync | DiscoveryConnectionsTests.ListAsync_SendsGetWithPaging |
| GET | `api/3/discovery_connections/{id}` | IDiscoveryConnections.GetAsync | DiscoveryConnectionsTests.GetAsync_SendsGet |
| POST | `api/3/discovery_connections/{id}/connect` | IDiscoveryConnections.ReconnectAsync | DiscoveryConnectionsTests.ReconnectAsync_SendsPostWithoutABody |
| GET | `api/3/sonar_queries` | ISonarQueries.ListAsync | SonarQueriesTests.ListAsync_SendsGet |
| POST | `api/3/sonar_queries` | ISonarQueries.CreateAsync | SonarQueriesTests.CreateAsync_PostsTheQuery |
| DELETE | `api/3/sonar_queries/{id}` | ISonarQueries.DeleteAsync | SonarQueriesTests.DeleteAsync_SendsDelete |
| GET | `api/3/sonar_queries/{id}` | ISonarQueries.GetAsync | SonarQueriesTests.GetAsync_SendsGet |
| PUT | `api/3/sonar_queries/{id}` | ISonarQueries.UpdateAsync | SonarQueriesTests.UpdateAsync_PutsTheQuery |
| GET | `api/3/sonar_queries/{id}/assets` | ISonarQueries.ListAssetsAsync | SonarQueriesTests.ListAssetsAsync_SendsGet |
| POST | `api/3/sonar_queries/search` | ISonarQueries.SearchAsync | SonarQueriesTests.SearchAsync_PostsTheCriteria |
