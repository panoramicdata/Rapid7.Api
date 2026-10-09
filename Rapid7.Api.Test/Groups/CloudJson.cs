namespace Rapid7.Api.Test.Groups;

/// <summary>Cloud Integrations (v4) responses shared by the Cloud tests, built from the v4 specification's examples.</summary>
internal static class CloudJson
{
	public const string Finding = """
		{
			"vulnerability_id": "acrobat-cve-2018-16030",
			"check_id": "acrobat-check",
			"status": "VULNERABLE_EXPL",
			"key": "key-123",
			"endpoint": { "port": 443, "protocol": "TCP" },
			"port": 443,
			"protocol": "TCP",
			"nic": "eth0",
			"proof": "<p>Proof</p>",
			"first_found": "2024-01-25T10:46:15Z",
			"last_found": "2024-02-01T00:00:00Z",
			"reintroduced": "2024-01-30T00:00:00Z",
			"remediation_date": "2024-12-25T00:00:00Z",
			"solution_id": "unknown-acrobat-cve-2018-16030",
			"solution_summary": "The solution is unknown",
			"solution_fix": "Take a look at all possible solutions",
			"solution_type": "workaround"
		}
		""";

	public const string Asset = $$"""
		{
			"id": "org-1-default-asset-7912",
			"type": "guest",
			"host_name": "host.example.test",
			"ip": "10.1.0.128",
			"mac": "00:50:56:8B:62:45",
			"os_description": "Microsoft Windows Server 2008 R2, Standard Edition SP1",
			"os_architecture": "x86_64",
			"os_family": "Windows",
			"os_name": "Windows Server 2008 R2, Standard Edition",
			"os_system_name": "Microsoft Windows",
			"os_type": "General",
			"os_vendor": "Microsoft",
			"os_version": "SP1",
			"assessed_for_policies": false,
			"assessed_for_vulnerabilities": true,
			"last_assessed_for_vulnerabilities": "2019-02-14T21:19:41.090Z",
			"last_scan_start": "2019-02-14T21:05:35.014Z",
			"last_scan_end": "2019-02-14T21:19:41.090Z",
			"risk_score": 74621.5625,
			"total_vulnerabilities": 276,
			"critical_vulnerabilities": 16,
			"severe_vulnerabilities": 229,
			"moderate_vulnerabilities": 31,
			"exploits": 24,
			"malware_kits": 1,
			"tags": [{ "name": "lab", "type": "SITE" }, { "name": "Windows", "type": "CUSTOM" }],
			"unique_identifiers": [{ "id": "10.0.0.1", "source": "Endpoint Agent" }],
			"credential_assessments": [{ "port": 22, "protocol": "TCP", "status": "SUCCESS" }],
			"new": [{{Finding}}],
			"remediated": [{ "vulnerability_id": "acrobat-cve-2018-16031", "status": "NOT_VULNERABLE" }],
			"same": [{ "vulnerability_id": "acrobat-cve-2018-16015", "status": "VULNERABLE_POTENTIAL" }]
		}
		""";

	public const string AssetPage = $$"""
		{
			"data": [{{Asset}}, { "id": "org-1-default-asset-198", "unique_identifiers": { "id": "4421d73d", "source": "R7 Agent" } }],
			"metadata": {
				"number": 0,
				"size": 2,
				"totalResources": 2195,
				"totalPages": 1098,
				"cursor": "cursor-1",
				"effectiveTime": "2024-01-25T00:00:00Z"
			},
			"effectiveTime": "2024-01-26T00:00:00Z",
			"links": [
				{ "href": "https://us.api.insight.test/vm/v4/integration/assets?page=0&size=2", "rel": "self" },
				{ "href": "https://us.api.insight.test/vm/v4/integration/assets?page=1&size=2&cursor=cursor-1", "rel": "next" }
			]
		}
		""";

	public const string Vulnerability = """
		{
			"id": "7-zip-cve-2016-2334",
			"title": "7-Zip: CVE-2016-2334: Heap-based buffer overflow vulnerability",
			"description": "Heap-based buffer overflow in 7zip before 16.00.",
			"categories": "7-Zip,Remote Execution",
			"cves": "CVE-2016-2334",
			"references": "bid:90531,cve:CVE-2016-2334",
			"added": "2018-05-16T00:00:00Z",
			"modified": "2018-06-08T00:00:00Z",
			"published": "2016-12-13T00:00:00Z",
			"severity": "critical",
			"severity_score": 9,
			"risk_score": 582.82,
			"denial_of_service": false,
			"cvss_v2_vector": "AV:N/AC:M/Au:N/C:C/I:C/A:C",
			"cvss_v2_score": 9.3,
			"cvss_v2_exploit_score": 8.5888,
			"cvss_v2_impact_score": 10.000845,
			"cvss_v2_access_vector": "network",
			"cvss_v2_access_complexity": "medium",
			"cvss_v2_authentication": "none",
			"cvss_v2_confidentiality_impact": "complete",
			"cvss_v2_integrity_impact": "complete",
			"cvss_v2_availability_impact": "partial",
			"cvss_v3_vector": "CVSS:3.0/AV:L/AC:L/PR:N/UI:R/S:U/C:H/I:H/A:H",
			"cvss_v3_score": 7.8,
			"cvss_v3_exploit_score": 1.8345766,
			"cvss_v3_impact_score": 5.873119,
			"cvss_v3_attack_vector": "local",
			"cvss_v3_attack_complexity": "low",
			"cvss_v3_privileges_required": "none",
			"cvss_v3_user_interaction": "required",
			"cvss_v3_scope": "unchanged",
			"cvss_v3_confidentiality_impact": "high",
			"cvss_v3_integrity_impact": "high",
			"cvss_v3_availability_impact": "low",
			"pci_cvss_score": 9.3,
			"pci_severity_score": 5,
			"pci_fail": true,
			"pci_status": "fail",
			"pci_special_notes": "note",
			"exploits": [{
				"id": "12345",
				"name": "7-Zip HFS+ overflow",
				"description": "An exploit.",
				"rank": "excellent",
				"skill_level": "expert",
				"source": "metasploit"
			}],
			"malware_kits": [{ "name": "Kit", "description": "A kit.", "popularity": "favored" }],
			"links": [{ "href": "http://nvd.example.test/CVE-2016-2334", "id": "CVE-2016-2334", "rel": "advisory", "source": "cve" }]
		}
		""";

	public const string VulnerabilityPage = $$"""
		{
			"data": [{{Vulnerability}}],
			"metadata": { "number": 0, "size": 1, "totalResources": 81631, "totalPages": 81631, "cursor": "-37745434:::_S:::7-zip-cve-2016-2334" },
			"links": []
		}
		""";

	public const string SitePage = """
		{
			"data": [{ "name": "docker hosts", "type": "SITE" }, { "name": "lab", "type": "SITE" }],
			"metadata": { "number": 0, "size": 2, "totalResources": 2, "totalPages": 1, "cursor": "-760687744:::_S:::lab" },
			"links": [{ "href": "https://us.api.insight.test/vm/v4/integration/sites?page=0&size=2", "rel": "self" }]
		}
		""";

	public const string Scan = """
		{
			"id": "bcc3fd8f-7bb6-41cc-a52f-4046c8742bf0",
			"name": "Scan API Example Name",
			"engine_id": "b177d730-5cde-468a-89a4-2c5b9d26b465",
			"status": "Success",
			"started": "2020-05-12T09:00:12.199Z",
			"finished": "2020-05-12T09:08:54.404Z",
			"details": "Scanned 3 assets"
		}
		""";

	public const string ScanEngine = """
		{
			"id": "6c384978-3545-455b-a69a-afa6e8cbd2dd",
			"name": "Scan Engine Example Name",
			"host_name": "127.0.0.1",
			"status": "HEALTHY",
			"last_seen": "2021-03-02T22:29:05.006Z",
			"registered": "2020-09-11T18:36:49.973Z",
			"profile": {
				"configuration": {
					"last_retrieved": "2023-08-13T13:23:17.433Z",
					"properties": ["property1:value1", "property2:value2"]
				}
			}
		}
		""";

	public const string Message = """{"message":"A response message"}""";

	/// <summary>
	/// <paramref name="json"/> as System.Text.Json writes it: with <c>&gt;</c> and <c>'</c> as Unicode escapes (u003E, u0027).
	/// </summary>
	public static string Escaped(string json)
	{
		var backslash = ((char)92).ToString();
		return json.Replace(">", backslash + "u003E", StringComparison.Ordinal).Replace("'", backslash + "u0027", StringComparison.Ordinal);
	}

	public const string NotFound = """{"status":404,"localized_message":"The requested resource does not exist.","message":"The requested resource does not exist."}""";
}
