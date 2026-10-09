using Rapid7.Api.Interfaces;

namespace Rapid7.Api;

public sealed partial class Rapid7Client
{
	/// <summary>Discovery connections to external asset sources (<c>api/3/discovery_connections</c>).</summary>
	public IDiscoveryConnections DiscoveryConnections => field ??= For<IDiscoveryConnections>();

	/// <summary>Sonar queries (<c>api/3/sonar_queries</c>).</summary>
	public ISonarQueries SonarQueries => field ??= For<ISonarQueries>();
}
