using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Reports;

/// <summary>One generation (history entry) of a report.</summary>
public sealed class ReportInstance : LinksResource
{
	/// <summary>The identifier of the report instance.</summary>
	[JsonPropertyName("id")]
	public int? Id { get; init; }

	/// <summary>Where generation stands.</summary>
	[JsonPropertyName("status")]
	public ReportInstanceStatus? Status { get; init; }

	/// <summary>When generation finished.</summary>
	[JsonPropertyName("generated")]
	public DateTimeOffset? Generated { get; init; }

	/// <summary>The size of the generated report.</summary>
	[JsonPropertyName("size")]
	public ReportSize? Size { get; init; }

	/// <summary>The address of the report in the console's web interface; use the download operation for the file itself.</summary>
	[JsonPropertyName("uri")]
	public Uri? Uri { get; init; }
}
