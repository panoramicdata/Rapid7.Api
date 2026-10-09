using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Cloud;

/// <summary>
/// The filters for an asset search (<c>POST v4/integration/assets</c>): an expression over asset properties and, from
/// <see cref="CloudVulnerabilitySearch"/>, one over the vulnerabilities on each asset. Leave both <see langword="null"/>
/// to return every asset the caller can see.
/// </summary>
public sealed class CloudAssetSearch : CloudVulnerabilitySearch
{
	/// <summary>
	/// A filter expression over asset properties, such as <c>last_scan_end &gt; 2019-09-04T23:16:57.903Z</c> or
	/// <c>last_scan_end &gt;= '2025-09-13T00:02:01Z' &amp;&amp; cvss_score &gt;= 9</c>. See Rapid7's Cloud Integrations API
	/// documentation for the searchable properties and operators.
	/// </summary>
	[JsonPropertyName("asset")]
	public string? Asset { get; init; }
}
