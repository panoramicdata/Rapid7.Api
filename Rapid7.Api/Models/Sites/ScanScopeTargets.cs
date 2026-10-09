using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Sites;

/// <summary>A list of scan target addresses within a scan scope.</summary>
public sealed class ScanScopeTargets
{
	/// <summary>
	/// The addresses: each a host name, an IPv4 or IPv6 address, an address range (<c>192.0.2.1 - 192.0.2.50</c>) or a CIDR
	/// block.
	/// </summary>
	[JsonPropertyName("addresses")]
	public IReadOnlyList<string> Addresses { get; init; } = [];
}
