using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Sites;

/// <summary>A site alert sent as an SNMP trap.</summary>
public sealed class SnmpAlert : SiteAlertBase
{
	/// <summary>Creates an SNMP alert.</summary>
	public SnmpAlert() : base(AlertNotificationType.Snmp)
	{
	}

	/// <summary>The SNMP management server to send traps to (required).</summary>
	[JsonPropertyName("server")]
	public string? Server { get; init; }

	/// <summary>The SNMP community name (required).</summary>
	[JsonPropertyName("community")]
	public string? Community { get; init; }
}
