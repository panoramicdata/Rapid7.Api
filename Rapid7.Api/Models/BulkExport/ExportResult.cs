using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.BulkExport;

/// <summary>The Parquet files of one dataset of a succeeded export.</summary>
public sealed class ExportResult
{
	/// <summary>The dataset prefix of the files, such as <c>asset</c> or <c>asset_vulnerability</c>.</summary>
	[JsonPropertyName("prefix")]
	public string? Prefix { get; init; }

	/// <summary>
	/// Pre-signed download URLs, one per Parquet file, valid for 15 minutes. They point at another host and carry their own
	/// authorisation: download them without the API key.
	/// </summary>
	[JsonPropertyName("urls")]
	public IReadOnlyList<Uri> Urls { get; init; } = [];
}
