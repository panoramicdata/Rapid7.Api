using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.BulkExport;

/// <summary>
/// A row of the <c>asset_policy</c> or <c>asset_scan_policy</c> dataset: the result of one XCCDF policy rule on an asset.
/// </summary>
public sealed class AssetPolicyRecord : BulkExportRecord
{
	/// <summary>The natural identifier of the XCCDF benchmark (<c>benchmarkNaturalId</c>).</summary>
	[JsonPropertyName("benchmarkNaturalId")]
	public string? BenchmarkNaturalId { get; init; }

	/// <summary>The benchmark title (<c>benchmarkTitle</c>).</summary>
	[JsonPropertyName("benchmarkTitle")]
	public string? BenchmarkTitle { get; init; }

	/// <summary>The benchmark version (<c>benchmarkVersion</c>).</summary>
	[JsonPropertyName("benchmarkVersion")]
	public string? BenchmarkVersion { get; init; }

	/// <summary>The natural identifier of the benchmark profile (<c>profileNaturalId</c>).</summary>
	[JsonPropertyName("profileNaturalId")]
	public string? ProfileNaturalId { get; init; }

	/// <summary>The profile title (<c>profileTitle</c>).</summary>
	[JsonPropertyName("profileTitle")]
	public string? ProfileTitle { get; init; }

	/// <summary>The policy publisher (<c>publisher</c>).</summary>
	[JsonPropertyName("publisher")]
	public string? Publisher { get; init; }

	/// <summary>The natural identifier of the XCCDF rule (<c>ruleNaturalId</c>).</summary>
	[JsonPropertyName("ruleNaturalId")]
	public string? RuleNaturalId { get; init; }

	/// <summary>The rule title (<c>ruleTitle</c>).</summary>
	[JsonPropertyName("ruleTitle")]
	public string? RuleTitle { get; init; }

	/// <summary>The rule result after any overrides (<c>finalStatus</c>).</summary>
	[JsonPropertyName("finalStatus")]
	public string? FinalStatus { get; init; }

	/// <summary>How the result was determined (<c>proof</c>).</summary>
	[JsonPropertyName("proof")]
	public string? Proof { get; init; }

	/// <summary>When the asset was last assessed against the policy (<c>lastAssessmentTimestamp</c>).</summary>
	[JsonPropertyName("lastAssessmentTimestamp")]
	public DateTimeOffset? LastAssessmentTimestamp { get; init; }

	/// <summary>Instructions for bringing the asset into compliance with the rule (<c>fixTexts</c>).</summary>
	[JsonPropertyName("fixTexts")]
	public IReadOnlyList<string> FixTexts { get; init; } = [];

	/// <summary>Why the rule matters (<c>rationales</c>).</summary>
	[JsonPropertyName("rationales")]
	public IReadOnlyList<string> Rationales { get; init; } = [];
}
