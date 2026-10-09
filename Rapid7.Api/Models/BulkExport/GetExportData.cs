using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.BulkExport;

/// <summary>The data of an <c>export</c> query response.</summary>
public sealed class GetExportData
{
	/// <summary>The export; absent when there is none with the identifier.</summary>
	[JsonPropertyName("export")]
	public Export? Export { get; init; }
}
