using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Administration;

/// <summary>The processors of the console host.</summary>
public sealed class CpuInfo
{
	/// <summary>The number of processors.</summary>
	[JsonPropertyName("count")]
	public int? Count { get; init; }

	/// <summary>The clock speed, in MHz.</summary>
	[JsonPropertyName("clockSpeed")]
	public int? ClockSpeed { get; init; }
}
