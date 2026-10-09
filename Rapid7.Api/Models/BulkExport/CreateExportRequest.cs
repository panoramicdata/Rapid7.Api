namespace Rapid7.Api.Models.BulkExport;

/// <summary>
/// The shared shape of the export mutations that take no settings: <c>mutation X { x(input: {}) { id } }</c>, with no
/// variables.
/// </summary>
public abstract class CreateExportRequest : GraphQLRequest<IReadOnlyDictionary<string, object>>
{
	private static readonly IReadOnlyDictionary<string, object> NoVariables = new Dictionary<string, object>();

	/// <summary>Creates the request for the mutation <paramref name="field"/>, run as <paramref name="operationName"/>.</summary>
	/// <param name="operationName">The operation name, such as <c>CreatePolicyExport</c>.</param>
	/// <param name="field">The mutation field, such as <c>createPolicyExport</c>.</param>
	private protected CreateExportRequest(string operationName, string field)
		: base(operationName, $"mutation {operationName} {{ {field}(input: {{}}) {{ id }} }}", NoVariables)
	{
	}
}
