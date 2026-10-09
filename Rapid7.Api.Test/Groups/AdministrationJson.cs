namespace Rapid7.Api.Test.Groups;

/// <summary>Administration responses, built from the API's schemas and examples (hosts and identifiers replaced).</summary>
internal static class AdministrationJson
{
	public const string CommandOutput = """
		{
			"output": "Security Console version 6.6.250",
			"links": [{ "href": "https://console.test:3780/api/3/administration/commands", "rel": "self" }]
		}
		""";

	public const string Info = """
		{
			"host": "CONSOLE",
			"fqdn": "console.example.test",
			"ip": "192.0.2.10",
			"operatingSystem": "Ubuntu Linux 22.04",
			"user": "root",
			"superuser": true,
			"serial": "0000SERIAL",
			"distinguishedName": "CN=Rapid7 Security Console/ O=Rapid7",
			"cpu": { "count": 8, "clockSpeed": 2600 },
			"memory": {
				"free": { "bytes": 45006848, "formatted": "42.9 MB" },
				"total": { "bytes": 17179869184, "formatted": "16 GB" }
			},
			"disk": {
				"free": { "bytes": 166532222976, "formatted": "155.1 GB" },
				"total": { "bytes": 499004735488, "formatted": "464.7 GB" },
				"installation": {
					"directory": "/opt/rapid7/nexpose",
					"total": { "bytes": 12125933077, "formatted": "11.3 GB" },
					"database": { "bytes": 5364047843, "formatted": "5 GB" },
					"scans": { "bytes": 1370433223, "formatted": "1.3 GB" },
					"reports": { "bytes": 24789050, "formatted": "23.6 MB" },
					"backups": { "bytes": 0, "formatted": "0 bytes" }
				}
			},
			"jvm": {
				"name": "OpenJDK 64-Bit Server VM",
				"vendor": "Azul Systems, Inc.",
				"version": "17.0.9",
				"startTime": "2026-02-13T20:35:35.076Z",
				"uptime": "PT8H21M7.978S"
			},
			"version": {
				"semantic": "6.6.250",
				"build": "2026-01-10-14-11",
				"changeset": "0000changeset",
				"platform": "Linux64",
				"update": {
					"product": "2200922472",
					"content": "3192129162",
					"contentPartial": "723680177",
					"id": { "productId": "281474976711146", "versionId": "490" }
				}
			},
			"links": [{ "href": "https://console.test:3780/api/3/administration/info", "rel": "self" }]
		}
		""";

	public const string License = """
		{
			"status": "Evaluation Mode",
			"edition": "InsightVM",
			"evaluation": true,
			"perpetual": false,
			"expires": "2026-12-31T23:59:59.999Z",
			"features": {
				"adaptiveSecurity": false,
				"agents": true,
				"dynamicDiscovery": true,
				"earlyAccess": false,
				"enginePool": true,
				"insightPlatform": true,
				"mobile": true,
				"multitenancy": false,
				"policyEditor": true,
				"policyManager": true,
				"remediationAnalytics": true,
				"reporting": { "advanced": true, "customizableCSVExport": true, "pci": false },
				"scanning": {
					"discovery": true,
					"scada": false,
					"virtual": true,
					"webApplication": true,
					"policy": {
						"scanning": true,
						"benchmarks": { "cis": true, "disa": false, "fdcc": true, "usgcb": false, "custom": true }
					}
				}
			},
			"limits": { "assets": 100000, "assetsWithHostedEngine": 1000, "scanEngines": 100, "users": 1000 },
			"links": [{ "href": "https://console.test:3780/api/3/administration/license", "rel": "self" }]
		}
		""";

	public const string Properties = """
		{
			"properties": { "java.version": "17.0.9", "os.name": "Linux", "nexpose.port": 3780 },
			"links": [{ "href": "https://console.test:3780/api/3/administration/properties", "rel": "self" }]
		}
		""";

	public const string Settings = """
		{
			"uuid": "00000000-0000-0000-0000-000000000001",
			"serialNumber": "0000SERIAL",
			"directory": "/opt/rapid7/nexpose",
			"assetLinking": true,
			"insightPlatform": true,
			"insightPlatformRegion": "us-east-1",
			"authentication": { "2fa": true, "loginLockThreshold": 5 },
			"database": {
				"vendor": "postgresql",
				"host": "127.0.0.1",
				"port": 5432,
				"url": "//127.0.0.1:5432/nexpose",
				"user": "nxpgsql",
				"maintenanceThreadPoolSize": 20,
				"connection": { "maximumPoolSize": -1, "maximumAdministrationPoolSize": 4, "maximumPreparedStatementPoolSize": 256 }
			},
			"risk": {
				"model": "risk_score_v2",
				"adjustWithCriticality": true,
				"criticalityModifiers": { "veryHigh": 2, "high": 1.5, "medium": 1, "low": 0.75, "veryLow": 0.5 }
			},
			"scan": {
				"connectionTimeout": "PT15S",
				"readTimeout": "PT15M",
				"statusIdleTimeout": "PT3M",
				"statusThreads": 3,
				"maximumThreads": -1,
				"incremental": true
			},
			"smtp": { "host": "mail.example.test", "port": 25, "sender": "security@example.test", "distributionId": "d-1" },
			"updates": { "enabled": true, "productAutoUpdate": false, "contentAutoUpdate": true },
			"web": { "port": 3780, "minThreads": 10, "maxThreads": 100, "sessionTimeout": "PT10M" },
			"links": [{ "href": "https://console.test:3780/api/3/administration/settings", "rel": "self" }]
		}
		""";
}
