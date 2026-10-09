using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Sites;

/// <summary>A site alert sent as a syslog message.</summary>
public sealed class SyslogAlert : SiteAlertBase
{
	/// <summary>Creates a syslog alert.</summary>
	public SyslogAlert() : base(AlertNotificationType.Syslog)
	{
	}

	/// <summary>The syslog server to send messages to (required).</summary>
	[JsonPropertyName("server")]
	public string? Server { get; init; }
}
