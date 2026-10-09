using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Policies;

/// <summary>Counts of compliance results: the total, and how many passed, failed or were not applicable.</summary>
public class PolicyResultCounts : LinksResource
{
	/// <summary>The total number counted.</summary>
	[JsonPropertyName("total")]
	public int? Total { get; init; }

	/// <summary>How many are compliant.</summary>
	[JsonPropertyName("totalPassed")]
	public int? TotalPassed { get; init; }

	/// <summary>How many are not compliant.</summary>
	[JsonPropertyName("totalFailed")]
	public int? TotalFailed { get; init; }

	/// <summary>How many are not applicable.</summary>
	[JsonPropertyName("totalNotApplicable")]
	public int? TotalNotApplicable { get; init; }
}
