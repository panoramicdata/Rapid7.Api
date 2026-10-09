using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Administration;

/// <summary>The console's email (SMTP) settings.</summary>
public sealed class SmtpSettings
{
	/// <summary>The mail server.</summary>
	[JsonPropertyName("host")]
	public string? Host { get; init; }

	/// <summary>The mail server port.</summary>
	[JsonPropertyName("port")]
	public int? Port { get; init; }

	/// <summary>The sender address.</summary>
	[JsonPropertyName("sender")]
	public string? Sender { get; init; }

	/// <summary>The distribution identifier.</summary>
	[JsonPropertyName("distributionId")]
	public string? DistributionId { get; init; }
}
