using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Credentials;

/// <summary>The SNMP v3 authentication protocol.</summary>
public enum SnmpV3AuthenticationType
{
	/// <summary>A value this client does not recognise.</summary>
	Unknown = 0,

	/// <summary>No authentication (<c>no-authentication</c>).</summary>
	[JsonStringEnumMemberName("no-authentication")]
	NoAuthentication,

	/// <summary>MD5 (<c>md5</c>).</summary>
	[JsonStringEnumMemberName("md5")]
	Md5,

	/// <summary>SHA (<c>sha</c>).</summary>
	[JsonStringEnumMemberName("sha")]
	Sha
}
