namespace Rapid7.Api.Models.Assets;

/// <summary>The documented operators of a <see cref="SearchFilter"/>. Which operators a field accepts depends on the field.</summary>
public static class SearchOperator
{
	/// <summary><c>are</c>: takes a string value.</summary>
	public const string Are = "are";

	/// <summary><c>contains</c>: takes a string value.</summary>
	public const string Contains = "contains";

	/// <summary><c>does-not-contain</c>: takes a string value.</summary>
	public const string DoesNotContain = "does-not-contain";

	/// <summary><c>does-not-include</c>: takes an array of strings.</summary>
	public const string DoesNotInclude = "does-not-include";

	/// <summary><c>ends-with</c>: takes a string value.</summary>
	public const string EndsWith = "ends-with";

	/// <summary><c>in</c>: takes an array of strings (or numbers, for enumerated fields).</summary>
	public const string In = "in";

	/// <summary><c>in-range</c>: takes numeric lower and upper bounds (or addresses, for <c>ip-address</c>).</summary>
	public const string InRange = "in-range";

	/// <summary><c>includes</c>: takes an array of strings.</summary>
	public const string Includes = "includes";

	/// <summary><c>is</c>: takes a string value.</summary>
	public const string Is = "is";

	/// <summary><c>is-applied</c>: takes no operand.</summary>
	public const string IsApplied = "is-applied";

	/// <summary><c>is-between</c>: takes lower and upper date bounds.</summary>
	public const string IsBetween = "is-between";

	/// <summary><c>is-earlier-than</c>: takes a number of days.</summary>
	public const string IsEarlierThan = "is-earlier-than";

	/// <summary><c>is-empty</c>: takes no operand.</summary>
	public const string IsEmpty = "is-empty";

	/// <summary><c>is-greater-than</c>: takes a numeric value.</summary>
	public const string IsGreaterThan = "is-greater-than";

	/// <summary><c>is-less-than</c>: takes a numeric value.</summary>
	public const string IsLessThan = "is-less-than";

	/// <summary><c>is-like</c>: takes a string value (a pattern).</summary>
	public const string IsLike = "is-like";

	/// <summary><c>is-not</c>: takes a string value.</summary>
	public const string IsNot = "is-not";

	/// <summary><c>is-not-applied</c>: takes no operand.</summary>
	public const string IsNotApplied = "is-not-applied";

	/// <summary><c>is-not-empty</c>: takes no operand.</summary>
	public const string IsNotEmpty = "is-not-empty";

	/// <summary><c>is-on-or-after</c>: takes a date value.</summary>
	public const string IsOnOrAfter = "is-on-or-after";

	/// <summary><c>is-on-or-before</c>: takes a date value.</summary>
	public const string IsOnOrBefore = "is-on-or-before";

	/// <summary><c>is-within-the-last</c>: takes a number of days.</summary>
	public const string IsWithinTheLast = "is-within-the-last";

	/// <summary><c>not-in</c>: takes an array of strings (or numbers, for enumerated fields).</summary>
	public const string NotIn = "not-in";

	/// <summary><c>not-in-range</c>: takes lower and upper bounds.</summary>
	public const string NotInRange = "not-in-range";

	/// <summary><c>not-like</c>: takes a string value (a pattern).</summary>
	public const string NotLike = "not-like";

	/// <summary><c>starts-with</c>: takes a string value.</summary>
	public const string StartsWith = "starts-with";
}
