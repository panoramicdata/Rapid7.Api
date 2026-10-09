using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Users;

/// <summary>The languages a user works and reports in, each an IETF BCP 47 tag such as <c>en-US</c>.</summary>
public sealed class LocalePreferences
{
	/// <summary>The language of the console user interface.</summary>
	[JsonPropertyName("default")]
	public string? Default { get; init; }

	/// <summary>The language reports are generated in.</summary>
	[JsonPropertyName("reports")]
	public string? Reports { get; init; }
}
