using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Assets;

/// <summary>A host name of an asset, and how it was discovered.</summary>
public sealed class HostName
{
	/// <summary>The local or fully qualified host name.</summary>
	[JsonPropertyName("name")]
	public string Name { get; init; } = string.Empty;

	/// <summary>How the host name was discovered.</summary>
	[JsonPropertyName("source")]
	public HostNameSource? Source { get; init; }
}
