using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Administration;

/// <summary>The Java system and environment properties of the console (<c>GET api/3/administration/properties</c>).</summary>
public sealed class EnvironmentProperties : Links
{
	/// <summary>Each property name and value, such as <c>java.version</c>; values that are not strings are read as their JSON text.</summary>
	[JsonPropertyName("properties")]
	public IReadOnlyDictionary<string, string> Properties { get; init; } = new Dictionary<string, string>();
}
