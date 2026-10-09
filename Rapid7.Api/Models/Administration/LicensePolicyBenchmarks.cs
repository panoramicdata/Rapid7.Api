using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Administration;

/// <summary>The policy benchmarks a licence allows scanning against.</summary>
public sealed class LicensePolicyBenchmarks
{
	/// <summary>Whether CIS benchmarks can be scanned.</summary>
	[JsonPropertyName("cis")]
	public bool? Cis { get; init; }

	/// <summary>Whether DISA benchmarks can be scanned.</summary>
	[JsonPropertyName("disa")]
	public bool? Disa { get; init; }

	/// <summary>Whether FDCC benchmarks can be scanned.</summary>
	[JsonPropertyName("fdcc")]
	public bool? Fdcc { get; init; }

	/// <summary>Whether USGCB benchmarks can be scanned.</summary>
	[JsonPropertyName("usgcb")]
	public bool? Usgcb { get; init; }

	/// <summary>Whether custom benchmarks can be used.</summary>
	[JsonPropertyName("custom")]
	public bool? Custom { get; init; }
}
