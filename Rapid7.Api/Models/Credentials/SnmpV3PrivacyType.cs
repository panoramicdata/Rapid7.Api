using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Credentials;

/// <summary>The SNMP v3 privacy (encryption) protocol.</summary>
public enum SnmpV3PrivacyType
{
	/// <summary>A value this client does not recognise.</summary>
	Unknown = 0,

	/// <summary>No privacy (<c>no-privacy</c>).</summary>
	[JsonStringEnumMemberName("no-privacy")]
	NoPrivacy,

	/// <summary>DES (<c>des</c>).</summary>
	[JsonStringEnumMemberName("des")]
	Des,

	/// <summary>AES-128 (<c>aes-128</c>).</summary>
	[JsonStringEnumMemberName("aes-128")]
	Aes128,

	/// <summary>AES-192 (<c>aes-192</c>).</summary>
	[JsonStringEnumMemberName("aes-192")]
	Aes192,

	/// <summary>AES-192 with the 3DES key extension (<c>aes-192-with-3-des-key-extension</c>).</summary>
	[JsonStringEnumMemberName("aes-192-with-3-des-key-extension")]
	Aes192With3DesKeyExtension,

	/// <summary>AES-256 (<c>aes-256</c>).</summary>
	[JsonStringEnumMemberName("aes-256")]
	Aes256,

	/// <summary>AES-256 with the 3DES key extension (<c>aes-256-with-3-des-key-extension</c>).</summary>
	[JsonStringEnumMemberName("aes-256-with-3-des-key-extension")]
	Aes256With3DesKeyExtension
}
