using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Credentials;

/// <summary>
/// The service a credential authenticates to and the secrets it needs. Which properties apply depends on
/// <see cref="Service"/> (for example <see cref="Username"/> and <see cref="Password"/> for <c>cifs</c>, <see cref="CommunityName"/>
/// for <c>snmp</c>, <see cref="PemKey"/> for <c>ssh-key</c>); set only those. The console leaves secrets out of its responses.
/// </summary>
public sealed class SharedCredentialAccount
{
	/// <summary>The service to authenticate to.</summary>
	[JsonPropertyName("service")]
	public CredentialService? Service { get; init; }

	/// <summary>The user name.</summary>
	[JsonPropertyName("username")]
	public string? Username { get; init; }

	/// <summary>The password.</summary>
	[JsonPropertyName("password")]
	public string? Password { get; init; }

	/// <summary>The domain or workgroup (CIFS, AS/400, CVS, SQL Server, Sybase).</summary>
	[JsonPropertyName("domain")]
	public string? Domain { get; init; }

	/// <summary>The database name (DB2, SQL Server, MySQL, PostgreSQL, Sybase, SAP HANA).</summary>
	[JsonPropertyName("database")]
	public string? Database { get; init; }

	/// <summary>The realm (HTTP, Kerberos).</summary>
	[JsonPropertyName("realm")]
	public string? Realm { get; init; }

	/// <summary>Whether to log on with Windows authentication (SQL Server, Sybase).</summary>
	[JsonPropertyName("useWindowsAuthentication")]
	public bool? UseWindowsAuthentication { get; init; }

	/// <summary>The LM/NTLM password hash (<c>cifshash</c>).</summary>
	[JsonPropertyName("ntlmHash")]
	public string? NtlmHash { get; init; }

	/// <summary>The password of the Notes ID file (<c>notes</c>).</summary>
	[JsonPropertyName("notesIDPassword")]
	public string? NotesIdPassword { get; init; }

	/// <summary>The Oracle SID (<c>oracle</c>).</summary>
	[JsonPropertyName("sid")]
	public string? Sid { get; init; }

	/// <summary>Whether to enumerate the SIDs of the Oracle listener (<c>oracle</c>).</summary>
	[JsonPropertyName("enumerateSids")]
	public bool? EnumerateSids { get; init; }

	/// <summary>The password of the Oracle listener (<c>oracle</c>).</summary>
	[JsonPropertyName("oracleListenerPassword")]
	public string? OracleListenerPassword { get; init; }

	/// <summary>The Oracle service name (<c>oracle-service-name</c>).</summary>
	[JsonPropertyName("serviceName")]
	public string? ServiceName { get; init; }

	/// <summary>The SNMP community name (<c>snmp</c>).</summary>
	[JsonPropertyName("communityName")]
	public string? CommunityName { get; init; }

	/// <summary>The SNMP v3 authentication type: <c>no-authentication</c>, <c>md5</c> or <c>sha</c>.</summary>
	[JsonPropertyName("authenticationType")]
	public string? AuthenticationType { get; init; }

	/// <summary>The SNMP v3 privacy type, for example <c>no-privacy</c>, <c>des</c> or <c>aes-256</c>.</summary>
	[JsonPropertyName("privacyType")]
	public string? PrivacyType { get; init; }

	/// <summary>The SNMP v3 privacy password.</summary>
	[JsonPropertyName("privacyPassword")]
	public string? PrivacyPassword { get; init; }

	/// <summary>How to elevate permissions after an SSH logon: <c>none</c>, <c>sudo</c>, <c>sudosu</c>, <c>su</c>, <c>pbrun</c> or <c>privileged-exec</c>.</summary>
	[JsonPropertyName("permissionElevation")]
	public string? PermissionElevation { get; init; }

	/// <summary>The user name to elevate permissions as (SSH).</summary>
	[JsonPropertyName("permissionElevationUsername")]
	public string? PermissionElevationUsername { get; init; }

	/// <summary>The password to elevate permissions with (SSH).</summary>
	[JsonPropertyName("permissionElevationPassword")]
	public string? PermissionElevationPassword { get; init; }

	/// <summary>The PEM-encoded private key (<c>ssh-key</c>).</summary>
	[JsonPropertyName("pemKey")]
	public string? PemKey { get; init; }

	/// <summary>The password of the private key (<c>ssh-key</c>).</summary>
	[JsonPropertyName("privateKeyPassword")]
	public string? PrivateKeyPassword { get; init; }

	/// <summary>The PEM-encoded certificate (<c>scan-assistant</c>).</summary>
	[JsonPropertyName("pemCert")]
	public string? PemCert { get; init; }

	/// <summary>When the certificate expires (<c>scan-assistant</c>).</summary>
	[JsonPropertyName("pemExpiration")]
	public string? PemExpiration { get; init; }

	/// <summary>The PKCS #12 data (<c>scan-assistant</c>).</summary>
	[JsonPropertyName("pkcs12_data")]
	public string? Pkcs12Data { get; init; }
}
