using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Assets;

/// <summary>
/// Criteria that select assets: a list of filters and whether an asset must match all of them or any one. Used by the
/// asset search, dynamic asset groups and tags. For example:
/// <code>
/// new SearchCriteria
/// {
/// 	Match = SearchMatch.All,
/// 	Filters =
/// 	[
/// 		new SearchFilter(SearchField.RiskScore, SearchOperator.IsGreaterThan) { Value = 5000 },
/// 		new SearchFilter(SearchField.OperatingSystem, SearchOperator.Contains) { Value = "windows" },
/// 	],
/// }
/// </code>
/// </summary>
public sealed class SearchCriteria
{
	/// <summary>Whether an asset must match all the filters or any one of them.</summary>
	[JsonPropertyName("match")]
	public SearchMatch? Match { get; init; }

	/// <summary>The filters.</summary>
	[JsonPropertyName("filters")]
	public IReadOnlyList<SearchFilter> Filters { get; init; } = [];
}
