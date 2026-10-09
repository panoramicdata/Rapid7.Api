using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.BulkExport;

/// <summary>
/// A GraphQL response: the requested <c>data</c>, and any <c>errors</c>. The Bulk Export client raises
/// <see cref="Rapid7GraphQLException"/> for a response that carries errors, so a response returned to a caller has none.
/// </summary>
/// <typeparam name="TData">The shape of the requested data.</typeparam>
public sealed class GraphQLResponse<TData>
{
	/// <summary>The requested data; absent when the operation failed.</summary>
	[JsonPropertyName("data")]
	public TData? Data { get; init; }

	/// <summary>The errors the operation raised; empty on success.</summary>
	[JsonPropertyName("errors")]
	public IReadOnlyList<GraphQLError> Errors { get; init; } = [];
}
