using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Administration;

/// <summary>The disk space the console installation uses, in total and by kind of data.</summary>
public sealed class InstallationDiskUsage
{
	/// <summary>The installation directory.</summary>
	[JsonPropertyName("directory")]
	public string? Directory { get; init; }

	/// <summary>The space the whole installation uses.</summary>
	[JsonPropertyName("total")]
	public ByteSize? Total { get; init; }

	/// <summary>The space the database uses.</summary>
	[JsonPropertyName("database")]
	public ByteSize? Database { get; init; }

	/// <summary>The space scan data uses.</summary>
	[JsonPropertyName("scans")]
	public ByteSize? Scans { get; init; }

	/// <summary>The space reports use (uncompressed).</summary>
	[JsonPropertyName("reports")]
	public ByteSize? Reports { get; init; }

	/// <summary>The space backups use.</summary>
	[JsonPropertyName("backups")]
	public ByteSize? Backups { get; init; }
}
