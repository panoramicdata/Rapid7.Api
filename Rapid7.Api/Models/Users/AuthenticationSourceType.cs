using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Users;

/// <summary>The kind of source that authenticates a user account.</summary>
public enum AuthenticationSourceType
{
	/// <summary>A value this client does not recognise.</summary>
	Unknown = 0,

	/// <summary>The console's own user store (<c>normal</c>).</summary>
	[JsonStringEnumMemberName("normal")]
	Normal,

	/// <summary>Kerberos (<c>kerberos</c>).</summary>
	[JsonStringEnumMemberName("kerberos")]
	Kerberos,

	/// <summary>An LDAP or Active Directory server (<c>ldap</c>).</summary>
	[JsonStringEnumMemberName("ldap")]
	Ldap,

	/// <summary>A SAML identity provider (<c>saml</c>).</summary>
	[JsonStringEnumMemberName("saml")]
	Saml,

	/// <summary>The built-in administrator source (<c>admin</c>).</summary>
	[JsonStringEnumMemberName("admin")]
	Admin
}
