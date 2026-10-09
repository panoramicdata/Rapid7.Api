using System.Reflection;
using System.Text.Json.Serialization;

namespace Rapid7.Api.Serialization;

/// <summary>
/// Fills a record from a Parquet row (column name to value), matching each column to the property whose
/// <see cref="JsonPropertyNameAttribute"/> names it, case-insensitively. Values are converted to the property type, so a
/// file whose columns are required rather than optional, 64-bit rather than 32-bit, or timestamps of any precision still
/// read. Columns without a property, and null values, are skipped.
/// </summary>
/// <typeparam name="T">The record type.</typeparam>
internal static class ParquetRecordMapper<T> where T : new()
{
	private static readonly Dictionary<string, PropertyInfo> Properties = typeof(T)
		.GetProperties(BindingFlags.Public | BindingFlags.Instance)
		.ToDictionary(p => p.GetCustomAttribute<JsonPropertyNameAttribute>()!.Name, StringComparer.OrdinalIgnoreCase);

	/// <summary>Creates a record from <paramref name="row"/>.</summary>
	public static T Map(IEnumerable<KeyValuePair<string, object>> row)
	{
		var record = new T();
		foreach (var (column, value) in row)
		{
			if (value is not null && Properties.TryGetValue(column, out var property))
			{
				property.SetValue(record, ParquetValues.Convert(value, property.PropertyType));
			}
		}

		return record;
	}
}
