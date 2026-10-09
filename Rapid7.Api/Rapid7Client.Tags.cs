using Rapid7.Api.Interfaces;

namespace Rapid7.Api;

public sealed partial class Rapid7Client
{
	/// <summary>Tags and their search criteria (<c>api/3/tags</c>).</summary>
	public ITags Tags => field ??= For<ITags>();

	/// <summary>The assets, asset groups and sites tags apply to (<c>api/3/tags/{id}/assets</c>, <c>asset_groups</c>, <c>sites</c>).</summary>
	public ITagMembers TagMembers => field ??= For<ITagMembers>();
}
