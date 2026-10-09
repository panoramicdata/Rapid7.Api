using Refit;

namespace Rapid7.Api.Models.Assets;

/// <summary>Optional query parameters for reading one service of an asset.</summary>
public sealed class AssetServiceOptions
{
	/// <summary>The network interface the service listens on (<c>nic</c>), to pick one of several services on the same port.</summary>
	[AliasAs("nic")]
	public string? Nic { get; init; }
}
