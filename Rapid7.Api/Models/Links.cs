using System.Text.Json.Serialization;

namespace Rapid7.Api.Models;

/// <summary>
/// A response that carries only hypermedia links: the usual answer to an update (<c>PUT</c>) or a delete, linking to the
/// resources the change affected.
/// </summary>
public class Links
{
	/// <summary>Links to the affected or related resources.</summary>
	[JsonPropertyName("links")]
	public IReadOnlyList<Link> Items { get; init; } = [];
}
