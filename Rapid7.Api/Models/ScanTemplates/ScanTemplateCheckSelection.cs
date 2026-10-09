using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.ScanTemplates;

/// <summary>Vulnerability check categories, types or individual checks, enabled and disabled.</summary>
public sealed record ScanTemplateCheckSelection : ScanTemplateSection
{
	/// <summary>The names or identifiers to enable.</summary>
	[JsonPropertyName("enabled")]
	public IReadOnlyList<string>? Enabled { get; init; }

	/// <summary>The names or identifiers to disable.</summary>
	[JsonPropertyName("disabled")]
	public IReadOnlyList<string>? Disabled { get; init; }
}
