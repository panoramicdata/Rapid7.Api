using Rapid7.Api.Models;
using Rapid7.Api.Models.ScanTemplates;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>
/// Scan templates: what scans discover and check, and how (<c>api/3/scan_templates</c>). Changing templates needs the
/// Manage Scan Templates privilege. Built-in templates can be read and copied but not changed or deleted.
/// </summary>
public interface IScanTemplates
{
	/// <summary>Lists every scan template, built-in and custom (<c>GET api/3/scan_templates</c>).</summary>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Every scan template, in one unpaged list.</returns>
	[Get("api/3/scan_templates")]
	Task<ResourceList<ScanTemplate>> ListAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Creates a scan template (<c>POST api/3/scan_templates</c>). Settings left <see langword="null"/> take the
	/// console's defaults.
	/// </summary>
	/// <param name="scanTemplate">The template; leave <see cref="ScanTemplate.Id"/> and the links <see langword="null"/>.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The new template's identifier.</returns>
	[Post("api/3/scan_templates")]
	Task<CreatedReference<string>> CreateAsync([Body] ScanTemplate scanTemplate, CancellationToken cancellationToken);

	/// <summary>Gets a scan template (<c>GET api/3/scan_templates/{id}</c>).</summary>
	/// <param name="templateId">The template identifier, such as <c>full-audit-without-web-spider</c>.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The scan template.</returns>
	[Get("api/3/scan_templates/{templateId}")]
	Task<ScanTemplate> GetAsync(string templateId, CancellationToken cancellationToken);

	/// <summary>
	/// Replaces a custom scan template's settings (<c>PUT api/3/scan_templates/{id}</c>). Send the whole template: read
	/// it, change it with a <see langword="with"/> expression, and send it back.
	/// </summary>
	/// <param name="templateId">The template identifier.</param>
	/// <param name="scanTemplate">The template's new settings.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the template.</returns>
	[Put("api/3/scan_templates/{templateId}")]
	Task<LinksResource> UpdateAsync(string templateId, [Body] ScanTemplate scanTemplate, CancellationToken cancellationToken);

	/// <summary>Deletes a custom scan template (<c>DELETE api/3/scan_templates/{id}</c>).</summary>
	/// <param name="templateId">The template identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to related resources.</returns>
	[Delete("api/3/scan_templates/{templateId}")]
	Task<LinksResource> DeleteAsync(string templateId, CancellationToken cancellationToken);
}
