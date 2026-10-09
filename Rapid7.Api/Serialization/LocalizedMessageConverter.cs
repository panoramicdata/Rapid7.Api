using Rapid7.Api.Models.Users;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rapid7.Api.Serialization;

/// <summary>
/// Reads a <see cref="LocalizedMessage"/> from its object form, or from a plain string (taken as the default text), and
/// writes the object form.
/// </summary>
internal sealed class LocalizedMessageConverter : JsonConverter<LocalizedMessage>
{
	/// <inheritdoc />
	public override LocalizedMessage Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType == JsonTokenType.String)
		{
			return new LocalizedMessage { DefaultValue = reader.GetString() };
		}

		var wire = JsonSerializer.Deserialize<Wire>(ref reader, options)!;
		return new LocalizedMessage { Key = wire.Key, DefaultValue = wire.DefaultValue, Arguments = wire.Arguments };
	}

	/// <inheritdoc />
	public override void Write(Utf8JsonWriter writer, LocalizedMessage value, JsonSerializerOptions options)
		=> JsonSerializer.Serialize(writer, new Wire { Key = value.Key, DefaultValue = value.DefaultValue, Arguments = value.Arguments }, options);

	private sealed class Wire
	{
		[JsonPropertyName("key")]
		public string? Key { get; init; }

		[JsonPropertyName("defaultValue")]
		public string? DefaultValue { get; init; }

		[JsonPropertyName("arguments")]
		public IReadOnlyList<JsonElement> Arguments { get; init; } = [];
	}
}
