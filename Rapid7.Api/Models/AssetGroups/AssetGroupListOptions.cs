using Refit;

namespace Rapid7.Api.Models.AssetGroups;

/// <summary>Filters, paging and sorting for listing asset groups.</summary>
public sealed class AssetGroupListOptions : PageOptions
{
	/// <summary>Only groups whose name contains this text, ignoring case (<c>name</c>).</summary>
	[AliasAs("name")]
	public string? Name { get; init; }

	/// <summary>Only groups of this type (<c>type</c>).</summary>
	[AliasAs("type")]
	public AssetGroupType? Type { get; init; }
}
