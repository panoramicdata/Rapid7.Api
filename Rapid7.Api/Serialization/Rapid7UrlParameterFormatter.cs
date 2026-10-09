using System.Globalization;
using System.Reflection;
using Refit;

namespace Rapid7.Api.Serialization;

/// <summary>
/// Formats query and path parameters: booleans as lowercase <c>true</c>/<c>false</c>, enums by their wire name, dates in
/// ISO 8601 and numbers in the invariant culture.
/// </summary>
internal sealed class Rapid7UrlParameterFormatter : DefaultUrlParameterFormatter
{
	/// <inheritdoc />
	public override string? Format(object? value, ICustomAttributeProvider attributeProvider, Type type)
		=> value switch
		{
			null => null,
			bool flag => flag ? "true" : "false",
			Enum member => WireNames.Of(member),
			DateTimeOffset moment => moment.ToString("yyyy-MM-ddTHH:mm:ss.fffK", CultureInfo.InvariantCulture),
			DateTime moment => moment.ToString("yyyy-MM-ddTHH:mm:ss.fffK", CultureInfo.InvariantCulture),
			DateOnly day => day.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
			_ => base.Format(value, attributeProvider, type)
		};
}
