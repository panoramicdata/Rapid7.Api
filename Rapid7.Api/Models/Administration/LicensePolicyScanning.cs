using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Administration;

/// <summary>The policy scanning a licence enables.</summary>
public sealed class LicensePolicyScanning
{
	/// <summary>Whether policy scanning is available.</summary>
	[JsonPropertyName("scanning")]
	public bool? Scanning { get; init; }

	/// <summary>The benchmarks that can be scanned.</summary>
	[JsonPropertyName("benchmarks")]
	public LicensePolicyBenchmarks? Benchmarks { get; init; }
}
