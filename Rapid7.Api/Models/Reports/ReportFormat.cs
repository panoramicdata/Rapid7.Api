using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Reports;

/// <summary>The output format of a report.</summary>
public enum ReportFormat
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>Asset Reporting Format (ARF) XML.</summary>
	[JsonStringEnumMemberName("arf-xml")]
	ArfXml,

	/// <summary>CSV export.</summary>
	[JsonStringEnumMemberName("csv-export")]
	CsvExport,

	/// <summary>CyberScope XML.</summary>
	[JsonStringEnumMemberName("cyberscope-xml")]
	CyberScopeXml,

	/// <summary>PDF.</summary>
	[JsonStringEnumMemberName("pdf")]
	Pdf,

	/// <summary>HTML.</summary>
	[JsonStringEnumMemberName("html")]
	Html,

	/// <summary>Nexpose simple XML.</summary>
	[JsonStringEnumMemberName("nexpose-simple-xml")]
	NexposeSimpleXml,

	/// <summary>OVAL XML.</summary>
	[JsonStringEnumMemberName("oval-xml")]
	OvalXml,

	/// <summary>Qualys XML.</summary>
	[JsonStringEnumMemberName("qualys-xml")]
	QualysXml,

	/// <summary>Rich Text Format.</summary>
	[JsonStringEnumMemberName("rtf")]
	Rtf,

	/// <summary>SCAP XML.</summary>
	[JsonStringEnumMemberName("scap-xml")]
	ScapXml,

	/// <summary>The CSV result of a SQL query against the reporting data model.</summary>
	[JsonStringEnumMemberName("sql-query")]
	SqlQuery,

	/// <summary>Plain text.</summary>
	[JsonStringEnumMemberName("text")]
	Text,

	/// <summary>XCCDF XML.</summary>
	[JsonStringEnumMemberName("xccdf-xml")]
	XccdfXml,

	/// <summary>XCCDF CSV.</summary>
	[JsonStringEnumMemberName("xccdf-csv")]
	XccdfCsv,

	/// <summary>XML.</summary>
	[JsonStringEnumMemberName("xml")]
	Xml,

	/// <summary>XML export.</summary>
	[JsonStringEnumMemberName("xml-export")]
	XmlExport,

	/// <summary>XML export, version 2.</summary>
	[JsonStringEnumMemberName("xml-export-v2")]
	XmlExportV2
}
