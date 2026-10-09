using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Sites;

/// <summary>
/// A site alert of any kind, as the list of all a site's alerts returns it: <see cref="SiteAlertBase.Notification"/> says
/// which kind, and only that kind's settings are filled.
/// </summary>
public sealed class SiteAlert : SiteAlertBase
{
	/// <summary>Creates an alert of a kind the response will set.</summary>
	public SiteAlert() : base(AlertNotificationType.Unknown)
	{
	}

	/// <summary>For an SNMP or syslog alert, the server it is sent to.</summary>
	[JsonPropertyName("server")]
	public string? Server { get; init; }

	/// <summary>For an SMTP alert, the recipients' email addresses.</summary>
	[JsonPropertyName("recipients")]
	public IReadOnlyList<string> Recipients { get; init; } = [];

	/// <summary>For an SNMP alert, the community name.</summary>
	[JsonPropertyName("community")]
	public string? Community { get; init; }

	/// <summary>For an SMTP alert, the relay server it is sent through.</summary>
	[JsonPropertyName("relayServer")]
	public string? RelayServer { get; init; }

	/// <summary>For an SMTP alert, whether the alert text is shortened.</summary>
	[JsonPropertyName("limitAlertText")]
	public bool? LimitAlertText { get; init; }

	/// <summary>For an SMTP alert, the sender's email address.</summary>
	[JsonPropertyName("senderEmailAddress")]
	public string? SenderEmailAddress { get; init; }
}
