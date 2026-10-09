namespace Rapid7.Api.Test.Groups;

/// <summary>A scan template with every documented field, shaped as <c>GET api/3/scan_templates/{id}</c> returns it.</summary>
internal static class ScanTemplateJson
{
	public const string Full = """
		{
			"id": "full-audit-without-web-spider",
			"name": "Full audit without Web Spider",
			"description": "Audits every system with safe checks only.",
			"discoveryOnly": false,
			"vulnerabilityEnabled": true,
			"policyEnabled": true,
			"webEnabled": false,
			"enableWindowsServices": true,
			"enhancedLogging": false,
			"maxParallelAssets": 10,
			"maxScanProcesses": 12,
			"discovery": {
				"asset": {
					"sendArpPings": true,
					"sendIcmpPings": false,
					"tcpPorts": [22, 443],
					"udpPorts": [161],
					"treatTcpResetAsAsset": true,
					"ipFingerprintingEnabled": true,
					"fingerprintRetries": 4,
					"fingerprintMinimumCertainty": 0.16,
					"collectWhoisInformation": false
				},
				"performance": {
					"retryLimit": 3,
					"packetRate": { "minimum": 450, "maximum": 15000, "defeatRateLimit": true },
					"parallelism": { "minimum": 0, "maximum": 1000 },
					"scanDelay": { "minimum": "PT0S", "maximum": "PT0.1S" },
					"timeout": { "minimum": "PT0S", "maximum": "PT3S", "initial": "PT0.5S" }
				},
				"service": {
					"tcp": {
						"ports": "well-known",
						"additionalPorts": "3078,8000-8080",
						"excludedPorts": "1024",
						"method": "SYN+RST",
						"links": [{ "href": "https://console.test:3780/api/3/scan_templates/full-audit-without-web-spider/discovery/service/tcp", "rel": "self" }]
					},
					"udp": {
						"ports": "custom",
						"additionalPorts": "4020-4032",
						"excludedPorts": "9899",
						"links": []
					},
					"serviceNameFile": "custom-services.txt"
				}
			},
			"checks": {
				"categories": { "enabled": ["Microsoft Windows"], "disabled": ["Oracle"], "links": [] },
				"types": { "enabled": ["Safe"], "disabled": ["Policy"], "links": [] },
				"individual": { "enabled": ["WINDOWS-HOTFIX-MS14-009"], "disabled": ["ssh-weak-ciphers"], "links": [] },
				"correlate": true,
				"potential": false,
				"unsafe": false,
				"links": [{ "href": "https://console.test:3780/api/3/scan_templates/full-audit-without-web-spider/checks", "rel": "self" }]
			},
			"policy": {
				"enabled": [1001, 1002],
				"recursiveWindowsFSSearch": true,
				"storeSCAP": false,
				"links": []
			},
			"database": {
				"db2": "database",
				"oracle": ["ORCL", "XE"],
				"postgres": "postgres",
				"links": []
			},
			"telnet": {
				"characterSet": "ASCII",
				"loginRegex": "(?:[l,L]ogin) *\\:",
				"passwordPromptRegex": "(?:[p,P]assword) *\\:",
				"failedLoginRegex": "(?:[i,I]ncorrect|[f,F]ail)",
				"questionableLoginRegex": "(?:[l,L]ast [l,L]ogin *\\:)",
				"links": []
			},
			"web": { "maxPages": 3000, "userAgent": "Mozilla/5.0" },
			"links": [{ "href": "https://console.test:3780/api/3/scan_templates/full-audit-without-web-spider", "rel": "self" }]
		}
		""";
}
