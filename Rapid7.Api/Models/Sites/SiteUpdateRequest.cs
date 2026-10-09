using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Sites;

/// <summary>
/// The complete settings of an existing site. Scan targets, asset groups and the discovery connection are changed through
/// their own endpoints.
/// </summary>
public sealed class SiteUpdateRequest : SiteRequest
{
	/// <summary>The site importance.</summary>
	[JsonPropertyName("importance")]
	public required SiteImportance Importance { get; init; }

	/// <summary>The identifier of the scan engine or engine pool to scan with.</summary>
	[JsonPropertyName("engineId")]
	public required int EngineId { get; init; }

	/// <summary>The identifier of the scan template to scan with.</summary>
	[JsonPropertyName("scanTemplateId")]
	public required string ScanTemplateId { get; init; }
}
