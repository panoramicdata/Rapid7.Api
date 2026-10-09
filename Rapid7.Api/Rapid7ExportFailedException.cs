using Rapid7.Api.Models.BulkExport;

namespace Rapid7.Api;

/// <summary>Raised when a bulk export that was being waited for ends with the status <c>FAILED</c>.</summary>
/// <param name="export">The failed export, as last reported.</param>
public sealed class Rapid7ExportFailedException(Export export)
	: Exception($"Bulk export {export?.Id} failed.")
{
	/// <summary>The failed export, as last reported.</summary>
	public Export Export { get; } = export ?? throw new ArgumentNullException(nameof(export));
}
