using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Sites;

/// <summary>The search that selects which assets of its discovery connection a dynamic site contains.</summary>
public sealed class DiscoverySearchCriteria
{
	/// <summary>The kind of discovery connection the filters apply to.</summary>
	[JsonPropertyName("connectionType")]
	public DiscoveryConnectionType? ConnectionType { get; init; }

	/// <summary>The filters an asset is matched against.</summary>
	[JsonPropertyName("filters")]
	public IReadOnlyList<DiscoverySearchCriteriaFilter>? Filters { get; init; }

	/// <summary>Whether any filter or every filter must match.</summary>
	[JsonPropertyName("match")]
	public DiscoverySearchMatch? Match { get; init; }
}
