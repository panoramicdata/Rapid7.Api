using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Scans;

/// <summary>A scan as listed across every site (<c>GET api/3/scans</c>): a <see cref="Scan"/> naming its site.</summary>
public sealed class GlobalScan : Scan
{
	/// <summary>The identifier of the scanned site.</summary>
	[JsonPropertyName("siteId")]
	public int? SiteId { get; init; }

	/// <summary>The name of the scanned site.</summary>
	[JsonPropertyName("siteName")]
	public string? SiteName { get; init; }
}
