using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Credentials;

/// <summary>The service a scan credential authenticates to, which decides the <see cref="CredentialAccount"/> fields that apply.</summary>
public enum CredentialService
{
	/// <summary>A value this client does not recognise.</summary>
	Unknown = 0,

	/// <summary>IBM AS/400 (<c>as400</c>): username, password, domain.</summary>
	[JsonStringEnumMemberName("as400")]
	As400,

	/// <summary>Microsoft Windows or Samba over SMB/CIFS (<c>cifs</c>): username, password, domain.</summary>
	[JsonStringEnumMemberName("cifs")]
	Cifs,

	/// <summary>Windows or Samba with an LM/NTLM hash (<c>cifshash</c>): username, NTLM hash, domain.</summary>
	[JsonStringEnumMemberName("cifshash")]
	CifsHash,

	/// <summary>Concurrent Versions System (<c>cvs</c>): username, password, domain.</summary>
	[JsonStringEnumMemberName("cvs")]
	Cvs,

	/// <summary>IBM DB2 (<c>db2</c>): username, password, database.</summary>
	[JsonStringEnumMemberName("db2")]
	Db2,

	/// <summary>FTP (<c>ftp</c>): username, password.</summary>
	[JsonStringEnumMemberName("ftp")]
	Ftp,

	/// <summary>Web site HTTP authentication (<c>http</c>): username, password, realm.</summary>
	[JsonStringEnumMemberName("http")]
	Http,

	/// <summary>Microsoft SQL Server (<c>ms-sql</c>): username, password, database, domain, Windows authentication.</summary>
	[JsonStringEnumMemberName("ms-sql")]
	MsSql,

	/// <summary>MySQL (<c>mysql</c>): username, password, database.</summary>
	[JsonStringEnumMemberName("mysql")]
	MySql,

	/// <summary>Lotus Notes/Domino (<c>notes</c>): username, Notes ID password.</summary>
	[JsonStringEnumMemberName("notes")]
	Notes,

	/// <summary>Oracle by SID (<c>oracle</c>): username, password, SID, SID enumeration and listener password.</summary>
	[JsonStringEnumMemberName("oracle")]
	Oracle,

	/// <summary>Oracle by service name (<c>oracle-service-name</c>): username, password, service name.</summary>
	[JsonStringEnumMemberName("oracle-service-name")]
	OracleServiceName,

	/// <summary>POP (<c>pop</c>): username, password.</summary>
	[JsonStringEnumMemberName("pop")]
	Pop,

	/// <summary>PostgreSQL (<c>postgresql</c>): username, password, database.</summary>
	[JsonStringEnumMemberName("postgresql")]
	PostgreSql,

	/// <summary>Remote execution (<c>remote-exec</c>): username, password.</summary>
	[JsonStringEnumMemberName("remote-exec")]
	RemoteExec,

	/// <summary>SNMP v1/v2c (<c>snmp</c>): community name.</summary>
	[JsonStringEnumMemberName("snmp")]
	Snmp,

	/// <summary>SNMP v3 (<c>snmpv3</c>): username, authentication type and password, privacy type and password.</summary>
	[JsonStringEnumMemberName("snmpv3")]
	SnmpV3,

	/// <summary>Secure Shell with a password (<c>ssh</c>): username, password, permission elevation.</summary>
	[JsonStringEnumMemberName("ssh")]
	Ssh,

	/// <summary>Secure Shell with a private key (<c>ssh-key</c>): username, PEM key and its password, permission elevation.</summary>
	[JsonStringEnumMemberName("ssh-key")]
	SshKey,

	/// <summary>Sybase SQL Server (<c>sybase</c>): username, password, database, domain, Windows authentication.</summary>
	[JsonStringEnumMemberName("sybase")]
	Sybase,

	/// <summary>Telnet (<c>telnet</c>): username, password.</summary>
	[JsonStringEnumMemberName("telnet")]
	Telnet,

	/// <summary>Kerberos (<c>kerberos</c>): username, password, realm.</summary>
	[JsonStringEnumMemberName("kerberos")]
	Kerberos,

	/// <summary>SAP HANA (<c>hana</c>): username, password, database.</summary>
	[JsonStringEnumMemberName("hana")]
	Hana,

	/// <summary>The Rapid7 Scan Assistant (<c>scan-assistant</c>): certificate, PKCS #12 data, password.</summary>
	[JsonStringEnumMemberName("scan-assistant")]
	ScanAssistant,

	/// <summary>VMware (<c>vmware</c>): username, password.</summary>
	[JsonStringEnumMemberName("vmware")]
	VMware
}
