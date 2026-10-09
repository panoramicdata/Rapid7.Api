using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.AssetDiscovery;

/// <summary>One filter of a Sonar query. Set the property its <see cref="Type"/> needs.</summary>
public sealed class SonarCriterion
{
	/// <summary>For <see cref="SonarCriterionType.ScanDateWithinTheLast"/>, the number of days.</summary>
	[JsonPropertyName("days")]
	public int? Days { get; init; }

	/// <summary>For <see cref="SonarCriterionType.DomainContains"/>, the domain text to match.</summary>
	[JsonPropertyName("domain")]
	public string? Domain { get; init; }

	/// <summary>For <see cref="SonarCriterionType.IpAddressRange"/>, the first address of the range.</summary>
	[JsonPropertyName("lower")]
	public string? Lower { get; init; }

	/// <summary>The kind of filter.</summary>
	[JsonPropertyName("type")]
	public SonarCriterionType? Type { get; init; }

	/// <summary>For <see cref="SonarCriterionType.IpAddressRange"/>, the last address of the range.</summary>
	[JsonPropertyName("upper")]
	public string? Upper { get; init; }
}
