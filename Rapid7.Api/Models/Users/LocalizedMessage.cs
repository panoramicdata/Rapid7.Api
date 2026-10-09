using Rapid7.Api.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Users;

/// <summary>
/// A localisable text, such as a role name or description: a message key, its arguments and the default (English) text.
/// </summary>
/// <remarks>
/// The OpenAPI specification describes this as an object (<c>key</c>, <c>defaultValue</c>, <c>arguments</c>); a plain JSON
/// string is also read, as the <see cref="DefaultValue"/>. It is always written as an object.
/// </remarks>
[JsonConverter(typeof(LocalizedMessageConverter))]
public sealed class LocalizedMessage
{
	/// <summary>The message key (<c>key</c>).</summary>
	public string? Key { get; init; }

	/// <summary>The text in the default language (<c>defaultValue</c>).</summary>
	public string? DefaultValue { get; init; }

	/// <summary>The values substituted into the message, as raw JSON (<c>arguments</c>).</summary>
	public IReadOnlyList<JsonElement> Arguments { get; init; } = [];

	/// <inheritdoc />
	public override string? ToString() => DefaultValue ?? Key;
}
