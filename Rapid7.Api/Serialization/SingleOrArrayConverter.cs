using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rapid7.Api.Serialization;

/// <summary>
/// Reads a list that the API sometimes sends as a single object rather than an array (the single object becomes a one-item
/// list), and writes it as an array.
/// </summary>
/// <typeparam name="T">The item type.</typeparam>
internal sealed class SingleOrArrayConverter<T> : JsonConverter<IReadOnlyList<T>>
{
	/// <inheritdoc />
	public override IReadOnlyList<T>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		=> reader.TokenType == JsonTokenType.StartArray
			? JsonSerializer.Deserialize<List<T>>(ref reader, options)
			: [JsonSerializer.Deserialize<T>(ref reader, options)!];

	/// <inheritdoc />
	public override void Write(Utf8JsonWriter writer, IReadOnlyList<T> value, JsonSerializerOptions options)
		=> JsonSerializer.Serialize(writer, value.ToList(), options);
}
