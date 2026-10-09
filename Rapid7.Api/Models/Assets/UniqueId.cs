using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Assets;

/// <summary>A unique identifier found on an asset, such as a hardware or operating system identifier.</summary>
public sealed class UniqueId
{
	/// <summary>The identifier.</summary>
	[JsonPropertyName("id")]
	public string Id { get; init; } = string.Empty;

	/// <summary>Where the identifier came from, such as <c>WQL</c> or <c>R7 Agent</c>.</summary>
	[JsonPropertyName("source")]
	public string? Source { get; init; }
}
