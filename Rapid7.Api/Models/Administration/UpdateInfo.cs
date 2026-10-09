using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Administration;

/// <summary>The most recent updates applied to the console.</summary>
public sealed class UpdateInfo
{
	/// <summary>The most recent product update.</summary>
	[JsonPropertyName("product")]
	public string? Product { get; init; }

	/// <summary>The most recent content update.</summary>
	[JsonPropertyName("content")]
	public string? Content { get; init; }

	/// <summary>The most recent content update applied in memory only.</summary>
	[JsonPropertyName("contentPartial")]
	public string? ContentPartial { get; init; }

	/// <summary>The identifiers of the installed update.</summary>
	[JsonPropertyName("id")]
	public UpdateIdentifiers? Id { get; init; }
}
