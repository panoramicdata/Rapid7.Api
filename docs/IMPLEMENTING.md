# Implementing endpoints

This is the contract for adding operations to Rapid7.Api. `GET api/3` (`IRoot`, `Rapid7Client.Root.cs`, `RootTests`,
`RootIntegrationTests`) is the worked example; copy its shape.

## Source of truth

- `docs/endpoints/<category>.md` lists every operation, one row per path and method: 330 Security Console (v3)
  operations in 20 tag files, 13 Cloud Integrations (v4) operations in `cloud-integrations.md`, and 5 Bulk Export
  GraphQL operations in `bulk-export.md`, 348 in all.
- The **OpenAPI specs are authoritative** for paths, parameters, request bodies and response schemas:
  `temp/spec/api-v3.json` and `temp/spec/api-v4.json` (git-ignored: the vendor grants no redistribution licence, so
  never commit them, and never copy large verbatim chunks of their prose; write your own XML docs). Bulk Export is
  documented at https://docs.rapid7.com/insightvm/bulk-export-api/ (and the `-ea` page for the software export and the
  Parquet column lists).
- When an operation is implemented, fill its row: `Client method` = `IInterface.Method` (several, comma-separated, when
  more than one method serves the operation), `Test` = `TestClass.TestMethod` of the unit test that pins its request.
  `InventoryTests` checks that each method exists with a matching verb and path (placeholder names ignored; for Bulk
  Export rows the `#operation` suffix is ignored too), that each test exists, and that every Refit method appears in a
  row.
- A category leaves `docs/pending-categories.txt` once every row is filled. Only the lead edits that file.

## Clients and layout

| API | Client | Base address | Refit paths start with |
|---|---|---|---|
| Security Console v3 | `Rapid7Client` | `https://<console>:3780/` | `api/3/...` |
| Cloud Integrations v4 | `Rapid7CloudClient` | `https://<region>.api.insight.rapid7.com/vm/` | `v4/integration/...` (and `admin/health`) |
| Bulk Export (GraphQL) | `Rapid7BulkExportClient` | `https://<region>.api.insight.rapid7.com/` | `export/graphql` |

- **Interfaces:** `Rapid7.Api/Interfaces/I<Family>.cs`, namespace `Rapid7.Api.Interfaces`, one interface per resource
  family (`ISites`, `ISiteTargets`, `IAssets`, `IAssetGroups`, `IVulnerabilities`, ...). Large tags (Site has 84
  operations) are split into several families by path. v4 interfaces are prefixed `ICloud` (`ICloudAssets`,
  `ICloudScans`, ...); the Bulk Export interface is `IBulkExport`. Interface and property names must be unique.
- **Client properties:** one partial file per category, `Rapid7Client.<Category>.cs` (or `Rapid7CloudClient.<...>.cs`,
  `Rapid7BulkExportClient.<...>.cs`), one line per interface: `public ISites Sites => field ??= For<ISites>();` with an
  XML summary naming the path family.
- **Models:** `Rapid7.Api/Models/<Category>/`, namespace `Rapid7.Api.Models.<Category>` (PascalCase tag name without
  spaces: `Sites`, `Assets`, `AssetGroups`, `Vulnerabilities`, `Policies`, `ScanEngines`, `Administration`, ...; v4 in
  `Models/Cloud/`, Bulk Export in `Models/BulkExport/`). Never name a namespace segment `System`. One public type per
  file.
- **Shared types** already in `Rapid7.Api.Models`: `Link`, `LinksResource` (a links-only response, the usual answer to PUT
  and DELETE, and the base of response types that carry links), `CreatedReference<TId>` (the answer to a create: `id` +
  links), `Page<T>` (`resources`, `page`, links), `PageMetadata`, `ResourceList<T>` (unpaged `resources`),
  `PageOptions` (`page`, `size`, repeated `sort`), `UserAction` (who submitted or reviewed something, when, and why). Reuse them;
  do not duplicate them. `Rapid7Paging.ReadAllAsync` reads every page. If a schema is shared by several categories (for
  example an address or a reference type), put it in the folder of the category that owns it and say so in your report.

## Interface methods

- Paths are relative, without a leading slash, with C# camelCase placeholder names
  (`[Get("api/3/sites/{siteId}/assets")]`). Path values are escaped as one segment.
- Every method returns `Task`/`Task<T>`, takes a **required** `CancellationToken cancellationToken` last, and has **no
  optional parameters** (Sonar S2360). Optional query parameters travel in one nullable options object
  (`[Query] PageOptions? paging` or a derived `XListOptions : PageOptions` with `[AliasAs("wire")]` properties);
  callers pass `null`.
- Request bodies are JSON: `[Body] XRequest request`. The client sends `Accept: application/json` itself.
- Paged collections return `Task<Page<X>>`; unpaged ones `Task<ResourceList<X>>`; single resources `Task<X>`; creates
  `Task<CreatedReference<int>>` (or `<string>`, per the spec's id type); updates and deletes `Task<LinksResource>` (or `Task`
  when the spec documents no body). Endpoints whose body is a bare JSON array or value (for example a list of ids, a
  string) take or return exactly that (`[Body] IEnumerable<int> ids`, `Task<IReadOnlyList<int>>`, `Task<string>`).
- Non-JSON responses (report and scan log downloads, certificates): return `Task<HttpContent>` or `Task<Stream>` (the
  caller disposes it), and say so in the XML docs.
- XML documentation on every public member: what the operation does in your own words, the verb and path in
  `<c>...</c>`, required privileges, and anything surprising.

## Models

- Hypermedia links are always exposed as `Links` (`[JsonPropertyName("links")]`). A response type with links derives
  from `LinksResource`; one that already derives from a request type (so cannot) declares its own `Links` property.
- `[JsonPropertyName("wire")]` on **every** property. Response properties are nullable unless the spec marks them
  required; collections are `IReadOnlyList<T>` defaulting to `[]`; dictionaries `IReadOnlyDictionary<string, T>`;
  numbers `int`/`long`/`double` as the spec's format says; dates `DateTimeOffset?` (or `DateOnly?` for `format: date`).
  `Rapid7Json.Options` already reads loose types (quoted numbers, `"true"`), keeps `[]` when a collection is `null`, and
  maps unknown enum values to `Unknown`.
- Enums for the spec's closed `enum` sets: first member `Unknown = 0`, the rest with
  `[JsonStringEnumMemberName("wire")]`. Use `string` where the set is open or very large.
- Request models use the C# `required` modifier for the spec's required properties; everything else is nullable and
  omitted when `null`. Where the spec reuses one schema for create and update, use one request type.
- Avoid duplicated property blocks across files (Codacy grades a file down for about 4 to 5 identical consecutive
  declarations): give request and response types that share fields a common base class.

## Tests (Rapid7.Api.Test, xUnit v3 + AwesomeAssertions)

- One test class per interface in `Rapid7.Api.Test/Groups/<Interface without I>Tests.cs` (split into partial files past
  ~300 lines), using `TestClient.CaptureAsync`, `ReadAsync` and `ShouldFailAsync` (the generic overloads take
  `TestClient.CreateCloud` or `TestClient.CreateBulkExport` for the platform clients), the
  `RecordedCall.ShouldBe(method, path, query, body)` assertion, and `LinkAssertions` (`ShouldBeSelfOnly`,
  `ShouldBeCreated`). Do not add another extension method named `ShouldBe`.
- **Every operation** has a test pinning the exact method, path (`Uri.AbsolutePath`), query and JSON body, and the
  interface has tests mapping realistic responses (built from the spec's examples and schemas; replace hosts and ids
  with neutral values) asserting every modelled field, plus at least one error-path test.
- No skipped tests (`failSkips` is on), no network, no sleeping. Shared JSON in constants or a fixture class; no
  copy-pasted test bodies. Line and branch coverage of what you add must be 100%.

## Integration tests (Rapid7.Api.IntegrationTest): written, not run

There is no live console or platform key yet. Write integration tests under `Rapid7.Api.IntegrationTest/<Category>/`
(`[Collection(Rapid7TestGroup.Name)]`, `Rapid7Fixture`) that would verify, against a live instance: every read operation,
and create/update/delete round trips for objects that are safe to create, named with `Rapid7Fixture.UniqueName(...)`
(prefix `rapid7api-test-`) and deleted in finally blocks. Build them, but do not run them; they fail loudly (naming the
missing setting) until settings exist. **Never** write a live test that starts or stops a scan, pairs or removes a scan
engine, changes licensing, restarts or updates the console, sends email or SNMP or syslog alerts, or touches anything
not created by the test.

## Code quality

- Zero warnings (`TreatWarningsAsErrors`), XML docs on all public API, file-scoped namespaces, tabs, CRLF. Files written
  by tools are LF: run `dotnet format whitespace Rapid7.Api.slnx` before building.
- Keep files small and simple (Codacy grade A).
- Build only what you touched: `dotnet build Rapid7.Api.Test/Rapid7.Api.Test.csproj`, then
  `dotnet test --project Rapid7.Api.Test/Rapid7.Api.Test.csproj --no-build` (exit code 0 = all passed).
