using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Administration;

/// <summary>An amount of memory or disk space, raw and human-readable.</summary>
public sealed class ByteSize
{
	/// <summary>The amount in bytes.</summary>
	[JsonPropertyName("bytes")]
	public long? Bytes { get; init; }

	/// <summary>The amount as the console displays it, such as <c>155.1 GB</c>.</summary>
	[JsonPropertyName("formatted")]
	public string? Formatted { get; init; }
}
