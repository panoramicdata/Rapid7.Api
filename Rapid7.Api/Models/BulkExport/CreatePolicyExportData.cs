using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.BulkExport;

/// <summary>The data of a <c>createPolicyExport</c> response.</summary>
public sealed class CreatePolicyExportData
{
	/// <summary>The export created.</summary>
	[JsonPropertyName("createPolicyExport")]
	public ExportReference? Export { get; init; }
}
