using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Sites;

/// <summary>What a new site scans: either static assets (targets and asset groups) or a discovery connection.</summary>
public sealed class ScanScope
{
	/// <summary>The targets and asset groups of a static site.</summary>
	[JsonPropertyName("assets")]
	public ScanScopeAssets? Assets { get; init; }

	/// <summary>The discovery connection of a dynamic site.</summary>
	[JsonPropertyName("connection")]
	public ScanScopeConnection? Connection { get; init; }
}
