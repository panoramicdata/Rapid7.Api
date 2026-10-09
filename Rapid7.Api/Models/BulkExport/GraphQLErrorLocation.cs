using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.BulkExport;

/// <summary>A position in a GraphQL document.</summary>
public sealed class GraphQLErrorLocation
{
	/// <summary>The line, from 1.</summary>
	[JsonPropertyName("line")]
	public int Line { get; init; }

	/// <summary>The column, from 1.</summary>
	[JsonPropertyName("column")]
	public int Column { get; init; }
}
