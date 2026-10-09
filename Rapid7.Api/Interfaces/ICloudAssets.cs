using Rapid7.Api.Models.Cloud;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>Assets across the Insight platform organisation (<c>v4/integration/assets</c>).</summary>
public interface ICloudAssets
{
	/// <summary>
	/// Searches the assets the caller can see, returning one page of inventory, assessment and vulnerability summary details
	/// (<c>POST v4/integration/assets</c>). Although a POST, this only reads, so it is allowed on a read-only client. Read
	/// every page with <see cref="Rapid7CursorPaging"/>.
	/// </summary>
	/// <param name="search">The asset and vulnerability filter expressions; leave both empty to match every asset.</param>
	/// <param name="options">Comparison times and what to include, or <see langword="null"/> for the defaults.</param>
	/// <param name="paging">The page number, size, sort and cursor, or <see langword="null"/> for the first page.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>One page of matching assets, with the cursor for the next.</returns>
	[Post("v4/integration/assets")]
	Task<CursorPage<CloudAsset>> SearchAsync(
		[Body] CloudAssetSearch search,
		[Query] CloudAssetOptions? options,
		[Query] CursorPageOptions? paging,
		CancellationToken cancellationToken);

	/// <summary>
	/// Gets the details and assessment of one asset the caller can see (<c>GET v4/integration/assets/{id}</c>).
	/// </summary>
	/// <param name="id">The asset identifier.</param>
	/// <param name="options">Comparison times and what to include, or <see langword="null"/> for the defaults.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The asset.</returns>
	[Get("v4/integration/assets/{id}")]
	Task<CloudAsset> GetAsync(string id, [Query] CloudAssetOptions? options, CancellationToken cancellationToken);
}
