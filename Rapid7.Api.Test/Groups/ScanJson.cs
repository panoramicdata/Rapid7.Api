using Rapid7.Api.Models;
using Rapid7.Api.Test.Support;

namespace Rapid7.Api.Test.Groups;

/// <summary>Responses shared by the scan, scan engine and engine pool tests, shaped as the Security Console returns them.</summary>
internal static class ScanJson
{
	/// <summary>An empty collection: a body every list, update and delete call can read.</summary>
	public const string Empty = """{"resources":[],"links":[]}""";

	/// <summary>A links-only answer to an update or delete.</summary>
	public const string LinksOnly = """{"links":[{"href":"https://console.test:3780/api/3/scans/28","rel":"self"}]}""";

	/// <summary>One scan engine, with every documented field.</summary>
	public const string Engine = """
		{
			"id": 2,
			"name": "Corporate Scan Engine 001",
			"address": "engine-001.example.test",
			"port": 40894,
			"sites": [1, 4],
			"status": "pending-authorization",
			"contentVersion": "1735689600",
			"productVersion": "6.6.250",
			"serialNumber": "ENG-0001",
			"isAWSPreAuthEngine": false,
			"lastRefreshedDate": "2026-09-01T10:00:00Z",
			"lastUpdatedDate": "2026-09-02T11:30:00.5Z",
			"links": [{ "href": "https://console.test:3780/api/3/scan_engines/2", "rel": "self" }]
		}
		""";

	/// <summary>Engine pools, unpaged.</summary>
	public const string Pools = """
		{
			"resources": [
				{
					"id": 7,
					"name": "Datacentre pool",
					"engines": [2, 3],
					"sites": [5],
					"links": [{ "href": "https://console.test:3780/api/3/scan_engine_pools/7", "rel": "self" }]
				}
			],
			"links": [{ "href": "https://console.test:3780/api/3/scan_engine_pools", "rel": "self" }]
		}
		""";

	/// <summary>A reference list of identifiers.</summary>
	public const string Ids = """{"resources":[2,3,11],"links":[{"href":"https://console.test:3780/api/3/scan_engine_pools/7/engines","rel":"self"}]}""";

	/// <summary>Asserts that <paramref name="call"/>, answered with <see cref="Ids"/>, reads the identifiers 2, 3 and 11.</summary>
	public static async Task ShouldReadIdsAsync(Func<Rapid7Client, CancellationToken, Task<ResourceList<int>>> call)
		=> (await TestClient.ReadAsync(call, Ids)).Resources.Should().Equal(2, 3, 11);

	/// <summary>One page of scans as listed across the console, with every documented field.</summary>
	public const string GlobalScans = """
		{
			"resources": [
				{
					"id": 28,
					"scanName": "Weekly external",
					"scanType": "Scheduled",
					"status": "finished",
					"message": "Completed normally",
					"startTime": "2026-09-05T01:00:00Z",
					"endTime": "2026-09-05T02:15:30Z",
					"duration": "PT1H15M30S",
					"startedBy": "Jane Admin",
					"startedByUsername": "jadmin",
					"engineId": 2,
					"engineName": "Corporate Scan Engine 001",
					"engineIds": { "id": 2, "newScanEngine": true, "scope": "silo" },
					"assets": 42,
					"vulnerabilities": { "critical": 16, "severe": 76, "moderate": 3, "total": 95 },
					"siteId": 5,
					"siteName": "External",
					"links": [{ "href": "https://console.test:3780/api/3/scans/28", "rel": "self" }]
				},
				{ "id": 29, "status": "integrating", "links": [] }
			],
			"page": { "number": 0, "size": 10, "totalPages": 1, "totalResources": 2 },
			"links": [{ "href": "https://console.test:3780/api/3/scans", "rel": "self" }]
		}
		""";

	/// <summary>One scan, as returned by <c>GET api/3/scans/{id}</c>.</summary>
	public const string Scan = """
		{
			"id": 30,
			"scanName": "Ad hoc",
			"scanType": "Manual",
			"status": "paused",
			"startTime": "2026-09-06T09:00:00Z",
			"engineId": 3,
			"engineIds": { "id": 3, "newScanEngine": false, "scope": "global" },
			"assets": 1,
			"vulnerabilities": { "critical": 0, "severe": 1, "moderate": 2, "total": 3 },
			"links": [{ "href": "https://console.test:3780/api/3/scans/30", "rel": "self" }]
		}
		""";
}
