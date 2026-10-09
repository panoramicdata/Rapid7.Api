using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Sites;

/// <summary>A new site. Only the name is required; the console defaults the rest.</summary>
public sealed class SiteCreateRequest : SiteRequest
{
	/// <summary>The site importance (by default <see cref="SiteImportance.Normal"/>).</summary>
	[JsonPropertyName("importance")]
	public SiteImportance? Importance { get; init; }

	/// <summary>The identifier of the scan engine or engine pool to scan with (by default the console's default engine).</summary>
	[JsonPropertyName("engineId")]
	public int? EngineId { get; init; }

	/// <summary>The identifier of the scan template to scan with (by default the console's default template).</summary>
	[JsonPropertyName("scanTemplateId")]
	public string? ScanTemplateId { get; init; }

	/// <summary>What the site scans: static targets and asset groups, or a discovery connection.</summary>
	[JsonPropertyName("scan")]
	public ScanScope? Scan { get; init; }
}
