using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Credentials;

/// <summary>How an SSH credential elevates the scan engine to administrative or root access.</summary>
public enum PermissionElevation
{
	/// <summary>A value this client does not recognise.</summary>
	Unknown = 0,

	/// <summary>No elevation (<c>none</c>), the console's default.</summary>
	[JsonStringEnumMemberName("none")]
	None,

	/// <summary><c>sudo</c>.</summary>
	[JsonStringEnumMemberName("sudo")]
	Sudo,

	/// <summary><c>sudo su</c> (<c>sudosu</c>).</summary>
	[JsonStringEnumMemberName("sudosu")]
	SudoSu,

	/// <summary><c>su</c>.</summary>
	[JsonStringEnumMemberName("su")]
	Su,

	/// <summary><c>pbrun</c>.</summary>
	[JsonStringEnumMemberName("pbrun")]
	Pbrun,

	/// <summary>Cisco privileged EXEC mode (<c>privileged-exec</c>).</summary>
	[JsonStringEnumMemberName("privileged-exec")]
	PrivilegedExec
}
