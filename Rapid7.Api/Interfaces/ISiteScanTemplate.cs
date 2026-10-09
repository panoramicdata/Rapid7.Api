using Rapid7.Api.Models;
using Rapid7.Api.Models.ScanTemplates;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>The scan template a site scans with by default (<c>api/3/sites/{id}/scan_template</c>).</summary>
public interface ISiteScanTemplate
{
	/// <summary>Reads the scan template a site scans with, in full (<c>GET api/3/sites/{id}/scan_template</c>).</summary>
	/// <param name="siteId">The site identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The scan template.</returns>
	[Get("api/3/sites/{siteId}/scan_template")]
	Task<ScanTemplate> GetAsync(int siteId, CancellationToken cancellationToken);

	/// <summary>
	/// Sets the scan template a site scans with (<c>PUT api/3/sites/{id}/scan_template</c>); the body is the identifier
	/// as a JSON string. Requires the Manage Sites privilege.
	/// </summary>
	/// <param name="siteId">The site identifier.</param>
	/// <param name="scanTemplateId">The scan template identifier, such as <c>full-audit-without-web-spider</c>.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the site's scan template.</returns>
	[Put("api/3/sites/{siteId}/scan_template")]
	Task<LinksResource> SetAsync(int siteId, [Body(BodySerializationMethod.Serialized)] string scanTemplateId, CancellationToken cancellationToken);
}
