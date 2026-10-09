using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Cloud;

/// <summary>A custom property to set on a scan engine.</summary>
public sealed class CloudScanEngineProperty
{
	/// <summary>The property name, such as <c>com.rapid7.property1</c>.</summary>
	[JsonPropertyName("name")]
	public required string Name { get; init; }

	/// <summary>The value to set.</summary>
	[JsonPropertyName("value")]
	public required string Value { get; init; }
}
