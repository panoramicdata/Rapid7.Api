using Rapid7.Api.Interfaces;

namespace Rapid7.Api;

public sealed partial class Rapid7Client
{
	/// <summary>Scans across the console and per site, and their status (<c>api/3/scans</c>, <c>api/3/sites/{id}/scans</c>).</summary>
	public IScans Scans => field ??= For<IScans>();
}
