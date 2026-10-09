namespace Rapid7.Api.Models.Assets;

/// <summary>The documented operators of a <see cref="SearchFilter"/>. Which operators a field accepts depends on the field.</summary>
public static class SearchOperator
{
	/// <summary><c>are</c>: takes a string value.</summary>
	public static string Are { get; } = "are";

	/// <summary><c>contains</c>: takes a string value.</summary>
	public static string Contains { get; } = "contains";

	/// <summary><c>does-not-contain</c>: takes a string value.</summary>
	public static string DoesNotContain { get; } = "does-not-contain";

	/// <summary><c>does-not-include</c>: takes an array of strings.</summary>
	public static string DoesNotInclude { get; } = "does-not-include";

	/// <summary><c>ends-with</c>: takes a string value.</summary>
	public static string EndsWith { get; } = "ends-with";

	/// <summary><c>in</c>: takes an array of strings (or numbers, for enumerated fields).</summary>
	public static string In { get; } = "in";

	/// <summary><c>in-range</c>: takes numeric lower and upper bounds (or addresses, for <c>ip-address</c>).</summary>
	public static string InRange { get; } = "in-range";

	/// <summary><c>includes</c>: takes an array of strings.</summary>
	public static string Includes { get; } = "includes";

	/// <summary><c>is</c>: takes a string value.</summary>
	public static string Is { get; } = "is";

	/// <summary><c>is-applied</c>: takes no operand.</summary>
	public static string IsApplied { get; } = "is-applied";

	/// <summary><c>is-between</c>: takes lower and upper date bounds.</summary>
	public static string IsBetween { get; } = "is-between";

	/// <summary><c>is-earlier-than</c>: takes a number of days.</summary>
	public static string IsEarlierThan { get; } = "is-earlier-than";

	/// <summary><c>is-empty</c>: takes no operand.</summary>
	public static string IsEmpty { get; } = "is-empty";

	/// <summary><c>is-greater-than</c>: takes a numeric value.</summary>
	public static string IsGreaterThan { get; } = "is-greater-than";

	/// <summary><c>is-less-than</c>: takes a numeric value.</summary>
	public static string IsLessThan { get; } = "is-less-than";

	/// <summary><c>is-like</c>: takes a string value (a pattern).</summary>
	public static string IsLike { get; } = "is-like";

	/// <summary><c>is-not</c>: takes a string value.</summary>
	public static string IsNot { get; } = "is-not";

	/// <summary><c>is-not-applied</c>: takes no operand.</summary>
	public static string IsNotApplied { get; } = "is-not-applied";

	/// <summary><c>is-not-empty</c>: takes no operand.</summary>
	public static string IsNotEmpty { get; } = "is-not-empty";

	/// <summary><c>is-on-or-after</c>: takes a date value.</summary>
	public static string IsOnOrAfter { get; } = "is-on-or-after";

	/// <summary><c>is-on-or-before</c>: takes a date value.</summary>
	public static string IsOnOrBefore { get; } = "is-on-or-before";

	/// <summary><c>is-within-the-last</c>: takes a number of days.</summary>
	public static string IsWithinTheLast { get; } = "is-within-the-last";

	/// <summary><c>not-in</c>: takes an array of strings (or numbers, for enumerated fields).</summary>
	public static string NotIn { get; } = "not-in";

	/// <summary><c>not-in-range</c>: takes lower and upper bounds.</summary>
	public static string NotInRange { get; } = "not-in-range";

	/// <summary><c>not-like</c>: takes a string value (a pattern).</summary>
	public static string NotLike { get; } = "not-like";

	/// <summary><c>starts-with</c>: takes a string value.</summary>
	public static string StartsWith { get; } = "starts-with";
}
