using Rapid7.Api.Interfaces;

namespace Rapid7.Api;

public sealed partial class Rapid7Client
{
	/// <summary>The API root, listing the available resources (<c>api/3</c>).</summary>
	public IRoot Root => field ??= For<IRoot>();
}
