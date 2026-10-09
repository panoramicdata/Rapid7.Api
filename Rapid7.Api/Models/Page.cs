using System.Text.Json.Serialization;

namespace Rapid7.Api.Models;

/// <summary>
/// One page of a Security Console (v3) collection: the resources, the page position, and links to the first, previous,
/// next and last pages. Read every page with <see cref="Rapid7Paging"/>.
/// </summary>
/// <typeparam name="T">The resource type.</typeparam>
public sealed class Page<T> : LinksResource
{
	/// <summary>The resources on this page.</summary>
	[JsonPropertyName("resources")]
	public IReadOnlyList<T> Resources { get; init; } = [];

	/// <summary>Where this page sits; absent on collections the console does not page.</summary>
	[JsonPropertyName("page")]
	public PageMetadata? PageInfo { get; init; }
}
