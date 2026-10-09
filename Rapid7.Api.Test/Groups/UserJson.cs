namespace Rapid7.Api.Test.Groups;

/// <summary>User, role and authentication source responses, built from the API's schemas (hosts and names replaced).</summary>
internal static class UserJson
{
	public const string User = """
		{
			"id": 9,
			"login": "jsmith",
			"name": "Jane Smith",
			"email": "jsmith@example.test",
			"enabled": true,
			"locked": false,
			"authentication": {
				"id": 1,
				"name": "Builtin Users",
				"type": "normal",
				"external": false,
				"links": [{ "href": "https://console.test:3780/api/3/authentication_sources/1", "rel": "self" }]
			},
			"locale": { "default": "en-US", "reports": "en-GB" },
			"role": {
				"id": "user",
				"name": "User",
				"allAssetGroups": false,
				"allSites": true,
				"superuser": false,
				"privileges": ["view-site-asset-data", "create-reports"]
			},
			"links": [{ "href": "https://console.test:3780/api/3/users/9", "rel": "self" }]
		}
		""";

	public const string UserPage = $$"""
		{
			"resources": [{{User}}],
			"page": { "number": 0, "size": 10, "totalPages": 1, "totalResources": 1 },
			"links": [{ "href": "https://console.test:3780/api/3/users?page=0&size=10", "rel": "self" }]
		}
		""";

	public const string TwoFactorKey = """
		{
			"key": "FAKESEEDFAKESEED",
			"links": [{ "href": "https://console.test:3780/api/3/users/9/2FA", "rel": "self" }]
		}
		""";

	public const string Privileges = """
		{
			"resources": ["all-permissions", "manage-sites"],
			"links": [{ "href": "https://console.test:3780/api/3/privileges", "rel": "self" }]
		}
		""";

	public const string Role = """
		{
			"id": "custom-auditor",
			"name": { "key": "role.auditor.name", "defaultValue": "Auditor", "arguments": ["a", 2] },
			"description": "Reads everything",
			"privileges": ["view-site-asset-data"],
			"links": [{ "href": "https://console.test:3780/api/3/roles/custom-auditor", "rel": "self" }]
		}
		""";

	public const string Roles = $$"""
		{
			"resources": [{{Role}}],
			"links": [{ "href": "https://console.test:3780/api/3/roles", "rel": "self" }]
		}
		""";

	public const string AuthenticationSource = """
		{
			"id": 4,
			"name": "Corporate LDAP",
			"type": "ldap",
			"external": true,
			"links": [{ "href": "https://console.test:3780/api/3/authentication_sources/4", "rel": "self" }]
		}
		""";

	public const string AuthenticationSources = $$"""
		{
			"resources": [{{AuthenticationSource}}],
			"links": [{ "href": "https://console.test:3780/api/3/authentication_sources", "rel": "self" }]
		}
		""";
}
