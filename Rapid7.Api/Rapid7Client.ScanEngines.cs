using Rapid7.Api.Interfaces;

namespace Rapid7.Api;

public sealed partial class Rapid7Client
{
	/// <summary>Scan engines and their pools, scans and sites (<c>api/3/scan_engines</c>).</summary>
	public IScanEngines ScanEngines => field ??= For<IScanEngines>();

	/// <summary>The scan engine pairing shared secret (<c>api/3/scan_engines/shared_secret</c>).</summary>
	public IScanEngineSharedSecret ScanEngineSharedSecret => field ??= For<IScanEngineSharedSecret>();

	/// <summary>Engine pools and their engines and sites (<c>api/3/scan_engine_pools</c>).</summary>
	public IScanEnginePools ScanEnginePools => field ??= For<IScanEnginePools>();
}
