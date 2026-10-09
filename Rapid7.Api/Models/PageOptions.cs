using Refit;

namespace Rapid7.Api.Models;

/// <summary>
/// Paging and sorting for a paged collection. Leave a property <see langword="null"/> for the API's default (page 0, size
/// 10 on the Security Console).
/// </summary>
public class PageOptions
{
	/// <summary>The zero-based index of the page to return (<c>page</c>).</summary>
	[AliasAs("page")]
	public int? Page { get; init; }

	/// <summary>The page size (<c>size</c>); the Security Console allows at most 500.</summary>
	[AliasAs("size")]
	public int? Size { get; init; }

	/// <summary>
	/// Sort criteria, each <c>property[,ASC|DESC]</c> (for example <c>riskScore,DESC</c>), sent as repeated <c>sort</c>
	/// parameters; earlier criteria take precedence.
	/// </summary>
	[AliasAs("sort")]
	[Query(CollectionFormat.Multi)]
	public IEnumerable<string>? Sort { get; init; }
}
