using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Reports;

/// <summary>Which vulnerability severities a report includes.</summary>
public enum ReportSeverityFilter
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>Every severity.</summary>
	[JsonStringEnumMemberName("all")]
	All,

	/// <summary>Critical only.</summary>
	[JsonStringEnumMemberName("critical")]
	Critical,

	/// <summary>Critical and severe.</summary>
	[JsonStringEnumMemberName("critical-and-severe")]
	CriticalAndSevere
}
