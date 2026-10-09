using Refit;

namespace Rapid7.Api.Models.Policies;

/// <summary>Filters, paging and sorting for the list of policies.</summary>
public sealed class PolicyListOptions : PageOptions
{
	/// <summary>Text that policy titles must contain (<c>filter</c>).</summary>
	[AliasAs("filter")]
	public string? Filter { get; init; }

	/// <summary>Whether to return only policies that have been evaluated against assets (<c>scannedOnly</c>).</summary>
	[AliasAs("scannedOnly")]
	public bool? ScannedOnly { get; init; }
}
