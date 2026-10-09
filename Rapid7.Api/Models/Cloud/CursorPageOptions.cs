using Refit;

namespace Rapid7.Api.Models.Cloud;

/// <summary>
/// Paging and sorting for a cursor-paged Cloud Integrations (v4) collection: the page number, size and sort criteria of
/// <see cref="PageOptions"/>, plus the cursor returned by the previous page. Leave a property <see langword="null"/> for the
/// API's default.
/// </summary>
public class CursorPageOptions : PageOptions
{
	/// <summary>
	/// The cursor from the previous page's <see cref="CursorPageMetadata.Cursor"/> (<c>cursor</c>), sent with the next page
	/// number to continue the same series; <see langword="null"/> for the first page.
	/// </summary>
	[AliasAs("cursor")]
	public string? Cursor { get; init; }
}
