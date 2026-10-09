using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Assets;

/// <summary>A network address discovered on an asset.</summary>
public sealed class Address
{
	/// <summary>The IPv4 or IPv6 address.</summary>
	[JsonPropertyName("ip")]
	public string? Ip { get; init; }

	/// <summary>The Media Access Control (MAC) address, as six colon-separated pairs of hexadecimal digits.</summary>
	[JsonPropertyName("mac")]
	public string? Mac { get; init; }
}
