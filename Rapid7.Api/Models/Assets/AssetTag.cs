using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Assets;

/// <summary>A tag applied to an asset, with how it came to be applied.</summary>
public sealed class AssetTag : LinksResource
{
	/// <summary>The display colour.</summary>
	[JsonPropertyName("color")]
	public AssetTagColor? Color { get; init; }

	/// <summary>When the tag was created.</summary>
	[JsonPropertyName("created")]
	public DateTimeOffset? Created { get; init; }

	/// <summary>The identifier of the tag.</summary>
	[JsonPropertyName("id")]
	public int? Id { get; init; }

	/// <summary>The name of the tag.</summary>
	[JsonPropertyName("name")]
	public string Name { get; init; } = string.Empty;

	/// <summary>For a criticality tag, the factor it applies to the risk score.</summary>
	[JsonPropertyName("riskModifier")]
	public double? RiskModifier { get; init; }

	/// <summary>The criteria that apply the tag to matching assets automatically, when the tag has any.</summary>
	[JsonPropertyName("searchCriteria")]
	public SearchCriteria? SearchCriteria { get; init; }

	/// <summary>Whether the tag ships with the console or was created by a user.</summary>
	[JsonPropertyName("source")]
	public AssetTagOrigin? Source { get; init; }

	/// <summary>The ways the tag came to be applied to the asset.</summary>
	[JsonPropertyName("sources")]
	public IReadOnlyList<AssetTagSource> Sources { get; init; } = [];

	/// <summary>The type of the tag.</summary>
	[JsonPropertyName("type")]
	public AssetTagType Type { get; init; }
}
