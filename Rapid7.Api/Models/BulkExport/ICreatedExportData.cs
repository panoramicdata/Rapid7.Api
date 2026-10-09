namespace Rapid7.Api.Models.BulkExport;

/// <summary>The data of a create-export mutation's response: the export it started.</summary>
internal interface ICreatedExportData
{
	/// <summary>The export created.</summary>
	ExportReference? Export { get; }
}
