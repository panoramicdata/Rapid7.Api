using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Assets;

/// <summary>A name and value pair enumerated on an asset, service, file or piece of software.</summary>
public sealed class Configuration
{
	/// <summary>The name of the setting.</summary>
	[JsonPropertyName("name")]
	public string Name { get; init; } = string.Empty;

	/// <summary>The value of the setting.</summary>
	[JsonPropertyName("value")]
	public string? Value { get; init; }
}
