using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Administration;

/// <summary>The limits a licence sets.</summary>
public sealed class LicenseLimits
{
	/// <summary>The most assets that can be assessed.</summary>
	[JsonPropertyName("assets")]
	public int? Assets { get; init; }

	/// <summary>The most assets the hosted scan engine can scan.</summary>
	[JsonPropertyName("assetsWithHostedEngine")]
	public int? AssetsWithHostedEngine { get; init; }

	/// <summary>The most scan engines that can be used.</summary>
	[JsonPropertyName("scanEngines")]
	public int? ScanEngines { get; init; }

	/// <summary>The most user accounts allowed.</summary>
	[JsonPropertyName("users")]
	public int? Users { get; init; }
}
