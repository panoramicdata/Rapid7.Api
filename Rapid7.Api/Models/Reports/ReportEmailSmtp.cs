using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Reports;

/// <summary>The mail server a report is sent through.</summary>
public sealed class ReportEmailSmtp
{
	/// <summary>Whether to use the console's global SMTP settings; if so, leave the relay and sender unset.</summary>
	[JsonPropertyName("global")]
	public bool? Global { get; init; }

	/// <summary>The SMTP relay host name or address.</summary>
	[JsonPropertyName("relay")]
	public string? Relay { get; init; }

	/// <summary>The sender address.</summary>
	[JsonPropertyName("sender")]
	public string? Sender { get; init; }
}
