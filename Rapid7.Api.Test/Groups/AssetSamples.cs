using System.Globalization;
using System.Text.Json;
using System.Text.Json.Nodes;
using Rapid7.Api.Test.Support;

namespace Rapid7.Api.Test.Groups;

/// <summary>
/// Responses for the asset, asset discovery and asset group tests, built from the shapes in the Security Console API
/// specification (hosts, addresses and identifiers replaced with neutral values), and the helpers that send and map them.
/// </summary>
internal static class AssetSamples
{
	public const string SelfLink = """{"href":"https://console.test:3780/api/3/assets/282","rel":"self"}""";

	public const string LinksJson = $$"""{"links":[{{SelfLink}}]}""";

	public const string EmptyPage = """{"resources":[],"page":{"number":0,"size":10,"totalPages":0,"totalResources":0},"links":[]}""";

	public const string EmptyList = """{"resources":[],"links":[]}""";

	public const string NotFound = """{"status":"NOT_FOUND","message":"The resource with identifier 999 was not found.","links":[]}""";

	public const string NotFoundMessage = "The resource with identifier 999 was not found.";

	public const string ConfigurationJson = """{"name":"ssl","value":"false"}""";

	public const string DatabaseJson = """{"description":"Microsoft SQL Server","id":13,"name":"MSSQL"}""";

	public const string GroupAccountJson = """{"id":972,"name":"Administrators"}""";

	public const string UserAccountJson = """{"fullName":"Smith, John","id":8952,"name":"john_smith"}""";

	public const string AssetFileJson = """{"attributes":[{"name":"comment","value":"Remote Admin"}],"name":"ADMIN$","size":-1,"type":"directory"}""";

	public const string WebApplicationJson = """
		{"id":30712,"pages":[{"linkType":"html-ref","path":"/docs/config/index.html","response":200}],"root":"/","virtualHost":"192.0.2.10"}
		""";

	public const string OperatingSystemJson = """
		{
			"architecture":"x86",
			"configurations":[{"name":"kernel","value":"6.0.6001"}],
			"cpe":{
				"edition":"enterprise","language":"en","other":"-","part":"o","product":"windows_server_2008","swEdition":"-",
				"targetHW":"x64","targetSW":"-","update":"sp1","v2.2":"cpe:/o:microsoft:windows_server_2008:-:sp1:enterprise",
				"v2.3":"cpe:2.3:o:microsoft:windows_server_2008:-:sp1:enterprise:*:*:*:*:*","vendor":"microsoft","version":"-"
			},
			"description":"Microsoft Windows Server 2008 Enterprise Edition SP1",
			"family":"Windows",
			"id":35,
			"product":"Windows Server 2008 Enterprise Edition",
			"systemName":"Microsoft Windows",
			"type":"Workstation",
			"vendor":"Microsoft",
			"version":"SP1"
		}
		""";

	public const string SoftwareJson = """
		{
			"configurations":[],
			"cpe":{"part":"a","product":"outlook","vendor":"microsoft","version":"2013","v2.3":"cpe:2.3:a:microsoft:outlook:2013:*:*:*:*:*:*:*"},
			"description":"Microsoft Outlook 2013 15.0.4867.1000",
			"family":"Office 2013",
			"id":3,
			"product":"Outlook 2013",
			"type":"Productivity",
			"vendor":"Microsoft",
			"version":"15.0.4867.1000"
		}
		""";

	public const string ServiceJson = $$"""
		{
			"configurations":[{{ConfigurationJson}}],
			"databases":[{{DatabaseJson}}],
			"family":"CIFS",
			"links":[{{SelfLink}}],
			"name":"CIFS Name Service",
			"nic":"eth0",
			"port":139,
			"product":"Samba",
			"protocol":"tcp",
			"userGroups":[{{GroupAccountJson}}],
			"users":[{{UserAccountJson}}],
			"vendor":"Samba",
			"version":"3.5.11",
			"webApplications":[{{WebApplicationJson}}]
		}
		""";

	public const string SearchCriteriaJson = """
		{"match":"all","filters":[{"field":"risk-score","operator":"is-greater-than","value":5000},{"field":"ip-address","operator":"in-range","lower":"192.0.2.1","upper":"192.0.2.254"},{"field":"site-id","operator":"in","values":[1,2]}]}
		""";

	public const string AssetTagJson = $$"""
		{
			"color":"red",
			"created":"2017-10-07T23:50:01.205Z",
			"id":6,
			"links":[{{SelfLink}}],
			"name":"High Value",
			"riskModifier":2,
			"searchCriteria":{{SearchCriteriaJson}},
			"source":"custom",
			"sources":[{"id":92,"links":[{{SelfLink}}],"source":"site"}],
			"type":"criticality"
		}
		""";

	/// <summary>The fields every asset (and every agent) carries.</summary>
	public const string AssetFields = $$"""
		"addresses":[{"ip":"192.0.2.10","mac":"AB:12:CD:34:EF:56"}],
		"assessedForPolicies":false,
		"assessedForVulnerabilities":true,
		"configurations":[{{ConfigurationJson}}],
		"databases":[{{DatabaseJson}}],
		"files":[{{AssetFileJson}}],
		"history":[{"date":"2018-04-09T06:23:49Z","description":"Imported from CMDB","scanId":28,"type":"SCAN","user":"nxadmin","version":8,"vulnerabilityExceptionId":0}],
		"hostName":"workstation-1.example.test",
		"hostNames":[{"name":"workstation-1.example.test","source":"dns"}],
		"id":282,
		"ids":[{"id":"c56b2c59-4e9b-4b89-85e2-13f8146eb071","source":"WQL"}],
		"ip":"192.0.2.10",
		"links":[{{SelfLink}}],
		"mac":"AB:12:CD:34:EF:56",
		"os":"Microsoft Windows Server 2008 Enterprise Edition SP1",
		"osCertainty":"0.75",
		"osFingerprint":{{OperatingSystemJson}},
		"rawRiskScore":31214.3,
		"riskScore":37457.16,
		"services":[{{ServiceJson}}],
		"software":[{{SoftwareJson}}],
		"type":"physical",
		"userGroups":[{{GroupAccountJson}}],
		"users":[{{UserAccountJson}}],
		"vulnerabilities":{"critical":16,"exploits":4,"malwareKits":0,"moderate":3,"severe":76,"total":95}
		""";

	public const string AssetJson = $$"""{{{AssetFields}}}""";

	public const string AssetGroupJson = $$"""
		{
			"assets":768,
			"description":"Assets with unacceptably high risk.",
			"id":61,
			"links":[{{SelfLink}}],
			"name":"High Risk Assets",
			"riskScore":4457823.78,
			"searchCriteria":{{SearchCriteriaJson}},
			"type":"dynamic",
			"vulnerabilities":{"critical":16,"moderate":3,"severe":76,"total":95}
		}
		""";

	/// <summary>A page holding only <paramref name="resource"/>.</summary>
	public static string Page(string resource)
		=> $$"""{"resources":[{{resource}}],"page":{"number":0,"size":10,"totalPages":1,"totalResources":1},"links":[{{SelfLink}}]}""";

	/// <summary>An unpaged list holding <paramref name="resources"/>.</summary>
	public static string List(string resources) => $$"""{"resources":[{{resources}}],"links":[{{SelfLink}}]}""";

	/// <summary>Sends <paramref name="call"/> and asserts its method, path, query and body.</summary>
	public static async Task SendsAsync(
		Func<Rapid7Client, CancellationToken, Task> call,
		HttpMethod method,
		string path,
		string query = "",
		string? body = null,
		string response = LinksJson)
		=> (await TestClient.CaptureAsync(call, response)).ShouldBe(method, path, query, body);

	/// <summary>
	/// Asserts that <paramref name="model"/>, written back to JSON, carries exactly the properties and values of
	/// <paramref name="json"/>: every field of the sample is modelled under its wire name, and nothing is invented.
	/// Timestamps are compared as instants, so <c>Z</c> and <c>+00:00</c> are equal.
	/// </summary>
	public static void ShouldRoundTrip(object model, string json)
	{
		var written = JsonSerializer.SerializeToNode(model, model.GetType(), Rapid7Json.Options);
		var expected = Normalize(JsonNode.Parse(json));
		var actual = Normalize(written);
		JsonNode.DeepEquals(actual, expected).Should().BeTrue("the model should write back{0}{1}{0}but wrote{0}{2}", Environment.NewLine, expected?.ToJsonString(), actual?.ToJsonString());
	}

	private static JsonNode? Normalize(JsonNode? node) => node switch
	{
		JsonObject obj => new JsonObject(obj.Select(p => KeyValuePair.Create(p.Key, Normalize(p.Value)))),
		JsonArray array => new JsonArray([.. array.Select(Normalize)]),
		JsonValue value when value.TryGetValue<string>(out var text)
			&& text.Contains('T', StringComparison.Ordinal)
			&& text.Contains(':', StringComparison.Ordinal)
			&& DateTimeOffset.TryParse(text, CultureInfo.InvariantCulture, DateTimeStyles.None, out var moment)
			=> JsonValue.Create(moment.UtcDateTime.ToString("O", CultureInfo.InvariantCulture)),
		_ => node?.DeepClone()
	};
}
