using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Administration;

/// <summary>The Java virtual machine running the console.</summary>
public sealed class JvmInfo
{
	/// <summary>The name of the virtual machine.</summary>
	[JsonPropertyName("name")]
	public string? Name { get; init; }

	/// <summary>The vendor of the virtual machine.</summary>
	[JsonPropertyName("vendor")]
	public string? Vendor { get; init; }

	/// <summary>The version of the virtual machine.</summary>
	[JsonPropertyName("version")]
	public string? Version { get; init; }

	/// <summary>When the virtual machine last started.</summary>
	[JsonPropertyName("startTime")]
	public DateTimeOffset? StartTime { get; init; }

	/// <summary>How long the virtual machine has run, as an ISO 8601 duration such as <c>PT8H21M7.978S</c>.</summary>
	[JsonPropertyName("uptime")]
	public string? Uptime { get; init; }
}
