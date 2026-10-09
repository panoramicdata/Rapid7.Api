using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Cloud;

/// <summary>The network protocol of a vulnerable endpoint.</summary>
public enum CloudEndpointProtocol
{
	/// <summary>Not reported, or a value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>Internet Protocol (<c>IP</c>).</summary>
	[JsonStringEnumMemberName("IP")]
	Ip,

	/// <summary>Internet Control Message Protocol (<c>ICMP</c>).</summary>
	[JsonStringEnumMemberName("ICMP")]
	Icmp,

	/// <summary>Internet Group Management Protocol (<c>IGMP</c>).</summary>
	[JsonStringEnumMemberName("IGMP")]
	Igmp,

	/// <summary>Gateway-to-Gateway Protocol (<c>GGP</c>).</summary>
	[JsonStringEnumMemberName("GGP")]
	Ggp,

	/// <summary>Transmission Control Protocol (<c>TCP</c>).</summary>
	[JsonStringEnumMemberName("TCP")]
	Tcp,

	/// <summary>PARC Universal Packet protocol (<c>PUP</c>).</summary>
	[JsonStringEnumMemberName("PUP")]
	Pup,

	/// <summary>User Datagram Protocol (<c>UDP</c>).</summary>
	[JsonStringEnumMemberName("UDP")]
	Udp,

	/// <summary>Internet Datagram Protocol (<c>IDP</c>).</summary>
	[JsonStringEnumMemberName("IDP")]
	Idp,

	/// <summary>Encapsulating Security Payload (<c>ESP</c>).</summary>
	[JsonStringEnumMemberName("ESP")]
	Esp,

	/// <summary>Network Disk protocol (<c>ND</c>).</summary>
	[JsonStringEnumMemberName("ND")]
	Nd,

	/// <summary>Raw IP packets (<c>RAW</c>).</summary>
	[JsonStringEnumMemberName("RAW")]
	Raw
}
