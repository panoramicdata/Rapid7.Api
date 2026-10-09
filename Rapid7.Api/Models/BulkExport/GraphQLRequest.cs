using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.BulkExport;

/// <summary>
/// A GraphQL request body: the document (<c>query</c>), its <c>variables</c> and the <c>operationName</c> to run. Each Bulk
/// Export operation has its own request type that fixes the document, so callers never write GraphQL.
/// </summary>
/// <typeparam name="TVariables">The type of the variables object.</typeparam>
public abstract class GraphQLRequest<TVariables> where TVariables : class
{
	/// <summary>Creates a request.</summary>
	/// <param name="operationName">The name of the operation in <paramref name="query"/> to run.</param>
	/// <param name="query">The GraphQL document.</param>
	/// <param name="variables">The variables the document declares.</param>
	private protected GraphQLRequest(string operationName, string query, TVariables variables)
	{
		OperationName = operationName;
		Query = query;
		Variables = variables;
	}

	/// <summary>The GraphQL document.</summary>
	[JsonPropertyName("query")]
	[JsonPropertyOrder(0)]
	public string Query { get; }

	/// <summary>The variables the document declares (an empty object when it declares none).</summary>
	[JsonPropertyName("variables")]
	[JsonPropertyOrder(1)]
	public TVariables Variables { get; }

	/// <summary>The name of the operation in <see cref="Query"/> to run.</summary>
	[JsonPropertyName("operationName")]
	[JsonPropertyOrder(2)]
	public string OperationName { get; }
}
