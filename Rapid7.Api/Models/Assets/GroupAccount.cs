using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Assets;

/// <summary>A group account enumerated on an asset or service.</summary>
public sealed class GroupAccount
{
	/// <summary>The identifier of the group on the asset.</summary>
	[JsonPropertyName("id")]
	public int? Id { get; init; }

	/// <summary>The name of the group.</summary>
	[JsonPropertyName("name")]
	public string Name { get; init; } = string.Empty;
}
