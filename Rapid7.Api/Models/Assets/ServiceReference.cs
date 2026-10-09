using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Assets;

/// <summary>A reference to a service on an asset: its protocol, port and network interface, with links to its details.</summary>
public sealed class ServiceReference : Links
{
	/// <summary>The network interface the service listens on, when known.</summary>
	[JsonPropertyName("nic")]
	public string? Nic { get; init; }

	/// <summary>The port of the service.</summary>
	[JsonPropertyName("port")]
	public int? Port { get; init; }

	/// <summary>The protocol of the service.</summary>
	[JsonPropertyName("protocol")]
	public ServiceProtocol? Protocol { get; init; }
}
