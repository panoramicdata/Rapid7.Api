using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.ScanTemplates;

/// <summary>
/// A scan template: what a scan discovers and checks, and how hard it works. Read templates with
/// <c>GET api/3/scan_templates</c>; send one to create or replace a template. To copy a template, read it and send it
/// back with a new <see cref="Name"/> and with <see cref="Id"/> and <see cref="ScanTemplateSection.Links"/> cleared.
/// </summary>
public sealed record ScanTemplate : ScanTemplateSection
{
	/// <summary>The template identifier (for example <c>full-audit-without-web-spider</c>); read-only.</summary>
	[JsonPropertyName("id")]
	public string? Id { get; init; }

	/// <summary>A short name for the template.</summary>
	[JsonPropertyName("name")]
	public string? Name { get; init; }

	/// <summary>A longer description of the template.</summary>
	[JsonPropertyName("description")]
	public string? Description { get; init; }

	/// <summary>Whether scans only discover assets, without assessing them.</summary>
	[JsonPropertyName("discoveryOnly")]
	public bool? DiscoveryOnly { get; init; }

	/// <summary>Whether scans assess vulnerabilities.</summary>
	[JsonPropertyName("vulnerabilityEnabled")]
	public bool? VulnerabilityEnabled { get; init; }

	/// <summary>Whether scans assess policy compliance.</summary>
	[JsonPropertyName("policyEnabled")]
	public bool? PolicyEnabled { get; init; }

	/// <summary>Whether scans spider and assess web applications.</summary>
	[JsonPropertyName("webEnabled")]
	public bool? WebEnabled { get; init; }

	/// <summary>
	/// Whether scans may temporarily reconfigure Windows services (remote registry, admin shares) on targets so that
	/// authenticated checks work; the services are restored afterwards.
	/// </summary>
	[JsonPropertyName("enableWindowsServices")]
	public bool? EnableWindowsServices { get; init; }

	/// <summary>Whether scans gather enhanced logs, which can use a great deal of disk space.</summary>
	[JsonPropertyName("enhancedLogging")]
	public bool? EnhancedLogging { get; init; }

	/// <summary>The most assets each scan engine scans at once.</summary>
	[JsonPropertyName("maxParallelAssets")]
	public int? MaxParallelAssets { get; init; }

	/// <summary>The most scan processes run at once against each asset.</summary>
	[JsonPropertyName("maxScanProcesses")]
	public int? MaxScanProcesses { get; init; }

	/// <summary>How assets and services are discovered.</summary>
	[JsonPropertyName("discovery")]
	public ScanTemplateDiscovery? Discovery { get; init; }

	/// <summary>Which vulnerability checks run.</summary>
	[JsonPropertyName("checks")]
	public ScanTemplateVulnerabilityChecks? Checks { get; init; }

	/// <summary>Which policies are assessed, and how.</summary>
	[JsonPropertyName("policy")]
	public ScanTemplatePolicy? Policy { get; init; }

	/// <summary>Database names to use when checking database servers.</summary>
	[JsonPropertyName("database")]
	public ScanTemplateDatabase? Database { get; init; }

	/// <summary>How Telnet logins are recognised.</summary>
	[JsonPropertyName("telnet")]
	public ScanTemplateTelnet? Telnet { get; init; }

	/// <summary>
	/// Web spider settings, which the API marks deprecated and does not describe; kept as raw JSON so that a template
	/// read and sent back keeps them unchanged.
	/// </summary>
	[JsonPropertyName("web")]
	public JsonElement? Web { get; init; }
}
