using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.BulkExport;

/// <summary>The answer to creating an export: its identifier, to poll with the <c>export</c> query.</summary>
public sealed class ExportReference
{
	/// <summary>The export identifier.</summary>
	[JsonPropertyName("id")]
	public string? Id { get; init; }
}
