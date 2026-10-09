using Rapid7.Api.Serialization;
using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.BulkExport;

/// <summary>
/// A bulk export: its status and, once it has succeeded, its Parquet files. Download URLs are valid for 15 minutes (query
/// the export again for fresh ones); the files are kept for 30 days.
/// </summary>
public sealed class Export
{
	/// <summary>The export identifier.</summary>
	[JsonPropertyName("id")]
	public string? Id { get; init; }

	/// <summary>Where the export is in its life cycle.</summary>
	[JsonPropertyName("status")]
	public ExportStatus Status { get; init; }

	/// <summary>The dataset (or kind of export) as the API names it; see <see cref="ExportDatasets"/>.</summary>
	[JsonPropertyName("dataset")]
	public string? Dataset { get; init; }

	/// <summary>When the export was created or last changed state.</summary>
	[JsonPropertyName("timestamp")]
	public DateTimeOffset? Timestamp { get; init; }

	/// <summary>
	/// The files, grouped by dataset prefix, once the export has succeeded; empty before. A single object on the wire is
	/// read as a one-item list.
	/// </summary>
	[JsonPropertyName("result")]
	[JsonConverter(typeof(SingleOrArrayConverter<ExportResult>))]
	public IReadOnlyList<ExportResult> Result { get; init; } = [];
}
