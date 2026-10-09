using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Assets;

/// <summary>
/// One filter of a <see cref="SearchCriteria"/>: an asset field, an operator, and the operand the operator needs. Which
/// operand depends on the operator: most take <see cref="Value"/> (a string or number, or an array of strings for
/// <c>in</c>, <c>not-in</c> and <c>includes</c>); ranges such as <c>in-range</c> and <c>is-between</c> take
/// <see cref="Lower"/> and <see cref="Upper"/>; <c>is-empty</c> and <c>is-applied</c> take none.
/// </summary>
/// <remarks>
/// Operands are written as their runtime type (a string, a number, an array). Operands read from the console arrive as
/// <see cref="System.Text.Json.JsonElement"/>.
/// </remarks>
/// <param name="field">The asset field to filter on, such as <see cref="SearchField.RiskScore"/>.</param>
/// <param name="operator">The operator, such as <see cref="SearchOperator.IsGreaterThan"/>.</param>
[method: JsonConstructor]
public sealed class SearchFilter(string field, string @operator)
{
	/// <summary>The asset field to filter on; see <see cref="SearchField"/> for the documented names.</summary>
	[JsonPropertyName("field")]
	public string Field { get; } = field;

	/// <summary>The operator; see <see cref="SearchOperator"/> for the documented names.</summary>
	[JsonPropertyName("operator")]
	public string Operator { get; } = @operator;

	/// <summary>The single operand of the operator.</summary>
	[JsonPropertyName("value")]
	public object? Value { get; init; }

	/// <summary>Several operands of the operator, for the consoles and operators that take a <c>values</c> array.</summary>
	[JsonPropertyName("values")]
	public IReadOnlyList<object>? Values { get; init; }

	/// <summary>The lower bound of a range.</summary>
	[JsonPropertyName("lower")]
	public object? Lower { get; init; }

	/// <summary>The upper bound of a range.</summary>
	[JsonPropertyName("upper")]
	public object? Upper { get; init; }
}
