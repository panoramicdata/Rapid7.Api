using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Rapid7.Api.Test.Groups;

/// <summary>Realistic report responses (shapes from the v3 OpenAPI schemas, values neutral), shared by the report tests.</summary>
internal static class ReportJson
{
	// Compact output that, like the client, leaves characters such as the + of a date offset unescaped.
	private static readonly JsonSerializerOptions Compact = new() { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };

	// Every property of the Report schema, including the ones only some formats and templates use.
	public const string Report = """
		{
			"baseline": "previous",
			"bureau": "Bureau",
			"component": "Component",
			"email": {
				"access": "zip",
				"additional": "file",
				"additionalRecipients": [ "security@example.test" ],
				"assetAccess": true,
				"owner": "url",
				"smtp": { "global": false, "relay": "mail.example.test", "sender": "reports@example.test" }
			},
			"enclave": "Enclave",
			"filters": {
				"categories": { "excluded": [ "Adobe" ], "included": [ "Microsoft" ], "links": [ { "href": "https://console.test:3780/api/3/vulnerability_categories", "rel": "Categories" } ] },
				"severity": "critical-and-severe",
				"statuses": [ "vulnerable", "vulnerable-version", "potentially-vulnerable", "vulnerable-and-validated" ]
			},
			"format": "pdf",
			"frequency": {
				"nextRuntimes": [ "2026-11-01T04:00:00Z" ],
				"repeat": { "dayOfWeek": "monday", "every": "day-of-month", "interval": 1, "weekOfMonth": 2 },
				"start": "2026-10-01T04:00:00Z",
				"type": "schedule"
			},
			"id": 17,
			"language": "en-US",
			"links": [ { "href": "https://console.test:3780/api/3/reports/17", "rel": "self" } ],
			"name": "Monthly Site Summary",
			"organization": "Example Ltd",
			"owner": 1,
			"policies": [ 84, 85 ],
			"policy": 84,
			"query": "SELECT * FROM dim_asset",
			"range": { "every": "month", "from": "2026-01-01", "interval": 1, "to": "2026-09-30" },
			"remediation": { "solutions": 25, "sort": "riskscore" },
			"riskTrend": {
				"allAssets": { "total": true, "trend": "average-risk" },
				"assetGroupMembership": "historical",
				"assetGroups": "average",
				"assets": true,
				"from": "P3M",
				"sites": "total",
				"tagMembership": "generation",
				"tags": "average",
				"to": "2026-09-30"
			},
			"scope": { "assetGroups": [ 3 ], "assets": [ 282 ], "scan": 28, "sites": [ 5 ], "tags": [ 7 ] },
			"storage": { "location": "monthly/site", "path": "$(install_dir)/nsc/reports/$(user)/monthly/site" },
			"template": "executive-overview",
			"timezone": "Europe/London",
			"users": [ 7 ],
			"version": "2.3.0"
		}
		""";

	public const string ReportPage = """{ "resources": [ """ + Report + """ ], "page": { "number": 0, "size": 10, "totalPages": 1, "totalResources": 1 }, "links": [] }""";

	/// <summary>
	/// <see cref="Report"/> as it is sent back on update: the identifier and top-level links left out, every other property
	/// in declaration order.
	/// </summary>
	public static readonly string ReportBody = JsonNode.Parse("""
		{
			"name": "Monthly Site Summary",
			"format": "pdf",
			"template": "executive-overview",
			"owner": 1,
			"language": "en-US",
			"timezone": "Europe/London",
			"baseline": "previous",
			"bureau": "Bureau",
			"component": "Component",
			"enclave": "Enclave",
			"organization": "Example Ltd",
			"query": "SELECT * FROM dim_asset",
			"version": "2.3.0",
			"policy": 84,
			"policies": [
				84,
				85
			],
			"users": [
				7
			],
			"scope": {
				"assets": [
					282
				],
				"sites": [
					5
				],
				"assetGroups": [
					3
				],
				"tags": [
					7
				],
				"scan": 28
			},
			"filters": {
				"severity": "critical-and-severe",
				"statuses": [
					"vulnerable",
					"vulnerable-version",
					"potentially-vulnerable",
					"vulnerable-and-validated"
				],
				"categories": {
					"included": [
						"Microsoft"
					],
					"excluded": [
						"Adobe"
					],
					"links": [
						{
							"href": "https://console.test:3780/api/3/vulnerability_categories",
							"rel": "Categories"
						}
					]
				}
			},
			"frequency": {
				"type": "schedule",
				"start": "2026-10-01T04:00:00+00:00",
				"repeat": {
					"every": "day-of-month",
					"interval": 1,
					"dayOfWeek": "monday",
					"weekOfMonth": 2
				},
				"nextRuntimes": [
					"2026-11-01T04:00:00Z"
				]
			},
			"email": {
				"owner": "url",
				"access": "zip",
				"additional": "file",
				"additionalRecipients": [
					"security@example.test"
				],
				"assetAccess": true,
				"smtp": {
					"global": false,
					"relay": "mail.example.test",
					"sender": "reports@example.test"
				}
			},
			"storage": {
				"location": "monthly/site",
				"path": "$(install_dir)/nsc/reports/$(user)/monthly/site"
			},
			"range": {
				"from": "2026-01-01",
				"to": "2026-09-30",
				"every": "month",
				"interval": 1
			},
			"remediation": {
				"solutions": 25,
				"sort": "riskscore"
			},
			"riskTrend": {
				"from": "P3M",
				"to": "2026-09-30",
				"allAssets": {
					"total": true,
					"trend": "average-risk"
				},
				"assets": true,
				"sites": "total",
				"assetGroups": "average",
				"assetGroupMembership": "historical",
				"tags": "average",
				"tagMembership": "generation"
			}
		}
		""")!.ToJsonString(Compact);

	public const string Template = """
		{
			"builtin": true,
			"description": "Details of discovered assets, vulnerabilities and users.",
			"id": "audit-report",
			"links": [ { "href": "https://console.test:3780/api/3/report_templates/audit-report", "rel": "self" } ],
			"name": "Audit Report",
			"sections": [ "Baseline Comparison", "Executive Summary" ],
			"type": "document"
		}
		""";

	public const string TemplateList = """{ "resources": [ """ + Template + """ ], "links": [] }""";

	public const string FormatList = """
		{
			"resources": [
				{ "format": "pdf", "templates": [ "audit-report", "executive-overview" ] },
				{ "format": "csv-export", "templates": [] }
			],
			"links": []
		}
		""";

	public const string Instance = """
		{
			"generated": "2026-06-01T18:56:03Z",
			"id": 5,
			"links": [ { "href": "https://console.test:3780/api/3/reports/17/history/5/output", "rel": "Download" } ],
			"size": { "bytes": 24789050, "formatted": "23.6 MB" },
			"status": "complete",
			"uri": "https://console.test:3780/reports/17/5/report.pdf"
		}
		""";

	public const string InstanceList = """{ "resources": [ """ + Instance + """ ], "links": [] }""";

	public const string LinksJson = """{ "links": [ { "href": "https://console.test:3780/api/3/reports", "rel": "Reports" } ] }""";

	public const string Created = """{ "id": 17, "links": [ { "href": "https://console.test:3780/api/3/reports/17", "rel": "self" } ] }""";
}
