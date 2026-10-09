using Rapid7.Api.Interfaces;

namespace Rapid7.Api;

public sealed partial class Rapid7Client
{
	/// <summary>Policy overrides, their review and expiry (<c>api/3/policy_overrides</c>).</summary>
	public IPolicyOverrides PolicyOverrides => field ??= For<IPolicyOverrides>();
}
