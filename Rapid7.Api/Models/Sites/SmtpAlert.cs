using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Sites;

/// <summary>A site alert sent by email through an SMTP relay.</summary>
public sealed class SmtpAlert : SiteAlertBase
{
	/// <summary>Creates an SMTP alert.</summary>
	public SmtpAlert() : base(AlertNotificationType.Smtp)
	{
	}

	/// <summary>The SMTP relay server to send through (required).</summary>
	[JsonPropertyName("relayServer")]
	public string? RelayServer { get; init; }

	/// <summary>The sender's email address.</summary>
	[JsonPropertyName("senderEmailAddress")]
	public string? SenderEmailAddress { get; init; }

	/// <summary>The recipients' email addresses (required).</summary>
	[JsonPropertyName("recipients")]
	public IReadOnlyList<string> Recipients { get; init; } = [];

	/// <summary>Whether to send a shortened alert text.</summary>
	[JsonPropertyName("limitAlertText")]
	public bool? LimitAlertText { get; init; }
}
