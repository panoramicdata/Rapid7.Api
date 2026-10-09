using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Administration;

/// <summary>The disk of the console host.</summary>
public sealed class DiskInfo
{
	/// <summary>The free disk space.</summary>
	[JsonPropertyName("free")]
	public ByteSize? Free { get; init; }

	/// <summary>The total disk space.</summary>
	[JsonPropertyName("total")]
	public ByteSize? Total { get; init; }

	/// <summary>The space the console installation uses.</summary>
	[JsonPropertyName("installation")]
	public InstallationDiskUsage? Installation { get; init; }
}
