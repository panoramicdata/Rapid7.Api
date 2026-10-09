using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Assets;

/// <summary>A web application discovered on a service, with the pages the spider found.</summary>
public sealed class WebApplication
{
	/// <summary>The identifier of the web application.</summary>
	[JsonPropertyName("id")]
	public long? Id { get; init; }

	/// <summary>The pages found.</summary>
	[JsonPropertyName("pages")]
	public IReadOnlyList<WebPage> Pages { get; init; } = [];

	/// <summary>The root path of the web application.</summary>
	[JsonPropertyName("root")]
	public string? Root { get; init; }

	/// <summary>The virtual host the web application is served on.</summary>
	[JsonPropertyName("virtualHost")]
	public string? VirtualHost { get; init; }
}
