# Testing

## Unit tests

`Rapid7.Api.Test` needs no network and runs in CI on every push and pull request:

```shell
dotnet test --project Rapid7.Api.Test/Rapid7.Api.Test.csproj
```

Every operation has a test that pins its exact HTTP method, path, query string and body, and tests that map
representative responses built from Rapid7's OpenAPI schemas. Skipped tests fail the run (`failSkips`).
`InventoryTests` keeps `docs/endpoints/*.md` and the Refit interfaces in step.

Coverage, as CI collects it:

```shell
dotnet build Rapid7.Api.Test/Rapid7.Api.Test.csproj
./Rapid7.Api.Test/bin/Debug/net10.0/Rapid7.Api.Test --coverage --coverage-settings "$PWD/coverage.config" --coverage-output-format cobertura --coverage-output coverage.cobertura.xml
```

(`--coverage-settings` must be an absolute path.)

## Integration tests

`Rapid7.Api.IntegrationTest` runs against a real InsightVM Security Console and Insight platform organisation. Rapid7
publishes no Docker image of the console, so supply your own test instance in user secrets:

```powershell
# Security Console (v3)
dotnet user-secrets set "Rapid7:BaseUrl" "https://<console-host>:3780" --project Rapid7.Api.IntegrationTest
dotnet user-secrets set "Rapid7:Username" "<user>" --project Rapid7.Api.IntegrationTest
dotnet user-secrets set "Rapid7:Password" "<password>" --project Rapid7.Api.IntegrationTest
# optional
dotnet user-secrets set "Rapid7:TrustedServerCertificateThumbprint" "<sha256>" --project Rapid7.Api.IntegrationTest
dotnet user-secrets set "Rapid7:TwoFactorToken" "<current code>" --project Rapid7.Api.IntegrationTest

# Insight platform (Cloud Integrations v4 and Bulk Export)
dotnet user-secrets set "Rapid7Platform:Region" "<us|us2|us3|eu|ca|au|ap|aps2|me1>" --project Rapid7.Api.IntegrationTest
dotnet user-secrets set "Rapid7Platform:ApiKey" "<API key>" --project Rapid7.Api.IntegrationTest

dotnet test --project Rapid7.Api.IntegrationTest/Rapid7.Api.IntegrationTest.csproj
```

Environment variables work too (`Rapid7__BaseUrl` etc.). A missing setting fails the tests that need it with a message
naming it; nothing is skipped.

The tests create only objects named `rapid7api-test-...` and delete them as they go. They never start or stop scans,
pair scan engines, change licensing, restart or update the console, or send alerts. Even so, point them only at a test
console.

**Status:** the integration tests are written and build, but have not yet been run against a live console or platform
organisation. CI does not run them.
