using Refit;

namespace Rapid7.Api.Models.Tags;

/// <summary>Paging, sorting and filters for the tag list.</summary>
public sealed class TagListOptions : PageOptions
{
	/// <summary>Only tags with this name (<c>name</c>).</summary>
	[AliasAs("name")]
	public string? Name { get; init; }

	/// <summary>Only tags of this kind (<c>type</c>).</summary>
	[AliasAs("type")]
	public TagType? Type { get; init; }
}
