using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Credentials;

/// <summary>The service a scan credential authenticates to.</summary>
public enum CredentialService
{
	/// <summary>A value this client does not recognise.</summary>
	Unknown = 0,

	/// <summary>IBM AS/400.</summary>
	[JsonStringEnumMemberName("as400")]
	As400,

	/// <summary>Microsoft Windows or Samba (SMB/CIFS).</summary>
	[JsonStringEnumMemberName("cifs")]
	Cifs,

	/// <summary>Microsoft Windows or Samba with an LM/NTLM hash (SMB/CIFS).</summary>
	[JsonStringEnumMemberName("cifshash")]
	CifsHash,

	/// <summary>Concurrent Versions System (CVS).</summary>
	[JsonStringEnumMemberName("cvs")]
	Cvs,

	/// <summary>IBM DB2.</summary>
	[JsonStringEnumMemberName("db2")]
	Db2,

	/// <summary>File Transfer Protocol (FTP).</summary>
	[JsonStringEnumMemberName("ftp")]
	Ftp,

	/// <summary>Web site HTTP authentication.</summary>
	[JsonStringEnumMemberName("http")]
	Http,

	/// <summary>Microsoft SQL Server.</summary>
	[JsonStringEnumMemberName("ms-sql")]
	MsSql,

	/// <summary>MySQL Server.</summary>
	[JsonStringEnumMemberName("mysql")]
	MySql,

	/// <summary>Lotus Notes/Domino.</summary>
	[JsonStringEnumMemberName("notes")]
	Notes,

	/// <summary>Oracle (by SID).</summary>
	[JsonStringEnumMemberName("oracle")]
	Oracle,

	/// <summary>Oracle (by service name).</summary>
	[JsonStringEnumMemberName("oracle-service-name")]
	OracleServiceName,

	/// <summary>Post Office Protocol (POP).</summary>
	[JsonStringEnumMemberName("pop")]
	Pop,

	/// <summary>PostgreSQL.</summary>
	[JsonStringEnumMemberName("postgresql")]
	PostgreSql,

	/// <summary>Remote execution.</summary>
	[JsonStringEnumMemberName("remote-exec")]
	RemoteExec,

	/// <summary>SNMP v1/v2c.</summary>
	[JsonStringEnumMemberName("snmp")]
	Snmp,

	/// <summary>SNMP v3.</summary>
	[JsonStringEnumMemberName("snmpv3")]
	SnmpV3,

	/// <summary>Secure Shell (SSH) with a password.</summary>
	[JsonStringEnumMemberName("ssh")]
	Ssh,

	/// <summary>Secure Shell (SSH) with a public key.</summary>
	[JsonStringEnumMemberName("ssh-key")]
	SshKey,

	/// <summary>Sybase SQL Server.</summary>
	[JsonStringEnumMemberName("sybase")]
	Sybase,

	/// <summary>Telnet.</summary>
	[JsonStringEnumMemberName("telnet")]
	Telnet,

	/// <summary>Kerberos.</summary>
	[JsonStringEnumMemberName("kerberos")]
	Kerberos,

	/// <summary>SAP HANA.</summary>
	[JsonStringEnumMemberName("hana")]
	Hana,

	/// <summary>The Rapid7 Scan Assistant.</summary>
	[JsonStringEnumMemberName("scan-assistant")]
	ScanAssistant,

	/// <summary>VMware.</summary>
	[JsonStringEnumMemberName("vmware")]
	VMware,
}
