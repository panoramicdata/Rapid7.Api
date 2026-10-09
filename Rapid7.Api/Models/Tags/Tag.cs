using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Tags;

/// <summary>A tag that can be applied to assets, asset groups and sites.</summary>
public sealed class Tag : Links
{
	/// <summary>The identifier of the tag.</summary>
	[JsonPropertyName("id")]
	public int? Id { get; init; }

	/// <summary>The name of the tag.</summary>
	[JsonPropertyName("name")]
	public string? Name { get; init; }

	/// <summary>The kind of tag: <c>custom</c>, <c>location</c>, <c>owner</c> or <c>criticality</c>.</summary>
	[JsonPropertyName("type")]
	public string? Type { get; init; }

	/// <summary>The display colour: <c>default</c>, <c>blue</c>, <c>green</c>, <c>orange</c>, <c>red</c> or <c>purple</c>.</summary>
	[JsonPropertyName("color")]
	public string? Color { get; init; }

	/// <summary>Whether the tag is <c>built-in</c> or <c>custom</c>.</summary>
	[JsonPropertyName("source")]
	public string? Source { get; init; }

	/// <summary>When the tag was created.</summary>
	[JsonPropertyName("created")]
	public DateTimeOffset? Created { get; init; }

	/// <summary>The risk score multiplier of a criticality tag.</summary>
	[JsonPropertyName("riskModifier")]
	public double? RiskModifier { get; init; }
}
