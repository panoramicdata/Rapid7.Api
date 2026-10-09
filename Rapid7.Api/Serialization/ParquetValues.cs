using System.Collections;
using System.Globalization;

namespace Rapid7.Api.Serialization;

/// <summary>Converts the values Parquet.Net reads into the property types of the Bulk Export records.</summary>
internal static class ParquetValues
{
	/// <summary>Converts <paramref name="value"/> (not null) to <paramref name="type"/>.</summary>
	public static object Convert(object value, Type type)
	{
		var target = Nullable.GetUnderlyingType(type) ?? type;
		if (target == typeof(string))
		{
			return Text(value);
		}

		if (target == typeof(DateTimeOffset))
		{
			return Moment(value);
		}

		return target == typeof(IReadOnlyList<string>)
			? List(value)
			: System.Convert.ChangeType(value, target, CultureInfo.InvariantCulture);
	}

	/// <summary>Text: Parquet.Net reads strings as <see cref="string"/>, or (inside lists) as character memory.</summary>
	private static string Text(object value)
		=> value switch
		{
			string text => text,
			ReadOnlyMemory<char> memory => memory.ToString(),
			_ => System.Convert.ToString(value, CultureInfo.InvariantCulture)!
		};

	/// <summary>A point in time; Parquet timestamps are UTC, so an unzoned <see cref="DateTime"/> is read as UTC.</summary>
	private static DateTimeOffset Moment(object value)
		=> value switch
		{
			DateTimeOffset moment => moment,
			DateTime moment => new DateTimeOffset(DateTime.SpecifyKind(moment, DateTimeKind.Utc)),
			DateOnly day => new DateTimeOffset(day.ToDateTime(TimeOnly.MinValue), TimeSpan.Zero),
			_ => throw new InvalidCastException($"Cannot read a {value.GetType().Name} as a timestamp.")
		};

	/// <summary>A list of text, leaving out null elements; a single value becomes a one-item list.</summary>
	private static List<string> List(object value)
		=> value is IEnumerable items and not string
			? [.. items.Cast<object?>().OfType<object>().Select(Text)]
			: [Text(value)];
}
