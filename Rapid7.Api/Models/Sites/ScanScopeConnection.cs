using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Sites;

/// <summary>The discovery connection a dynamic site takes its assets from.</summary>
public sealed class ScanScopeConnection
{
	/// <summary>The identifier of the discovery connection.</summary>
	[JsonPropertyName("id")]
	public required long Id { get; init; }
}
