using Refit;

namespace Rapid7.Api.Models.Cloud;

/// <summary>
/// Optional query parameters of the asset endpoints: a pair of times to compare the vulnerabilities on each asset between,
/// and what to include. Leave a property <see langword="null"/> for the API's default.
/// </summary>
public sealed class CloudAssetOptions
{
	/// <summary>
	/// The time to treat as now when comparing (<c>currentTime</c>): changes are reported from <see cref="ComparisonTime"/>
	/// up to this time.
	/// </summary>
	[AliasAs("currentTime")]
	public DateTimeOffset? CurrentTime { get; init; }

	/// <summary>
	/// The earlier time to compare each asset against (<c>comparisonTime</c>). When set, assets report the vulnerabilities
	/// that are new and remediated since then; Rapid7 recommends a page size of 50 with it.
	/// </summary>
	[AliasAs("comparisonTime")]
	public DateTimeOffset? ComparisonTime { get; init; }

	/// <summary>Whether to list, when comparing, the vulnerabilities present at both times (<c>includeSame</c>; default false).</summary>
	[AliasAs("includeSame")]
	public bool? IncludeSame { get; init; }

	/// <summary>Whether to include the unique identifiers found on each asset (<c>includeUniqueIdentifiers</c>; default true).</summary>
	[AliasAs("includeUniqueIdentifiers")]
	public bool? IncludeUniqueIdentifiers { get; init; }

	/// <summary>
	/// Whether to include vulnerabilities remediated between the two times that appear in neither snapshot
	/// (<c>includeInterSnapshotRemediations</c>; default false).
	/// </summary>
	[AliasAs("includeInterSnapshotRemediations")]
	public bool? IncludeInterSnapshotRemediations { get; init; }
}
