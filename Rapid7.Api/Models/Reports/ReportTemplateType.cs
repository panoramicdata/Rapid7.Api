using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Reports;

/// <summary>The kind of a report template.</summary>
public enum ReportTemplateType
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>A printable document made of sections.</summary>
	[JsonStringEnumMemberName("document")]
	Document,

	/// <summary>Data-oriented output.</summary>
	[JsonStringEnumMemberName("export")]
	Export,

	/// <summary>A file-based template.</summary>
	[JsonStringEnumMemberName("file")]
	File
}
