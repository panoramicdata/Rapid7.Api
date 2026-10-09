using Rapid7.Api.Interfaces;

namespace Rapid7.Api;

public sealed partial class Rapid7Client
{
	/// <summary>Assets: listing, searching, reading, importing and deleting them (<c>api/3/assets</c>).</summary>
	public IAssets Assets => field ??= For<IAssets>();

	/// <summary>What was discovered on an asset, and the tags applied to it (<c>api/3/assets/{id}/...</c>).</summary>
	public IAssetDetails AssetDetails => field ??= For<IAssetDetails>();

	/// <summary>The services discovered on an asset (<c>api/3/assets/{id}/services</c>).</summary>
	public IAssetServices AssetServices => field ??= For<IAssetServices>();

	/// <summary>The operating system and software catalogues (<c>api/3/operating_systems</c>, <c>api/3/software</c>).</summary>
	public IAssetCatalog AssetCatalog => field ??= For<IAssetCatalog>();
}
