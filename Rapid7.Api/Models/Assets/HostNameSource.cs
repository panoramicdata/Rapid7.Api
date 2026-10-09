using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Assets;

/// <summary>How a host name was discovered.</summary>
public enum HostNameSource
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>Supplied by a user, for example in the targets of a site.</summary>
	[JsonStringEnumMemberName("user")]
	User,

	/// <summary>Resolved through DNS.</summary>
	[JsonStringEnumMemberName("dns")]
	Dns,

	/// <summary>Discovered through NetBIOS.</summary>
	[JsonStringEnumMemberName("netbios")]
	NetBios,

	/// <summary>Discovered through DCE/RPC.</summary>
	[JsonStringEnumMemberName("dce")]
	Dce,

	/// <summary>Reported by endpoint security software.</summary>
	[JsonStringEnumMemberName("epsec")]
	EndpointSecurity,

	/// <summary>Discovered through LDAP.</summary>
	[JsonStringEnumMemberName("ldap")]
	Ldap,

	/// <summary>Another source.</summary>
	[JsonStringEnumMemberName("other")]
	Other
}
