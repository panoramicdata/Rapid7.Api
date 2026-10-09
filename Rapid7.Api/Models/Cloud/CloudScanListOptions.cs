using Refit;

namespace Rapid7.Api.Models.Cloud;

/// <summary>
/// Paging for the scan list, and whether to include each scan's details. The scan list pages by number only and does not
/// document sorting. Leave a property <see langword="null"/> for the API's default.
/// </summary>
public sealed class CloudScanListOptions : PageOptions
{
	/// <summary>Whether to include additional details about each scan (<c>includeDetails</c>; default false).</summary>
	[AliasAs("includeDetails")]
	public bool? IncludeDetails { get; init; }
}
