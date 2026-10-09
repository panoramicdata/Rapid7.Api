using System.Text.Json.Serialization;

namespace Rapid7.Api.Models;

/// <summary>An unpaged Security Console (v3) collection: every resource in one response, with links.</summary>
/// <typeparam name="T">The resource type.</typeparam>
public sealed class ResourceList<T> : Links
{
	/// <summary>The resources.</summary>
	[JsonPropertyName("resources")]
	public IReadOnlyList<T> Resources { get; init; } = [];
}
