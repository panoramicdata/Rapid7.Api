using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Cloud;

/// <summary>An identifier found on an asset, such as a hardware, agent or operating system identifier.</summary>
public sealed class CloudUniqueIdentifier
{
	/// <summary>The identifier.</summary>
	[JsonPropertyName("id")]
	public string? Id { get; init; }

	/// <summary>Where the identifier came from, such as <c>R7 Agent</c>.</summary>
	[JsonPropertyName("source")]
	public string? Source { get; init; }
}
