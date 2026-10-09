namespace Rapid7.Api.Test.Groups;

/// <summary>
/// Realistic policy compliance responses (shapes from the v3 OpenAPI schemas; hosts, names and identifiers neutral), shared
/// by the policy, policy rule, policy group and asset policy tests.
/// </summary>
internal static class PolicyJson
{
	public const string SelfLink = """{ "href": "https://console.test:3780/api/3/policies/84", "rel": "self" }""";

	public const string Policy = """
		{
			"benchmarkName": "CIS Example Server Benchmark",
			"benchmarkVersion": "1.4.0",
			"category": "CIS",
			"description": "Level 1 settings for servers.",
			"failedAssetsCount": 3,
			"failedRulesCount": 12,
			"id": "xccdf_org.example_profile_Level_1",
			"isCustom": false,
			"links": [ { "href": "https://console.test:3780/api/3/policies/84", "rel": "self" } ],
			"notApplicableAssetsCount": 1,
			"notApplicableRulesCount": 4,
			"passedAssetsCount": 7,
			"passedRulesCount": 250,
			"policyName": "Level 1 - Member Server",
			"ruleCompliance": 0.95,
			"ruleComplianceDelta": -0.02,
			"scope": "Built-in",
			"status": "FAIL",
			"surrogateId": 84,
			"title": "Example Server Level 1",
			"unscoredRules": 2
		}
		""";

	public const string PolicyPage = """{ "resources": [ """ + Policy + """ ], "page": { "number": 0, "size": 10, "totalPages": 1, "totalResources": 1 }, "links": [ """ + SelfLink + """ ] }""";

	public const string Item = """
		{
			"assets": { "total": 10, "totalFailed": 3, "totalNotApplicable": 1, "totalPassed": 6, "links": [] },
			"description": "Password settings.",
			"hasOverride": true,
			"id": 71,
			"isUnscored": false,
			"links": [ { "href": "https://console.test:3780/api/3/policies/84/groups/71", "rel": "self" } ],
			"name": "xccdf_org.example_group_1.1",
			"policy": { "name": "xccdf_org.example_profile_Level_1", "title": "Example Server Level 1", "version": "1.4.0", "links": [] },
			"rules": { "total": 5, "totalFailed": 1, "totalNotApplicable": 0, "totalPassed": 4, "unscored": 1, "links": [] },
			"scope": "Built-in",
			"status": "PASS",
			"title": "Password Policy",
			"type": "group"
		}
		""";

	public const string ItemPage = """{ "resources": [ """ + Item + """ ], "links": [] }""";

	public const string Rule = """
		{
			"assets": { "total": 10, "totalFailed": 2, "totalNotApplicable": 3, "totalPassed": 5, "links": [] },
			"benchmark": { "name": "xccdf_org.example_benchmark", "title": "CIS Example Server Benchmark", "version": "1.4.0", "links": [] },
			"description": "Keeps a history of passwords.",
			"id": "xccdf_org.example_rule_1.1.1",
			"isCustom": true,
			"links": [ { "href": "https://console.test:3780/api/3/policies/84/rules/53", "rel": "self" } ],
			"name": "xccdf_org.example_rule_1.1.1",
			"role": "unscored",
			"scope": "Custom",
			"status": "NOT_APPLICABLE",
			"surrogateId": 53,
			"title": "Enforce password history"
		}
		""";

	public const string RulePage = """{ "resources": [ """ + Rule + """ ], "links": [] }""";

	public const string Group = """
		{
			"assets": { "total": 4, "totalFailed": 1, "totalNotApplicable": 0, "totalPassed": 3, "links": [] },
			"benchmark": { "name": "xccdf_org.example_benchmark", "title": "CIS Example Server Benchmark", "version": "1.4.0", "links": [] },
			"description": "Account policies.",
			"id": "xccdf_org.example_group_1",
			"links": [ { "href": "https://console.test:3780/api/3/policies/84/groups/71", "rel": "self" } ],
			"name": "xccdf_org.example_group_1",
			"policy": { "name": "xccdf_org.example_profile_Level_1", "title": "Example Server Level 1", "version": "1.4.0", "links": [] },
			"scope": "Built-in",
			"status": "FAIL",
			"surrogateId": 71,
			"title": "Account Policies"
		}
		""";

	public const string GroupPage = """{ "resources": [ """ + Group + """ ], "links": [] }""";

	public const string Asset = """
		{
			"hostname": "server01.example.test",
			"id": 282,
			"ip": "192.0.2.10",
			"links": [ { "href": "https://console.test:3780/api/3/assets/282", "rel": "Asset" } ],
			"os": {
				"architecture": "x86_64",
				"configurations": [ { "name": "kernel", "value": "5.15" }, { "name": "selinux" } ],
				"cpe": {
					"edition": "enterprise",
					"language": "en",
					"other": "other-info",
					"part": "o",
					"product": "example_server",
					"swEdition": "server",
					"targetHW": "x64",
					"targetSW": "none",
					"update": "sp1",
					"v2.2": "cpe:/o:example:example_server:2:sp1:enterprise",
					"v2.3": "cpe:2.3:o:example:example_server:2:sp1:enterprise:*:*:*:*:*",
					"vendor": "example",
					"version": "2"
				},
				"description": "Example Server 2 SP1",
				"family": "Linux",
				"id": 35,
				"product": "Example Server",
				"systemName": "Example Linux",
				"type": "General",
				"vendor": "Example",
				"version": "2"
			},
			"status": "failed"
		}
		""";

	public const string AssetPage = """{ "resources": [ """ + Asset + """ ], "links": [] }""";

	public const string ControlPage = """
		{
			"resources": [
				{
					"cceItemId": "CCE-35219-5",
					"ccePlatform": "cpe:/o:example:example_server",
					"controlName": "AC-7",
					"id": "AC-7",
					"links": [],
					"publishedDate": 1388534400000
				}
			],
			"links": []
		}
		""";

	public const string Summary = """
		{
			"decreasedCompliance": 1,
			"increasedCompliance": 2,
			"links": [ { "href": "https://console.test:3780/api/3/policy/summary", "rel": "self" } ],
			"numberOfPolicies": 120,
			"overallCompliance": 0.81,
			"scannedPolicies": 5
		}
		""";

	public const string NotFound = """{"status":"NOT_FOUND","message":"The resource could not be found.","links":[]}""";

	public const string NotFoundMessage = "The resource could not be found.";
}
