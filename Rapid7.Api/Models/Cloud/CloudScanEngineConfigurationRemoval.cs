using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Cloud;

/// <summary>
/// The custom properties to remove from a scan engine (<c>DELETE v4/integration/scan/engine/{id}/configuration</c>).
/// </summary>
public sealed class CloudScanEngineConfigurationRemoval
{
	/// <summary>The names of the properties to remove, such as <c>com.rapid7.exampleProperty1</c>.</summary>
	[JsonPropertyName("properties")]
	public required IReadOnlyList<string> Properties { get; init; }
}
