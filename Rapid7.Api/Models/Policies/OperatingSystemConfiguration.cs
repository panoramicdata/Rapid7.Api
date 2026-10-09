using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Policies;

/// <summary>A configuration name and value found on an operating system.</summary>
public sealed class OperatingSystemConfiguration
{
	/// <summary>The configuration name.</summary>
	[JsonPropertyName("name")]
	public string Name { get; init; } = string.Empty;

	/// <summary>The configuration value.</summary>
	[JsonPropertyName("value")]
	public string? Value { get; init; }
}
