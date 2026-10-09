using Rapid7.Api.Interfaces;

namespace Rapid7.Api;

public sealed partial class Rapid7Client
{
	/// <summary>Sites: list, create, read, update and delete (<c>api/3/sites</c>).</summary>
	public ISites Sites => field ??= For<ISites>();

	/// <summary>A site's SMTP, SNMP and syslog alerts (<c>api/3/sites/{id}/alerts</c>).</summary>
	public ISiteAlerts SiteAlerts => field ??= For<ISiteAlerts>();

	/// <summary>A site's scan schedules (<c>api/3/sites/{id}/scan_schedules</c>).</summary>
	public ISiteScanSchedules SiteScanSchedules => field ??= For<ISiteScanSchedules>();

	/// <summary>A site's organization and contact details (<c>api/3/sites/{id}/organization</c>).</summary>
	public ISiteOrganization SiteOrganization => field ??= For<ISiteOrganization>();

	/// <summary>The scan engine a site scans with (<c>api/3/sites/{id}/scan_engine</c>).</summary>
	public ISiteScanEngine SiteScanEngine => field ??= For<ISiteScanEngine>();

	/// <summary>The scan template a site scans with (<c>api/3/sites/{id}/scan_template</c>).</summary>
	public ISiteScanTemplate SiteScanTemplate => field ??= For<ISiteScanTemplate>();
}
