using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.ScanTemplates;

/// <summary>How a scan probes TCP ports.</summary>
public enum TcpDiscoveryMethod
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>Half-open probes: a SYN packet only.</summary>
	[JsonStringEnumMemberName("SYN")]
	Syn,

	/// <summary>A SYN packet followed by a reset.</summary>
	[JsonStringEnumMemberName("SYN+RST")]
	SynRst,

	/// <summary>A SYN packet with the FIN flag set.</summary>
	[JsonStringEnumMemberName("SYN+FIN")]
	SynFin,

	/// <summary>A SYN packet with the ECE flag set.</summary>
	[JsonStringEnumMemberName("SYN+ECE")]
	SynEce,

	/// <summary>Full TCP connections.</summary>
	[JsonStringEnumMemberName("Full")]
	Full
}
