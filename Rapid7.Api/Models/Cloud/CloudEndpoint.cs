using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Cloud;

/// <summary>The port and protocol a vulnerability finding was made on.</summary>
public sealed class CloudEndpoint
{
	/// <summary>The port.</summary>
	[JsonPropertyName("port")]
	public long? Port { get; init; }

	/// <summary>The protocol.</summary>
	[JsonPropertyName("protocol")]
	public CloudEndpointProtocol Protocol { get; init; }
}
