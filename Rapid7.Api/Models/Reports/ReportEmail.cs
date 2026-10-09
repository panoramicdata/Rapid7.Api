using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Reports;

/// <summary>Who receives a generated report by email, and how.</summary>
public sealed class ReportEmail
{
	/// <summary>How the owner receives the report.</summary>
	[JsonPropertyName("owner")]
	public ReportDistribution? Owner { get; init; }

	/// <summary>How users explicitly granted access receive the report.</summary>
	[JsonPropertyName("access")]
	public ReportDistribution? Access { get; init; }

	/// <summary>How the additional recipients receive the report (a link is not allowed).</summary>
	[JsonPropertyName("additional")]
	public ReportDistribution? Additional { get; init; }

	/// <summary>The email addresses of additional recipients.</summary>
	[JsonPropertyName("additionalRecipients")]
	public IReadOnlyList<string>? AdditionalRecipients { get; init; }

	/// <summary>Whether every user with access to the report's assets receives it.</summary>
	[JsonPropertyName("assetAccess")]
	public bool? AssetAccess { get; init; }

	/// <summary>The mail server settings.</summary>
	[JsonPropertyName("smtp")]
	public ReportEmailSmtp? Smtp { get; init; }
}
