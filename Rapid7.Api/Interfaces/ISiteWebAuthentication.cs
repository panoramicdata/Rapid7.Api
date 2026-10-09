using Rapid7.Api.Models;
using Rapid7.Api.Models.Sites;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>The web application authentications configured in a site (<c>api/3/sites/{id}/web_authentication</c>).</summary>
public interface ISiteWebAuthentication
{
	/// <summary>Lists the site's HTML form authentications (<c>GET api/3/sites/{id}/web_authentication/html_forms</c>).</summary>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Every HTML form authentication.</returns>
	[Get("api/3/sites/{siteId}/web_authentication/html_forms")]
	Task<ResourceList<WebFormAuthentication>> ListHtmlFormsAsync(int siteId, CancellationToken cancellationToken);

	/// <summary>
	/// Lists the site's HTTP header authentications (<c>GET api/3/sites/{id}/web_authentication/http_headers</c>). The
	/// header values are secrets and are not returned.
	/// </summary>
	/// <param name="siteId">The identifier of the site.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Every HTTP header authentication.</returns>
	[Get("api/3/sites/{siteId}/web_authentication/http_headers")]
	Task<ResourceList<WebHeaderAuthentication>> ListHttpHeadersAsync(int siteId, CancellationToken cancellationToken);
}
