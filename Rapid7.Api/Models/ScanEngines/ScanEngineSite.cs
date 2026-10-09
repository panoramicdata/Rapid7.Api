using Rapid7.Api.Models.Assets;
using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.ScanEngines;

/// <summary>
/// A site as listed for the scan engine it is assigned to (<c>GET api/3/scan_engines/{id}/sites</c>). The console uses
/// its general site schema here.
/// </summary>
public sealed class ScanEngineSite : Links
{
	/// <summary>The site identifier.</summary>
	[JsonPropertyName("id")]
	public int? Id { get; init; }

	/// <summary>The site name.</summary>
	[JsonPropertyName("name")]
	public string? Name { get; init; }

	/// <summary>The site description.</summary>
	[JsonPropertyName("description")]
	public string? Description { get; init; }

	/// <summary>The site importance (for example <c>normal</c> or <c>high</c>).</summary>
	[JsonPropertyName("importance")]
	public string? Importance { get; init; }

	/// <summary>How the site's assets are defined.</summary>
	[JsonPropertyName("type")]
	public ScanEngineSiteType? Type { get; init; }

	/// <summary>The discovery connection behind a dynamic site; absent for other sites.</summary>
	[JsonPropertyName("connectionType")]
	public ScanEngineSiteConnectionType? ConnectionType { get; init; }

	/// <summary>The number of assets in the site.</summary>
	[JsonPropertyName("assets")]
	public int? Assets { get; init; }

	/// <summary>When the site was last scanned.</summary>
	[JsonPropertyName("lastScanTime")]
	public DateTimeOffset? LastScanTime { get; init; }

	/// <summary>The site's risk score, adjusted for asset criticality.</summary>
	[JsonPropertyName("riskScore")]
	public double? RiskScore { get; init; }

	/// <summary>The identifier of the scan engine the site scans with.</summary>
	[JsonPropertyName("scanEngine")]
	public int? ScanEngine { get; init; }

	/// <summary>The identifier of the site's scan template.</summary>
	[JsonPropertyName("scanTemplate")]
	public string? ScanTemplate { get; init; }

	/// <summary>The vulnerabilities found in the site, by severity.</summary>
	[JsonPropertyName("vulnerabilities")]
	public VulnerabilityCounts? Vulnerabilities { get; init; }
}
