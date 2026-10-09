using Rapid7.Api.Interfaces;

namespace Rapid7.Api;

public sealed partial class Rapid7Client
{
	/// <summary>Static and dynamic asset groups and their search criteria (<c>api/3/asset_groups</c>).</summary>
	public IAssetGroups AssetGroups => field ??= For<IAssetGroups>();

	/// <summary>The assets in asset groups and the users who can access them (<c>api/3/asset_groups/{id}/assets</c>, <c>.../users</c>).</summary>
	public IAssetGroupMembers AssetGroupMembers => field ??= For<IAssetGroupMembers>();

	/// <summary>The tags applied to asset groups (<c>api/3/asset_groups/{id}/tags</c>).</summary>
	public IAssetGroupTags AssetGroupTags => field ??= For<IAssetGroupTags>();

	/// <summary>Assets assessed by Insight Agents (<c>api/3/agents</c>).</summary>
	public IAgents Agents => field ??= For<IAgents>();
}
