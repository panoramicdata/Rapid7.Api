using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Reports;

/// <summary>A report template.</summary>
public sealed class ReportTemplate : Links
{
	/// <summary>The identifier of the template, such as <c>audit-report</c>.</summary>
	[JsonPropertyName("id")]
	public string? Id { get; init; }

	/// <summary>The name of the template.</summary>
	[JsonPropertyName("name")]
	public string? Name { get; init; }

	/// <summary>What the template reports.</summary>
	[JsonPropertyName("description")]
	public string? Description { get; init; }

	/// <summary>The kind of template.</summary>
	[JsonPropertyName("type")]
	public ReportTemplateType? Type { get; init; }

	/// <summary>Whether the template ships with the console.</summary>
	[JsonPropertyName("builtin")]
	public bool? Builtin { get; init; }

	/// <summary>For a document template, its sections.</summary>
	[JsonPropertyName("sections")]
	public IReadOnlyList<string> Sections { get; init; } = [];
}
