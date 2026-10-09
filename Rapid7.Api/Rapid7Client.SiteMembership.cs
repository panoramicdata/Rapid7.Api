using Rapid7.Api.Interfaces;

namespace Rapid7.Api;

public sealed partial class Rapid7Client
{
	/// <summary>A site's own and shared scan credentials (<c>api/3/sites/{id}/site_credentials</c>, <c>shared_credentials</c>).</summary>
	public ISiteCredentials SiteCredentials => field ??= For<ISiteCredentials>();

	/// <summary>A static site's included and excluded targets and asset groups (<c>api/3/sites/{id}/included_targets</c> and siblings).</summary>
	public ISiteTargets SiteTargets => field ??= For<ISiteTargets>();

	/// <summary>A site's assets (<c>api/3/sites/{id}/assets</c>).</summary>
	public ISiteAssets SiteAssets => field ??= For<ISiteAssets>();

	/// <summary>The tags applied to a site (<c>api/3/sites/{id}/tags</c>).</summary>
	public ISiteTags SiteTags => field ??= For<ISiteTags>();

	/// <summary>The users with access to a site (<c>api/3/sites/{id}/users</c>).</summary>
	public ISiteUsers SiteUsers => field ??= For<ISiteUsers>();

	/// <summary>A dynamic site's discovery connection and search (<c>api/3/sites/{id}/discovery_connection</c>, <c>discovery_search_criteria</c>).</summary>
	public ISiteDiscovery SiteDiscovery => field ??= For<ISiteDiscovery>();

	/// <summary>A site's web application authentications (<c>api/3/sites/{id}/web_authentication</c>).</summary>
	public ISiteWebAuthentication SiteWebAuthentication => field ??= For<ISiteWebAuthentication>();
}
