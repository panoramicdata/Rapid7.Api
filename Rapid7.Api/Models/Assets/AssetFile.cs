using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Assets;

/// <summary>A file or directory discovered on an asset while searching its file systems and shares.</summary>
public sealed class AssetFile
{
	/// <summary>The attributes of the entry, as name and value pairs.</summary>
	[JsonPropertyName("attributes")]
	public IReadOnlyList<Configuration> Attributes { get; init; } = [];

	/// <summary>The name of the file or directory.</summary>
	[JsonPropertyName("name")]
	public string Name { get; init; } = string.Empty;

	/// <summary>The size in bytes; the console reports <c>-1</c> for a directory.</summary>
	[JsonPropertyName("size")]
	public long? Size { get; init; }

	/// <summary>Whether the entry is a file or a directory.</summary>
	[JsonPropertyName("type")]
	public AssetFileType Type { get; init; }
}
