using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Scans;

/// <summary>A change to request in a scan's state (<c>POST api/3/scans/{id}/{status}</c>).</summary>
public enum ScanStatusChange
{
	/// <summary>No change; the console rejects it. Present only as the default value.</summary>
	Unknown = 0,

	/// <summary>Pause a running scan.</summary>
	[JsonStringEnumMemberName("pause")]
	Pause,

	/// <summary>Stop a running or paused scan.</summary>
	[JsonStringEnumMemberName("stop")]
	Stop,

	/// <summary>Resume a paused scan.</summary>
	[JsonStringEnumMemberName("resume")]
	Resume
}
