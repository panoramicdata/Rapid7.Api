using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Cloud;

/// <summary>
/// Where a page sits in a Cloud Integrations (v4) collection, plus the stateless cursor that requests the next page of the
/// same series.
/// </summary>
public sealed class CursorPageMetadata : PageMetadata
{
	/// <summary>
	/// The cursor to send (with the next page number) to continue this series of page requests; absent on collections that
	/// page by number only, such as scans and scan engines.
	/// </summary>
	[JsonPropertyName("cursor")]
	public string? Cursor { get; init; }

	/// <summary>The point in time the series of pages reflects, when supplied here rather than on the page itself.</summary>
	[JsonPropertyName("effectiveTime")]
	public DateTimeOffset? EffectiveTime { get; init; }
}
