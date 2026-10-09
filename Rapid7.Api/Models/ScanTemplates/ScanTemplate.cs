using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.ScanTemplates;

/// <summary>A scan template: how scans discover assets and services and which checks they run.</summary>
public sealed class ScanTemplate : Links
{
	/// <summary>The scan template identifier, such as <c>full-audit-without-web-spider</c>.</summary>
	[JsonPropertyName("id")]
	public string? Id { get; init; }

	/// <summary>The scan template name.</summary>
	[JsonPropertyName("name")]
	public string? Name { get; init; }

	/// <summary>A description of the scan template.</summary>
	[JsonPropertyName("description")]
	public string? Description { get; init; }

	/// <summary>Whether scans only discover assets, without checking them for vulnerabilities or policy compliance.</summary>
	[JsonPropertyName("discoveryOnly")]
	public bool? DiscoveryOnly { get; init; }

	/// <summary>Whether vulnerability checks run.</summary>
	[JsonPropertyName("vulnerabilityEnabled")]
	public bool? VulnerabilityEnabled { get; init; }

	/// <summary>Whether policy checks run.</summary>
	[JsonPropertyName("policyEnabled")]
	public bool? PolicyEnabled { get; init; }

	/// <summary>Whether scans may enable Windows services they need (such as Remote Registry) while they run.</summary>
	[JsonPropertyName("enableWindowsServices")]
	public bool? EnableWindowsServices { get; init; }

	/// <summary>Whether scans log in extra detail.</summary>
	[JsonPropertyName("enhancedLogging")]
	public bool? EnhancedLogging { get; init; }

	/// <summary>The most assets an engine scans at once.</summary>
	[JsonPropertyName("maxParallelAssets")]
	public int? MaxParallelAssets { get; init; }

	/// <summary>The most scan processes an engine runs at once.</summary>
	[JsonPropertyName("maxScanProcesses")]
	public int? MaxScanProcesses { get; init; }

	/// <summary>Which vulnerability checks run.</summary>
	[JsonPropertyName("checks")]
	public ScanTemplateVulnerabilityChecks? Checks { get; init; }

	/// <summary>The databases to connect to when checking database servers.</summary>
	[JsonPropertyName("database")]
	public ScanTemplateDatabase? Database { get; init; }

	/// <summary>How assets and services are discovered.</summary>
	[JsonPropertyName("discovery")]
	public ScanTemplateDiscovery? Discovery { get; init; }

	/// <summary>The policy checks and their options.</summary>
	[JsonPropertyName("policy")]
	public ScanTemplatePolicy? Policy { get; init; }

	/// <summary>How Telnet logins are recognised.</summary>
	[JsonPropertyName("telnet")]
	public ScanTemplateTelnet? Telnet { get; init; }
}
