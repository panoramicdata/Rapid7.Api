using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Administration;

/// <summary>The installed version of the console.</summary>
public sealed class VersionInfo
{
	/// <summary>The semantic version, such as <c>6.6.250</c>.</summary>
	[JsonPropertyName("semantic")]
	public string? Semantic { get; init; }

	/// <summary>The build number.</summary>
	[JsonPropertyName("build")]
	public string? Build { get; init; }

	/// <summary>The source changeset of the build.</summary>
	[JsonPropertyName("changeset")]
	public string? Changeset { get; init; }

	/// <summary>The build platform, such as <c>Linux64</c>.</summary>
	[JsonPropertyName("platform")]
	public string? Platform { get; init; }

	/// <summary>The most recent product and content updates.</summary>
	[JsonPropertyName("update")]
	public UpdateInfo? Update { get; init; }
}
