using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Sites;

/// <summary>What a scan schedule does when its next run comes round while the previous scan is still running.</summary>
public enum ScanScheduleOverlap
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>Stop the running scan and start it again.</summary>
	[JsonStringEnumMemberName("restart-scan")]
	RestartScan,

	/// <summary>Let the running scan carry on.</summary>
	[JsonStringEnumMemberName("resume-scan")]
	ResumeScan
}
