using System.Text.Json;

namespace Rapid7.Api.Models.BulkExport;

/// <summary>
/// Reads an export: its status and, once it has succeeded, the download URLs of its files (the <c>export</c> query).
/// </summary>
/// <remarks>
/// The identifier is written into the document as a GraphQL string literal (as in Rapid7's documentation), escaped, rather
/// than passed as a variable, because the documentation does not name the argument's GraphQL type.
/// </remarks>
public sealed class GetExportRequest : GraphQLRequest<IReadOnlyDictionary<string, object>>
{
	private static readonly IReadOnlyDictionary<string, object> NoVariables = new Dictionary<string, object>();

	/// <summary>Creates the request.</summary>
	/// <param name="id">The export identifier, as returned when the export was created.</param>
	/// <exception cref="ArgumentException"><paramref name="id"/> is null, empty or white space.</exception>
	public GetExportRequest(string id)
		: base("GetExport", Document(id), NoVariables)
	{
	}

	private static string Document(string id)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(id);
		// A JSON string literal is a valid GraphQL string literal, so this escapes quotes, backslashes and control characters.
		return $"query GetExport {{ export(id: {JsonSerializer.Serialize(id)}) {{ id status dataset timestamp result {{ prefix urls }} }} }}";
	}
}
