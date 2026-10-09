using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Cloud;

/// <summary>
/// One page of a Cloud Integrations (v4) collection: the resources (<c>data</c>), the page position and cursor
/// (<c>metadata</c>), and links to the first, previous, next and last pages. Read every page with
/// <see cref="Rapid7CursorPaging"/>.
/// </summary>
/// <typeparam name="T">The resource type.</typeparam>
public sealed class CursorPage<T> : Links
{
	/// <summary>The resources on this page.</summary>
	[JsonPropertyName("data")]
	public IReadOnlyList<T> Data { get; init; } = [];

	/// <summary>Where this page sits, and the cursor for the next one.</summary>
	[JsonPropertyName("metadata")]
	public CursorPageMetadata? Metadata { get; init; }

	/// <summary>The point in time the page reflects, when supplied.</summary>
	[JsonPropertyName("effectiveTime")]
	public DateTimeOffset? EffectiveTime { get; init; }
}
