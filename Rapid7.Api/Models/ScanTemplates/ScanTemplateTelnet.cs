using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.ScanTemplates;

/// <summary>How a scan template recognises Telnet login prompts and results (regular expressions).</summary>
public sealed class ScanTemplateTelnet : Links
{
	/// <summary>The character set to use.</summary>
	[JsonPropertyName("characterSet")]
	public string? CharacterSet { get; init; }

	/// <summary>Matches a login prompt.</summary>
	[JsonPropertyName("loginRegex")]
	public string? LoginRegex { get; init; }

	/// <summary>Matches a password prompt.</summary>
	[JsonPropertyName("passwordPromptRegex")]
	public string? PasswordPromptRegex { get; init; }

	/// <summary>Matches a failed login.</summary>
	[JsonPropertyName("failedLoginRegex")]
	public string? FailedLoginRegex { get; init; }

	/// <summary>Matches a login whose success is uncertain.</summary>
	[JsonPropertyName("questionableLoginRegex")]
	public string? QuestionableLoginRegex { get; init; }
}
