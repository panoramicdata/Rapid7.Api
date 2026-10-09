using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Sites;

/// <summary>How a site alert is delivered.</summary>
public enum AlertNotificationType
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>By email, through an SMTP relay.</summary>
	[JsonStringEnumMemberName("SMTP")]
	Smtp,

	/// <summary>As an SNMP trap.</summary>
	[JsonStringEnumMemberName("SNMP")]
	Snmp,

	/// <summary>As a syslog message.</summary>
	[JsonStringEnumMemberName("Syslog")]
	Syslog
}
