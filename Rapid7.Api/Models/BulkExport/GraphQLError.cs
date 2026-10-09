using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.BulkExport;

/// <summary>An error a GraphQL operation raised.</summary>
public sealed class GraphQLError
{
	/// <summary>A description of the error.</summary>
	[JsonPropertyName("message")]
	public string? Message { get; init; }

	/// <summary>Where in the GraphQL document the error arose; empty when not reported.</summary>
	[JsonPropertyName("locations")]
	public IReadOnlyList<GraphQLErrorLocation> Locations { get; init; } = [];

	/// <summary>
	/// The path to the response field that failed, each segment a field name or (as its digits) a list index; empty when
	/// not reported.
	/// </summary>
	[JsonPropertyName("path")]
	public IReadOnlyList<string> Path { get; init; } = [];

	/// <summary>Additional, server-specific details, such as an error code; empty when not reported.</summary>
	[JsonPropertyName("extensions")]
	public IReadOnlyDictionary<string, JsonElement> Extensions { get; init; } = new Dictionary<string, JsonElement>();

	/// <inheritdoc />
	public override string ToString()
		=> Path.Count == 0 ? Message ?? "(no message)" : $"{Message ?? "(no message)"} (at {string.Join('.', Path)})";
}
