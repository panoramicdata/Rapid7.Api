using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Reports;

/// <summary>Where the generation of a report instance stands.</summary>
public enum ReportInstanceStatus
{
	/// <summary>Unknown (also used for values this library does not recognise).</summary>
	[JsonStringEnumMemberName("unknown")]
	Unknown = 0,

	/// <summary>Generation was aborted.</summary>
	[JsonStringEnumMemberName("aborted")]
	Aborted,

	/// <summary>Generation failed.</summary>
	[JsonStringEnumMemberName("failed")]
	Failed,

	/// <summary>Generation finished.</summary>
	[JsonStringEnumMemberName("complete")]
	Complete,

	/// <summary>Generation is in progress.</summary>
	[JsonStringEnumMemberName("running")]
	Running
}
