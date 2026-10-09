using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Reports;

/// <summary>When a report is generated.</summary>
public enum ReportFrequencyType
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>After each scan of its scope.</summary>
	[JsonStringEnumMemberName("scan")]
	Scan,

	/// <summary>On a schedule.</summary>
	[JsonStringEnumMemberName("schedule")]
	Schedule,

	/// <summary>Only on demand.</summary>
	[JsonStringEnumMemberName("none")]
	None
}
