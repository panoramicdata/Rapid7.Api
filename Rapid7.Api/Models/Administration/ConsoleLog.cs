using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Administration;

/// <summary>A Security Console log file that can be downloaded (<c>GET api/3/administration/logs</c>).</summary>
public enum ConsoleLog
{
	/// <summary>Not a log; never send it.</summary>
	Unknown = 0,

	/// <summary>The web access log (<c>access.log</c>).</summary>
	[JsonStringEnumMemberName("access.log")]
	Access,

	/// <summary>The audit log (<c>audit.log</c>).</summary>
	[JsonStringEnumMemberName("audit.log")]
	Audit,

	/// <summary>The authentication log (<c>auth.log</c>).</summary>
	[JsonStringEnumMemberName("auth.log")]
	Auth,

	/// <summary>The main console log (<c>nsc.log</c>).</summary>
	[JsonStringEnumMemberName("nsc.log")]
	Nsc
}
