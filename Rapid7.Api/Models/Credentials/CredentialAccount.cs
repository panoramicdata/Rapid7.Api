using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Credentials;

/// <summary>
/// The service a scan credential authenticates to and the account details it needs. Which properties apply depends on
/// <see cref="Service"/> (see <see cref="CredentialService"/>); leave the others <see langword="null"/>.
/// </summary>
/// <remarks>
/// Every property documented as a secret (passwords, hashes, community names, private keys) is write-only: the console
/// accepts it but never returns it, so it reads as <see langword="null"/>. The client never logs request bodies.
/// </remarks>
public sealed class CredentialAccount
{
	/// <summary>The service to authenticate to.</summary>
	[JsonPropertyName("service")]
	public CredentialService? Service { get; init; }

	/// <summary>The account's user name.</summary>
	[JsonPropertyName("username")]
	public string? Username { get; init; }

	/// <summary>The account's password. A secret; write-only.</summary>
	[JsonPropertyName("password")]
	public string? Password { get; init; }

	/// <summary>The domain (AS/400, CIFS, CIFS hash, CVS, Microsoft SQL Server, Sybase).</summary>
	[JsonPropertyName("domain")]
	public string? Domain { get; init; }

	/// <summary>The database name (DB2, HANA, Microsoft SQL Server, MySQL, PostgreSQL, Sybase).</summary>
	[JsonPropertyName("database")]
	public string? Database { get; init; }

	/// <summary>The realm (HTTP, Kerberos).</summary>
	[JsonPropertyName("realm")]
	public string? Realm { get; init; }

	/// <summary>Whether to sign in to the database with Windows authentication (Microsoft SQL Server, Sybase).</summary>
	[JsonPropertyName("useWindowsAuthentication")]
	public bool? UseWindowsAuthentication { get; init; }

	/// <summary>The NTLM password hash (CIFS hash). A secret; write-only.</summary>
	[JsonPropertyName("ntlmHash")]
	public string? NtlmHash { get; init; }

	/// <summary>The Notes ID password (Lotus Notes). A secret; write-only.</summary>
	[JsonPropertyName("notesIDPassword")]
	public string? NotesIdPassword { get; init; }

	/// <summary>The Oracle SID (database name); a default is used when <see langword="null"/>.</summary>
	[JsonPropertyName("sid")]
	public string? Sid { get; init; }

	/// <summary>Whether to try to enumerate Oracle SIDs, which needs <see cref="OracleListenerPassword"/>.</summary>
	[JsonPropertyName("enumerateSids")]
	public bool? EnumerateSids { get; init; }

	/// <summary>The Oracle Net Listener password, used to enumerate SIDs. A secret; write-only.</summary>
	[JsonPropertyName("oracleListenerPassword")]
	public string? OracleListenerPassword { get; init; }

	/// <summary>The Oracle service name; a default is used when <see langword="null"/>.</summary>
	[JsonPropertyName("serviceName")]
	public string? ServiceName { get; init; }

	/// <summary>The SNMP community name. A secret; write-only.</summary>
	[JsonPropertyName("communityName")]
	public string? CommunityName { get; init; }

	/// <summary>The SNMP v3 authentication protocol.</summary>
	[JsonPropertyName("authenticationType")]
	public SnmpV3AuthenticationType? AuthenticationType { get; init; }

	/// <summary>The SNMP v3 privacy protocol.</summary>
	[JsonPropertyName("privacyType")]
	public SnmpV3PrivacyType? PrivacyType { get; init; }

	/// <summary>The SNMP v3 privacy password, needed with any privacy protocol. A secret; write-only.</summary>
	[JsonPropertyName("privacyPassword")]
	public string? PrivacyPassword { get; init; }

	/// <summary>How an SSH session elevates its permissions; <see cref="Credentials.PermissionElevation.None"/> by default.</summary>
	[JsonPropertyName("permissionElevation")]
	public PermissionElevation? PermissionElevation { get; init; }

	/// <summary>The user name to elevate to; only with an elevation other than none or <c>pbrun</c>.</summary>
	[JsonPropertyName("permissionElevationUsername")]
	public string? PermissionElevationUsername { get; init; }

	/// <summary>The password of the elevated account. A secret; write-only.</summary>
	[JsonPropertyName("permissionElevationPassword")]
	public string? PermissionElevationPassword { get; init; }

	/// <summary>The PEM-format private key (SSH key). A secret; write-only.</summary>
	[JsonPropertyName("pemKey")]
	public string? PemKey { get; init; }

	/// <summary>The password protecting the private key (SSH key). A secret; write-only.</summary>
	[JsonPropertyName("privateKeyPassword")]
	public string? PrivateKeyPassword { get; init; }

	/// <summary>The PEM-format public key certificate (Scan Assistant).</summary>
	[JsonPropertyName("pemCert")]
	public string? PemCert { get; init; }

	/// <summary>When the Scan Assistant certificate expires, as the console formats it.</summary>
	[JsonPropertyName("pemExpiration")]
	public string? PemExpiration { get; init; }

	/// <summary>The PKCS #12 (.p12/.pfx) container holding the Scan Assistant private key. A secret.</summary>
	[JsonPropertyName("pkcs12_data")]
	public string? Pkcs12Data { get; init; }
}
