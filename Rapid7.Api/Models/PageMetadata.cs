using System.Text.Json.Serialization;

namespace Rapid7.Api.Models;

/// <summary>Where a page sits in a paged collection.</summary>
public class PageMetadata
{
	/// <summary>The zero-based index of this page.</summary>
	[JsonPropertyName("number")]
	public long Number { get; init; }

	/// <summary>The page size requested.</summary>
	[JsonPropertyName("size")]
	public long Size { get; init; }

	/// <summary>The number of pages available.</summary>
	[JsonPropertyName("totalPages")]
	public long TotalPages { get; init; }

	/// <summary>The number of resources available across all pages.</summary>
	[JsonPropertyName("totalResources")]
	public long TotalResources { get; init; }
}
