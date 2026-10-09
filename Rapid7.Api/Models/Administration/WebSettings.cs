using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Administration;

/// <summary>The console's web server settings.</summary>
public sealed class WebSettings
{
	/// <summary>The port the web server listens on.</summary>
	[JsonPropertyName("port")]
	public int? Port { get; init; }

	/// <summary>The fewest request-handling threads.</summary>
	[JsonPropertyName("minThreads")]
	public int? MinThreads { get; init; }

	/// <summary>The most request-handling threads.</summary>
	[JsonPropertyName("maxThreads")]
	public int? MaxThreads { get; init; }

	/// <summary>The session timeout, as an ISO 8601 duration such as <c>PT10M</c>.</summary>
	[JsonPropertyName("sessionTimeout")]
	public string? SessionTimeout { get; init; }
}
