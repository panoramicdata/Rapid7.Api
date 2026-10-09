using Refit;

namespace Rapid7.Api.Models.Scans;

/// <summary>Paging, sorting and the active filter for scan lists.</summary>
public sealed class ScanListOptions : PageOptions
{
	/// <summary>
	/// <see langword="true"/> for running scans only, <see langword="false"/> for past scans only (<c>active</c>);
	/// <see langword="null"/> for the console's default, which is past scans.
	/// </summary>
	[AliasAs("active")]
	public bool? Active { get; init; }
}
