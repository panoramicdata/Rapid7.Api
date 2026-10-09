using Rapid7.Api.Interfaces;

namespace Rapid7.Api;

public sealed partial class Rapid7Client
{
	/// <summary>Console administration: info, licence, logs, properties, settings and commands (<c>api/3/administration</c>).</summary>
	public IAdministration Administration => field ??= For<IAdministration>();
}
