using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Administration;

/// <summary>The identifiers of an installed update.</summary>
public sealed class UpdateIdentifiers
{
	/// <summary>The product update identifier.</summary>
	[JsonPropertyName("productId")]
	public string? ProductId { get; init; }

	/// <summary>The version update identifier.</summary>
	[JsonPropertyName("versionId")]
	public string? VersionId { get; init; }
}
