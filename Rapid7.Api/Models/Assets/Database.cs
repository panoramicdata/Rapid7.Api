using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Assets;

/// <summary>A database enumerated on an asset or service.</summary>
public sealed class Database
{
	/// <summary>A description of the database, such as the product hosting it.</summary>
	[JsonPropertyName("description")]
	public string? Description { get; init; }

	/// <summary>The identifier of the database.</summary>
	[JsonPropertyName("id")]
	public int? Id { get; init; }

	/// <summary>The name of the database instance.</summary>
	[JsonPropertyName("name")]
	public string Name { get; init; } = string.Empty;
}
