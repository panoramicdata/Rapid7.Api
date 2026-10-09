using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Assets;

/// <summary>An asset (a scanned device) and its key facts.</summary>
public sealed class Asset : Links
{
	/// <summary>The identifier of the asset.</summary>
	[JsonPropertyName("id")]
	public long? Id { get; init; }

	/// <summary>The primary IPv4 or IPv6 address.</summary>
	[JsonPropertyName("ip")]
	public string? Ip { get; init; }

	/// <summary>The primary host name.</summary>
	[JsonPropertyName("hostName")]
	public string? HostName { get; init; }

	/// <summary>The primary MAC address.</summary>
	[JsonPropertyName("mac")]
	public string? Mac { get; init; }

	/// <summary>The full description of the operating system.</summary>
	[JsonPropertyName("os")]
	public string? Os { get; init; }

	/// <summary>The certainty (0 to 1) of the operating system fingerprint.</summary>
	[JsonPropertyName("osCertainty")]
	public string? OsCertainty { get; init; }

	/// <summary>The kind of asset: <c>unknown</c>, <c>guest</c>, <c>hypervisor</c>, <c>physical</c> or <c>mobile</c>.</summary>
	[JsonPropertyName("type")]
	public string? Type { get; init; }

	/// <summary>The risk score, with criticality adjustments.</summary>
	[JsonPropertyName("riskScore")]
	public double? RiskScore { get; init; }

	/// <summary>The risk score, before criticality adjustments.</summary>
	[JsonPropertyName("rawRiskScore")]
	public double? RawRiskScore { get; init; }

	/// <summary>Whether the asset has been assessed for policies.</summary>
	[JsonPropertyName("assessedForPolicies")]
	public bool? AssessedForPolicies { get; init; }

	/// <summary>Whether the asset has been assessed for vulnerabilities.</summary>
	[JsonPropertyName("assessedForVulnerabilities")]
	public bool? AssessedForVulnerabilities { get; init; }
}
