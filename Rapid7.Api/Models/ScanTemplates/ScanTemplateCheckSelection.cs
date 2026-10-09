using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.ScanTemplates;

/// <summary>
/// Vulnerability check categories, types or individual check identifiers that a scan template enables and disables (the
/// spec's three identically shaped schemas).
/// </summary>
public sealed class ScanTemplateCheckSelection : Links
{
	/// <summary>The enabled categories, types or checks.</summary>
	[JsonPropertyName("enabled")]
	public IReadOnlyList<string> Enabled { get; init; } = [];

	/// <summary>The disabled categories, types or checks.</summary>
	[JsonPropertyName("disabled")]
	public IReadOnlyList<string> Disabled { get; init; } = [];
}
