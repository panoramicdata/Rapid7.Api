using Rapid7.Api.Models;
using Rapid7.Api.Models.Sites;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>
/// A site's alerts, which notify people of scan events and vulnerability findings by email (SMTP), SNMP trap or syslog
/// (<c>api/3/sites/{id}/alerts</c>). Changing alerts requires the Manage Site Alerts privilege. Creating or updating an
/// alert sends nothing; alerts fire when the site's scans raise the events they are enabled for.
/// </summary>
public interface ISiteAlerts
{
	/// <summary>Lists every alert of a site, of all kinds (<c>GET api/3/sites/{id}/alerts</c>).</summary>
	/// <param name="siteId">The site identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The alerts.</returns>
	[Get("api/3/sites/{siteId}/alerts")]
	Task<ResourceList<SiteAlert>> ListAsync(int siteId, CancellationToken cancellationToken);

	/// <summary>Deletes every alert of a site, of all kinds (<c>DELETE api/3/sites/{id}/alerts</c>).</summary>
	/// <param name="siteId">The site identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the site.</returns>
	[Delete("api/3/sites/{siteId}/alerts")]
	Task<LinksResource> DeleteAllAsync(int siteId, CancellationToken cancellationToken);

	/// <summary>Lists a site's SMTP (email) alerts (<c>GET api/3/sites/{id}/alerts/smtp</c>).</summary>
	/// <param name="siteId">The site identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The SMTP alerts.</returns>
	[Get("api/3/sites/{siteId}/alerts/smtp")]
	Task<ResourceList<SmtpAlert>> ListSmtpAsync(int siteId, CancellationToken cancellationToken);

	/// <summary>
	/// Replaces all of a site's SMTP alerts (<c>PUT api/3/sites/{id}/alerts/smtp</c>): alerts left out are deleted.
	/// </summary>
	/// <param name="siteId">The site identifier.</param>
	/// <param name="alerts">The site's SMTP alerts.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the site's SMTP alerts.</returns>
	[Put("api/3/sites/{siteId}/alerts/smtp")]
	Task<LinksResource> ReplaceSmtpAsync(int siteId, [Body] IEnumerable<SmtpAlert> alerts, CancellationToken cancellationToken);

	/// <summary>Adds an SMTP alert to a site (<c>POST api/3/sites/{id}/alerts/smtp</c>).</summary>
	/// <param name="siteId">The site identifier.</param>
	/// <param name="alert">The new alert.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The new alert's identifier and a link to it.</returns>
	[Post("api/3/sites/{siteId}/alerts/smtp")]
	Task<CreatedReference<int>> CreateSmtpAsync(int siteId, [Body] SmtpAlert alert, CancellationToken cancellationToken);

	/// <summary>Deletes all of a site's SMTP alerts (<c>DELETE api/3/sites/{id}/alerts/smtp</c>).</summary>
	/// <param name="siteId">The site identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the site.</returns>
	[Delete("api/3/sites/{siteId}/alerts/smtp")]
	Task<LinksResource> DeleteAllSmtpAsync(int siteId, CancellationToken cancellationToken);

	/// <summary>Reads one of a site's SMTP alerts (<c>GET api/3/sites/{id}/alerts/smtp/{alertId}</c>).</summary>
	/// <param name="siteId">The site identifier.</param>
	/// <param name="alertId">The alert identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The alert.</returns>
	[Get("api/3/sites/{siteId}/alerts/smtp/{alertId}")]
	Task<SmtpAlert> GetSmtpAsync(int siteId, int alertId, CancellationToken cancellationToken);

	/// <summary>Replaces one of a site's SMTP alerts (<c>PUT api/3/sites/{id}/alerts/smtp/{alertId}</c>).</summary>
	/// <param name="siteId">The site identifier.</param>
	/// <param name="alertId">The alert identifier.</param>
	/// <param name="alert">The alert's new settings.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the alert.</returns>
	[Put("api/3/sites/{siteId}/alerts/smtp/{alertId}")]
	Task<LinksResource> UpdateSmtpAsync(int siteId, int alertId, [Body] SmtpAlert alert, CancellationToken cancellationToken);

	/// <summary>Deletes one of a site's SMTP alerts (<c>DELETE api/3/sites/{id}/alerts/smtp/{alertId}</c>).</summary>
	/// <param name="siteId">The site identifier.</param>
	/// <param name="alertId">The alert identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the site's SMTP alerts.</returns>
	[Delete("api/3/sites/{siteId}/alerts/smtp/{alertId}")]
	Task<LinksResource> DeleteSmtpAsync(int siteId, int alertId, CancellationToken cancellationToken);

	/// <summary>Lists a site's SNMP alerts (<c>GET api/3/sites/{id}/alerts/snmp</c>).</summary>
	/// <param name="siteId">The site identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The SNMP alerts.</returns>
	[Get("api/3/sites/{siteId}/alerts/snmp")]
	Task<ResourceList<SnmpAlert>> ListSnmpAsync(int siteId, CancellationToken cancellationToken);

	/// <summary>
	/// Replaces all of a site's SNMP alerts (<c>PUT api/3/sites/{id}/alerts/snmp</c>): alerts left out are deleted.
	/// </summary>
	/// <param name="siteId">The site identifier.</param>
	/// <param name="alerts">The site's SNMP alerts.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the site's SNMP alerts.</returns>
	[Put("api/3/sites/{siteId}/alerts/snmp")]
	Task<LinksResource> ReplaceSnmpAsync(int siteId, [Body] IEnumerable<SnmpAlert> alerts, CancellationToken cancellationToken);

	/// <summary>Adds an SNMP alert to a site (<c>POST api/3/sites/{id}/alerts/snmp</c>).</summary>
	/// <param name="siteId">The site identifier.</param>
	/// <param name="alert">The new alert.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The new alert's identifier and a link to it.</returns>
	[Post("api/3/sites/{siteId}/alerts/snmp")]
	Task<CreatedReference<int>> CreateSnmpAsync(int siteId, [Body] SnmpAlert alert, CancellationToken cancellationToken);

	/// <summary>Deletes all of a site's SNMP alerts (<c>DELETE api/3/sites/{id}/alerts/snmp</c>).</summary>
	/// <param name="siteId">The site identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the site.</returns>
	[Delete("api/3/sites/{siteId}/alerts/snmp")]
	Task<LinksResource> DeleteAllSnmpAsync(int siteId, CancellationToken cancellationToken);

	/// <summary>Reads one of a site's SNMP alerts (<c>GET api/3/sites/{id}/alerts/snmp/{alertId}</c>).</summary>
	/// <param name="siteId">The site identifier.</param>
	/// <param name="alertId">The alert identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The alert.</returns>
	[Get("api/3/sites/{siteId}/alerts/snmp/{alertId}")]
	Task<SnmpAlert> GetSnmpAsync(int siteId, int alertId, CancellationToken cancellationToken);

	/// <summary>Replaces one of a site's SNMP alerts (<c>PUT api/3/sites/{id}/alerts/snmp/{alertId}</c>).</summary>
	/// <param name="siteId">The site identifier.</param>
	/// <param name="alertId">The alert identifier.</param>
	/// <param name="alert">The alert's new settings.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the alert.</returns>
	[Put("api/3/sites/{siteId}/alerts/snmp/{alertId}")]
	Task<LinksResource> UpdateSnmpAsync(int siteId, int alertId, [Body] SnmpAlert alert, CancellationToken cancellationToken);

	/// <summary>Deletes one of a site's SNMP alerts (<c>DELETE api/3/sites/{id}/alerts/snmp/{alertId}</c>).</summary>
	/// <param name="siteId">The site identifier.</param>
	/// <param name="alertId">The alert identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the site's SNMP alerts.</returns>
	[Delete("api/3/sites/{siteId}/alerts/snmp/{alertId}")]
	Task<LinksResource> DeleteSnmpAsync(int siteId, int alertId, CancellationToken cancellationToken);

	/// <summary>Lists a site's syslog alerts (<c>GET api/3/sites/{id}/alerts/syslog</c>).</summary>
	/// <param name="siteId">The site identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The syslog alerts.</returns>
	[Get("api/3/sites/{siteId}/alerts/syslog")]
	Task<ResourceList<SyslogAlert>> ListSyslogAsync(int siteId, CancellationToken cancellationToken);

	/// <summary>
	/// Replaces all of a site's syslog alerts (<c>PUT api/3/sites/{id}/alerts/syslog</c>): alerts left out are deleted.
	/// </summary>
	/// <param name="siteId">The site identifier.</param>
	/// <param name="alerts">The site's syslog alerts.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the site's syslog alerts.</returns>
	[Put("api/3/sites/{siteId}/alerts/syslog")]
	Task<LinksResource> ReplaceSyslogAsync(int siteId, [Body] IEnumerable<SyslogAlert> alerts, CancellationToken cancellationToken);

	/// <summary>Adds a syslog alert to a site (<c>POST api/3/sites/{id}/alerts/syslog</c>).</summary>
	/// <param name="siteId">The site identifier.</param>
	/// <param name="alert">The new alert.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The new alert's identifier and a link to it.</returns>
	[Post("api/3/sites/{siteId}/alerts/syslog")]
	Task<CreatedReference<int>> CreateSyslogAsync(int siteId, [Body] SyslogAlert alert, CancellationToken cancellationToken);

	/// <summary>Deletes all of a site's syslog alerts (<c>DELETE api/3/sites/{id}/alerts/syslog</c>).</summary>
	/// <param name="siteId">The site identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the site.</returns>
	[Delete("api/3/sites/{siteId}/alerts/syslog")]
	Task<LinksResource> DeleteAllSyslogAsync(int siteId, CancellationToken cancellationToken);

	/// <summary>Reads one of a site's syslog alerts (<c>GET api/3/sites/{id}/alerts/syslog/{alertId}</c>).</summary>
	/// <param name="siteId">The site identifier.</param>
	/// <param name="alertId">The alert identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The alert.</returns>
	[Get("api/3/sites/{siteId}/alerts/syslog/{alertId}")]
	Task<SyslogAlert> GetSyslogAsync(int siteId, int alertId, CancellationToken cancellationToken);

	/// <summary>Replaces one of a site's syslog alerts (<c>PUT api/3/sites/{id}/alerts/syslog/{alertId}</c>).</summary>
	/// <param name="siteId">The site identifier.</param>
	/// <param name="alertId">The alert identifier.</param>
	/// <param name="alert">The alert's new settings.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the alert.</returns>
	[Put("api/3/sites/{siteId}/alerts/syslog/{alertId}")]
	Task<LinksResource> UpdateSyslogAsync(int siteId, int alertId, [Body] SyslogAlert alert, CancellationToken cancellationToken);

	/// <summary>Deletes one of a site's syslog alerts (<c>DELETE api/3/sites/{id}/alerts/syslog/{alertId}</c>).</summary>
	/// <param name="siteId">The site identifier.</param>
	/// <param name="alertId">The alert identifier.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the site's syslog alerts.</returns>
	[Delete("api/3/sites/{siteId}/alerts/syslog/{alertId}")]
	Task<LinksResource> DeleteSyslogAsync(int siteId, int alertId, CancellationToken cancellationToken);
}
