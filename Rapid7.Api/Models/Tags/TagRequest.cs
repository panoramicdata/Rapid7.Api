using Rapid7.Api.Models.Assets;
using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Tags;

/// <summary>A tag to create, or a tag's new settings.</summary>
public sealed class TagRequest
{
	/// <summary>The tag's label.</summary>
	[JsonPropertyName("name")]
	public required string Name { get; init; }

	/// <summary>The kind of tag; a criticality tag's name must be one of the console's criticality levels.</summary>
	[JsonPropertyName("type")]
	public required TagType Type { get; init; }

	/// <summary>The colour user interfaces show the tag in, or <see langword="null"/> for the default.</summary>
	[JsonPropertyName("color")]
	public TagColor? Color { get; init; }

	/// <summary>The factor to apply to the risk score of tagged assets; criticality tags only.</summary>
	[JsonPropertyName("riskModifier")]
	public double? RiskModifier { get; init; }

	/// <summary>Search criteria that apply the tag to matching assets automatically, or <see langword="null"/> for none.</summary>
	[JsonPropertyName("searchCriteria")]
	public SearchCriteria? SearchCriteria { get; init; }
}
