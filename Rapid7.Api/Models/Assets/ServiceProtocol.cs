using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Assets;

/// <summary>The network protocol of a service.</summary>
public enum ServiceProtocol
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>Internet Protocol.</summary>
	[JsonStringEnumMemberName("ip")]
	Ip,

	/// <summary>Internet Control Message Protocol.</summary>
	[JsonStringEnumMemberName("icmp")]
	Icmp,

	/// <summary>Internet Group Management Protocol.</summary>
	[JsonStringEnumMemberName("igmp")]
	Igmp,

	/// <summary>Gateway-to-Gateway Protocol.</summary>
	[JsonStringEnumMemberName("ggp")]
	Ggp,

	/// <summary>Transmission Control Protocol.</summary>
	[JsonStringEnumMemberName("tcp")]
	Tcp,

	/// <summary>PARC Universal Packet.</summary>
	[JsonStringEnumMemberName("pup")]
	Pup,

	/// <summary>User Datagram Protocol.</summary>
	[JsonStringEnumMemberName("udp")]
	Udp,

	/// <summary>Internet Datagram Protocol.</summary>
	[JsonStringEnumMemberName("idp")]
	Idp,

	/// <summary>Encapsulating Security Payload.</summary>
	[JsonStringEnumMemberName("esp")]
	Esp,

	/// <summary>Network Disk protocol.</summary>
	[JsonStringEnumMemberName("nd")]
	Nd,

	/// <summary>Raw IP packets.</summary>
	[JsonStringEnumMemberName("raw")]
	Raw
}
