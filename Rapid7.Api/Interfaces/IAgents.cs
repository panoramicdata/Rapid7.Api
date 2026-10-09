using Rapid7.Api.Models;
using Rapid7.Api.Models.AssetGroups;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>Assets assessed by Rapid7 Insight Agents (<c>api/3/agents</c>).</summary>
public interface IAgents
{
	/// <summary>Lists one page of the assets that have an Insight Agent (<c>GET api/3/agents</c>).</summary>
	/// <param name="paging">Paging and sorting, or <see langword="null"/> for the first page of 10.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The page of agent assets.</returns>
	[Get("api/3/agents")]
	Task<Page<Agent>> ListAsync([Query] PageOptions? paging, CancellationToken cancellationToken);
}
