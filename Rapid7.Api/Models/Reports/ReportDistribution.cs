using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Reports;

/// <summary>How a generated report is delivered by email.</summary>
public enum ReportDistribution
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>As an attached file.</summary>
	[JsonStringEnumMemberName("file")]
	File,

	/// <summary>As an attached zip archive.</summary>
	[JsonStringEnumMemberName("zip")]
	Zip,

	/// <summary>As a link to the report (not available for additional recipients).</summary>
	[JsonStringEnumMemberName("url")]
	Url,

	/// <summary>Not delivered.</summary>
	[JsonStringEnumMemberName("none")]
	None
}
