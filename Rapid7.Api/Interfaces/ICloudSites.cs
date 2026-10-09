using Rapid7.Api.Models.Cloud;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>Sites across the Insight platform organisation (<c>v4/integration/sites</c>).</summary>
public interface ICloudSites
{
	/// <summary>
	/// Lists one page of sites, each as a tag of type <c>SITE</c> (<c>POST v4/integration/sites</c>; sent without a body).
	/// Although a POST, this only reads, so it is allowed on a read-only client. Read every page with
	/// <see cref="Rapid7CursorPaging"/>.
	/// </summary>
	/// <param name="paging">The page number, size, sort and cursor, or <see langword="null"/> for the first page.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>One page of sites, with the cursor for the next.</returns>
	[Post("v4/integration/sites")]
	Task<CursorPage<CloudTag>> ListAsync([Query] CursorPageOptions? paging, CancellationToken cancellationToken);
}
