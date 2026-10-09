using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.BulkExport;

/// <summary>
/// The columns every Bulk Export dataset shares. Each property names its Parquet column with
/// <see cref="JsonPropertyNameAttribute"/>; a column missing from a file leaves its property <see langword="null"/>.
/// Read records with <see cref="Rapid7Parquet.ReadAsync{T}"/> or <see cref="Rapid7BulkExportClient"/>.
/// </summary>
public abstract class BulkExportRecord
{
	/// <summary>The Insight platform organisation identifier (<c>orgId</c>).</summary>
	[JsonPropertyName("orgId")]
	public string? OrgId { get; init; }

	/// <summary>The asset identifier (<c>assetId</c>).</summary>
	[JsonPropertyName("assetId")]
	public string? AssetId { get; init; }
}
