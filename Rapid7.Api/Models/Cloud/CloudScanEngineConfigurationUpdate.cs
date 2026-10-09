using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Cloud;

/// <summary>The custom properties to set on a scan engine (<c>POST v4/integration/scan/engine/{id}/configuration</c>).</summary>
public sealed class CloudScanEngineConfigurationUpdate
{
	/// <summary>The properties to set.</summary>
	[JsonPropertyName("properties")]
	public required IReadOnlyList<CloudScanEngineProperty> Properties { get; init; }
}
