using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.ScanTemplates;

/// <summary>How a scan template probes TCP ports.</summary>
public enum ScanTemplateTcpMethod
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>A SYN (half-open) scan.</summary>
	[JsonStringEnumMemberName("SYN")]
	Syn,

	/// <summary>A SYN scan that sends a reset after each answer.</summary>
	[JsonStringEnumMemberName("SYN+RST")]
	SynRst,

	/// <summary>A SYN scan with the FIN flag set.</summary>
	[JsonStringEnumMemberName("SYN+FIN")]
	SynFin,

	/// <summary>A SYN scan with the ECE flag set.</summary>
	[JsonStringEnumMemberName("SYN+ECE")]
	SynEce,

	/// <summary>A full connection.</summary>
	[JsonStringEnumMemberName("Full")]
	Full
}
