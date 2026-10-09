using Rapid7.Api.Models;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>The root of the Security Console API (<c>api/3</c>).</summary>
public interface IRoot
{
	/// <summary>Lists the resources (endpoints) the API offers, as links (<c>GET api/3</c>).</summary>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>A link to each top-level resource.</returns>
	[Get("api/3")]
	Task<Links> GetAsync(CancellationToken cancellationToken);
}
