using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Scans;

/// <summary>
/// Overrides for a site scan started on demand (<c>POST api/3/sites/{id}/scans</c>). Leave every property
/// <see langword="null"/> to scan the whole site with its own engine and template.
/// </summary>
public sealed class AdhocScanRequest
{
	/// <summary>A name for the scan.</summary>
	[JsonPropertyName("name")]
	public string? Name { get; init; }

	/// <summary>The identifier of the scan template to use instead of the site's.</summary>
	[JsonPropertyName("templateId")]
	public string? TemplateId { get; init; }

	/// <summary>The identifier of the scan engine to use instead of the site's.</summary>
	[JsonPropertyName("engineId")]
	public int? EngineId { get; init; }

	/// <summary>Host names and IP addresses to scan, each of which must be in the site's scope.</summary>
	[JsonPropertyName("hosts")]
	public IReadOnlyList<string>? Hosts { get; init; }

	/// <summary>The identifiers of asset groups to scan, each of which must be assigned to the site.</summary>
	[JsonPropertyName("assetGroupIds")]
	public IReadOnlyList<int>? AssetGroupIds { get; init; }
}
