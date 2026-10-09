using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.AssetDiscovery;

/// <summary>The filters of a Sonar query; an asset must match all of them.</summary>
public sealed class SonarCriteria
{
	/// <summary>The filters.</summary>
	[JsonPropertyName("filters")]
	public IReadOnlyList<SonarCriterion> Filters { get; init; } = [];
}
