using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Administration;

/// <summary>The features a licence enables.</summary>
public sealed class LicenseFeatures
{
	/// <summary>Whether Adaptive Security is available.</summary>
	[JsonPropertyName("adaptiveSecurity")]
	public bool? AdaptiveSecurity { get; init; }

	/// <summary>Whether agents can be used.</summary>
	[JsonPropertyName("agents")]
	public bool? Agents { get; init; }

	/// <summary>Whether dynamic discovery connections can be used.</summary>
	[JsonPropertyName("dynamicDiscovery")]
	public bool? DynamicDiscovery { get; init; }

	/// <summary>Whether early-access features are available.</summary>
	[JsonPropertyName("earlyAccess")]
	public bool? EarlyAccess { get; init; }

	/// <summary>Whether scan engine pools can be used.</summary>
	[JsonPropertyName("enginePool")]
	public bool? EnginePool { get; init; }

	/// <summary>Whether the Insight platform can be used.</summary>
	[JsonPropertyName("insightPlatform")]
	public bool? InsightPlatform { get; init; }

	/// <summary>Whether mobile device features are available.</summary>
	[JsonPropertyName("mobile")]
	public bool? Mobile { get; init; }

	/// <summary>Whether multitenancy is available.</summary>
	[JsonPropertyName("multitenancy")]
	public bool? Multitenancy { get; init; }

	/// <summary>Whether policies can be edited.</summary>
	[JsonPropertyName("policyEditor")]
	public bool? PolicyEditor { get; init; }

	/// <summary>Whether the policy manager is available.</summary>
	[JsonPropertyName("policyManager")]
	public bool? PolicyManager { get; init; }

	/// <summary>Whether Remediation Analytics is available.</summary>
	[JsonPropertyName("remediationAnalytics")]
	public bool? RemediationAnalytics { get; init; }

	/// <summary>The reporting features available.</summary>
	[JsonPropertyName("reporting")]
	public LicenseReporting? Reporting { get; init; }

	/// <summary>The scanning features available.</summary>
	[JsonPropertyName("scanning")]
	public LicenseScanning? Scanning { get; init; }
}
