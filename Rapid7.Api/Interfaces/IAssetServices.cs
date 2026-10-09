using Rapid7.Api.Models;
using Rapid7.Api.Models.Assets;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>
/// The services discovered on an asset and what was enumerated through each
/// (<c>api/3/assets/{id}/services/{protocol}/{port}/...</c>).
/// </summary>
public interface IAssetServices
{
	/// <summary>Lists references to the services discovered on an asset (<c>GET api/3/assets/{id}/services</c>).</summary>
	/// <param name="assetId">The identifier of the asset.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Each service's protocol, port and network interface, with links to its details.</returns>
	[Get("api/3/assets/{assetId}/services")]
	Task<ResourceList<ServiceReference>> ListAsync(long assetId, CancellationToken cancellationToken);

	/// <summary>Reads a service of an asset (<c>GET api/3/assets/{id}/services/{protocol}/{port}</c>).</summary>
	/// <param name="assetId">The identifier of the asset.</param>
	/// <param name="protocol">The protocol of the service.</param>
	/// <param name="port">The port of the service.</param>
	/// <param name="options">The network interface, to pick one of several services on the port, or <see langword="null"/>.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The service.</returns>
	[Get("api/3/assets/{assetId}/services/{protocol}/{port}")]
	Task<Service> GetAsync(long assetId, ServiceProtocol protocol, int port, [Query] AssetServiceOptions? options, CancellationToken cancellationToken);

	/// <summary>Lists the settings enumerated on a service (<c>GET api/3/assets/{id}/services/{protocol}/{port}/configurations</c>).</summary>
	/// <param name="assetId">The identifier of the asset.</param>
	/// <param name="protocol">The protocol of the service.</param>
	/// <param name="port">The port of the service.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The settings, as name and value pairs.</returns>
	[Get("api/3/assets/{assetId}/services/{protocol}/{port}/configurations")]
	Task<ResourceList<Configuration>> ListConfigurationsAsync(long assetId, ServiceProtocol protocol, int port, CancellationToken cancellationToken);

	/// <summary>Lists the databases enumerated through a service (<c>GET api/3/assets/{id}/services/{protocol}/{port}/databases</c>).</summary>
	/// <param name="assetId">The identifier of the asset.</param>
	/// <param name="protocol">The protocol of the service.</param>
	/// <param name="port">The port of the service.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The databases.</returns>
	[Get("api/3/assets/{assetId}/services/{protocol}/{port}/databases")]
	Task<ResourceList<Database>> ListDatabasesAsync(long assetId, ServiceProtocol protocol, int port, CancellationToken cancellationToken);

	/// <summary>Lists the group accounts enumerated through a service (<c>GET api/3/assets/{id}/services/{protocol}/{port}/user_groups</c>).</summary>
	/// <param name="assetId">The identifier of the asset.</param>
	/// <param name="protocol">The protocol of the service.</param>
	/// <param name="port">The port of the service.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The group accounts.</returns>
	[Get("api/3/assets/{assetId}/services/{protocol}/{port}/user_groups")]
	Task<ResourceList<GroupAccount>> ListUserGroupsAsync(long assetId, ServiceProtocol protocol, int port, CancellationToken cancellationToken);

	/// <summary>Lists the user accounts enumerated through a service (<c>GET api/3/assets/{id}/services/{protocol}/{port}/users</c>).</summary>
	/// <param name="assetId">The identifier of the asset.</param>
	/// <param name="protocol">The protocol of the service.</param>
	/// <param name="port">The port of the service.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The user accounts.</returns>
	[Get("api/3/assets/{assetId}/services/{protocol}/{port}/users")]
	Task<ResourceList<UserAccount>> ListUsersAsync(long assetId, ServiceProtocol protocol, int port, CancellationToken cancellationToken);

	/// <summary>
	/// Lists the identifiers of the web applications found on a service
	/// (<c>GET api/3/assets/{id}/services/{protocol}/{port}/web_applications</c>).
	/// </summary>
	/// <param name="assetId">The identifier of the asset.</param>
	/// <param name="protocol">The protocol of the service.</param>
	/// <param name="port">The port of the service.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The web application identifiers.</returns>
	[Get("api/3/assets/{assetId}/services/{protocol}/{port}/web_applications")]
	Task<ResourceList<WebApplicationReference>> ListWebApplicationsAsync(long assetId, ServiceProtocol protocol, int port, CancellationToken cancellationToken);

	/// <summary>
	/// Reads a web application found on a service, with the pages the spider found
	/// (<c>GET api/3/assets/{id}/services/{protocol}/{port}/web_applications/{webApplicationId}</c>).
	/// </summary>
	/// <param name="assetId">The identifier of the asset.</param>
	/// <param name="protocol">The protocol of the service.</param>
	/// <param name="port">The port of the service.</param>
	/// <param name="webApplicationId">The identifier of the web application.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The web application.</returns>
	[Get("api/3/assets/{assetId}/services/{protocol}/{port}/web_applications/{webApplicationId}")]
	Task<WebApplication> GetWebApplicationAsync(long assetId, ServiceProtocol protocol, int port, long webApplicationId, CancellationToken cancellationToken);
}
