using Rapid7.Api.Interfaces;

namespace Rapid7.Api;

public sealed partial class Rapid7Client
{
	/// <summary>Shared scan credentials, whose secrets are write-only (<c>api/3/shared_credentials</c>).</summary>
	public ISharedCredentials SharedCredentials => field ??= For<ISharedCredentials>();
}
