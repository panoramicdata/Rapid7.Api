using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Cloud;

/// <summary>A confirmation message, the answer to changing a scan engine configuration.</summary>
public sealed class CloudMessage
{
	/// <summary>The message.</summary>
	[JsonPropertyName("message")]
	public string? Message { get; init; }
}
