using Rapid7.Api.Models;
using System.Text.Json;
using System.Xml;
using System.Xml.Linq;

namespace Rapid7.Api;

/// <summary>
/// Turns a non-success response into a <see cref="Rapid7ApiException"/>. The Security Console (v3) answers
/// <c>{"status":"NOT_FOUND","message":"...","links":[...]}</c>; the Cloud Integrations API (v4) answers
/// <c>{"status":404,"message":"...","localized_message":"..."}</c>, or the same as <c>&lt;ErrorResource&gt;</c> XML. Any other
/// body (a proxy's HTML page, nothing at all) falls back to a message naming the HTTP status.
/// </summary>
internal static class Rapid7ErrorMapper
{
	public static async Task<Exception?> CreateAsync(HttpResponseMessage response)
	{
		if (response.IsSuccessStatusCode)
		{
			return null;
		}

		var body = await ReadBodyAsync(response).ConfigureAwait(false);
		var error = Parse(body);
		var message = error.Message
			?? error.LocalizedMessage
			?? $"HTTP {(int)response.StatusCode} ({response.ReasonPhrase ?? response.StatusCode.ToString()})";
		return new Rapid7ApiException(response.StatusCode, error.Status, message, error.LocalizedMessage, error.Links);
	}

	internal static ErrorBody Parse(string body)
	{
		var trimmed = body.TrimStart();
		if (trimmed.StartsWith('{'))
		{
			return ParseJson(trimmed);
		}

		return trimmed.StartsWith('<') ? ParseXml(trimmed) : ErrorBody.Empty;
	}

	private static ErrorBody ParseJson(string body)
	{
		try
		{
			using var document = JsonDocument.Parse(body);
			var root = document.RootElement;
			return new ErrorBody(
				ReadText(root, "status"),
				ReadText(root, "message"),
				ReadText(root, "localized_message") ?? ReadText(root, "localizedMessage"),
				ReadLinks(root));
		}
		catch (JsonException)
		{
			return ErrorBody.Empty;
		}
	}

	private static ErrorBody ParseXml(string body)
	{
		try
		{
			var root = XDocument.Parse(body).Root!;
			return new ErrorBody(
				Element(root, "status"),
				Element(root, "message"),
				Element(root, "localizedMessage") ?? Element(root, "localized_message"),
				[]);
		}
		catch (XmlException)
		{
			return ErrorBody.Empty;
		}
	}

	/// <summary>A string property's text, or a number's digits; <see langword="null"/> when absent or empty.</summary>
	private static string? ReadText(JsonElement element, string name)
	{
		if (element.ValueKind != JsonValueKind.Object || !element.TryGetProperty(name, out var property))
		{
			return null;
		}

		var text = property.ValueKind switch
		{
			JsonValueKind.String => property.GetString(),
			JsonValueKind.Number => property.GetRawText(),
			_ => null
		};
		return string.IsNullOrWhiteSpace(text) ? null : text;
	}

	private static List<Link> ReadLinks(JsonElement root)
		=> root.ValueKind == JsonValueKind.Object
			&& root.TryGetProperty("links", out var links)
			&& links.ValueKind == JsonValueKind.Array
				? [.. links.EnumerateArray()
					.Where(l => l.ValueKind == JsonValueKind.Object)
					.Select(l => new Link { Href = ReadText(l, "href"), Rel = ReadText(l, "rel") })]
				: [];

	private static string? Element(XElement root, string name)
		=> root.Elements().FirstOrDefault(e => e.Name.LocalName == name)?.Value.Trim() is { Length: > 0 } text ? text : null;

	internal static async Task<string> ReadBodyAsync(HttpResponseMessage response)
	{
		try
		{
			return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
		}
		catch (Exception exception) when (exception is InvalidOperationException or HttpRequestException or IOException)
		{
			// An unsupported charset in Content-Type, or a connection lost while reading: report the status, not the read.
			return string.Empty;
		}
	}

	/// <summary>The parts of an error body.</summary>
	internal sealed record ErrorBody(string? Status, string? Message, string? LocalizedMessage, IReadOnlyList<Link> Links)
	{
		public static ErrorBody Empty { get; } = new(null, null, null, []);
	}
}
