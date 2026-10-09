# Bulk Export API (GraphQL) operations

Source: https://docs.rapid7.com/insightvm/bulk-export-api/

Every operation is a GraphQL request (`POST`) to the regional endpoint `https://<region>.api.insight.rapid7.com/export/graphql`; the part after `#` names the GraphQL mutation or query. One row per operation.

| Method | Path | Client method | Test |
|---|---|---|---|
| POST | `export/graphql#createAssetSoftwareExport` | `IBulkExport.CreateAssetSoftwareExportAsync` | `BulkExportTests.CreateAssetSoftwareExportAsync_PostsTheMutation` |
| POST | `export/graphql#createPolicyExport` | `IBulkExport.CreatePolicyExportAsync` | `BulkExportTests.CreatePolicyExportAsync_PostsTheMutation` |
| POST | `export/graphql#createVulnerabilityExport` | `IBulkExport.CreateVulnerabilityExportAsync` | `BulkExportTests.CreateVulnerabilityExportAsync_PostsTheMutation` |
| POST | `export/graphql#createVulnerabilityRemediationExport` | `IBulkExport.CreateVulnerabilityRemediationExportAsync` | `BulkExportTests.CreateVulnerabilityRemediationExportAsync_PostsTheMutationWithTheDateRange` |
| POST | `export/graphql#export` | `IBulkExport.GetExportAsync` | `BulkExportTests.GetExportAsync_PostsTheQueryWithTheIdAsAStringLiteral` |
