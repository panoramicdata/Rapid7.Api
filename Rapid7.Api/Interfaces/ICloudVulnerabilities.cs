using Rapid7.Api.Models.Cloud;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>The vulnerabilities InsightVM can assess (<c>v4/integration/vulnerabilities</c>).</summary>
public interface ICloudVulnerabilities
{
	/// <summary>
	/// Searches the vulnerabilities InsightVM can assess, returning one page (<c>POST v4/integration/vulnerabilities</c>).
	/// Although a POST, this only reads, so it is allowed on a read-only client. Read every page with
	/// <see cref="Rapid7CursorPaging"/>.
	/// </summary>
	/// <param name="search">The vulnerability filter expression; leave it empty to match every vulnerability.</param>
	/// <param name="paging">The page number, size, sort and cursor, or <see langword="null"/> for the first page.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>One page of matching vulnerabilities, with the cursor for the next.</returns>
	[Post("v4/integration/vulnerabilities")]
	Task<CursorPage<CloudVulnerability>> SearchAsync(
		[Body] CloudVulnerabilitySearch search,
		[Query] CursorPageOptions? paging,
		CancellationToken cancellationToken);
}
