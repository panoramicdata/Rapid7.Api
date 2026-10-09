# Bulk Export API (GraphQL) operations

Source: https://docs.rapid7.com/insightvm/bulk-export-api/

Every operation is a GraphQL request (`POST`) to the regional endpoint `https://<region>.api.insight.rapid7.com/export/graphql`; the part after `#` names the GraphQL mutation or query. One row per operation.

| Method | Path | Client method | Test |
|---|---|---|---|
| POST | `export/graphql#createAssetSoftwareExport` |  |  |
| POST | `export/graphql#createPolicyExport` |  |  |
| POST | `export/graphql#createVulnerabilityExport` |  |  |
| POST | `export/graphql#createVulnerabilityRemediationExport` |  |  |
| POST | `export/graphql#export` |  |  |
