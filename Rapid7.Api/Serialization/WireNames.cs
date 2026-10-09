using System.Collections.Concurrent;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Rapid7.Api.Serialization;

/// <summary>
/// The wire names of enum members: the <see cref="JsonStringEnumMemberNameAttribute"/> name where present, otherwise the
/// member name. Shared by JSON, form and query formatting so every channel spells a value the same way.
/// </summary>
internal static class WireNames
{
	private static readonly ConcurrentDictionary<Type, EnumNames> Cache = new();

	/// <summary>The wire name of an enum value; an undefined value is written as its number.</summary>
	public static string Of(Enum value)
	{
		var names = Names(value.GetType());
		return names.ByValue.TryGetValue(value, out var name) ? name : Convert.ToInt64(value, System.Globalization.CultureInfo.InvariantCulture).ToString(System.Globalization.CultureInfo.InvariantCulture);
	}

	/// <summary>
	/// Parses a wire name case-insensitively, returning the default value for an unrecognised name. A member's C# name is
	/// accepted too when no member uses it as a wire name, so a member written as <c>"H"</c> also reads <c>"HIGH"</c>.
	/// </summary>
	public static T Parse<T>(string wireName) where T : struct, Enum
	{
		var names = Names(typeof(T));
		return names.ByName.TryGetValue(wireName, out var value) ? (T)value : default;
	}

	// Non-generic, so the cached Build delegate is shared rather than created per enum type.
	private static EnumNames Names(Type enumType) => Cache.GetOrAdd(enumType, Build);

	private static EnumNames Build(Type enumType)
	{
		var byName = new Dictionary<string, Enum>(StringComparer.OrdinalIgnoreCase);
		var byValue = new Dictionary<Enum, string>();
		var fields = enumType.GetFields(BindingFlags.Public | BindingFlags.Static);
		foreach (var field in fields)
		{
			var value = (Enum)field.GetValue(null)!;
			var name = field.GetCustomAttribute<JsonStringEnumMemberNameAttribute>()?.Name ?? field.Name;
			byName[name] = value;
			byValue.TryAdd(value, name);
		}

		// Member names as alternative spellings, where they do not clash with a wire name: some APIs document a value both
		// abbreviated and in full (CVSS components are "H" or "HIGH").
		foreach (var field in fields)
		{
			byName.TryAdd(field.Name, (Enum)field.GetValue(null)!);
		}

		return new EnumNames(byName, byValue);
	}

	private sealed record EnumNames(Dictionary<string, Enum> ByName, Dictionary<Enum, string> ByValue);
}
