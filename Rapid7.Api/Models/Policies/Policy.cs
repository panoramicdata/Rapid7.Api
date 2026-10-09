using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Policies;

/// <summary>A compliance policy (benchmark profile) with its compliance results across the assets the caller can access.</summary>
public sealed class Policy : PolicyResource
{
	/// <summary>The name of the policy.</summary>
	[JsonPropertyName("policyName")]
	public string? PolicyName { get; init; }

	/// <summary>The name of the benchmark the policy belongs to.</summary>
	[JsonPropertyName("benchmarkName")]
	public string? BenchmarkName { get; init; }

	/// <summary>The version of the benchmark the policy belongs to.</summary>
	[JsonPropertyName("benchmarkVersion")]
	public string? BenchmarkVersion { get; init; }

	/// <summary>The family of benchmarks the policy belongs to, such as CIS, FDCC or USGCB.</summary>
	[JsonPropertyName("category")]
	public string? Category { get; init; }

	/// <summary>Whether the policy was created by users.</summary>
	[JsonPropertyName("isCustom")]
	public bool? IsCustom { get; init; }

	/// <summary>The number of assets that comply with the policy.</summary>
	[JsonPropertyName("passedAssetsCount")]
	public int? PassedAssetsCount { get; init; }

	/// <summary>The number of assets that do not comply with the policy.</summary>
	[JsonPropertyName("failedAssetsCount")]
	public int? FailedAssetsCount { get; init; }

	/// <summary>The number of assets checked to which the policy does not apply.</summary>
	[JsonPropertyName("notApplicableAssetsCount")]
	public int? NotApplicableAssetsCount { get; init; }

	/// <summary>The number of rules every scanned asset complies with.</summary>
	[JsonPropertyName("passedRulesCount")]
	public int? PassedRulesCount { get; init; }

	/// <summary>The number of rules at least one scanned asset fails.</summary>
	[JsonPropertyName("failedRulesCount")]
	public int? FailedRulesCount { get; init; }

	/// <summary>The number of rules that apply to no scanned asset.</summary>
	[JsonPropertyName("notApplicableRulesCount")]
	public int? NotApplicableRulesCount { get; init; }

	/// <summary>The number of rules whose role is unscored, which do not affect the compliance score.</summary>
	[JsonPropertyName("unscoredRules")]
	public int? UnscoredRules { get; init; }

	/// <summary>The share of rules that pass, from 0 to 1.</summary>
	[JsonPropertyName("ruleCompliance")]
	public double? RuleCompliance { get; init; }

	/// <summary>The change in rule compliance between the last two scans.</summary>
	[JsonPropertyName("ruleComplianceDelta")]
	public double? RuleComplianceDelta { get; init; }
}
