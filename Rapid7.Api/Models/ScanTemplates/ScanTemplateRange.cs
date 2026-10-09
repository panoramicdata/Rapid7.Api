using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.ScanTemplates;

/// <summary>A lower and upper bound on a scan template's discovery setting.</summary>
/// <typeparam name="T">The bound type: a number, or an ISO 8601 duration string.</typeparam>
public class ScanTemplateRange<T>
{
	/// <summary>The lower bound.</summary>
	[JsonPropertyName("minimum")]
	public T? Minimum { get; init; }

	/// <summary>The upper bound.</summary>
	[JsonPropertyName("maximum")]
	public T? Maximum { get; init; }
}
