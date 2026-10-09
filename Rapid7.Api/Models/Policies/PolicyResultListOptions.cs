using Refit;

namespace Rapid7.Api.Models.Policies;

/// <summary>Paging and sorting for lists of policy results by asset, with an option to leave out non-applicable results.</summary>
public sealed class PolicyResultListOptions : PageOptions
{
	/// <summary>Whether to return only results that apply (<c>applicableOnly</c>).</summary>
	[AliasAs("applicableOnly")]
	public bool? ApplicableOnly { get; init; }
}
