namespace Rapid7.Api.Test.Groups;

/// <summary>Bulk Export GraphQL bodies and responses shared by the Bulk Export tests.</summary>
internal static class BulkExportJson
{
	public const string ExportId = "0bb7d3a1-52a6-4b6f-9f2f-3d5e2b1c0a11";

	public const string ExportQuery
		= "query GetExport { export(id: `" + ExportId + "`) { id status dataset timestamp result { prefix urls } } }";

	public const string SucceededExport = """
		{
			"data": {
				"export": {
					"id": "0bb7d3a1-52a6-4b6f-9f2f-3d5e2b1c0a11",
					"status": "SUCCEEDED",
					"dataset": "asset_software",
					"timestamp": "2026-10-09T08:15:30.123Z",
					"result": [
						{ "prefix": "asset_software", "urls": ["https://files.example.test/asset_software/part-0.parquet?X-Amz-Signature=abc", "https://files.example.test/asset_software/part-1.parquet?X-Amz-Signature=def"] },
						{ "prefix": "asset", "urls": ["https://files.example.test/asset/part-0.parquet?X-Amz-Signature=ghi"] }
					]
				}
			}
		}
		""";

	/// <summary>
	/// <paramref name="json"/> as System.Text.Json writes it: each backtick becomes an escaped quote (u0022), and
	/// <c>&gt;</c> and <c>'</c> become Unicode escapes too.
	/// </summary>
	public static string Escaped(string json)
		=> CloudJson.Escaped(json).Replace("`", ((char)92).ToString() + "u0022", StringComparison.Ordinal);

	/// <summary>A GraphQL request body.</summary>
	public static string Body(string query, string variables, string operationName)
		=> Escaped($$"""{"query":"{{query}}","variables":{{variables}},"operationName":"{{operationName}}"}""");

	/// <summary>The body of a create mutation that takes no settings.</summary>
	public static string CreateBody(string operationName, string field)
		=> Body($"mutation {operationName} {{ {field}(input: {{}}) {{ id }} }}", "{}", operationName);

	/// <summary>A create mutation response with the given field and export id.</summary>
	public static string Created(string field) => "{\"data\":{\"" + field + "\":{\"id\":\"" + ExportId + "\"}}}";

	/// <summary>An export query response with the given status and no result.</summary>
	public static string ExportWithStatus(string status)
		=> "{\"data\":{\"export\":{\"id\":\"" + ExportId + "\",\"status\":\"" + status + "\",\"dataset\":\"asset_software\",\"timestamp\":null,\"result\":null}}}";
}
