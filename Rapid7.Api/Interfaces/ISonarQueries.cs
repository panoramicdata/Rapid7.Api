using Rapid7.Api.Models;
using Rapid7.Api.Models.AssetDiscovery;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>Sonar queries, which discover assets from Rapid7 Project Sonar data (<c>api/3/sonar_queries</c>).</summary>
public interface ISonarQueries
{
	/// <summary>Lists every saved Sonar query (<c>GET api/3/sonar_queries</c>).</summary>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The queries.</returns>
	[Get("api/3/sonar_queries")]
	Task<ResourceList<SonarQuery>> ListAsync(CancellationToken cancellationToken);

	/// <summary>Saves a new Sonar query (<c>POST api/3/sonar_queries</c>).</summary>
	/// <param name="request">The name and criteria of the query.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The identifier of the new query.</returns>
	[Post("api/3/sonar_queries")]
	Task<CreatedReference<long>> CreateAsync([Body] SonarQueryRequest request, CancellationToken cancellationToken);

	/// <summary>Reads a Sonar query (<c>GET api/3/sonar_queries/{id}</c>).</summary>
	/// <param name="queryId">The identifier of the query.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The query.</returns>
	[Get("api/3/sonar_queries/{queryId}")]
	Task<SonarQuery> GetAsync(long queryId, CancellationToken cancellationToken);

	/// <summary>Replaces the name and criteria of a Sonar query (<c>PUT api/3/sonar_queries/{id}</c>).</summary>
	/// <param name="queryId">The identifier of the query.</param>
	/// <param name="request">The new name and criteria.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to related resources.</returns>
	[Put("api/3/sonar_queries/{queryId}")]
	Task<LinksResource> UpdateAsync(long queryId, [Body] SonarQueryRequest request, CancellationToken cancellationToken);

	/// <summary>Deletes a Sonar query (<c>DELETE api/3/sonar_queries/{id}</c>).</summary>
	/// <param name="queryId">The identifier of the query.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to related resources.</returns>
	[Delete("api/3/sonar_queries/{queryId}")]
	Task<LinksResource> DeleteAsync(long queryId, CancellationToken cancellationToken);

	/// <summary>Runs a saved Sonar query and lists the assets it discovers (<c>GET api/3/sonar_queries/{id}/assets</c>).</summary>
	/// <param name="queryId">The identifier of the query.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The discovered assets.</returns>
	[Get("api/3/sonar_queries/{queryId}/assets")]
	Task<ResourceList<DiscoveryAsset>> ListAssetsAsync(long queryId, CancellationToken cancellationToken);

	/// <summary>
	/// Lists the assets Sonar criteria discover, without saving a query (<c>POST api/3/sonar_queries/search</c>). The
	/// request is a POST but changes nothing, so a read-only client allows it. The console answers with a bare array.
	/// </summary>
	/// <param name="criteria">The criteria.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The discovered assets.</returns>
	[Post("api/3/sonar_queries/search")]
	Task<IReadOnlyList<DiscoveryAsset>> SearchAsync([Body] SonarCriteria criteria, CancellationToken cancellationToken);
}
