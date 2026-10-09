using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Assets;

/// <summary>The identifier of a web application on a service.</summary>
public sealed class WebApplicationReference
{
	/// <summary>The identifier of the web application.</summary>
	[JsonPropertyName("id")]
	public long? Id { get; init; }
}
