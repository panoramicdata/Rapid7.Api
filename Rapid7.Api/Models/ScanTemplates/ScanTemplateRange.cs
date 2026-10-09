using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.ScanTemplates;

/// <summary>A minimum and maximum scan template setting, such as a parallelism range or a range of durations.</summary>
/// <typeparam name="T">
/// The value type: a nullable <see cref="int"/> for counts and rates, <see cref="string"/> for ISO 8601 durations such as
/// <c>PT0.5S</c>.
/// </typeparam>
public record ScanTemplateRange<T>
{
	/// <summary>The lower bound.</summary>
	[JsonPropertyName("minimum")]
	public T? Minimum { get; init; }

	/// <summary>The upper bound.</summary>
	[JsonPropertyName("maximum")]
	public T? Maximum { get; init; }
}
