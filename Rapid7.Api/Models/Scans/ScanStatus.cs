using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Scans;

/// <summary>The state of a scan.</summary>
public enum ScanStatus
{
	/// <summary>The console does not know the scan's state (also any value this library does not recognise).</summary>
	[JsonStringEnumMemberName("unknown")]
	Unknown = 0,

	/// <summary>The scan ended abnormally, for example because the console restarted.</summary>
	[JsonStringEnumMemberName("aborted")]
	Aborted,

	/// <summary>The scan is in progress.</summary>
	[JsonStringEnumMemberName("running")]
	Running,

	/// <summary>The scan completed.</summary>
	[JsonStringEnumMemberName("finished")]
	Finished,

	/// <summary>A user stopped the scan.</summary>
	[JsonStringEnumMemberName("stopped")]
	Stopped,

	/// <summary>The scan failed.</summary>
	[JsonStringEnumMemberName("error")]
	Error,

	/// <summary>The scan is paused and can be resumed.</summary>
	[JsonStringEnumMemberName("paused")]
	Paused,

	/// <summary>The scan has been handed to its scan engine.</summary>
	[JsonStringEnumMemberName("dispatched")]
	Dispatched,

	/// <summary>The scan's results are being integrated into the console's data.</summary>
	[JsonStringEnumMemberName("integrating")]
	Integrating
}
