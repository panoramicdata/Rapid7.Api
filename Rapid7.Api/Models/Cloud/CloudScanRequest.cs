using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Cloud;

/// <summary>
/// What to scan, how and when (<c>POST v4/integration/scan</c>). Every property is optional; leave one
/// <see langword="null"/> to omit it.
/// </summary>
public sealed class CloudScanRequest
{
	/// <summary>The scan name.</summary>
	[JsonPropertyName("name")]
	public string? Name { get; init; }

	/// <summary>The identifiers of the assets to scan.</summary>
	[JsonPropertyName("asset_ids")]
	public IReadOnlyList<string>? AssetIds { get; init; }

	/// <summary>The identifiers of the scan engines to use.</summary>
	[JsonPropertyName("engine_ids")]
	public IReadOnlyList<string>? EngineIds { get; init; }

	/// <summary>The identifiers of vulnerabilities to limit the scan to.</summary>
	[JsonPropertyName("vulnerability_ids")]
	public IReadOnlyList<string>? VulnerabilityIds { get; init; }

	/// <summary>The identifiers of solutions to limit the scan to.</summary>
	[JsonPropertyName("solution_ids")]
	public IReadOnlyList<string>? SolutionIds { get; init; }

	/// <summary>The names of the Security Console sites whose credentials the scan may use.</summary>
	[JsonPropertyName("credential_sources")]
	public IReadOnlyList<string>? CredentialSources { get; init; }

	/// <summary>The name of the Security Console site that receives the scanned assets.</summary>
	[JsonPropertyName("result_consumer")]
	public string? ResultConsumer { get; init; }

	/// <summary>When to start the scan; <see langword="null"/> to start it now.</summary>
	[JsonPropertyName("start_time")]
	public DateTimeOffset? StartTime { get; init; }

	/// <summary>Assets, engines, vulnerabilities or solutions to leave out of the scan, in the same shape.</summary>
	[JsonPropertyName("exclusions")]
	public CloudScanRequest? Exclusions { get; init; }
}
