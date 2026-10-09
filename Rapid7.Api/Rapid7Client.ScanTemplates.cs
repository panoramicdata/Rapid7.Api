using Rapid7.Api.Interfaces;

namespace Rapid7.Api;

public sealed partial class Rapid7Client
{
	/// <summary>Scan templates (<c>api/3/scan_templates</c>).</summary>
	public IScanTemplates ScanTemplates => field ??= For<IScanTemplates>();
}
