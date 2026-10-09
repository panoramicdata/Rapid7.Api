using System.Text.Json.Serialization;

namespace Rapid7.Api.Models;

/// <summary>
/// A resource that carries hypermedia links. On its own it is a links-only response: the usual answer to an update
/// (<c>PUT</c>) or a delete, linking to the resources the change affected. It is also the base of every response type
/// that carries links, so callers read <see cref="Links"/> on all of them.
/// </summary>
public class LinksResource
{
	/// <summary>Links to the affected or related resources.</summary>
	[JsonPropertyName("links")]
	public IReadOnlyList<Link> Links { get; init; } = [];
}
