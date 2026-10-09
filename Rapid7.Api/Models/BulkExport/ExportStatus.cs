using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.BulkExport;

/// <summary>Where a bulk export is in its life cycle.</summary>
public enum ExportStatus
{
	/// <summary>Not reported, or a value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>Accepted and waiting to start (<c>PENDING</c>).</summary>
	[JsonStringEnumMemberName("PENDING")]
	Pending,

	/// <summary>Being produced (<c>PROCESSING</c>).</summary>
	[JsonStringEnumMemberName("PROCESSING")]
	Processing,

	/// <summary>Finished: the files can be downloaded (<c>SUCCEEDED</c>).</summary>
	[JsonStringEnumMemberName("SUCCEEDED")]
	Succeeded,

	/// <summary>Failed: no files were produced (<c>FAILED</c>).</summary>
	[JsonStringEnumMemberName("FAILED")]
	Failed
}
