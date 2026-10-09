using System.Text.Json.Serialization;
using Rapid7.Api.Models;

namespace Rapid7.Api.Models.Vulnerabilities;

/// <summary>A way to remediate one or more vulnerabilities.</summary>
public class Solution : LinksResource
{
	/// <summary>Further information about the solution.</summary>
	[JsonPropertyName("additionalInformation")]
	public Content? AdditionalInformation { get; init; }

	/// <summary>The systems or software the solution applies to.</summary>
	[JsonPropertyName("appliesTo")]
	public string? AppliesTo { get; init; }

	/// <summary>The estimated time to apply the solution, as an ISO 8601 duration such as <c>PT10M</c>.</summary>
	[JsonPropertyName("estimate")]
	public string? Estimate { get; init; }

	/// <summary>Whether a fix is available.</summary>
	[JsonPropertyName("fixAvailable")]
	public bool? FixAvailable { get; init; }

	/// <summary>The identifier, such as <c>ubuntu-upgrade-libexpat1</c>.</summary>
	[JsonPropertyName("id")]
	public string? Id { get; init; }

	/// <summary>The steps to apply the solution.</summary>
	[JsonPropertyName("steps")]
	public Content? Steps { get; init; }

	/// <summary>A summary of the solution.</summary>
	[JsonPropertyName("summary")]
	public Content? Summary { get; init; }

	/// <summary>The kind of solution.</summary>
	[JsonPropertyName("type")]
	public SolutionType? Type { get; init; }
}
