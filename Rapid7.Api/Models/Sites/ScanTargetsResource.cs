using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Sites;

/// <summary>The included or excluded scan targets of a static site.</summary>
public sealed class ScanTargetsResource : LinksResource
{
	/// <summary>
	/// The targets, each a host name, an IPv4 or IPv6 address, an IPv4 range (<c>10.0.0.1 - 10.0.0.9</c>) or a CIDR block.
	/// </summary>
	[JsonPropertyName("addresses")]
	public IReadOnlyList<string> Addresses { get; init; } = [];
}
