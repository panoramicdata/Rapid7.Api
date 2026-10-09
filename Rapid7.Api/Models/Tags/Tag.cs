using Rapid7.Api.Models.Assets;
using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Tags;

/// <summary>A tag: a label applied to assets directly, through sites or asset groups, or by search criteria.</summary>
public sealed class Tag : Links
{
	/// <summary>The tag identifier.</summary>
	[JsonPropertyName("id")]
	public int? Id { get; init; }

	/// <summary>The tag's label.</summary>
	[JsonPropertyName("name")]
	public string Name { get; init; } = string.Empty;

	/// <summary>The kind of tag.</summary>
	[JsonPropertyName("type")]
	public TagType Type { get; init; }

	/// <summary>Whether the tag is built in or was created by a user.</summary>
	[JsonPropertyName("source")]
	public TagSource? Source { get; init; }

	/// <summary>When the tag was created.</summary>
	[JsonPropertyName("created")]
	public DateTimeOffset? Created { get; init; }

	/// <summary>The colour user interfaces show the tag in.</summary>
	[JsonPropertyName("color")]
	public TagColor? Color { get; init; }

	/// <summary>The search criteria that apply the tag to matching assets automatically, if any.</summary>
	[JsonPropertyName("searchCriteria")]
	public SearchCriteria? SearchCriteria { get; init; }

	/// <summary>The factor applied to the risk score of tagged assets; criticality tags only.</summary>
	[JsonPropertyName("riskModifier")]
	public double? RiskModifier { get; init; }
}
