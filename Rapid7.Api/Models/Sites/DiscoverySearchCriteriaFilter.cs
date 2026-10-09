using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Sites;

/// <summary>
/// One filter of a dynamic site's discovery search: a field, an operator, and the value, values or range to compare with.
/// Which fields and operators are valid depends on the discovery connection type.
/// </summary>
/// <remarks>
/// The operands are untyped in the API (a string, number or boolean, depending on the field). Set them to any value
/// <see cref="System.Text.Json"/> can write; values read from a response are <see cref="System.Text.Json.JsonElement"/>.
/// </remarks>
public sealed class DiscoverySearchCriteriaFilter
{
	/// <summary>The field to filter on, for example <c>IP_ADDRESS</c> or <c>VSPHERE_GUEST_OS_FAMILY</c>.</summary>
	[JsonPropertyName("field")]
	public string? Field { get; init; }

	/// <summary>The lower bound of a range comparison.</summary>
	[JsonPropertyName("lower")]
	public object? Lower { get; init; }

	/// <summary>The comparison, for example <c>IS</c>, <c>CONTAINS</c> or <c>IN_RANGE</c>.</summary>
	[JsonPropertyName("operator")]
	public string? Operator { get; init; }

	/// <summary>The upper bound of a range comparison.</summary>
	[JsonPropertyName("upper")]
	public object? Upper { get; init; }

	/// <summary>The single value to compare with.</summary>
	[JsonPropertyName("value")]
	public object? Value { get; init; }

	/// <summary>The values to compare with, for operators that take several.</summary>
	[JsonPropertyName("values")]
	public IReadOnlyList<object>? Values { get; init; }
}
