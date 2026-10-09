using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.ScanTemplates;

/// <summary>How a scan template recognises the prompts and answers of Telnet logins.</summary>
public sealed record ScanTemplateTelnet : ScanTemplateSection
{
	/// <summary>The character set of the Telnet session (for example <c>ASCII</c>).</summary>
	[JsonPropertyName("characterSet")]
	public string? CharacterSet { get; init; }

	/// <summary>A regular expression matching the login prompt.</summary>
	[JsonPropertyName("loginRegex")]
	public string? LoginRegex { get; init; }

	/// <summary>A regular expression matching the password prompt.</summary>
	[JsonPropertyName("passwordPromptRegex")]
	public string? PasswordPromptRegex { get; init; }

	/// <summary>A regular expression matching an answer that means the login failed.</summary>
	[JsonPropertyName("failedLoginRegex")]
	public string? FailedLoginRegex { get; init; }

	/// <summary>A regular expression matching an answer that may wrongly look like a failed login.</summary>
	[JsonPropertyName("questionableLoginRegex")]
	public string? QuestionableLoginRegex { get; init; }
}
