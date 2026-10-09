using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Administration;

/// <summary>The memory of the console host.</summary>
public sealed class MemoryInfo
{
	/// <summary>The free memory.</summary>
	[JsonPropertyName("free")]
	public ByteSize? Free { get; init; }

	/// <summary>The total memory.</summary>
	[JsonPropertyName("total")]
	public ByteSize? Total { get; init; }
}
