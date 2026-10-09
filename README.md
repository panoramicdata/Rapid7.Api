# Rapid7.Api

[![NuGet](https://img.shields.io/nuget/v/Rapid7.Api.svg)](https://www.nuget.org/packages/Rapid7.Api)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4.svg)](https://dotnet.microsoft.com/)
[![CI](https://github.com/panoramicdata/Rapid7.Api/actions/workflows/ci.yml/badge.svg)](https://github.com/panoramicdata/Rapid7.Api/actions/workflows/ci.yml)
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/4f07e5acaee648f8820686b8cfcdfee4)](https://app.codacy.com/gh/panoramicdata/Rapid7.Api/dashboard)
[![Codacy Coverage](https://app.codacy.com/project/badge/Coverage/4f07e5acaee648f8820686b8cfcdfee4)](https://app.codacy.com/gh/panoramicdata/Rapid7.Api/dashboard)

A typed, modern .NET client for [Rapid7 InsightVM](https://docs.rapid7.com/insightvm/restful-api/): the Security
Console API (v3), the Cloud Integrations API (v4) and the Bulk Export API, with typed Parquet records. Built on Refit and
System.Text.Json.

Rapid7.Api is an independent open-source project by Panoramic Data Limited. It is not made, endorsed or supported by
Rapid7. Rapid7 and InsightVM are trademarks of Rapid7.

## Installation

```shell
dotnet add package Rapid7.Api
```

Rapid7.Api targets .NET 10. The package version follows the Security Console API version it implements: `3.0.x` is
API v3.

## Three APIs, three clients

| API | Client | Address | Authentication |
|---|---|---|---|
| Security Console API v3 (330 operations) | `Rapid7Client` | your console, `https://<console>:3780` | user name and password (HTTP basic), plus a 2FA token for 2FA accounts |
| Cloud Integrations API v4 (13 operations) | `Rapid7CloudClient` | `https://<region>.api.insight.rapid7.com/vm/` | Insight platform API key |
| Bulk Export API (GraphQL, 5 operations) | `Rapid7BulkExportClient` | `https://<region>.api.insight.rapid7.com/export/graphql` | Insight platform API key (Platform Administrator) |

Every operation in Rapid7's API references is implemented: see [Coverage](#coverage).

## Security Console (v3)

```csharp
using Rapid7.Api;
using Rapid7.Api.Models.Assets;

using var client = new Rapid7Client(new Rapid7ClientOptions
{
	BaseUrl = "https://console.example.com:3780",
	Username = "api-user",
	Password = password,
	TrustedServerCertificateThumbprint = "<SHA-256 thumbprint>" // for the console's self-signed certificate
});

// One page...
var sites = await client.Sites.ListAsync(null, cancellationToken);
Console.WriteLine($"{sites.PageInfo!.TotalResources} sites");

// ...or every page.
await foreach (var site in Rapid7Paging.ReadAllAsync((page, ct) => client.Sites.ListAsync(page, ct), 500, cancellationToken))
{
	Console.WriteLine($"{site.Id}: {site.Name} ({site.Assets} assets)");
}

// Search assets with typed criteria.
var risky = await client.Assets.SearchAsync(
	new SearchCriteria
	{
		Match = SearchMatch.All,
		Filters = [new SearchFilter(SearchField.RiskScore, SearchOperator.IsGreaterThan) { Value = 10_000 }]
	},
	null,
	cancellationToken);
foreach (var asset in risky.Resources)
{
	Console.WriteLine($"{asset.Ip} {asset.HostName}: {asset.RiskScore}");
}
```

Each group of endpoints is a property of the client: `Sites`, `SiteTargets`, `SiteCredentials`, `Assets`,
`AssetGroups`, `Vulnerabilities`, `VulnerabilityExceptions`, `Scans`, `ScanEngines`, `ScanTemplates`, `Tags`,
`Policies`, `Reports`, `Users`, `Roles`, `SharedCredentials`, `Administration` and more. Methods take a required
`CancellationToken`; optional parameters travel in an options or paging object, so pass `null` for none. Creates return
the new resource's id, and paged collections come back as `Page<T>` with `Resources` and `PageInfo`.

For a two-factor account, set `TwoFactorToken` to the current code on a new client.

## Cloud Integrations (v4)

```csharp
using Rapid7.Api;
using Rapid7.Api.Models.Cloud;

using var cloud = new Rapid7CloudClient(new Rapid7PlatformOptions { Region = "eu", ApiKey = apiKey });

var page = await cloud.Assets.SearchAsync(
	new CloudAssetSearch { Asset = "last_scan_end > 2026-01-01T00:00:00Z", Vulnerability = "severity IN ['Critical']" },
	null,
	new CursorPageOptions { Size = 100 },
	cancellationToken);
```

`Rapid7CursorPaging.ReadAllAsync` follows the cursor through every page.

## Bulk Export

The Bulk Export API produces Parquet files of your organisation's assets, vulnerabilities, policies, remediations and
software. The client creates the export, waits for it, downloads the files (without sending your API key to the
pre-signed download host) and reads them into typed records:

```csharp
using Rapid7.Api;
using Rapid7.Api.Models.BulkExport;

using var bulk = new Rapid7BulkExportClient(new Rapid7PlatformOptions { Region = "us", ApiKey = apiKey });

var export = await bulk.ExportVulnerabilitiesAsync(null, cancellationToken); // waits until it has succeeded
await foreach (var finding in bulk.ReadRecordsAsync<AssetVulnerabilityRecord>(export, ExportDatasets.AssetVulnerability, cancellationToken))
{
	Console.WriteLine($"{finding.AssetId}: {finding.Title} ({finding.Severity})");
}
```

GraphQL errors raise `Rapid7GraphQLException`; an export that fails raises `Rapid7ExportFailedException`.

## Read-only mode

Set `ReadOnly = true` on the options and the client refuses, before anything is sent, every request that could change
InsightVM: any PUT or DELETE, and any POST other than searches (and, for Bulk Export, exports). It raises
`Rapid7ReadOnlyException`.

## Errors, retries and timeouts

- A non-success response raises `Rapid7ApiException` with the status code, Rapid7's status and message, and any links.
- 429 and 503 are retried for any verb, other 5xx for idempotent verbs, honouring `Retry-After`, with exponential
  back-off (`MaxRetries`, `RetryBaseDelay`, `MaxRetryDelay`). A connection that could not be established is retried
  too, since nothing was sent.
- `Timeout` applies per attempt and raises `TimeoutException`; cancelling your token raises `OperationCanceledException`.
- Passwords, 2FA tokens, API keys and query strings are never logged; pass an `ILogger` as `Logger` to see method, path
  and retry decisions.
- The console's self-signed certificate is trusted by pinning its SHA-256 thumbprint
  (`TrustedServerCertificateThumbprint`) rather than by turning validation off; `ServerCertificateValidationCallback`
  gives full control.

## Coverage

`docs/endpoints/` lists every operation in Rapid7's InsightVM v3 and v4 OpenAPI specifications and the Bulk Export API
documentation (348 in all), with the client method that implements it and the test that pins its request.
`InventoryTests` keeps that list and the code in step.

| API | Operations | Status |
|---|---|---|
| Security Console v3: sites, assets, asset groups, discovery, vulnerabilities, exceptions, checks, scans, scan engines and templates, tags, policies, overrides, reports, users, roles, credentials, administration | 330 | Complete |
| Cloud Integrations v4 | 13 | Complete |
| Bulk Export (GraphQL) with Parquet records | 5 | Complete |

Where Rapid7's specification is ambiguous or self-contradictory, the XML documentation of the affected member says what
the client does.

## Quality

- Every operation has a unit test pinning its exact HTTP method, path, query and body, and tests mapping
  representative responses. The library has 100% line and branch coverage.
- **Live verification is pending:** integration tests for every safe operation are written and build, but have not yet
  run against a live console or Insight platform organisation (Rapid7 publishes no console Docker image). See
  [docs/TESTING.md](docs/TESTING.md) to run them against your own test instance.
- Zero compiler warnings, nullable reference types, XML documentation on every public member.

## Links

- NuGet: https://www.nuget.org/packages/Rapid7.Api
- Source: https://github.com/panoramicdata/Rapid7.Api
- Issues: https://github.com/panoramicdata/Rapid7.Api/issues
- Contributing: [CONTRIBUTING.md](CONTRIBUTING.md); adding or changing endpoints: [docs/IMPLEMENTING.md](docs/IMPLEMENTING.md)

## License

MIT. See [LICENSE](LICENSE).
