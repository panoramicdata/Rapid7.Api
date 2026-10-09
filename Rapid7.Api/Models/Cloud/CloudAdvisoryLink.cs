using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Cloud;

/// <summary>A reference from a vulnerability to an advisory or security standard.</summary>
public sealed class CloudAdvisoryLink
{
	/// <summary>The address of the advisory.</summary>
	[JsonPropertyName("href")]
	public string? Href { get; init; }

	/// <summary>The identifier of the reference within its source, such as a CVE or bug identifier.</summary>
	[JsonPropertyName("id")]
	public string? Id { get; init; }

	/// <summary>The source of the reference, such as <c>cve</c>, <c>bid</c>, <c>xf</c> or <c>url</c>.</summary>
	[JsonPropertyName("source")]
	public string? Source { get; init; }

	/// <summary>The link relation, typically <c>advisory</c>.</summary>
	[JsonPropertyName("rel")]
	public string? Rel { get; init; }
}
