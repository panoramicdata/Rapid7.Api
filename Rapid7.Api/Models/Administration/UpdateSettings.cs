using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Administration;

/// <summary>The console's update settings.</summary>
public sealed class UpdateSettings
{
	/// <summary>Whether updates are enabled.</summary>
	[JsonPropertyName("enabled")]
	public bool? Enabled { get; init; }

	/// <summary>Whether product updates are applied automatically.</summary>
	[JsonPropertyName("productAutoUpdate")]
	public bool? ProductAutoUpdate { get; init; }

	/// <summary>Whether content updates are applied automatically.</summary>
	[JsonPropertyName("contentAutoUpdate")]
	public bool? ContentAutoUpdate { get; init; }
}
