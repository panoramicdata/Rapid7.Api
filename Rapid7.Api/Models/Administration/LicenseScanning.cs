using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Administration;

/// <summary>The scanning features a licence enables.</summary>
public sealed class LicenseScanning
{
	/// <summary>Whether discovery scans can be run.</summary>
	[JsonPropertyName("discovery")]
	public bool? Discovery { get; init; }

	/// <summary>Whether SCADA scanning is available.</summary>
	[JsonPropertyName("scada")]
	public bool? Scada { get; init; }

	/// <summary>Whether virtual environments can be scanned.</summary>
	[JsonPropertyName("virtual")]
	public bool? Virtual { get; init; }

	/// <summary>Whether web applications can be scanned.</summary>
	[JsonPropertyName("webApplication")]
	public bool? WebApplication { get; init; }

	/// <summary>The policy scanning available.</summary>
	[JsonPropertyName("policy")]
	public LicensePolicyScanning? Policy { get; init; }
}
