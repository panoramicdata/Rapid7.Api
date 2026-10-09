using Rapid7.Api.Models.Assets;
using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Sites;

/// <summary>A site: a set of assets the console scans together, with its configuration and a risk summary.</summary>
public sealed class Site : Links
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

	/// <summary>The site importance.</summary>
	[JsonPropertyName("importance")]
	public SiteImportance? Importance { get; init; }

	/// <summary>How the site's assets are defined.</summary>
	[JsonPropertyName("type")]
	public SiteType? Type { get; init; }

	/// <summary>For a dynamic site, the kind of discovery connection its assets come from.</summary>
	[JsonPropertyName("connectionType")]
	public SiteConnectionType? ConnectionType { get; init; }

	/// <summary>The number of assets in the site.</summary>
	[JsonPropertyName("assets")]
	public int? Assets { get; init; }

	/// <summary>The site's risk score, with its importance applied.</summary>
	[JsonPropertyName("riskScore")]
	public double? RiskScore { get; init; }

	/// <summary>When the site was last scanned.</summary>
	[JsonPropertyName("lastScanTime")]
	public DateTimeOffset? LastScanTime { get; init; }

	/// <summary>The identifier of the scan engine (or engine pool) that scans the site by default.</summary>
	[JsonPropertyName("scanEngine")]
	public int? ScanEngine { get; init; }

	/// <summary>The identifier of the scan template the site's scans use by default.</summary>
	[JsonPropertyName("scanTemplate")]
	public string? ScanTemplate { get; init; }

	/// <summary>The site's vulnerability counts by severity.</summary>
	[JsonPropertyName("vulnerabilities")]
	public VulnerabilityCounts? Vulnerabilities { get; init; }
}
